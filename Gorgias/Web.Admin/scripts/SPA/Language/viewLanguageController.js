(function (app) {
    'use strict';
    app.controller('viewLanguageController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewLanguageController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.LanguageID = $route.current.params.languageid;
        $scope.hasFile = false;

        loadLanguage();

        function loadLanguage() {
            apiService.get($scope.baseURL + 'api/Language/LanguageID/' + $scope.LanguageID, null,
            LanguageLoadCompleted,
            LanguageLoadFailed);
        }

        function LanguageLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Language loaded");
        }

        function LanguageLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Language/LanguageID/' + $scope.LanguageID, $scope.object,
            updateLanguageSucceded,
            updateLanguageFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateLanguageSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.LanguageID = LanguageID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateLanguageFailed(response) {
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