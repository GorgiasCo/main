(function (app) {
    'use strict';
    app.controller('addFBActivityController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addFBActivityController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.FBActivityID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/FBActivity/', $scope.object,
            addFBActivitySucceded,
            addFBActivityFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addFBActivitySucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.FBActivityID = result.data.Result.FBActivityID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addFBActivityFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/fbactivity');
        }
        



        
    }]);
})(angular.module('heroesApp'));