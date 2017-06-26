(function (app) {
    'use strict';
    app.controller('listprofileProfileTagController', ['$scope', '$route', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$filter', function ($scope, $route, $http, $compile, apiService, ngAuthSettings, $location, notificationService, $filter) {

        var vm = this;        
        $scope.item = $route.current.params.id;        

        console.log("listprofile ProfileTag loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};
        $scope.tags = [];
        $scope.insertTag = insertObject;        

        $scope.delete = deleteRow;
        $scope.ProfileID = $route.current.params.id;
        //$scope.object = { TagName: "", ProfileTagStatus: false };
        $scope.object.ProfileID = Number($scope.ProfileID);
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        vm.message = "hi";
        $scope.dtInstance = {};

        function updateProfileTagSucceded(result) {
            console.log("Success" + result.data.Result);
            //$scope.dtInstance._renderer.rerender();
            loadProfileTag();
            notificationService.displaySuccess('Deleted.');
        }

        function updateProfileTagFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function deleteRow(item) {
            vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
            if (confirm($filter('translate')('DeleteNote'))) {
                apiService.deleteItem($scope.baseURL + 'api/ProfileTag/TagID/ProfileID/' + item.TagID + '/' + item.ProfileID, null,
                updateProfileTagSucceded,
                updateProfileTagFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
            //vm.dtInstance.reloadData();
        }       
        //end datatables ;)

        //Insert Tag ;)
        function insertObject() {
            console.log("Insert Tag");
            console.log($scope.object);
            apiService.post($scope.baseURL + 'api/ProfileTag/', $scope.object,
            addProfileTagSucceded,
            addProfileTagFailed);
            console.log("Post!!!" + $scope.object);
        }

        function addProfileTagSucceded(result) {
            console.log("Success");
            console.log(result.data.Result);
            $scope.object.TagName = "";
            loadProfileTag();
        }

        function addProfileTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //End of Insert Tag
        //Get all Tags
        function loadProfileTag() {
            apiService.get($scope.baseURL + 'api/ProfileTags/ProfileID/' + $scope.ProfileID + '/1/30/', null,
            ProfileTagLoadCompleted,
            ProfileTagLoadFailed);
        }

        function ProfileTagLoadCompleted(response) {
            console.log("Tags Me Loads ;)");
            console.log(response.data.Result);
            $scope.tags = response.data.Result.Items;
            notificationService.displaySuccess("ProfileTag loaded");
        }

        function ProfileTagLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End of all Tags
        loadProfileTag();
    }]);
})(angular.module('gorgiasapp'));