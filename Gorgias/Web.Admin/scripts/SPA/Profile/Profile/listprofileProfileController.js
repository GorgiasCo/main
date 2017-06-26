(function (app) {
    'use strict';
    app.controller('listprofileProfileController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        console.log("listprofileProfileSocialNetworkController Address loaded ;)");

        $scope.object = {};

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;

        $scope.isList = true;
        $scope.isAdd = false;
        $scope.isEdit = false;

        $scope.edit = edit;
        $scope.addNew = addNew;
        $scope.redirect = redirect;

        $scope.items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //datatables
        //$scope.dtColumns = [
        //    //here We will add .withOption('name','column_name') for send column name to the server
        //    DTColumnBuilder.newColumn("ProfileFullname", "Fullname").withOption('name', 'ProfileFullname').renderWith(viewHtml),                
        //    DTColumnBuilder.newColumn("ProfileIsPeople", "IsPeople").withOption('name', 'ProfileIsPeople'),
        //    DTColumnBuilder.newColumn("ProfileIsDeleted", "IsDeleted").withOption('name', 'ProfileIsDeleted'),
        //    DTColumnBuilder.newColumn("ProfileDateCreated", "DateCreated").withOption('name', 'ProfileDateCreated'),
        //    DTColumnBuilder.newColumn("ProfileDescription", "Description").withOption('name', 'ProfileDescription'),
        //    DTColumnBuilder.newColumn("ProfileView", "View").withOption('name', 'ProfileView'),
        //    DTColumnBuilder.newColumn("ProfileLike", "Like").withOption('name', 'ProfileLike'),
        //    DTColumnBuilder.newColumn("ProfileURL", "URL").withOption('name', 'ProfileURL'),
        //    DTColumnBuilder.newColumn("ProfileShortDescription", "ShortDescription").withOption('name', 'ProfileShortDescription'),
        //    DTColumnBuilder.newColumn("ProfileEmail", "Email").withOption('name', 'ProfileEmail'),
        //    DTColumnBuilder.newColumn("ProfileStatus", "Status").withOption('name', 'ProfileStatus'),
        //    DTColumnBuilder.newColumn("ProfileIsConfirmed", "IsConfirmed").withOption('name', 'ProfileIsConfirmed'),
        //    DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        //]

        
        //Address Object ;)
        $scope.object = { ProfileID: $route.current.params.id };

        function addNew() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = false;
            $scope.object = {};

            //Load Components
            loadIndustries();
            loadProfileTypes();
            loadThemes();            
        }

        function edit() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = true;

            //Pass Item ID's ;)
            //$scope.objectItemID = item.ProfileID;

            //Load Components
            loadItems();
            loadIndustries();
            loadProfileTypes();
            loadThemes();
        }

        function redirect() {
            $scope.isList = true;
            $scope.isAdd = false;
        }
        //datatables  api/ExternalLinks/ProfileID/

        //Load Profile Item ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/Profile/ProfileID/' + $scope.item, null,
            itemsLoadCompleted,
            itemsLoadFailed);
        }

        function itemsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Profile ;) Loaded");
            console.log("object ;)");
            console.log($scope.object);
        }

        function itemsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        //Insert Object ;) New Profile @ User Admin Required some Changes ;)
        function insertObject() {
            if ($scope.isEdit == false) {
                if ($scope.hasFile) {
                    apiService.post($scope.baseURL + 'api/Profile/', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    notificationService.displayError('Profile Image is required');
                }                
            } else {
                apiService.post($scope.baseURL + 'api/Profile/ProfileID/' + $scope.item, $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("Update Post!!!" + $scope.object);
            }
        }

        function objectSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                $scope.dzMethods.processQueue();
            } else {
                redirect();
            }            
        }

        function objectFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Load Components ;)
        function loadIndustries() {
            apiService.get($scope.baseURL + 'api/Industries', null,
            industriesLoadCompleted,
            industriesLoadFailed);
        }

        function industriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Industries = response.data.Result;
            notificationService.displaySuccess("Industries Loaded");
        }

        function industriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        function loadProfileTypes() {
            apiService.get($scope.baseURL + 'api/ProfileTypes', null,
            profileTypesLoadCompleted,
            profileTypesLoadFailed);
        }

        function profileTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ProfileTypes = response.data.Result;
            notificationService.displaySuccess("ProfileTypes Loaded");
        }

        function profileTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        function loadThemes() {
            apiService.get($scope.baseURL + 'api/Themes', null,
            themesLoadCompleted,
            themesLoadFailed);
        }

        function themesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Themes = response.data.Result;
            notificationService.displaySuccess("Themes Loaded");
        }

        function themesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        function loadSubscriptionTypes() {
            apiService.get($scope.baseURL + 'api/SubscriptionTypes', null,
            subscriptionTypesLoadCompleted,
            subscriptionTypesLoadFailed);
        }

        function subscriptionTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.SubscriptionTypes = response.data.Result;
            notificationService.displaySuccess("SubscriptionTypes Loaded");
        }

        function subscriptionTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End Load Components

        $scope.masterFileName = "profile";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile';
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
                redirect();
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
        loadItems();

    }]);
})(angular.module('heroesApp'));     