(function (app) {
    'use strict';
    app.controller('addAlbumController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', function ($scope, $route, $http, apiService, ngAuthSettings) {

        var vm = this;
        $scope.object = {};
        $scope.error = "hi salam hallo";
        console.log("addAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.masterFileName = "album";
        $scope.AddObject = insertObject;
        $scope.upload = uploadImages;

        function insertObject() {
            apiService.post('http://localhost:43587/api/album/', $scope.object,
            addMovieSucceded,
            addMovieFailed);
            console.log("Post!!!" + $scope.object);
        }

        function loadCategories() {
            apiService.get($scope.baseURL+'api/categories/1/10', null,
            genresLoadCompleted,
            genresLoadFailed);
        }

        function genresLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Categories = response.data.Result.Items;
        }

        function genresLoadFailed(response) {
            //notificationService.displayError(response.data);
        }

        function loadProfiles() {
            apiService.get($scope.baseURL+'api/profiles/1/10', null,
            profilesLoadCompleted,
            profilesLoadFailed);
        }

        function profilesLoadCompleted(response) {
            $scope.Profiles = response.data.Result.Items;
        }

        function profilesLoadFailed(response) {
            //notificationService.displayError(response.data);
        }

        function uploadImages() {
            console.log("Uploaded");
        }

        function addMovieSucceded(result) {
            console.log("add category triggered 3>" + result.data.Result);
        }

        function addMovieFailed(response) {
            console.log("add category triggered !!! Fail");
            $scope.error = response.statusText;
            //notificationService.displayError(response.statusText);
        }

        //dropzone ;)
        $scope.dzOptions = {
            url: 'http://localhost:43587/api/images?MasterFileName=thanksgod',
            paramName: 'photo',
            maxFilesize: '13',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            removedfile: function (file) {
                var _ref;
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
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzMethods = {};
        $scope.removedfile = function () {
            $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)


        //Load Related Information
        loadCategories();
        loadProfiles();
    }]);
})(angular.module('heroesApp'));