(function (app) {
    'use strict';
    app.controller('addReportTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addReportTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ReportTypeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ReportType/', $scope.object,
            addReportTypeSucceded,
            addReportTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addReportTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ReportTypeID = result.data.Result.ReportTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addReportTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/reporttype');
        }
        



        
    }]);
})(angular.module('heroesApp'));