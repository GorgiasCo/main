(function (app) {
    'use strict';
    app.controller('editPaymentController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editPaymentController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.PaymentID = $route.current.params.paymentid;
        
        //$scope.PaymentID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadPayment();

        function loadPayment() {
            apiService.get($scope.baseURL + 'api/Payment/PaymentID/' + $scope.PaymentID, null,
            PaymentLoadCompleted,
            PaymentLoadFailed);
        }

        function PaymentLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Payment loaded");
        }

        function PaymentLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Payment/PaymentID/' + $scope.PaymentID, $scope.object,
            updatePaymentSucceded,
            updatePaymentFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updatePaymentSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.PaymentID = PaymentID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updatePaymentFailed(response) {
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