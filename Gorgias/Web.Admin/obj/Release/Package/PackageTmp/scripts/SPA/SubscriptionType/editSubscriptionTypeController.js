(function (app) {
    'use strict';
    app.controller('editSubscriptionTypeController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editSubscriptionTypeController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.SubscriptionTypeID = $route.current.params.subscriptiontypeid;
        
        //$scope.SubscriptionTypeID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadSubscriptionType();

        function loadSubscriptionType() {
            apiService.get($scope.baseURL + 'api/SubscriptionType/SubscriptionTypeID/' + $scope.SubscriptionTypeID, null,
            SubscriptionTypeLoadCompleted,
            SubscriptionTypeLoadFailed);
        }

        function SubscriptionTypeLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("SubscriptionType loaded");
        }

        function SubscriptionTypeLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/SubscriptionType/SubscriptionTypeID/' + $scope.SubscriptionTypeID, $scope.object,
            updateSubscriptionTypeSucceded,
            updateSubscriptionTypeFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateSubscriptionTypeSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.SubscriptionTypeID = SubscriptionTypeID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateSubscriptionTypeFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/subscriptiontype');
        }
        



        
        $scope.masterFileName = "subscriptionType";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.SubscriptionTypeImage + '&MasterFileName=subscriptionType',
            paramName : 'photo',
            maxFilesize : '10',
            acceptedFiles : 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.SubscriptionTypeImage = 'subscriptiontype-' + file.name;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.SubscriptionTypeImage + '&MasterFileName=subscriptionType';
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