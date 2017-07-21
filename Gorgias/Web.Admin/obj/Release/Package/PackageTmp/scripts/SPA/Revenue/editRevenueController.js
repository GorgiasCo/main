(function (app) {
    'use strict';
    app.controller('editRevenueController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editRevenueController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.RevenueID = $route.current.params.revenueid;
        
        //$scope.RevenueID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadRevenue();

        function loadRevenue() {
            apiService.get($scope.baseURL + 'api/Revenue/RevenueID/' + $scope.RevenueID, null,
            RevenueLoadCompleted,
            RevenueLoadFailed);
        }

        function RevenueLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Revenue loaded");
        }

        function RevenueLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Revenue/RevenueID/' + $scope.RevenueID, $scope.object,
            updateRevenueSucceded,
            updateRevenueFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateRevenueSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.RevenueID = RevenueID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateRevenueFailed(response) {
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