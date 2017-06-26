(function (app) {
    'use strict';
    app.controller('listprofileProfileIndustryController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.item = $route.current.params.id;

        console.log("listprofile ProfileIndustry loaded ;)");

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

        function updateProfileIndustrySucceded(result) {
            console.log("Success" + result.data.Result);
            loadProfileIndustries();
            notificationService.displaySuccess('Deleted.');
        }

        function updateProfileIndustryFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function deleteRow(item) {
            vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/ProfileIndustry/IndustryID/ProfileID/' + item.IndustryID + '/' + $scope.ProfileID, null,
                updateProfileIndustrySucceded,
                updateProfileIndustryFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
            //vm.dtInstance.reloadData();
        }      

        //Insert Tag ;)
        function insertObject() {
            console.log("Insert Tag");
            console.log($scope.object);
            apiService.post($scope.baseURL + 'api/ProfileIndustry/', $scope.object,
            addProfileIndustrySucceded,
            addProfileIndustryFailed);
            console.log("Post!!!" + $scope.object);
        }

        function addProfileIndustrySucceded(result) {
            console.log("Success");
            console.log(result.data.Result);
            $scope.object.TagName = "";
            loadProfileIndustries();
        }

        function addProfileIndustryFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //End of Insert Tag
        //Get all Industries
        function loadProfileIndustries() {
            apiService.get($scope.baseURL + 'api/ProfileIndustry/ProfileID/' + $scope.ProfileID, null,
            ProfileTagLoadCompleted,
            ProfileTagLoadFailed);
        }

        function ProfileTagLoadCompleted(response) {
            console.log("Industires Me Loads ;)");
            console.log(response.data.Result);
            $scope.profileIndustries = response.data.Result;
            notificationService.displaySuccess("ProfileTag loaded");
        }

        function ProfileTagLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadIndustries() {
            apiService.get($scope.baseURL + 'api/Industries', null,
            industriesLoadCompleted,
            industriesLoadFailed);
        }

        function industriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Industries = response.data.Result;
            notificationService.displaySuccess("Tags Loaded");
        }

        function industriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }        
        //End of all Industries
        loadIndustries();
        loadProfileIndustries();
    }]);
})(angular.module('heroesApp'));