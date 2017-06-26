(function (app) {
    'use strict';
    app.controller('emailconfirmationController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', 'ModalService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, ModalService) {

        var vm = this;
        $scope.object = {};
        $scope.Profile = {};
        $scope.SubscribeMode = 'Subscribe';
        $scope.RequestedProfileFullname = 'You need to login.';
        $scope.isLoaded = false;
        ngAuthSettings.headercreative = 'header-creative';

        console.log("contactController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //$scope.ProfileID = $route.current.params.id;
        $scope.ResetPasswordUserID = $route.current.params.userId;
        $scope.ResetPasswordCode = $route.current.params.code;

        $scope.ActivatingMessage = 'Activating your account, Please Wait!';

        loadProfile();

        $scope.goToPortal = function () {
            $location.url('/');
        }

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/account/ConfirmEmail?userid=' + $scope.ResetPasswordUserID + '&code=' + $scope.ResetPasswordCode, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.isLoaded = true;
            $scope.isSuccess = response.data.Result;
            if ($scope.isSuccess == 'true') {
                $scope.ActivatingMessage = 'Done, Now you are one of the Georgias, Enjoy and Earn Money';
            } else {
                $scope.ActivatingMessage = 'Your token is not valid. Please try again or contact our supports. Thanks';
            }
        }

        function ProfileLoadFailed(response) {
            $scope.ActivatingMessage = 'There is an issue, Please try again later. Thanks for your patient.';
            $scope.isLoaded = true;
            notificationService.displayError(response.data.Errors);
        }

    }]);
})(angular.module('gorgiasapp'));