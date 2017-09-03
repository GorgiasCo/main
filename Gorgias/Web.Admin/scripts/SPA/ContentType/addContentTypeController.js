(function (app) {
    'use strict';
    app.controller('addContentTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addContentTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ContentTypeID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/ContentType/', $scope.object,
            addContentTypeSucceded,
            addContentTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addContentTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ContentTypeID = result.data.Result.ContentTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addContentTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/contenttype');
        }
        
        function loadContentTypeParents() {
            apiService.get($scope.baseURL + 'api/ContentTypes', null,
            contentTypeParentsLoadCompleted,
            contentTypeParentsLoadFailed);
        }

        function contentTypeParentsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ContentTypeParents = response.data.Result;
            notificationService.displaySuccess("ContentTypeParents Loaded");
        }

        function contentTypeParentsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }



        loadContentTypeParents()
        
    }]);
})(angular.module('heroesApp'));