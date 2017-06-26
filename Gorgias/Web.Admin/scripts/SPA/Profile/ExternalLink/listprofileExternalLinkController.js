(function (app) {
    'use strict';
    app.controller('listprofileExternalLinkController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        console.log("listprofile Address loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;

        $scope.isList = true;
        $scope.isAdd = false;
        $scope.isEdit = false;

        $scope.edit = edit;
        $scope.delete = deleteRow;
        $scope.addNew = addNew;
        $scope.redirect = redirect;

        $scope.items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //Address Object ;)
        $scope.object = { ProfileID: $route.current.params.id };

        function addNew() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = false;
            $scope.object = { ProfileID: $route.current.params.id };

            //Load Components
            loadLinkTypes();
        }

        function edit(item) {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = true;

            //Pass Item ID's ;)
            $scope.objectItemID = item.LinkTypeID;

            //Load Components
            loadExternalLink(item);
            loadLinkTypes();
        }

        function redirect() {
            $scope.isList = true;
            $scope.isAdd = false;
        }
        //datatables  api/ExternalLinks/ProfileID/
       
        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/ExternalLinks/ProfileID/' + $scope.item + '/1/30', null,
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

        //Insert Object ;)
        function insertObject() {
            if ($scope.isEdit == false) {
                apiService.post($scope.baseURL + 'api/ExternalLink/', $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("New Post!!!" + $scope.object);
            } else {
                apiService.post($scope.baseURL + 'api/ExternalLink/LinkTypeID/ProfileID/' + $scope.objectItemID + "/" + $scope.item, $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("Update Post!!!" + $scope.object);
            }
        }

        function objectSucceded(result) {
            console.log("Success" + result.data.Result);
            loadItems();
            $scope.object = { ProfileID: $route.current.params.id, AddressStatus: true };
            redirect();
        }

        function objectFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Delete Item
        function deleteRow(item) {
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/ExternalLink/LinkTypeID/ProfileID/' + item.LinkTypeID + '/' + item.ProfileID, null,
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

        //Loading Components ;)
        function loadExternalLink(item) {
            apiService.get($scope.baseURL + 'api/ExternalLink/LinkTypeID/ProfileID/' + item.LinkTypeID + "/" + item.ProfileID, null,
            ExternalLinkLoadCompleted,
            ExternalLinkLoadFailed);
        }

        function ExternalLinkLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ExternalLink loaded");
        }

        function ExternalLinkLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadLinkTypes() {
            apiService.get($scope.baseURL + 'api/LinkTypes', null,
            linktypesLoadCompleted,
            linktypesLoadFailed);
        }

        function linktypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.LinkTypes = response.data.Result;
            notificationService.displaySuccess("LinkTypes Loaded");
        }

        function linktypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End Loading Components ;)

        loadItems();
    }]);
})(angular.module('heroesApp'));     