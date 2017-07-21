(function (app) {
    'use strict';
    app.controller('editProfileCommissionController', ['$scope', '$state', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', 'authService',
        function ($scope, $state, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, authService) {

            var vm = this;
            $scope.object = {};

            console.log("editProfileCommissionController loaded ;)");
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
            $scope.CountryID = authService.authentication.countryID;

            $scope.AddObject = insertObject;


            $scope.ProfileCommissionID = $stateParams.id;//$route.current.params.profilecommissionid;

            //$scope.ProfileCommissionID = $route.current.params.id;                        
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
                apiService.post($scope.baseURL + 'api/ProfileCommission/ProfileCommissionID/' + $scope.ProfileCommissionID, $scope.object,
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
                apiService.get($scope.baseURL + 'api/UserRoles/Country', null,
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