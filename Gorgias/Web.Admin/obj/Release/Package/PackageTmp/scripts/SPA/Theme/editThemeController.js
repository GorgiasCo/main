(function (app) {
    'use strict';
    app.controller('editThemeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editThemeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.ThemeID = $route.current.params.themeid;
        
        //$scope.ThemeID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadTheme();

        function loadTheme() {
            apiService.get($scope.baseURL + 'api/Theme/ThemeID/' + $scope.ThemeID, null,
            ThemeLoadCompleted,
            ThemeLoadFailed);
        }

        function ThemeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Theme loaded");
        }

        function ThemeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Theme/ThemeID/' + $scope.ThemeID, $scope.object,
            updateThemeSucceded,
            updateThemeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateThemeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ThemeID = ThemeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateThemeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/theme');
        }
        




        
    }]);
})(angular.module('heroesApp'));