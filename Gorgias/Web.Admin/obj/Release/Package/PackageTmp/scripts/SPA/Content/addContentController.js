(function (app) {
    'use strict';
    app.controller('addContentController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addContentController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.ContentID = 0;
        $scope.hasFile = false;

        function insertObject() {
            if ($scope.hasFile) {
                $scope.dzImagesMethods.processQueue();
            } else {
                apiService.post($scope.baseURL + 'api/Content/', $scope.object,
                addContentSucceded,
                addContentFailed);
                console.log("Post!!!" + $scope.object);
                console.log("Post!!!" + $scope.object);
            }
        }
     
        function addContentSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ContentID = result.data.Result.ContentID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addContentFailed(response) {
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

        //dropzone Contents ;)
        $scope.dzImagesOptions = {
            url: $scope.baseURL + 'api/images/?MasterFileName=content',
            paramName: 'photo',
            maxFilesize: '100',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 10,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    console.log($scope.hasFile);
                    $scope.filename = file.name;
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/?MasterFileName=content';
                });
                this.on("complete", function (file) {
                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        //redirectBack();
                    }
                });
                this.on("queuecomplete", function (file) {
                    alert("All files have uploaded ");
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
        $scope.dzImagesCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
                console.log(xhr, 'Yooohoooo', xhr.Result[0].FileUrl);
                $scope.object.ContentURL = xhr.Result[0].FileUrl;
            },
            'complete': function (file, xhr) {
                $scope.removedfile(file)
                //$scope.object.ContentURL = file.name;
                console.log(file, xhr, 'done');
                insertObject();
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzImagesMethods = {};
        $scope.removedfile = function (file) {
            $scope.dzImagesMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

        loadAlbums()
        
    }]);
})(angular.module('heroesApp'));