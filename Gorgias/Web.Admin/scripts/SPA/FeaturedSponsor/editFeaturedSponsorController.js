(function (app) {
    'use strict';
    app.controller('editFeaturedSponsorController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editFeaturedSponsorController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.FeatureID = $route.current.params.featureid;
        $scope.ProfileID = $route.current.params.profileid;
        
        //$scope.FeaturedSponsorID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadFeaturedSponsor();

        function loadFeaturedSponsor() {
            apiService.get($scope.baseURL + 'api/FeaturedSponsor/FeatureID/ProfileID/' + $scope.FeatureID + "/"  + $scope.ProfileID, null,
            FeaturedSponsorLoadCompleted,
            FeaturedSponsorLoadFailed);
        }

        function FeaturedSponsorLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("FeaturedSponsor loaded");
        }

        function FeaturedSponsorLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/FeaturedSponsor/FeatureID/ProfileID/' + $scope.FeatureID + "/"  + $scope.ProfileID, $scope.object,
            updateFeaturedSponsorSucceded,
            updateFeaturedSponsorFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateFeaturedSponsorSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.FeaturedSponsorID = FeaturedSponsorID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateFeaturedSponsorFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/featuredsponsor');
        }
        

     
       function loadFeatures() {
            apiService.get($scope.baseURL + 'api/Features', null,
            featuresLoadCompleted,
            featuresLoadFailed);
        }

        function featuresLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Features = response.data.Result;
            notificationService.displaySuccess("Features Loaded");
        }

        function featuresLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadFeatures();
     
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