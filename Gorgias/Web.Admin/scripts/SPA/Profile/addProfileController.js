(function (app) {
    'use strict';
    app.controller('addProfileController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Profile/', $scope.object,
            addProfileSucceded,
            addProfileFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileSucceded(result) {
            $scope.ProfileID = result.data.Result.ProfileID;
            console.log($scope.ProfileID);
            console.log("Success");
            console.log(result.data.Result.ProfileID);
            console.log(result.data.Result);
            if ($scope.hasFile) {
                $scope.ProfileID = result.data.Result.ProfileID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profile');
        }
        
        function loadIndustries() {
            apiService.get($scope.baseURL + 'api/Industries', null,
            industriesLoadCompleted,
            industriesLoadFailed);
        }

        function industriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Industries = response.data.Result;
            notificationService.displaySuccess("Industries Loaded");
        }

        function industriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
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

        function loadThemes() {
            apiService.get($scope.baseURL + 'api/Themes', null,
            themesLoadCompleted,
            themesLoadFailed);
        }

        function themesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Themes = response.data.Result;
            notificationService.displaySuccess("Themes Loaded");
        }

        function themesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        function loadSubscriptionTypes() {
            apiService.get($scope.baseURL + 'api/SubscriptionTypes', null,
            subscriptionTypesLoadCompleted,
            subscriptionTypesLoadFailed);
        }

        function subscriptionTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.SubscriptionTypes = response.data.Result;
            notificationService.displaySuccess("SubscriptionTypes Loaded");
        }

        function subscriptionTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }


        
        $scope.masterFileName = "profile";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;                    
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
                    $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile';
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
                $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
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

        //loadIndustries()
        loadProfileTypes()
        loadThemes()
        loadSubscriptionTypes()
        
    }]);
})(angular.module('heroesApp'));