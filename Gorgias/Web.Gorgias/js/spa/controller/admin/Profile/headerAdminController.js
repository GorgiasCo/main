(function (app) {
    'use strict';
    app.controller('headerAdminController', ['$scope', '$route', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', 'notificationService', '$window', 'Page', 'isMobile', '$timeout', function ($scope, $route, $http, apiService, authService, ngAuthSettings, $location, notificationService, $window, Page, isMobile, $timeout) {

        $scope.ProfileID = $route.current.params.id;
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.Profile = {};

        $scope.timestamp = new Date().getUTCMilliseconds();

        loadProfile();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Web/Admin/Mini/Profile/' + $scope.ProfileID, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.Profile = response.data.Result;            
        }

        function ProfileLoadFailed(response) {
            //notificationService.displayError(response.data.Errors);
        }

    }]);
})(angular.module('gorgiasapp'));