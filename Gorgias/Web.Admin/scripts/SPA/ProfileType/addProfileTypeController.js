(function (app) {
    'use strict';
    app.controller('addProfileTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileTypeID = 0;
        $scope.hasFile = false;

        loadProfileTypes();

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileType/', $scope.object,
            addProfileTypeSucceded,
            addProfileTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileTypeID = result.data.Result.ProfileTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profiletype');
        }
        
        function loadProfileTypes() {
            apiService.get($scope.baseURL + 'api/ProfileTypes', null,
            profileTypesLoadCompleted,
            profileTypesLoadFailed);
        }

        function profileTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ProfileTypes = response.data.Result;
            notificationService.displaySuccess("ProfileTypes Loaded");
        }

        function profileTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        
        $scope.masterFileName = "profileType";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.ProfileTypeImage + '&MasterFileName=profileType',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.ProfileTypeImage = 'profiletype-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.ProfileTypeImage + '&MasterFileName=profileType';
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