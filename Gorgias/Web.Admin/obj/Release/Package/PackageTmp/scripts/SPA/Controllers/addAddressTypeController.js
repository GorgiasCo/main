(function (app) {
    'use strict';
    app.controller('addAddressTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};

        console.log("addAlbumController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.AddObject = insertObject;
        $scope.upload = uploadImages;

        function insertObject() {
            apiService.post('http://localhost:43587/api/AddressType/', $scope.object,
            addAddressTypeSucceded,
            addAddressTypeFailed);
            console.log("Post!!!" + $scope.object);
        }

        function uploadImages() {
            console.log("Uploaded");
        }

        function addAddressTypeSucceded(result) {
            console.log("add AddressType triggered 3>" + result.data.Result);
            redirectBack();
        }

        function addAddressTypeFailed(response) {
            console.log("add category triggered !!! Fail");
            console.log(response);
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
        }

        function redirectBack() {
            $location.url('addressType');
        }


        $scope.masterFileName = "addressType";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images?MasterFileName=' + $scope.masterFileName,
            paramName: 'photo',
            maxFilesize: '1',
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


    }]);
})(angular.module('heroesApp'));