(function (app) {
    'use strict';
    app.controller('addFeaturedSponsorController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addFeaturedSponsorController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.FeaturedSponsorID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/FeaturedSponsor/', $scope.object,
            addFeaturedSponsorSucceded,
            addFeaturedSponsorFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addFeaturedSponsorSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.FeaturedSponsorID = result.data.Result.FeaturedSponsorID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addFeaturedSponsorFailed(response) {
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