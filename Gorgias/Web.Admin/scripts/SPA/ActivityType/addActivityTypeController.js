(function (app) {
    'use strict';
    app.controller('addActivityTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addActivityTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ActivityTypeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ActivityType/', $scope.object,
            addActivityTypeSucceded,
            addActivityTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addActivityTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ActivityTypeID = result.data.Result.ActivityTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addActivityTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/activitytype');
        }
        
        function loadActivityTypeParents() {
            apiService.get($scope.baseURL + 'api/ActivityTypes', null,
            activityTypeParentsLoadCompleted,
            activityTypeParentsLoadFailed);
        }

        function activityTypeParentsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ActivityTypeParents = response.data.Result;
            notificationService.displaySuccess("ActivityTypeParents Loaded");
        }

        function activityTypeParentsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadActivityTypeParents()
        
    }]);
})(angular.module('heroesApp'));