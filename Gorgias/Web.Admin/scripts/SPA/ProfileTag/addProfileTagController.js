(function (app) {
    'use strict';
    app.controller('addProfileTagController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileTagController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileTagID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileTag/', $scope.object,
            addProfileTagSucceded,
            addProfileTagFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileTagSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileTagID = result.data.Result.ProfileTagID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profiletag');
        }
        

     
       function loadTags() {
            apiService.get($scope.baseURL + 'api/Tags', null,
            tagsLoadCompleted,
            tagsLoadFailed);
        }

        function tagsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Tags = response.data.Result;
            notificationService.displaySuccess("Tags Loaded");
        }

        function tagsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadTags();
     
       function loadProfiles() {
            apiService.get($scope.baseURL + 'api/Profiles', null,
            profilesLoadCompleted,
            profilesLoadFailed);
        }

        function profilesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Profiles = response.data.Result;
            notificationService.displaySuccess("Profiles Loaded");
        }

        function profilesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadProfiles();


        
    }]);
})(angular.module('heroesApp'));