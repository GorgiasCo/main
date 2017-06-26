(function (app) {
    'use strict';
    app.controller('headerMenuController', ['$scope', '$state' ,'$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', '$translate',
        function ($scope, $state, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, $translate) {

            var vm = this;

            $scope.language = 'en';
            $scope.languages = ['en', 'zh', 'my'];
            $scope.updateLanguage = function () {
                $translate.use($scope.language);
                $state.reload();
            };

            $scope.language = $translate.proposedLanguage() || $translate.use()

            $scope.signout = function () {
                authService.fillAuthData();
                authService.logOut();
                $location.url('/access/login');
                console.log('good bye ;)');
            }

        }]);
})(angular.module('gorgiasapp'));