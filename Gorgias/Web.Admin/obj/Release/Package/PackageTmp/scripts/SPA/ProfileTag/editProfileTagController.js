(function (app) {
    'use strict';
    app.controller('editProfileTagController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.object = {};
        
        console.log("editProfileTagController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        
        $scope.AddObject = insertObject;        
        
        
        $scope.TagID = $route.current.params.tagid;
        $scope.ProfileID = $route.current.params.profileid;
        
        //$scope.ProfileTagID = $route.current.params.id;                        
        $scope.hasFile = false;

        loadProfileTag();

        function loadProfileTag() {
            apiService.get($scope.baseURL + 'api/ProfileTag/TagID/ProfileID/' + $scope.TagID + "/"  + $scope.ProfileID, null,
            ProfileTagLoadCompleted,
            ProfileTagLoadFailed);
        }

        function ProfileTagLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("ProfileTag loaded");
        }

        function ProfileTagLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL +'api/ProfileTag/TagID/ProfileID/' + $scope.TagID + "/"  + $scope.ProfileID, $scope.object,
            updateProfileTagSucceded,
            updateProfileTagFailed);
            console.log("Post!!!" + $scope.object);
        }
     
        function updateProfileTagSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileTagID = ProfileTagID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }            
        }

        function updateProfileTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profiletag');
        }
        

     
       function loadTags() {
            apiService.get($scope.baseURL + 'api/Tags', null,
            tagsLoadCompleted,
            tagsLoadFailed);
        }

        function tagsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Tags = response.data.Result;
            notificationService.displaySuccess("Tags Loaded");
        }

        function tagsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
     
        loadTags();
     
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
     
        loadProfiles();



        
    }]);
})(angular.module('heroesApp'));