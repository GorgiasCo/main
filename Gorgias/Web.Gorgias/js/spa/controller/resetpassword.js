(function (app) {
    'use strict';
    app.controller('resetpasswordController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', 'ModalService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, ModalService) {

        var vm = this;
        $scope.object = {};
        $scope.loginMessage = '';

        $scope.isDone = false;

        $scope.submitForm = submit;
        console.log("contactController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.object.Username = $route.current.params.userId;
        $scope.object.Code = $route.current.params.code;
        $scope.object.Password = '';
        $scope.object.ConfirmedPassword = '';

        $scope.close = function () {
            $location.url('/');
        }

        $scope.redirect = function () {
            $location.url('/');
        }

        function submit(isValid) {
            if (isValid) {
                console.log(authService.authentication, 'post Contact Validation');
                if ($scope.object.Password === $scope.object.ConfirmedPassword) {
                    apiService.post($scope.baseURL + 'api/account/ResetPassword', $scope.object,
                        ProfileLoadCompleted,
                        ProfileLoadFailed);
                } else {
                    $scope.loginMessage = 'Password is not match, Try again.';
                }
            }
        }

        function ProfileLoadCompleted(response) {
            $scope.loginMessage = 'Password updated.';
            $scope.isDone = true;
        }

        function ProfileLoadFailed(response) {
            $scope.isDone = false;
            $scope.loginMessage = response.data.Errors[0];
        }

    }]);
})(angular.module('gorgiasapp'));