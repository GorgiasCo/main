(function (app) {
    'use strict';
    app.controller('viewAlbumController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.AlbumID = $route.current.params.albumid;
        $scope.hasFile = false;

        loadAlbum();

        function loadAlbum() {
            apiService.get($scope.baseURL + 'api/Album/AlbumID/' + $scope.AlbumID, null,
            AlbumLoadCompleted,
            AlbumLoadFailed);
        }

        function AlbumLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Album loaded");
        }

        function AlbumLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Album/AlbumID/' + $scope.AlbumID, $scope.object,
            updateAlbumSucceded,
            updateAlbumFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateAlbumSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.AlbumID = AlbumID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateAlbumFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/album');
        }
        
        function loadCategories() {
            apiService.get($scope.baseURL + 'api/Categories', null,
            categoriesLoadCompleted,
            categoriesLoadFailed);
        }

        function categoriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Categories = response.data.Result;
            notificationService.displaySuccess("Categories Loaded");
        }

        function categoriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
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



        loadCategories()
        loadProfiles()
        
    }]);
})(angular.module('heroesApp'));