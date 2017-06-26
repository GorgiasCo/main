(function (app) {
    'use strict';
    app.controller('viewRequestTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewRequestTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.RequestTypeID = $route.current.params.requesttypeid;
        $scope.hasFile = false;

        loadRequestType();

        function loadRequestType() {
            apiService.get($scope.baseURL + 'api/RequestType/RequestTypeID/' + $scope.RequestTypeID, null,
            RequestTypeLoadCompleted,
            RequestTypeLoadFailed);
        }

        function RequestTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("RequestType loaded");
        }

        function RequestTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/RequestType/RequestTypeID/' + $scope.RequestTypeID, $scope.object,
            updateRequestTypeSucceded,
            updateRequestTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateRequestTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.RequestTypeID = RequestTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateRequestTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/requesttype');
        }
        



        
    }]);
})(angular.module('heroesApp'));