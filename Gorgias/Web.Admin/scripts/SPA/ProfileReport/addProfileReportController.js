(function (app) {
    'use strict';
    app.controller('addProfileReportController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addProfileReportController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ProfileReportID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileReport/', $scope.object,
            addProfileReportSucceded,
            addProfileReportFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addProfileReportSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileReportID = result.data.Result.ProfileReportID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addProfileReportFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profilereport');
        }
        
        function loadReportTypes() {
            apiService.get($scope.baseURL + 'api/ReportTypes', null,
            reportTypesLoadCompleted,
            reportTypesLoadFailed);
        }

        function reportTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ReportTypes = response.data.Result;
            notificationService.displaySuccess("ReportTypes Loaded");
        }

        function reportTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
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
        function loadRevenues() {
            apiService.get($scope.baseURL + 'api/Revenues', null,
            revenuesLoadCompleted,
            revenuesLoadFailed);
        }

        function revenuesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Revenues = response.data.Result;
            notificationService.displaySuccess("Revenues Loaded");
        }

        function revenuesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadReportTypes()
        loadProfiles()
        loadRevenues()
        
    }]);
})(angular.module('heroesApp'));