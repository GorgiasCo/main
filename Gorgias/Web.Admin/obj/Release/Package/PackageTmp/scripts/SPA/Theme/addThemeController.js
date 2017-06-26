(function (app) {
    'use strict';
    app.controller('addThemeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addThemeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ThemeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Theme/', $scope.object,
            addThemeSucceded,
            addThemeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addThemeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ThemeID = result.data.Result.ThemeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addThemeFailed(response) {
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