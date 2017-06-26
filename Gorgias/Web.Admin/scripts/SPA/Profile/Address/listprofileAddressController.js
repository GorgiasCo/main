(function (app) {
    'use strict';
    app.controller('listprofileAddressController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', 'NgMap', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService, NgMap) {
        var vm = this;

        $scope.item = $route.current.params.id;
        $scope.objectItemID = 0;

        $scope.googleMapsUrl = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyCDxJuNOxTZS9JfDS94t96pYlsq2XbAYRw';

        $scope.AddObject = insertObject;
        $scope.hasFile = false;

        console.log("listprofile Address loaded ;)");

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

        $scope.items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //Address Object ;)
        $scope.object = { ProfileID: $route.current.params.id, AddressStatus: true };

        vm.stores = {
            foo: { position: [41, -87], items: [1, 2, 3, 4] },
            bar: { position: [41, -83], items: [5, 6, 7, 8] }
        };

        $scope.isValidated = false;
        $scope.mapIsValidate = false;
        vm.validate = false;
        console.log($scope.isValidated, 'isValidated');
        $scope.initMap = function (mapId) {            
            console.log('functioncalled', mapId);
            $scope.isValidated = true;
            var currentAddress = $scope.object.AddressAddress;
            $scope.object.AddressAddress = '13 West Clarendon Road';
            NgMap.getMap("foo22").then(function (map) {
                vm.map = map;
                console.log(map.getCenter());
                console.log('markers', map.markers);
                console.log('shapes', map.shapes);
                $scope.mapIsValidate = true;
                //var center = vm.map.getCenter();
                google.maps.event.trigger('foo22', "resize");
                //vm.map.setCenter(center);
                console.log($scope.isValidated, 'isValidated');
                vm.validate = true;

                $scope.object.AddressAddress = currentAddress;

                NgMap.getMap("foo22").then(function (map) {
                    vm.map = map;
                    console.log(map.getCenter());
                    console.log('markers', map.markers);
                    console.log('shapes', map.shapes);
                    $scope.mapIsValidate = true;
                    //var center = vm.map.getCenter();
                    google.maps.event.trigger('foo22', "resize");
                    //vm.map.setCenter(center);
                    console.log($scope.isValidated, 'isValidated');
                    vm.validate = true;
                });

            });

            //$scope.object.AddressAddress = currentAddress;
            //NgMap.getMap("foo22").then(function (map) {
            //    vm.map = map;
            //    console.log(map.getCenter());
            //    console.log('markers', map.markers);
            //    console.log('shapes', map.shapes);
            //    $scope.mapIsValidate = true;
            //    //var center = vm.map.getCenter();
            //    google.maps.event.trigger('foo22', "resize");
            //    //vm.map.setCenter(center);
            //    console.log($scope.isValidated, 'isValidated');
            //    vm.validate = true;
            //});
            if (!$scope.isValidated) {
                //vm.map = NgMap.initMap(mapId);
                //console.log('vm.map 2', vm.map);
                //$scope.mapIsValidate = true;
                //var center = vm.map.getCenter();
                //google.maps.event.trigger('foo22', "resize");
                //vm.map.setCenter(center);
                
            }            
        }

        $scope.initEditMap = function (mapId) {
            console.log('functioncalled', mapId);
            var currentAddress = $scope.object.AddressAddress;
            $scope.object.AddressAddress = '13 West Clarendon Road';
            NgMap.getMap("foo22").then(function (map) {
                vm.map = map;
                var center = vm.map.getCenter();
                vm.map.setCenter(center);
                google.maps.event.trigger('foo22', "resize");
                $scope.object.AddressAddress = currentAddress;
                NgMap.getMap("foo22").then(function (map) {
                    vm.map = map;
                    var center = vm.map.getCenter();
                    vm.map.setCenter(center);
                    google.maps.event.trigger('foo22', "resize");
                });
            });
        }

        vm.showStore = function (evt, storeId) {
            vm.store = vm.stores[storeId];
            console.log('vm.map', vm.map)
            vm.map.showInfoWindow('bar', this);
        };


        //vm.initMap('foo');
        
        //var map;
        $scope.$on('mapInitialized', function (evt, evtMap) {
            //map = evtMap;
            $scope.markerMove = function (e) {
                $scope.latitude = vm.map.markers[0].getPosition().lat();
                $scope.longitude = vm.map.markers[0].getPosition().lng();
                console.log('Dragged');
                console.log(vm.map.markers[0].getPosition().lng() + ' - ' + vm.map.markers[0].getPosition().lat())
            }
        });

        $scope.toggleBounce = function() {
            if (this.getAnimation() != null) {
                this.setAnimation(null);
            } else {
                this.setAnimation(google.maps.Animation.BOUNCE);
            }
        }

        function addNew() {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = false;
            $scope.object = { ProfileID: $route.current.params.id, AddressStatus: true};          
            $scope.isValidated = false;

            //Load Components
            loadAddressTypes();
            loadCities();
        }

        function edit(item) {
            $scope.isList = false;
            $scope.isAdd = true;
            $scope.isEdit = true;

            $scope.isValidated = true;
            //Pass Item ID's ;)
            $scope.objectItemID = item.AddressID;

            //Load Components
            loadAddressTypes();
            loadCities();
            loadAddress(item);
        }

        function redirect() {
            $scope.isList = true;
            $scope.isAdd = false;            
        }

        //Loading Itam ;)
        function loadAddress(item) {
            apiService.get($scope.baseURL + 'api/Address/AddressID/' + item.AddressID, null,
            AddressLoadCompleted,
            AddressLoadFailed);
        }

        function AddressLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            $scope.initEditMap('foo22');
            notificationService.displaySuccess("Address loaded");
        }

        function AddressLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
                      
        //Load Profile Items ;)
        function loadItems() {
            apiService.get($scope.baseURL + 'api/Addresses/Profile/' + $scope.item + '/1/30' , null,
            itemsLoadCompleted,
            itemsLoadFailed);
        }

        function itemsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.items = response.data.Result.Items;
            notificationService.displaySuccess("Addresses Loaded");
        }

        function itemsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        //Insert Object ;)
        function insertObject() {
            if ($scope.isEdit == false) {
                if ($scope.mapIsValidate == true) {
                    $scope.latitude = vm.map.markers[0].getPosition().lat();
                    $scope.longitude = vm.map.markers[0].getPosition().lng();
                    $scope.object.AddressStringLocation = $scope.latitude + '#' + $scope.longitude;
                    apiService.post($scope.baseURL + 'api/Address/', $scope.object,
                        objectSucceded,
                        objectFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    alert("Please Validate Your Address on Map.");
                }                
            } else {
                $scope.latitude = vm.map.markers[0].getPosition().lat();
                $scope.longitude = vm.map.markers[0].getPosition().lng();
                $scope.object.AddressStringLocation = $scope.latitude + '#' + $scope.longitude;
                $scope.object.AddressLocation = null;
                apiService.post($scope.baseURL + 'api/Address/AddressID/' + $scope.objectItemID, $scope.object,
                    objectSucceded,
                    objectFailed);
                console.log("Update Post!!!" + $scope.object);
            }            
        }

        $scope.ImageAddressID = 0;

        function objectSucceded(result) {
            console.log("Success" + result.data.Result, $scope.hasFile);

            if ($scope.hasFile) {
                console.log(result.data.Result, 'hi address');
                $scope.ImageAddressID = result.data.Result.AddressID;
                console.log($scope.ImageAddressID, 'hi address');
                $scope.dzMethods.processQueue();
            } else {
                loadItems();
                $scope.object = { ProfileID: $route.current.params.id, AddressStatus: true };
                redirect();
            }
        }

        function objectFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Delete Object ;)
        function deleteRow(item) {
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/Address/AddressID/' + item.AddressID, null,
                deleteItemSucceded,
                deleteItemFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
        }

        function deleteItemSucceded(result) {
            console.log("Success" + result.data.Result);
            loadItems();
            notificationService.displaySuccess('Deleted.');
        }

        function deleteItemFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //Extra Components loading ;)
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
        //End new Address

        $scope.masterFileName = "address";
        //dropzone ;)
        $scope.dzOptions = {
            url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.object.AddressImage + '&MasterFileName=address',
            paramName: 'photo',
            maxFilesize: '10',
            acceptedFiles: 'image/jpeg, images/jpg, image/png',
            addRemoveLinks: true,
            maxFiles: 1,
            autoProcessQueue: false,
            init: function () {
                this.on("addedfile", function (file) {
                    $scope.hasFile = true;
                    $scope.object.AddressImage = 'address-' + $scope.ImageAddressID + ".jpg";
                    console.log($scope.hasFile, 'hi hi', $scope.ImageAddressID, 'lol', $scope.object.AddressImage);
                }),
                this.on("processing", function (file) {
                    $scope.object.AddressImage = 'address-' + $scope.ImageAddressID + ".jpg";
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
            },
            'complete': function (file, xhr) {                
                console.log(file, xhr, 'done');
                $scope.removedfile(file);
                loadItems();
                $scope.object = { ProfileID: $route.current.params.id, AddressStatus: true };
                redirect();
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


        loadItems();
    }]);
})(angular.module('heroesApp'));