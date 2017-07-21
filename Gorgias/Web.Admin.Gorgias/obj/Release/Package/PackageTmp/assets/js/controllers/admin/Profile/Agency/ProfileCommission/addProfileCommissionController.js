(function (app) {
    'use strict';
    app.controller('addProfileCommissionController', ['$scope', '$state', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', 'authService',
        function ($scope, $state, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, authService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileCommissionController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.CountryID = authService.authentication.countryID;

        $scope.AddObject = insertObject;
        
        $scope.ProfileCommissionID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileCommission/', $scope.object,
            addProfileCommissionSucceded,
            addProfileCommissionFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileCommissionSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileCommissionID = result.data.Result.ProfileCommissionID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileCommissionFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            //$location.url('/profilecommission');
            $state.go('app.forms.commission', {});
        }
        
        function loadProfiles() {
            apiService.get($scope.baseURL + 'api/Profiles/Country/' + $scope.CountryID, null,
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
            apiService.get($scope.baseURL + 'api/UserRoles/', null,
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
            apiService.get($scope.baseURL + 'api/Users/Country/' + $scope.CountryID, null,
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
})(angular.module('gorgiasapp'));