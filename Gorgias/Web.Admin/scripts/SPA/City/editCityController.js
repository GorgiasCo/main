(function (app) {
    'use strict';
    app.controller('editCityController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editCityController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.CityID = $route.current.params.cityid;
        
        //$scope.CityID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadCity();

        function loadCity() {
            apiService.get($scope.baseURL + 'api/City/CityID/' + $scope.CityID, null,
            CityLoadCompleted,
            CityLoadFailed);
        }

        function CityLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("City loaded");
        }

        function CityLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/City/CityID/' + $scope.CityID, $scope.object,
            updateCitySucceded,
            updateCityFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateCitySucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.CityID = CityID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateCityFailed(response) {
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