(function (app) {
    'use strict';
    app.controller('addProfileAttributeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileAttributeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileAttributeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileAttribute/', $scope.object,
            addProfileAttributeSucceded,
            addProfileAttributeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileAttributeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileAttributeID = result.data.Result.ProfileAttributeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileAttributeFailed(response) {
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