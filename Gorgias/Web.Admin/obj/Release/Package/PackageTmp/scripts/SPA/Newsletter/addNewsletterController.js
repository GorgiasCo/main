(function (app) {
    'use strict';
    app.controller('addNewsletterController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addNewsletterController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.NewsletterID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Newsletter/', $scope.object,
            addNewsletterSucceded,
            addNewsletterFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addNewsletterSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.NewsletterID = result.data.Result.NewsletterID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addNewsletterFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/newsletter');
        }
        



        
    }]);
})(angular.module('heroesApp'));