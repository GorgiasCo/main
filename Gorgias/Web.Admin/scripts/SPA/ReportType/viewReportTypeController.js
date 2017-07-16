(function (app) {
    'use strict';
    app.controller('viewReportTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewReportTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ReportTypeID = $route.current.params.reporttypeid;
        $scope.hasFile = false;

        loadReportType();

        function loadReportType() {
            apiService.get($scope.baseURL + 'api/ReportType/ReportTypeID/' + $scope.ReportTypeID, null,
            ReportTypeLoadCompleted,
            ReportTypeLoadFailed);
        }

        function ReportTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ReportType loaded");
        }

        function ReportTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ReportType/ReportTypeID/' + $scope.ReportTypeID, $scope.object,
            updateReportTypeSucceded,
            updateReportTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateReportTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ReportTypeID = ReportTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateReportTypeFailed(response) {
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