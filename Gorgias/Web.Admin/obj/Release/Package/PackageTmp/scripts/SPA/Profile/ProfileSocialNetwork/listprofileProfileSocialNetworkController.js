(function (app) {
    'use strict';
    app.controller('listprofileProfileSocialNetworkController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        console.log("listprofileProfileSocialNetworkController Address loaded ;)");

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
            loadSocialNetworks();
        }

        function edit(item) {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = true;

            //Pass Item ID's ;)
            $scope.objectItemID = item.SocialNetworkID;

            //Load Components
            loadProfileSocialNetwork(item);
            loadSocialNetworks();
        }

        function redirect() {
            $scope.isList = true;
            $scope.isAdd = false;
        }
        //datatables  api/ExternalLinks/ProfileID/

        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/ProfileSocialNetworks/ProfileID/' + $scope.item + '/1/30', null,
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
            if ($scope.isEdit == false) {
                apiService.post($scope.baseURL + 'api/ProfileSocialNetwork/', $scope.object,
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
            $scope.object = { ProfileID: $route.current.params.id };
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

        //Load Item ;)
        function loadProfileSocialNetwork(item) {
            apiService.get($scope.baseURL + 'api/ProfileSocialNetwork/SocialNetworkID/ProfileID/' + item.SocialNetworkID + "/" + item.ProfileID, null,
            ProfileSocialNetworkLoadCompleted,
            ProfileSocialNetworkLoadFailed);
        }

        function ProfileSocialNetworkLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ProfileSocialNetwork loaded");
        }

        function ProfileSocialNetworkLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End Load Item ;)

        //Load Components ;)

        function loadSocialNetworks() {
            apiService.get($scope.baseURL + 'api/SocialNetworks', null,
            socialnetworksLoadCompleted,
            socialnetworksLoadFailed);
        }

        function socialnetworksLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.SocialNetworks = response.data.Result;
            notificationService.displaySuccess("SocialNetworks Loaded");
        }

        function socialnetworksLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }        

        //End Load Components ;)

        loadItems();

    }]);
})(angular.module('heroesApp'));     