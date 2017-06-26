(function (app) {
    'use strict';
    app.controller('editAdminProfileUploadController', ['$scope', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService',
        function ($scope, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};

        console.log("addAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.AlbumID = 0;
        $scope.hasFile = false;
        $scope.uploadDone = false;

        $scope.ProfileID = $stateParams.id;//$route.current.params.id;

        $scope.masterFileName = "album";
        //dropzone web cover image ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=webcover-' + $scope.ProfileID + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: true,
            init: function () {
                this.on("addedfile", function (file) {                                        
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=webcover-' + $scope.ProfileID + '&MasterFileName=profile';
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
            },
            'complete': function (file, xhr) {
                $scope.removedfile(file);
                //$location.path('/admin/profile/' + $scope.ProfileID);
                $route.reload();
                console.log(file, xhr, 'done yohooo');
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzMethods = {};
        $scope.removedfile = function (file) {
            $scope.dzMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

        //dropzone Slider Image ;)
        $scope.dzImagesOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=mobileprofile-' + $scope.ProfileID + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: true,
            init: function () {
                this.on("addedfile", function (file) {
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=mobileprofile-' + $scope.ProfileID + '&MasterFileName=profile';
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
            'complete': function (file, xhr) {
                $scope.removedImagefile(file);
                console.log(file, xhr, 'done');
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzImagesMethods = {};
        $scope.removedImagefile = function (file) {
            $scope.dzImagesMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

        //dropzone mini image normal user Image ;)
        $scope.dzMiniImagesOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=slider-mini-' + $scope.ProfileID + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: true,
            init: function () {
                this.on("addedfile", function (file) {
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=slider-mini-' + $scope.ProfileID + '&MasterFileName=profile';
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
        $scope.dzMiniImagesCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
            },
            'complete': function (file, xhr) {
                $scope.removedMinifile(file);
                console.log(file, xhr, 'done');
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzMiniImagesMethods = {};
        $scope.removedMinifile = function (file) {
            $scope.dzMiniImagesMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)


        //dropzone description image normal user Image ;)
        $scope.dzDescriptionImagesOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=mobile-' + $scope.ProfileID + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: true,
            init: function () {
                this.on("addedfile", function (file) {
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=mobile-' + $scope.ProfileID + '&MasterFileName=profile';
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
        $scope.dzDescriptionImagesCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                $scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
            },
            'complete': function (file, xhr) {
                $scope.removedDescriptionfile(file);
                console.log(file, xhr, 'done');
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzDescriptionImagesMethods = {};
        $scope.removedDescriptionfile = function (file) {
            $scope.dzDescriptionImagesMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)

    }]);
})(angular.module('gorgiasapp'));