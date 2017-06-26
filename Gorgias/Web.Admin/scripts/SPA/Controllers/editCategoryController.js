(function (app) {
    'use strict';
    app.controller('editCategoryController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', function ($scope, $route, $http, apiService, ngAuthSettings) {

        var vm = this;
        $scope.object = {};
        $scope.error = "hi salam hallo";
        console.log("editCategoryController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.masterFileName = "category";
        $scope.AddObject = search;       
        $scope.upload = uploadImages;

        $scope.CategoryID = $route.current.params.id;
        $scope.hasFile = false;        

        loadCategory();

        function loadCategory() {
            apiService.get($scope.baseURL + 'api/Category/CategoryID/' + $scope.CategoryID, null,
            countriesLoadCompleted,
            countriesLoadFailed);
        }

        function countriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            //notificationService.displaySuccess("Category Loaded");
        }

        function countriesLoadFailed(response) {
            //notificationService.displayError(response.data.Errors);
        }

        function search() {            
            apiService.post('http://localhost:43587/api/category/CategoryID/' + $scope.CategoryID, $scope.object,
            addMovieSucceded,
            addMovieFailed);
            console.log("Post!!!" + $scope.object);            
        }

        function uploadImages() {
            console.log("Uploaded");
        }
        
        function addMovieSucceded(result) {            
            console.log("add category triggered 3>" + result.data.Result);
            if ($scope.hasFile) {
                $scope.CategoryID = result.data.Result.CategoryID;
                $scope.dzMethods.processQueue();
            } else {
                //back
            }
            console.log(result.data.Result);
        }

        function addMovieFailed(response) {
            console.log("add category triggered !!! Fail");
            $scope.error = response.statusText;
            //notificationService.displayError(response.statusText);
        }

        //dropzone ;)
        $scope.dzOptions = {
            url: 'http://localhost:43587/api/images/name?ImageName=' + $scope.CategoryID + '&MasterFileName=category',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = 'http://localhost:43587/api/images/name?ImageName=' + $scope.CategoryID + '&MasterFileName=category';
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
            'addedfile' : function(file){
                console.log(file);
                $scope.newFile = file;
            },
            'success' : function(file, xhr){
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

    }]);
})(angular.module('heroesApp'));