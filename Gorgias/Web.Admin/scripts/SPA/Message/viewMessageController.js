(function (app) {
    'use strict';
    app.controller('viewMessageController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewMessageController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.MessageID = $route.current.params.messageid;
        $scope.hasFile = false;

        loadMessage();

        function loadMessage() {
            apiService.get($scope.baseURL + 'api/Message/MessageID/' + $scope.MessageID, null,
            MessageLoadCompleted,
            MessageLoadFailed);
        }

        function MessageLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Message loaded");
        }

        function MessageLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Message/MessageID/' + $scope.MessageID, $scope.object,
            updateMessageSucceded,
            updateMessageFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateMessageSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.MessageID = MessageID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateMessageFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/message');
        }
        
        function loadProfiles() {
            apiService.get($scope.baseURL + 'api/Profiles', null,
            profilesLoadCompleted,
            profilesLoadFailed);
        }

        function profilesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Profiles = response.data.Result;
            notificationService.displaySuccess("Profiles Loaded");
        }

        function profilesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadProfiles()
        
    }]);
})(angular.module('heroesApp'));