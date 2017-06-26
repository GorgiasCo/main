(function (app) {
    'use strict';
    app.controller('addProfileSocialNetworkController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileSocialNetworkController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileSocialNetworkID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileSocialNetwork/', $scope.object,
            addProfileSocialNetworkSucceded,
            addProfileSocialNetworkFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileSocialNetworkSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileSocialNetworkID = result.data.Result.ProfileSocialNetworkID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileSocialNetworkFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profilesocialnetwork');
        }
        

     
       function loadSocialNetworks() {
            apiService.get($scope.baseURL + 'api/SocialNetworks', null,
            socialnetworksLoadCompleted,
            socialnetworksLoadFailed);
        }

        function socialnetworksLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.SocialNetworks = response.data.Result;
            notificationService.displaySuccess("SocialNetworks Loaded");
        }

        function socialnetworksLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadSocialNetworks();
     
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