(function (app) {
    'use strict';
    app.controller('listprofileProfileAttributeController', ['$scope', '$stateParams', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$filter',
        function ($scope, $stateParams, $http, $compile, apiService, ngAuthSettings, $location, notificationService, $filter) {

            $scope.item = $stateParams.id;//$route.current.params.id;
            $scope.objectItemID = 0;

            $scope.AddObject = insertObject;
            $scope.hasFile = false;

            console.log("listprofileProfileAttributeController Address loaded ;)");

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
            $scope.object = { ProfileID: $stateParams.id};//$route.current.params.id 

            function addNew() {
                $scope.isList = false;
                $scope.isAdd = true;
                $scope.isEdit = false;
                $scope.object = { ProfileID: $stateParams.id};//$route.current.params.id 

                //Load Components
                loadAttributes();
            }

            function edit(item) {
                $scope.isList = false;
                $scope.isAdd = true;
                $scope.isEdit = true;

                //Pass Item ID's ;)
                $scope.objectItemID = item.AttributeID;
                console.log('edit');
                console.log(item.AttributeID);
                //Load Components
                loadProfileAttribute(item);
                loadAttributes();
            }

            function redirect() {
                $scope.isList = true;
                $scope.isAdd = false;
            }

            $scope.attributeUpdate = function (item) {
                console.log(item);
                if ($scope.isEdit == false) {
                    $scope.object = { ProfileID: $stateParams.id, AttributeID: item.AttributeID };//$route.current.params.id
                    $scope.object.ProfileAttributeNote = item.AttributeDescription;
                }
            };

            //Load Profile Items ;)
            function loadItems() {
                apiService.get($scope.baseURL + 'api/ProfileAttributes/ProfileID/' + $scope.item + '/1/30', null,
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
                    apiService.post($scope.baseURL + 'api/ProfileAttribute/', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    apiService.post($scope.baseURL + 'api/ProfileAttribute/AttributeID/ProfileID/' + $scope.objectItemID + "/" + $scope.item, $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("Update Post!!!" + $scope.object);
                }
            }

            function objectSucceded(result) {
                console.log("Success" + result.data.Result);
                loadItems();
                $scope.object = { ProfileID: $stateParams.id};//$route.current.params.id 
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
                if (confirm($filter('translate')('DeleteNote'))) {
                    apiService.deleteItem($scope.baseURL + 'api/ProfileAttribute/AttributeID/ProfileID/' + item.AttributeID + '/' + item.ProfileID, null,
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
            function loadProfileAttribute(item) {
                apiService.get($scope.baseURL + 'api/ProfileAttribute/AttributeID/ProfileID/' + item.AttributeID + "/" + item.ProfileID, null,
                ProfileAttributeLoadCompleted,
                ProfileAttributeLoadFailed);
            }

            function ProfileAttributeLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.object = response.data.Result;
                notificationService.displaySuccess("ProfileAttribute loaded");
            }

            function ProfileAttributeLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            function loadAttributes() {
                apiService.get($scope.baseURL + 'api/Attributes', null,
                attributesLoadCompleted,
                attributesLoadFailed);
            }

            function attributesLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.Attributes = response.data.Result;
                notificationService.displaySuccess("Attributes Loaded");
            }

            function attributesLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }
            //End Loading Components ;)

            loadItems();
        }]);
})(angular.module('gorgiasapp'));