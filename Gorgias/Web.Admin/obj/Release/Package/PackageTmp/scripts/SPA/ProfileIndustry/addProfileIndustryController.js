(function (app) {
    'use strict';
    app.controller('addProfileIndustryController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};

        console.log("addProfileIndustryController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.AddObject = insertObject;

        function insertObject() {
            apiService.post($scope.baseURL + 'api/ProfileIndustry/', $scope.object,
            addProfileIndustrySucceded,
            addProfileIndustryFailed);
            console.log("Post!!!" + $scope.object);
        }

        function addProfileIndustrySucceded(result) {
            console.log("Success" + result.data.Result);
            redirectBack();            
        }

        function addProfileIndustryFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profileindustry');
        }

        function loadIndustries() {
            apiService.get($scope.baseURL + 'api/Industries', null,
            industriesLoadCompleted,
            industriesLoadFailed);
        }

        function industriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Industries = response.data.Result;
            notificationService.displaySuccess("Tags Loaded");
        }

        function industriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        loadIndustries();

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