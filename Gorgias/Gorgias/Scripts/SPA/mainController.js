(function (app) {
    'use strict';
    app.controller('mainController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder) {
        console.log("main loaded ;)");
    }]);
})(angular.module('heroesApp'));