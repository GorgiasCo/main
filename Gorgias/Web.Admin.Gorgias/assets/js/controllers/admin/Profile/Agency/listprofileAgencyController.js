(function (app) {
    'use strict';
    app.controller('listprofileAgencyController', ['$scope', '$stateParams', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', 'authService', '$filter',
        function ($scope, $stateParams, $http, $compile, apiService, ngAuthSettings, $location, notificationService, authService, $filter) {

            var vm = this;
            $scope.item = $stateParams.id; //authService.authentication.userUserID;//

            console.log("listprofileAgencyController Connection loaded ;)", $scope.item);

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pagesize = 2;
            $scope.Items = {};

            $scope.edit = edit;
            $scope.delete = deleteRow;

            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
            $scope.timestamp = new Date().getUTCMilliseconds();

            $scope.isAgency = false;

            //Load Profile Items ;)
            function loadItems() {
                apiService.get($scope.baseURL + 'api/Web/UserProfile/Agency/Profile/' + $scope.item, null,
                itemsLoadCompleted,
                itemsLoadFailed);
            }

            function itemsLoadCompleted(response) {
                console.log(response.data.Result, 'Agency WOW');
                if (response.data.Result.length > 0) {
                    $scope.isAgency = true;
                    $scope.items = response.data.Result;
                } else {
                    $scope.isAgency = false;
                }
                console.log($scope.isAgency, 'isAgency ;)', $scope.items);
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
                    $scope.object = { ConnectStatus: false };
                } else {
                    $scope.object = { ConnectStatus: true };
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
                if (confirm($filter('translate')('DeleteNote'))) {
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
})(angular.module('gorgiasapp'));