(function (app) {
    'use strict';
    app.controller('contactReportController', ['$scope', '$route', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', 'notificationService', 'close', function ($scope, $route, $http, apiService, authService, ngAuthSettings, $location, notificationService, close) {

        $scope.username = '';
        $scope.contactReportMessageResult = 'Your Profile could not send any contact. Your profile must be approved first. Thanks';

        $scope.closeContactReport = function (item) {
            close();
        }

    }]);
})(angular.module('gorgiasapp'));