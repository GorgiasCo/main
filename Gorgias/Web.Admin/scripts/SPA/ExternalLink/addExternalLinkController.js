(function (app) {
    'use strict';
    app.controller('addExternalLinkController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addExternalLinkController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ExternalLinkID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ExternalLink/', $scope.object,
            addExternalLinkSucceded,
            addExternalLinkFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addExternalLinkSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ExternalLinkID = result.data.Result.ExternalLinkID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addExternalLinkFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/externallink');
        }
        

     
       function loadLinkTypes() {
            apiService.get($scope.baseURL + 'api/LinkTypes', null,
            linktypesLoadCompleted,
            linktypesLoadFailed);
        }

        function linktypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.LinkTypes = response.data.Result;
            notificationService.displaySuccess("LinkTypes Loaded");
        }

        function linktypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadLinkTypes();
     
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