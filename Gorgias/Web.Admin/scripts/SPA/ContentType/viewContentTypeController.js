(function (app) {
    'use strict';
    app.controller('viewContentTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewContentTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ContentTypeID = $route.current.params.contenttypeid;
        $scope.hasFile = false;

        loadContentType();

        function loadContentType() {
            apiService.get($scope.baseURL + 'api/ContentType/ContentTypeID/' + $scope.ContentTypeID, null,
            ContentTypeLoadCompleted,
            ContentTypeLoadFailed);
        }

        function ContentTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ContentType loaded");
        }

        function ContentTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ContentType/ContentTypeID/' + $scope.ContentTypeID, $scope.object,
            updateContentTypeSucceded,
            updateContentTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateContentTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ContentTypeID = ContentTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateContentTypeFailed(response) {
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