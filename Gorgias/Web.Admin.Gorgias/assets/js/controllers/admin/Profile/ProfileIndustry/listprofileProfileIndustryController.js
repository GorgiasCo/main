(function (app) {
    'use strict';
    app.controller('listprofileProfileIndustryController', ['$scope', '$stateParams', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$filter', 'authService',
        function ($scope, $stateParams, $http, $compile, apiService, ngAuthSettings, $location, notificationService, $filter, authService) {

            var vm = this;
            $scope.item = $stateParams.id;//$route.current.params.id;

            console.log("listprofile ProfileIndustry loaded ;)");

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pagesize = 2;
            $scope.Items = {};
            $scope.tags = [];
            $scope.insertTag = insertObject;

            $scope.delete = deleteRow;
            $scope.ProfileID = $stateParams.id;//$route.current.params.id;
            console.log($stateParams.id, 'ProfileID');
            //$scope.object = { TagName: "", ProfileTagStatus: false };
            $scope.object = {};
            $scope.object.ProfileID = $stateParams.id;//Number($scope.ProfileID);
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
            $scope.adminLimitation = 1;

            checkAdminLimitation();

            function checkAdminLimitation() {
                console.log(authService.authentication.userRole, 'UserRole');
                if (authService.authentication.userRole == 0) {
                    $scope.adminLimitation = 3;
                } else {
                    $scope.adminLimitation = 1;
                }
            }

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
                if (confirm($filter('translate')('DeleteNote'))) {
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
                console.log("Insert Profile Industry");
                console.log($scope.object);
                apiService.post($scope.baseURL + 'api/ProfileIndustry/', $scope.object,
                addProfileIndustrySucceded,
                addProfileIndustryFailed);
                console.log("Post!!!", $scope.object);
            }

            function addProfileIndustrySucceded(result) {
                console.log("Success");
                console.log(result.data.Result);
                loadProfileIndustries();
            }

            function addProfileIndustryFailed(response) {
                console.log("Fail", response);
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
                console.log(response.data.Result);
                $scope.profileIndustries = response.data.Result;
                console.log("Industires Me Loads ;)", $scope.profileIndustries.length);
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
})(angular.module('gorgiasapp'));