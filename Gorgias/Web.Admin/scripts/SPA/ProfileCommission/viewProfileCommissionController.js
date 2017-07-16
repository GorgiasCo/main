(function (app) {
    'use strict';
    app.controller('viewProfileCommissionController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewProfileCommissionController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ProfileCommissionID = $route.current.params.profilecommissionid;
        $scope.hasFile = false;

        loadProfileCommission();

        function loadProfileCommission() {
            apiService.get($scope.baseURL + 'api/ProfileCommission/ProfileCommissionID/' + $scope.ProfileCommissionID, null,
            ProfileCommissionLoadCompleted,
            ProfileCommissionLoadFailed);
        }

        function ProfileCommissionLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ProfileCommission loaded");
        }

        function ProfileCommissionLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileCommission/ProfileCommissionID/' + $scope.ProfileCommissionID, $scope.object,
            updateProfileCommissionSucceded,
            updateProfileCommissionFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateProfileCommissionSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileCommissionID = ProfileCommissionID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateProfileCommissionFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profilecommission');
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
        function loadUserRoles() {
            apiService.get($scope.baseURL + 'api/UserRoles', null,
            userRolesLoadCompleted,
            userRolesLoadFailed);
        }

        function userRolesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.UserRoles = response.data.Result;
            notificationService.displaySuccess("UserRoles Loaded");
        }

        function userRolesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadProfiles()
        loadUsers()
        loadUserRoles()
        
    }]);
})(angular.module('heroesApp'));