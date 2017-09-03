(function (app) {
    'use strict';
    app.controller('editActivityTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editActivityTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.ActivityTypeID = $route.current.params.activitytypeid;
        
        //$scope.ActivityTypeID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadActivityType();

        function loadActivityType() {
            apiService.get($scope.baseURL + 'api/ActivityType/ActivityTypeID/' + $scope.ActivityTypeID, null,
            ActivityTypeLoadCompleted,
            ActivityTypeLoadFailed);
        }

        function ActivityTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ActivityType loaded");
        }

        function ActivityTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ActivityType/ActivityTypeID/' + $scope.ActivityTypeID, $scope.object,
            updateActivityTypeSucceded,
            updateActivityTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateActivityTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ActivityTypeID = ActivityTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateActivityTypeFailed(response) {
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