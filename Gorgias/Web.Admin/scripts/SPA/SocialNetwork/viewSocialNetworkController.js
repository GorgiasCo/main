﻿(function (app) {
    'use strict';
    app.controller('viewSocialNetworkController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewSocialNetworkController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.SocialNetworkID = $route.current.params.socialnetworkid;
        $scope.hasFile = false;

        loadSocialNetwork();

        function loadSocialNetwork() {
            apiService.get($scope.baseURL + 'api/SocialNetwork/SocialNetworkID/' + $scope.SocialNetworkID, null,
            SocialNetworkLoadCompleted,
            SocialNetworkLoadFailed);
        }

        function SocialNetworkLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("SocialNetwork loaded");
        }

        function SocialNetworkLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/SocialNetwork/SocialNetworkID/' + $scope.SocialNetworkID, $scope.object,
            updateSocialNetworkSucceded,
            updateSocialNetworkFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateSocialNetworkSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.SocialNetworkID = SocialNetworkID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateSocialNetworkFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/socialnetwork');
        }
        


        
        $scope.masterFileName = "socialNetwork";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.SocialNetworkImage + '&MasterFileName=socialNetwork',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.SocialNetworkImage = 'socialnetwork-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.SocialNetworkImage + '&MasterFileName=socialNetwork';
                });
            },
            removedfile: function (file) {
                var _ref;
                $scope.hasFile = false;
                console.log($scope.hasFile);
                console.log(file);
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            }
        };

        //Handle events for dropzone
        //Visit http://www.dropzonejs.com/#events for more events
        $scope.dzCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
                redirectBack();
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzMethods = {};
        $scope.removedfile = function () {
            $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

        
    }]);
})(angular.module('heroesApp'));