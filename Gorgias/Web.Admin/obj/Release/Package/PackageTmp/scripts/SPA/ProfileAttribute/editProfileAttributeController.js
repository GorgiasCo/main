(function (app) {
    'use strict';
    app.controller('editProfileAttributeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editProfileAttributeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.AttributeID = $route.current.params.attributeid;
        $scope.ProfileID = $route.current.params.profileid;
        
        //$scope.ProfileAttributeID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadProfileAttribute();

        function loadProfileAttribute() {
            apiService.get($scope.baseURL + 'api/ProfileAttribute/AttributeID/ProfileID/' + $scope.AttributeID + "/"  + $scope.ProfileID, null,
            ProfileAttributeLoadCompleted,
            ProfileAttributeLoadFailed);
        }

        function ProfileAttributeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ProfileAttribute loaded");
        }

        function ProfileAttributeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileAttribute/AttributeID/ProfileID/' + $scope.AttributeID + "/"  + $scope.ProfileID, $scope.object,
            updateProfileAttributeSucceded,
            updateProfileAttributeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateProfileAttributeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileAttributeID = ProfileAttributeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateProfileAttributeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profileattribute');
        }
        

     
       function loadAttributes() {
            apiService.get($scope.baseURL + 'api/Attributes', null,
            attributesLoadCompleted,
            attributesLoadFailed);
        }

        function attributesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Attributes = response.data.Result;
            notificationService.displaySuccess("Attributes Loaded");
        }

        function attributesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadAttributes();
     
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