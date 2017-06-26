(function (app) {
    'use strict';
    app.controller('editAlbumTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editAlbumTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.AlbumTypeID = $route.current.params.albumtypeid;
        
        //$scope.AlbumTypeID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadAlbumType();

        function loadAlbumType() {
            apiService.get($scope.baseURL + 'api/AlbumType/AlbumTypeID/' + $scope.AlbumTypeID, null,
            AlbumTypeLoadCompleted,
            AlbumTypeLoadFailed);
        }

        function AlbumTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("AlbumType loaded");
        }

        function AlbumTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/AlbumType/AlbumTypeID/' + $scope.AlbumTypeID, $scope.object,
            updateAlbumTypeSucceded,
            updateAlbumTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateAlbumTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.AlbumTypeID = AlbumTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateAlbumTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/albumtype');
        }
        




        
    }]);
})(angular.module('heroesApp'));