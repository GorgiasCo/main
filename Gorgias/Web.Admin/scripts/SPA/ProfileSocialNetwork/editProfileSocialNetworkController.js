(function (app) {
    'use strict';
    app.controller('editProfileSocialNetworkController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editProfileSocialNetworkController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.SocialNetworkID = $route.current.params.socialnetworkid;
        $scope.ProfileID = $route.current.params.profileid;
        
        //$scope.ProfileSocialNetworkID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadProfileSocialNetwork();

        function loadProfileSocialNetwork() {
            apiService.get($scope.baseURL + 'api/ProfileSocialNetwork/SocialNetworkID/ProfileID/' + $scope.SocialNetworkID + "/"  + $scope.ProfileID, null,
            ProfileSocialNetworkLoadCompleted,
            ProfileSocialNetworkLoadFailed);
        }

        function ProfileSocialNetworkLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ProfileSocialNetwork loaded");
        }

        function ProfileSocialNetworkLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileSocialNetwork/SocialNetworkID/ProfileID/' + $scope.SocialNetworkID + "/"  + $scope.ProfileID, $scope.object,
            updateProfileSocialNetworkSucceded,
            updateProfileSocialNetworkFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateProfileSocialNetworkSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileSocialNetworkID = ProfileSocialNetworkID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateProfileSocialNetworkFailed(response) {
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