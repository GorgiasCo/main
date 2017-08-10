(function (app) {
    'use strict';
    app.controller('listprofileProfileCommissionController', ['$scope', '$stateParams', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$filter', 'authService',
        function ($scope, $stateParams, $http, $compile, apiService, ngAuthSettings, $location, notificationService, $filter, authService) {

            $scope.item = $stateParams.id;//$route.current.params.id;
            $scope.objectItemID = 0;

            $scope.AddObject = insertObject;
            $scope.hasFile = false;
            $scope.CountryID = authService.authentication.countryID;

            console.log("listprofileProfileSocialNetworkController Address loaded ;)");

            $scope.isShareError = false;
            $scope.ShareError = "Total share is more than 100% ;)";

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
                loadUsers();
                loadUserRoles();
            }

            function edit(item) {
                $scope.isList = false;
                $scope.isAdd = true;
                $scope.isEdit = true;

                //Pass Item ID's ;)
                $scope.objectItemID = item.SocialNetworkID;

                //Load Components
                loadUsers();
                loadUserRoles();
            }

            function redirect() {
                $scope.isList = true;
                $scope.isAdd = false;
            }
            //datatables  api/ExternalLinks/ProfileID/

            //Load Profile Items ;)
            function loadItems() {
                apiService.get($scope.baseURL + 'api/ProfileCommissions/Profile/' + $scope.item + '/1/30', null,
                itemsLoadCompleted,
                itemsLoadFailed);
            }

            function itemsLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.items = response.data.Result.Items;
                notificationService.displaySuccess("ProfileSocialNetworks Loaded");
            }

            function itemsLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            //Insert Object ;)
            function insertObject() {
                $scope.object.ProfileID = $scope.item;
                if ($scope.isEdit == false) {
                    apiService.post($scope.baseURL + 'api/ProfileCommission/', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    apiService.post($scope.baseURL + 'api/ProfileSocialNetwork/SocialNetworkID/ProfileID/' + $scope.objectItemID + "/" + $scope.item, $scope.object,
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
                $scope.isShareError = true;
                $scope.error = response.data.Errors;
                notificationService.displayError(response.statusText);
                notificationService.displayError(response.data.Errors);
            }

            //Delete Item
            function deleteRow(item) {
                if (confirm($filter('translate')('DeleteNote'))) {
                    apiService.deleteItem($scope.baseURL + 'api/ProfileSocialNetwork/SocialNetworkID/ProfileID/' + item.SocialNetworkID + '/' + item.ProfileID, null,
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
           
            //Load Components ;)

            function loadUserRoles() {
                apiService.get($scope.baseURL + 'api/UserRoles/', null,
                userrolesLoadCompleted,
                userrolesLoadFailed);
            }

            function userrolesLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.UserRoles = response.data.Result;
                notificationService.displaySuccess("UserRoles Loaded");
            }

            function userrolesLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            function loadUsers() {
                apiService.get($scope.baseURL + 'api/Users/Country/' + $scope.CountryID, null,
                usersLoadCompleted,
                usersLoadFailed);
            }

            function usersLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.Users = response.data.Result;
                notificationService.displaySuccess("Users Loaded");
            }

            function usersLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            

            //End Load Components ;)

            loadItems();

        }]);
})(angular.module('gorgiasapp'));