(function (app) {
    'use strict';
    app.controller('listprofileConnectionController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.item = $route.current.params.id;

        console.log("listprofile Connection loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};

        $scope.edit = edit;
        $scope.delete = deleteRow;

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/Connections/RequestedProfileID/' + $scope.item + '/false/1/200', null,
            itemsLoadCompleted,
            itemsLoadFailed);
        }

        function itemsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.items = response.data.Result.Items;
            notificationService.displaySuccess("External Loaded");
        }

        function itemsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        //Edit Item
        function edit(item) {
            $scope.object = item;
            console.log(item);
            if (item.ConnectStatus == true) {
                $scope.object = {ConnectStatus : false};
            } else {
                $scope.object = {ConnectStatus : true};
            }
            apiService.post($scope.baseURL + 'api/Connection/ProfileID/RequestedProfileID/RequestTypeID/' + item.RequestedProfileID + "/" + item.ProfileID + "/" + item.RequestTypeID, $scope.object,
                updateConnectionSucceded,
                updateConnectionFailed);
            console.log("Post!!!" + $scope.object);
        }

        function updateConnectionSucceded(result) {
            console.log("Success" + result.data.Result);
            loadItems();         
        }

        function updateConnectionFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Delete Item
        function deleteRow(item) {
            console.log(item);
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/Connection/ProfileID/RequestedProfileID/RequestTypeID/' + item.ProfileID + '/' + item.RequestedProfileID + '/' + item.RequestTypeID, null,
                updateExternalLinkSucceded,
                updateExternalLinkFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
        }

        function updateExternalLinkSucceded(result) {
            console.log("Success" + result.data.Result);
            loadItems();
            notificationService.displaySuccess('Deleted.');
        }

        function updateExternalLinkFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }
        //end ;)       

        loadItems();
    }]);
})(angular.module('heroesApp'));