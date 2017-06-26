(function (app) {
    'use strict';
    app.controller('addAddressController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("addAddressController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;
        
        $scope.AddressID = 0;
        $scope.hasFile = false;

        function insertObject() {
            apiService.post($scope.baseURL +'api/Address/', $scope.object,
            addAddressSucceded,
            addAddressFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function addAddressSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                $scope.AddressID = result.data.Result.AddressID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function addAddressFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/address');
        }
        
        function loadCities() {
            apiService.get($scope.baseURL + 'api/Cities', null,
            citiesLoadCompleted,
            citiesLoadFailed);
        }

        function citiesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Cities = response.data.Result;
            notificationService.displaySuccess("Cities Loaded");
        }

        function citiesLoadFailed(response) {
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
        function loadAddressTypes() {
            apiService.get($scope.baseURL + 'api/AddressTypes', null,
            addressTypesLoadCompleted,
            addressTypesLoadFailed);
        }

        function addressTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.AddressTypes = response.data.Result;
            notificationService.displaySuccess("AddressTypes Loaded");
        }

        function addressTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        $scope.masterFileName = "address";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.AddressImage + '&MasterFileName=address',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.AddressImage = 'address-' + $scope.AddressID + ".jpg";
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.AddressImage + '&MasterFileName=address';
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

        loadCities()
        loadProfiles()
        loadAddressTypes()
        
    }]);
})(angular.module('heroesApp'));