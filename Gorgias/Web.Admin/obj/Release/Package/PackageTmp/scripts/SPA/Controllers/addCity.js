(function (app) {
    'use strict';
    app.controller('addCityController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        $scope.error = "hi salam hallo";
        console.log("addAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.masterFileName = "album";
        $scope.AddObject = insertObject;
        $scope.upload = uploadImages;

        function insertObject() {
            apiService.post('http://localhost:43587/api/City/', $scope.object,
            addCitySucceded,
            addCityFailed);
            console.log("Post!!!" + $scope.object);
        }

        function uploadImages() {
            console.log("Uploaded");
        }

        function addCitySucceded(result) {
            console.log("add City triggered 3>" + result.data.Result);
            redirectBack();
        }

        function addCityFailed(response) {
            console.log("add category triggered !!! Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('city');
        }

        function loadCountries() {
            apiService.get($scope.baseURL + 'api/Countries/1/10', null,
            countriesLoadCompleted,
            countriesLoadFailed);
        }

        function countriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Countries = response.data.Result.Items;
            notificationService.displaySuccess("Countries Loaded");
        }

        function countriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        loadCountries()

    }]);
})(angular.module('heroesApp'));