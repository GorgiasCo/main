(function (app) {
    'use strict';
    app.controller('viewUserProfileController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewUserProfileController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ProfileID = $route.current.params.profileid;
        $scope.UserRoleID = $route.current.params.userroleid;
        $scope.UserID = $route.current.params.userid;
        $scope.hasFile = false;

        loadUserProfile();

        function loadUserProfile() {
            apiService.get($scope.baseURL + 'api/UserProfile/ProfileID/UserRoleID/UserID/' + $scope.ProfileID + "/"  + $scope.UserRoleID + "/"  + $scope.UserID, null,
            UserProfileLoadCompleted,
            UserProfileLoadFailed);
        }

        function UserProfileLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("UserProfile loaded");
        }

        function UserProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/UserProfile/UserProfileID/' + $scope.UserProfileID, $scope.object,
            updateUserProfileSucceded,
            updateUserProfileFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateUserProfileSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.UserProfileID = UserProfileID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateUserProfileFailed(response) {
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