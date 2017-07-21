(function (app) {
    'use strict';
    app.controller('editFBActivityController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editFBActivityController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.FBActivityID = $route.current.params.fbactivityid;
        
        //$scope.FBActivityID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadFBActivity();

        function loadFBActivity() {
            apiService.get($scope.baseURL + 'api/FBActivity/FBActivityID/' + $scope.FBActivityID, null,
            FBActivityLoadCompleted,
            FBActivityLoadFailed);
        }

        function FBActivityLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("FBActivity loaded");
        }

        function FBActivityLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/FBActivity/FBActivityID/' + $scope.FBActivityID, $scope.object,
            updateFBActivitySucceded,
            updateFBActivityFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateFBActivitySucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.FBActivityID = FBActivityID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateFBActivityFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/fbactivity');
        }
        




        
    }]);
})(angular.module('heroesApp'));