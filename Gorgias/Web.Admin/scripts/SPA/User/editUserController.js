(function (app) {
    'use strict';
    app.controller('editUserController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editUserController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.UserID = $route.current.params.userid;
        
        //$scope.UserID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadUser();
        loadCountries();

        function loadUser() {
            apiService.get($scope.baseURL + 'api/User/UserID/' + $scope.UserID, null,
            UserLoadCompleted,
            UserLoadFailed);
        }

        function UserLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("User loaded");
        }

        function UserLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/User/UserID/' + $scope.UserID, $scope.object,
            updateUserSucceded,
            updateUserFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateUserSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.UserID = UserID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateUserFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/user');
        }
        
        function loadCountries() {
            apiService.get($scope.baseURL + 'api/Countries', null,
            countriesLoadCompleted,
            countriesLoadFailed);
        }

        function countriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Countries = response.data.Result;
            notificationService.displaySuccess("Cities Loaded");
        }

        function countriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }




        
    }]);
})(angular.module('heroesApp'));