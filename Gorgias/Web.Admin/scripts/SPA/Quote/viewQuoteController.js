(function (app) {
    'use strict';
    app.controller('viewQuoteController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewQuoteController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.QuoteID = $route.current.params.quoteid;
        $scope.hasFile = false;

        loadQuote();

        function loadQuote() {
            apiService.get($scope.baseURL + 'api/Quote/QuoteID/' + $scope.QuoteID, null,
            QuoteLoadCompleted,
            QuoteLoadFailed);
        }

        function QuoteLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Quote loaded");
        }

        function QuoteLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Quote/QuoteID/' + $scope.QuoteID, $scope.object,
            updateQuoteSucceded,
            updateQuoteFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateQuoteSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.QuoteID = QuoteID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateQuoteFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/quote');
        }
        
        function loadCategories() {
            apiService.get($scope.baseURL + 'api/Categories', null,
            categoriesLoadCompleted,
            categoriesLoadFailed);
        }

        function categoriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Categories = response.data.Result;
            notificationService.displaySuccess("Categories Loaded");
        }

        function categoriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadCategories()
        
    }]);
})(angular.module('heroesApp'));