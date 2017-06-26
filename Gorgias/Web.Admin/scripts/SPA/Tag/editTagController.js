(function (app) {
    'use strict';
    app.controller('editTagController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editTagController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.TagID = $route.current.params.tagid;
        
        //$scope.TagID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadTag();

        function loadTag() {
            apiService.get($scope.baseURL + 'api/Tag/TagID/' + $scope.TagID, null,
            TagLoadCompleted,
            TagLoadFailed);
        }

        function TagLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Tag loaded");
        }

        function TagLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Tag/TagID/' + $scope.TagID, $scope.object,
            updateTagSucceded,
            updateTagFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateTagSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.TagID = TagID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/tag');
        }
        




        
    }]);
})(angular.module('heroesApp'));