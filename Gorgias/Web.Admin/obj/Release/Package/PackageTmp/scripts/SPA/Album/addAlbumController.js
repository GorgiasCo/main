(function (app) {
    'use strict';
    app.controller('addAlbumController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.AlbumID = 0;
        $scope.hasFile = false;
        $scope.uploadDone = false;        

        function insertObject() {
            apiService.post($scope.baseURL +'api/Album/Hottest/', $scope.object,
            addAlbumSucceded,
            addAlbumFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addAlbumSucceded(result) {
            console.log("Success Album");
            console.log(result.data.Result);
            if ($scope.hasFile) {
                //$scope.AlbumID = result.data.Result.AlbumID;
                $scope.object = result.data.Result;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addAlbumFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/album');
        }
        
        function loadAlbumTypes() {
            apiService.get($scope.baseURL + 'api/AlbumTypes', null,
            albumTypeLoadCompleted,
            albumTypeLoadFailed);
        }

        function albumTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.AlbumTypes = response.data.Result;
            notificationService.displaySuccess("Categories Loaded");
        }

        function albumTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
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

        loadAlbumTypes();
        loadCategories();
        loadProfiles();

        $scope.filename = '';
        $scope.masterFileName = "album";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.filename + '&MasterFileName=album',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.filename = 'hottest-album-' + file.name;
                    //$scope.object.cdn_albums = ngAuthSettings.cdn_images + 'album-' + file.name;
                    $scope.object.AlbumCover = ngAuthSettings.cdn_albums + 'hottest-album-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.filename + '&MasterFileName=album';
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
                $scope.uploadDone = true;
                $scope.dzImagesMethods.processQueue();
                //redirectBack();
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

        //dropzone Contents ;)
        $scope.dzImagesOptions = {
            url: $scope.baseURL + 'api/images/Album/?AlbumID=' + $scope.object.AlbumID + '&MasterFileName=content',
            paramName: 'photo',
            maxFilesize: '100',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 10,
            autoProcessQueue: true,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;                    
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/Album/?AlbumID=' + $scope.object.AlbumID + '&MasterFileName=content';
                });
                this.on("complete", function (file) {
                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        redirectBack();
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
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzImagesMethods = {};
        $scope.removedfile = function () {
            $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)


        
    }]);
})(angular.module('heroesApp'));