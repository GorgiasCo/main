(function (app) {
    'use strict';
    app.controller('addUserController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addUserController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.UserID = 0;
        $scope.hasFile = false;

        loadCountries();

        function insertObject() {
            apiService.post($scope.baseURL +'api/User/', $scope.object,
            addUserSucceded,
            addUserFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addUserSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.UserID = result.data.Result.UserID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addUserFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/user');
        }
        
        function loadCountries() {
            apiService.get($scope.baseURL + 'api/Countries', null,
            countriesLoadCompleted,
            countriesLoadFailed);
        }

        function countriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Countries = response.data.Result;
            notificationService.displaySuccess("Cities Loaded");
        }

        function countriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }


        
    }]);
})(angular.module('heroesApp'));