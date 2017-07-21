(function (app) {
    'use strict';
    app.controller('addRevenueController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addRevenueController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.RevenueID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Revenue/', $scope.object,
            addRevenueSucceded,
            addRevenueFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addRevenueSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.RevenueID = result.data.Result.RevenueID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addRevenueFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/revenue');
        }
        



        
    }]);
})(angular.module('heroesApp'));