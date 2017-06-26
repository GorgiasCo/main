(function (app) {
    'use strict';
    app.controller('downloadAppController', ['$scope', '$route', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', 'notificationService', 'close', '$window', function ($scope, $route, $http, apiService, authService, ngAuthSettings, $location, notificationService, close, $window) {

        $scope.close = function () {
            console.log('Close Login');
            close(false); // close, but give 500ms for bootstrap to animate
        };

        $scope.downloadAndroid = function () {
            //$window.location.href = 'https://play.google.com/store/apps/details?id=com.gorgias.app';
            close(true); // close, but give 500ms for bootstrap to animate
        };

        $scope.downloadIos = function () {
            $window.location.href = 'https://itunes.apple.com/my/app/gorgias/id1193285323?mt=8';
            //close(true); // close, but give 500ms for bootstrap to animate
        };

    }]);
})(angular.module('gorgiasapp'));