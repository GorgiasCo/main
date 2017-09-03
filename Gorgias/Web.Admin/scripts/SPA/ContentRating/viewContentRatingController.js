(function (app) {
    'use strict';
    app.controller('viewContentRatingController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewContentRatingController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.ContentRatingID = $route.current.params.contentratingid;
        $scope.hasFile = false;

        loadContentRating();

        function loadContentRating() {
            apiService.get($scope.baseURL + 'api/ContentRating/ContentRatingID/' + $scope.ContentRatingID, null,
            ContentRatingLoadCompleted,
            ContentRatingLoadFailed);
        }

        function ContentRatingLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ContentRating loaded");
        }

        function ContentRatingLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ContentRating/ContentRatingID/' + $scope.ContentRatingID, $scope.object,
            updateContentRatingSucceded,
            updateContentRatingFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateContentRatingSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ContentRatingID = ContentRatingID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateContentRatingFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/contentrating');
        }
        
        function loadContentRatingParents() {
            apiService.get($scope.baseURL + 'api/ContentRatings', null,
            contentRatingParentsLoadCompleted,
            contentRatingParentsLoadFailed);
        }

        function contentRatingParentsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ContentRatingParents = response.data.Result;
            notificationService.displaySuccess("ContentRatingParents Loaded");
        }

        function contentRatingParentsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }


        
        $scope.masterFileName = "contentRating";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.ContentRatingImage + '&MasterFileName=contentRating',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.ContentRatingImage = 'contentrating-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.ContentRatingImage + '&MasterFileName=contentRating';
                });
            },
            removedfile: function (file) {
                var _ref;
                $scope.hasFile = false;
                console.log($scope.hasFile);
                console.log(file);
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            }
        };

        //Handle events for dropzone
        //Visit http://www.dropzonejs.com/#events for more events
        $scope.dzCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
                redirectBack();
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzMethods = {};
        $scope.removedfile = function () {
            $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

        loadContentRatingParents()
        
    }]);
})(angular.module('heroesApp'));