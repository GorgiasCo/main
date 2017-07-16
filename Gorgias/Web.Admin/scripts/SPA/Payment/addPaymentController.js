(function (app) {
    'use strict';
    app.controller('addPaymentController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addPaymentController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.PaymentID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Payment/', $scope.object,
            addPaymentSucceded,
            addPaymentFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addPaymentSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.PaymentID = result.data.Result.PaymentID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addPaymentFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/payment');
        }
        
        function loadProfileCommissions() {
            apiService.get($scope.baseURL + 'api/ProfileCommissions', null,
            profileCommissionsLoadCompleted,
            profileCommissionsLoadFailed);
        }

        function profileCommissionsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ProfileCommissions = response.data.Result;
            notificationService.displaySuccess("ProfileCommissions Loaded");
        }

        function profileCommissionsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadProfileCommissions()
        
    }]);
})(angular.module('heroesApp'));