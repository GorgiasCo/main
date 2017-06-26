(function (app) {
    'use strict';
    app.controller('viewUserController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewUserController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.UserID = $route.current.params.userid;
        $scope.hasFile = false;

        loadUser();

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
        



        
    }]);
})(angular.module('heroesApp'));