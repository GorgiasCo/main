(function (app) {
    'use strict';
    app.controller('addCityController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addCityController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.CityID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/City/', $scope.object,
            addCitySucceded,
            addCityFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addCitySucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.CityID = result.data.Result.CityID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addCityFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/city');
        }
        
        function loadCountries() {
            apiService.get($scope.baseURL + 'api/Countries', null,
            countriesLoadCompleted,
            countriesLoadFailed);
        }

        function countriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Countries = response.data.Result;
            notificationService.displaySuccess("Countries Loaded");
        }

        function countriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadCountries()
        
    }]);
})(angular.module('heroesApp'));