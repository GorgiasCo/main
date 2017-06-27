(function (app) {
    'use strict';
    app.controller('listprofileAlbumController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', 'uuid', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService, uuid) {

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;
        $scope.AlbumItems = [];

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        console.log("listprofileProfileSocialNetworkController Address loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;

        $scope.isList = true;
        $scope.isAdd = false;
        $scope.isEdit = false;

        $scope.edit = edit;
        $scope.delete = deleteRow;
        $scope.addNew = addNew;
        $scope.redirect = redirect;
        $scope.deleteContent = deleteContentRow;

        $scope.hasFile = false;
        $scope.uploadDone = false;

        $scope.items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;


        //Object ;)
        $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true };

        function addNew() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = false;
            $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true };

            //Load Components
            loadCategories();
        }

        function edit(item) {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = true;

            //Pass Item ID's ;)
            $scope.objectItemID = item.AlbumID;

            //Load Components
            loadAlbum(item);
            loadContent();
            loadCategories();
        }

        function redirect() {
            $scope.isList = true;
            $scope.isAdd = false;
        }
        //datatables  api/ExternalLinks/ProfileID/

        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/Albums/Profile/' + $scope.item + '/1/30', null,
            itemsLoadCompleted,
            itemsLoadFailed);
        }

        function itemsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.items = response.data.Result.Items;
            notificationService.displaySuccess("Albums Loaded");
        }

        function itemsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        //Insert Object ;)
        function insertObject() {
            if ($scope.isEdit == false) {
                if ($scope.hasFile) {
                    //$scope.object.AlbumAvailability = 0;
                    apiService.post($scope.baseURL + 'api/Album/Hottest', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    notificationService.displayError('Album cover is required.');
                }
            } else {
                //$scope.object.AlbumAvailability = 0;
                apiService.post($scope.baseURL + 'api/Album/Hottest/AlbumID/' + $scope.objectItemID, $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("Update Post!!!" + $scope.object);
            }
        }

        function objectSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.AlbumID = result.data.Result.AlbumID;
                $scope.object = result.data.Result;
                $scope.dzMethods.processQueue();
            } else {
                loadItems();
                redirect();
            }
        }

        function objectFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Delete Item
        function deleteRow(item) {
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/Album/AlbumID/' + item.AlbumID, null,
                updateExternalLinkSucceded,
                updateExternalLinkFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
        }

        function updateExternalLinkSucceded(result) {
            console.log("Success" + result.data.Result);
            loadItems();
            notificationService.displaySuccess('Deleted.');
        }

        function updateExternalLinkFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }
        //end ;)      

        //Load Components
        function loadAlbum(item) {
            apiService.get($scope.baseURL + 'api/Album/AlbumID/' + item.AlbumID, null,
            AlbumLoadCompleted,
            AlbumLoadFailed);
        }

        function AlbumLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Album loaded");
        }

        function AlbumLoadFailed(response) {
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
        //End Load Components

        //Dropzone Uplod Files ;)
        $scope.masterFileName = "album";
        $scope.filename = '';
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.filename,
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    if ($scope.object.AlbumAvailability > 0) {
                        $scope.hasFile = true;
                        $scope.albumV4Name = uuid.v4();
                        $scope.filename = 'hottest-album-' + $scope.albumV4Name + '.jpg';
                        $scope.object.AlbumCover = ngAuthSettings.cdn_albums + 'hottest-album-' + $scope.albumV4Name + '.jpg';//file.name;
                        console.log($scope.hasFile);
                    } else {
                        $scope.hasFile = true;
                        $scope.albumV4Name = uuid.v4();
                        $scope.filename = 'album-' + $scope.albumV4Name + '.jpg' + '&MasterFileName=album';
                        $scope.object.AlbumCover = ngAuthSettings.cdn_albums + 'album-' + $scope.albumV4Name + '.jpg';//file.name;
                        console.log($scope.hasFile);
                    }
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
                if ($scope.object.AlbumAvailability > 0) {
                    loadSendNotification(1);
                } else {
                    loadSendNotification(2)
                }
                $scope.dzImagesMethods.processQueue();
                //redirectBack();
            },
            'complete': function (file, xhr) {
                $scope.removedfile(file)
                console.log(file, xhr, 'done');
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
                    file.filename = uuid.v4();
                    this.options.url = $scope.baseURL + 'api/images/Album/?AlbumID=' + $scope.object.AlbumID + '&MasterFileName=content';
                });
                this.on("complete", function (file) {
                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        //;
                    }
                });
                this.on("queuecomplete", function (file) {
                    alert("All files have been uploaded ");
                    if ($scope.isEdit) {
                        loadSendNotification(3);
                        loadContent();
                    } else {
                        loadItems();
                        $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true };
                    }                    
                    //redirect();
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
                $scope.removedContentfile(file)
                console.log(file, xhr, 'done');
            },
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzImagesMethods = {};
        $scope.removedContentfile = function (file) {
            $scope.dzImagesMethods.removeFile(file); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)   

        loadItems();


        //Load Content for Edit ;)
        function loadContent() {
            apiService.get($scope.baseURL + 'api/Contents/Album/' + $scope.objectItemID + '/1/100/', null,
            ContentLoadCompleted,
            ContentLoadFailed);
        }

        function ContentLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.AlbumItems = response.data.Result.Items;
            notificationService.displaySuccess("Content loaded");
        }

        function ContentLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End Content for Edit ;)

        //Delete Content Item
        function deleteContentRow(item) {
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/Content/ContentID/' + item.ContentID, null,
                updateDeleteContentSucceded,
                updateDeleteContentFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
        }

        function updateDeleteContentSucceded(result) {
            console.log("Success" + result.data.Result);
            loadContent();
            notificationService.displaySuccess('Deleted.');
        }

        function updateDeleteContentFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }
        //end ;)      

        loadAvailabilityItems();

        function loadAvailabilityItems() {
            apiService.get($scope.baseURL + 'api/Mobile/Album/Availability/' + $scope.item, null,
            availabilityItemsLoadCompleted,
            availabilityItemsLoadFailed);
        }

        function availabilityItemsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Availabilities = response.data.Result;
            notificationService.displaySuccess("Albums Loaded");
        }

        function availabilityItemsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        loadProfile();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Profile/ProfileID/' + $scope.item, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response.data.Result, 'Notification Profile Profile Profile ;)');
            $scope.nProfile = response.data.Result;
        }

        function ProfileLoadFailed(error) {
            console.log(error, 'Notification');
        }

        function _secondsToString(seconds) {
            var value = seconds;
            console.log(seconds, 'Notification');

            var units = {
                "year": 24 * 60 * 365,
                "month": 24 * 60 * 30,
                "week": 24 * 60 * 7,
                "day": 24 * 60,
                "hour": 60,
                "minute": 1
            }

            $scope.resultN = []

            for (var name in units) {
                var p = Math.floor(value / units[name]);
                if (p == 1) $scope.resultN.push(" " + p + " " + name);
                if (p >= 2) $scope.resultN.push(" " + p + " " + name + "s");
                value %= units[name]
            }
            //return result;
            console.log($scope.resultN, 'Notification');
        }

        _secondsToString(60);
        console.log($scope.resultN, 'Notification');



        function loadSendNotification(item) {
            if (item == 1) {
                //New Candid
                _secondsToString($scope.object.AlbumAvailability);

                for (var i = 0, len = $scope.Availabilities.length; i < len; i++) {
                    var obj = $scope.Availabilities[i];
                    console.log(obj, 'Notification lol');
                    if (obj.AlbumAvailability == $scope.object.AlbumAvailability) {
                        console.log(obj, 'Notification II');
                        $scope.AvailabilityName = obj.AlbumTypeName;
                    }
                }

                $scope.notificationDate = {
                    body: $scope.nProfile.ProfileFullname + " has published awesome candid " + $scope.AvailabilityName + " expires in" + $scope.resultN,
                    title: "ICONIC Candid",
                    albumid: $scope.object.AlbumID,
                    channelid: "ch" + $scope.item
                };
            } else if (item == 2) {
                //New Album                    
                if ($scope.object.CategoryID != 6) {
                    $scope.albumType = ' album';
                } else {
                    $scope.albumType = ' moment';
                }
                $scope.notificationDate = {
                    body: $scope.nProfile.ProfileFullname + " has published new awesome " + $scope.albumType,
                    title: "Update",
                    albumid: $scope.object.AlbumID,
                    channelid: "chp" + $scope.item
                };
            } else {
                //Update Album
                //New Album                    
                if ($scope.object.CategoryID != 6) {
                    $scope.albumType = ' album';
                } else {
                    $scope.albumType = ' moment';
                }
                $scope.notificationDate = {
                    body: $scope.nProfile.ProfileFullname + " has updated the album",
                    title: "Update",
                    albumid: $scope.object.AlbumID,
                    channelid: "chp" + $scope.item
                };
            }
            console.log($scope.notificationDate, 'Notification');
            apiService.post($scope.baseURL + 'api/web/notification', $scope.notificationDate,
            sendNotificationLoadCompleted,
            sendNotificationLoadFailed);
        }

        function sendNotificationLoadCompleted(response) {
            console.log(response.data.Result, 'Notification');

        }

        function sendNotificationLoadFailed(response) {
            console.log(response, 'Notification');
        }


    }]);
})(angular.module('heroesApp'));