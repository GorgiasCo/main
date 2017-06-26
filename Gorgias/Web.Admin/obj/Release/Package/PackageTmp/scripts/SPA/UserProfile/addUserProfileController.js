(function (app) {
    'use strict';
    app.controller('addUserProfileController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addUserProfileController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.UserProfileID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/UserProfile/', $scope.object,
            addUserProfileSucceded,
            addUserProfileFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addUserProfileSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.UserProfileID = result.data.Result.UserProfileID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addUserProfileFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/userprofile');
        }
        

     
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
     
       function loadUserRoles() {
            apiService.get($scope.baseURL + 'api/UserRoles', null,
            userrolesLoadCompleted,
            userrolesLoadFailed);
        }

        function userrolesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.UserRoles = response.data.Result;
            notificationService.displaySuccess("UserRoles Loaded");
        }

        function userrolesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadUserRoles();
     
       function loadUsers() {
            apiService.get($scope.baseURL + 'api/Users', null,
            usersLoadCompleted,
            usersLoadFailed);
        }

        function usersLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Users = response.data.Result;
            notificationService.displaySuccess("Users Loaded");
        }

        function usersLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadUsers();


        
    }]);
})(angular.module('heroesApp'));