(function (app) {
    'use strict';
    app.controller('mainController', ['$scope', '$route', '$http', '$compile', 'authService', '$location', 'DTColumnBuilder', 'ngAuthSettings', 'validityService', 'ngAuthValueSettings', function ($scope, $route, $http, $compile, authService, $location, DTColumnBuilder, ngAuthSettings, validityService, ngAuthValueSettings) {
        
        $scope.logOut = function () {
            authService.logOut();
            ngAuthValueSettings.isAuthenticated = false;
            $scope.authentication = authService.authentication;
            $scope.isValid = authService.authentication.isAuth;
            $location.path('/login');
        }

        $scope.authentication = authService.authentication;
        $scope.isValid = authService.authentication.isAuth;

        $scope.$watch('isValid', function (newVal) {
            // if new value is not null do your all computation
            console.log($scope.isValid, 'Watch isValid new val', newVal);
        });

        console.log("main Master loaded ;)");
        console.log(authService.authentication.isAuth, 'authenticated');

    }]);
})(angular.module('heroesApp'));