(function (app) {
    'use strict';
    app.controller('viewFeatureController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("viewFeatureController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        $scope.FeatureID = $route.current.params.featureid;
        $scope.hasFile = false;

        loadFeature();

        function loadFeature() {
            apiService.get($scope.baseURL + 'api/Feature/FeatureID/' + $scope.FeatureID, null,
            FeatureLoadCompleted,
            FeatureLoadFailed);
        }

        function FeatureLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Feature loaded");
        }

        function FeatureLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/Feature/FeatureID/' + $scope.FeatureID, $scope.object,
            updateFeatureSucceded,
            updateFeatureFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateFeatureSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.FeatureID = FeatureID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateFeatureFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/feature');
        }
        


        
        $scope.masterFileName = "feature";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.FeatureImage + '&MasterFileName=feature',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.FeatureImage = 'feature-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.FeatureImage + '&MasterFileName=feature';
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

        
    }]);
})(angular.module('heroesApp'));