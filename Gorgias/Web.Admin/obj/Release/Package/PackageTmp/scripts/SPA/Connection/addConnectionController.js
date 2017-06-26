(function (app) {
    'use strict';
    app.controller('addConnectionController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addConnectionController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ConnectionID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Connection/', $scope.object,
            addConnectionSucceded,
            addConnectionFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addConnectionSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ConnectionID = result.data.Result.ConnectionID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addConnectionFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/connection');
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
     
       function loadRequestedProfiles() {
            apiService.get($scope.baseURL + 'api/RequestedProfiles', null,
            requestedprofilesLoadCompleted,
            requestedprofilesLoadFailed);
        }

        function requestedprofilesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.RequestedProfiles = response.data.Result;
            notificationService.displaySuccess("RequestedProfiles Loaded");
        }

        function requestedprofilesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadRequestedProfiles();
     
       function loadRequestTypes() {
            apiService.get($scope.baseURL + 'api/RequestTypes', null,
            requesttypesLoadCompleted,
            requesttypesLoadFailed);
        }

        function requesttypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.RequestTypes = response.data.Result;
            notificationService.displaySuccess("RequestTypes Loaded");
        }

        function requesttypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadRequestTypes();


        
    }]);
})(angular.module('heroesApp'));