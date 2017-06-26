(function (app) {
    'use strict';
    app.controller('addTagController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addTagController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.TagID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Tag/', $scope.object,
            addTagSucceded,
            addTagFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addTagSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.TagID = result.data.Result.TagID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/tag');
        }
        



        
    }]);
})(angular.module('heroesApp'));