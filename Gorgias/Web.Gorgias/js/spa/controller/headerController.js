(function (app) {
    'use strict';
    app.controller('headerController', ['$scope', '$route', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', '$window', 'Page', function ($scope, $route, $http, apiService, authService, ngAuthSettings, $location, $window, Page) {

        $scope.Page = Page;
        //if ($route.current != null) {
        //    console.log($route.current.params.name, $route.current.params.profileid, 'meta header ;) faster');
        //    $scope.Page.setTitle($route.current.params.name);
        //    $scope.Page.setProfile($route.current.params.profileid);
        //}        
    }]);
})(angular.module('gorgiasapp'));