(function (app) {
    'use strict';
    app.controller('addLanguageController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addLanguageController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.LanguageID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Language/', $scope.object,
            addLanguageSucceded,
            addLanguageFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addLanguageSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.LanguageID = result.data.Result.LanguageID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addLanguageFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/language');
        }
        



        
    }]);
})(angular.module('heroesApp'));