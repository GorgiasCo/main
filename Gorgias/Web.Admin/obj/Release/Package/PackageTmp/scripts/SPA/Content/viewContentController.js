(function (app) {
    'use strict';
    app.controller('viewContentController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewContentController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ContentID = $route.current.params.contentid;
        $scope.hasFile = false;

        loadContent();

        function loadContent() {
            apiService.get($scope.baseURL + 'api/Content/ContentID/' + $scope.ContentID, null,
            ContentLoadCompleted,
            ContentLoadFailed);
        }

        function ContentLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Content loaded");
        }

        function ContentLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Content/ContentID/' + $scope.ContentID, $scope.object,
            updateContentSucceded,
            updateContentFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateContentSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ContentID = ContentID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateContentFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/content');
        }
        
        function loadAlbums() {
            apiService.get($scope.baseURL + 'api/Albums', null,
            albumsLoadCompleted,
            albumsLoadFailed);
        }

        function albumsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Albums = response.data.Result;
            notificationService.displaySuccess("Albums Loaded");
        }

        function albumsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadAlbums()
        
    }]);
})(angular.module('heroesApp'));