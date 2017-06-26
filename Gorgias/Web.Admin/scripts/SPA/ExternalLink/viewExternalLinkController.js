(function (app) {
    'use strict';
    app.controller('viewExternalLinkController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewExternalLinkController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.LinkTypeID = $route.current.params.linktypeid;
        $scope.ProfileID = $route.current.params.profileid;
        $scope.hasFile = false;

        loadExternalLink();

        function loadExternalLink() {
            apiService.get($scope.baseURL + 'api/ExternalLink/LinkTypeID/ProfileID/' + $scope.LinkTypeID + "/"  + $scope.ProfileID, null,
            ExternalLinkLoadCompleted,
            ExternalLinkLoadFailed);
        }

        function ExternalLinkLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ExternalLink loaded");
        }

        function ExternalLinkLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ExternalLink/ExternalLinkID/' + $scope.ExternalLinkID, $scope.object,
            updateExternalLinkSucceded,
            updateExternalLinkFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateExternalLinkSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ExternalLinkID = ExternalLinkID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateExternalLinkFailed(response) {
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