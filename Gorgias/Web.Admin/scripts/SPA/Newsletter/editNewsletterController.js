(function (app) {
    'use strict';
    app.controller('editNewsletterController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editNewsletterController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.NewsletterID = $route.current.params.newsletterid;
        
        //$scope.NewsletterID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadNewsletter();

        function loadNewsletter() {
            apiService.get($scope.baseURL + 'api/Newsletter/NewsletterID/' + $scope.NewsletterID, null,
            NewsletterLoadCompleted,
            NewsletterLoadFailed);
        }

        function NewsletterLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Newsletter loaded");
        }

        function NewsletterLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Newsletter/NewsletterID/' + $scope.NewsletterID, $scope.object,
            updateNewsletterSucceded,
            updateNewsletterFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateNewsletterSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.NewsletterID = NewsletterID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateNewsletterFailed(response) {
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