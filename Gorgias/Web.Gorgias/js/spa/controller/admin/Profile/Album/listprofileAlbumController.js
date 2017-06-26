(function (app) {
    'use strict';
    app.controller('listprofileAlbumController', ['$scope', '$route', '$http', '$compile', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$filter', 'uuid', function ($scope, $route, $http, $compile, apiService, ngAuthSettings, $location, notificationService, $filter, uuid) {

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;
        $scope.AlbumItems = [];

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        $scope.timestamp = new Date().getUTCMilliseconds();

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
        $scope.extraImages = false;
        $scope.uploadDone = true;

        $scope.items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;


        //Object ;)
        $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true, AlbumAvailability: 0 };

        function addNew() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = false;
            $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true, AlbumAvailability: 0 };

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
            //$scope.isList = true;
            //$scope.isAdd = false;
            $route.reload();
        }
        //datatables  api/ExternalLinks/ProfileID/

        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/Albums/Profile/' + $scope.item + '/1/50', null,
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
                    $scope.object.AlbumAvailability = 0;
                    apiService.post($scope.baseURL + 'api/Album/Hottest', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    notificationService.displayError('Album cover is required.');
                }
            } else {
                $scope.object.AlbumAvailability = 0;
                apiService.post($scope.baseURL + 'api/Album/Hottest/AlbumID/' + $scope.objectItemID, $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("Update Post!!!" , $scope.object);
            }
        }

        function objectSucceded(result) {
            console.log("Success", result.data.Result);
            if ($scope.isEdit == false) {
                if ($scope.hasFile) {
                    //$scope.AlbumID = result.data.Result.AlbumID;
                    $scope.object = result.data.Result;
                    $scope.dzMethods.processQueue();
                } else {
                    loadItems();
                    redirect();
                }
            } else {
                if ($scope.hasFile) {
                    //$scope.AlbumID = result.data.Result.AlbumID;
                    $scope.object = result.data.Result;
                    $scope.dzMethods.processQueue();                    
                } else {
                    if ($scope.extraImages == true) {
                        $scope.dzImagesMethods.processQueue();
                    } else {
                        loadItems();
                        redirect();
                    }
                }                
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
            if (confirm($filter('translate')('DeleteNote'))) {
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
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.filename + '&MasterFileName=album',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    //$scope.object.AlbumCover = 'album-' + file.name;
                    var newFileName = uuid.v4() + '.jpg';
                    $scope.filename = 'album-' + newFileName;// file.name;//uuid.v4()
                    console.log('filename', newFileName, $scope.filename);
                    $scope.object.AlbumCover = ngAuthSettings.cdn_albums + 'album-' + newFileName;//file.name;//uuid.v4()
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.filename + '&MasterFileName=album';
                });
                this.on("queuecomplete", function (file) {
                    if ($scope.extraImages == true) {
                        $scope.dzImagesMethods.processQueue();
                    } else {
                        loadItems();
                        redirect();
                    }
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
                if ($scope.extraImages == true) {
                    $scope.dzImagesMethods.processQueue();
                }                
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
            parallelUploads: 10,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    //$scope.hasFile = true;
                    $scope.extraImages = true;
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    file.filename = uuid.v4();
                    console.log(uuid.v4(), 'content Name');
                    this.options.url = $scope.baseURL + 'api/images/Album/?AlbumID=' + $scope.object.AlbumID + '&MasterFileName=content';
                });
                this.on("complete", function (file) {

                });
                this.on("queuecomplete", function (file) {
                    alert("All files have been uploaded ");
                    if ($scope.isEdit) {
                        loadContent();
                    } else {
                        loadItems();
                        $scope.object = { ProfileID: $route.current.params.id, AlbumStatus: true };
                    }
                    redirect();
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
                $scope.removedImagefile(file)
                console.log(file, xhr, 'done');
            },
            'queuecomplete': function () {
                console.log('All Images Done ;)');
            }
        };


        //Apply methods for dropzone
        //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
        $scope.dzImagesMethods = {};
        $scope.removedImagefile = function (file) {
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
            if (confirm($filter('translate')('DeleteNote'))) {
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

    }]);
})(angular.module('gorgiasapp'));