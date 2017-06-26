(function (app) {
    'use strict';
    app.controller('addRequestTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addRequestTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.RequestTypeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/RequestType/', $scope.object,
            addRequestTypeSucceded,
            addRequestTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addRequestTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.RequestTypeID = result.data.Result.RequestTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addRequestTypeFailed(response) {
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