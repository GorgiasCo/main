(function (app) {
    'use strict';
    app.controller('mainDashboardController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};

        console.log("mainDashboardController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

       
    }]);
})(angular.module('heroesApp'));