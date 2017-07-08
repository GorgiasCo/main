(function (app) {
    'use strict';
    app.controller('addProfileController', ['$scope', '$state', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', 'authService', 'NgMap',
        function ($scope, $state, $http, apiService, ngAuthSettings, $location, notificationService, authService, NgMap) {

            var vm = this;
            $scope.object = {};
            $scope.objectAddress = { ProfileID: $scope.ProfileID, AddressStatus: true };//$route.current.params.id

            console.log("addProfileController loaded ;)");
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

            $scope.AddObject = insertObject;
            $scope.redirect = redirectBack;

            $scope.ProfileID = 0;
            $scope.hasFile = false;

            checkValidity();
            //|| $scope.ProfileID != authService.authentication.userID
            function checkValidity() {
                if (authService.authentication.userID == 0 || authService.authentication.userRole != 0) {
                    $location.url('/access/login');
                }
            }

            function insertObject() {
                apiService.post($scope.baseURL + 'api/Profile/', $scope.object,
                addProfileSucceded,
                addProfileFailed);
                console.log("Post!!!" + $scope.object);
            }

            function addProfileSucceded(result) {
                $scope.ProfileID = result.data.Result.ProfileID;
                console.log($scope.ProfileID);
                console.log("Success");
                console.log(result.data.Result.ProfileID);
                console.log(result.data.Result);
                $scope.objectAddress.ProfileID = $scope.ProfileID;//$route.current.params.id
                if ($scope.hasFile) {
                    $scope.ProfileID = result.data.Result.ProfileID;
                    $scope.dzMethods.processQueue();
                } else {
                    redirectBack();
                }
            }

            function addProfileFailed(response) {
                console.log("Fail");
                $scope.error = response.data.Errors;
                notificationService.displayError(response.statusText);
                notificationService.displayError(response.data.Errors);
            }

            console.log('redirect wow');


            function redirectBack() {
                $state.go('app.forms.country', {});
                console.log('redirect wow');
                //$location.url('/profile');
            }

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
                        console.log($scope.hasFile);
                    }),
                    this.on("processing", function (file) {
                        $scope.imagename = 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
                        $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
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
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    $scope.newFile = file;
                },
                'success': function (file, xhr) {
                    console.log('success ;)');
                    console.log(file, xhr);
                    insertObjectAddress();
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

            //loadIndustries()
            loadProfileTypes();
            loadThemes();
            loadSubscriptionTypes();

            //Address Begin

            //Load Components
            loadAddressTypes();
            loadCities();
            //Address Object ;)

            vm.stores = {
                foo: { position: [41, -87], items: [1, 2, 3, 4] },
                bar: { position: [41, -83], items: [5, 6, 7, 8] }
            };

            $scope.mapIsValidate = false;

            //vm.initMap = function (mapId) {            
            //    if ($scope.mapIsValidate == false) {

            //    }            vm.map = NgMap.initMap(mapId);
            //        console.log('vm.map 2', vm.map);
            //        $scope.mapIsValidate = true;
            //}

            $scope.isValidated = false;
            $scope.mapIsValidate = false;
            vm.validate = false;
            console.log($scope.isValidated, 'isValidated');
            $scope.initMap = function (mapId) {
                console.log('functioncalled', mapId);
                $scope.isValidated = true;
                var currentAddress = $scope.objectAddress.AddressAddress;
                $scope.objectAddress.AddressAddress = '13 West Clarendon Road';
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

                    $scope.objectAddress.AddressAddress = currentAddress;

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
            }

            $scope.initEditMap = function (mapId) {
                console.log('functioncalled', mapId);
                var currentAddress = $scope.objectAddress.AddressAddress;
                $scope.objectAddress.AddressAddress = '13 West Clarendon Road';
                NgMap.getMap("foo22").then(function (map) {
                    vm.map = map;
                    var center = vm.map.getCenter();
                    vm.map.setCenter(center);
                    google.maps.event.trigger('foo22', "resize");
                    $scope.objectAddress.AddressAddress = currentAddress;
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

            $scope.toggleBounce = function () {
                if (this.getAnimation() != null) {
                    this.setAnimation(null);
                } else {
                    this.setAnimation(google.maps.Animation.BOUNCE);
                }
            }


            //Insert Object ;)
            function insertObjectAddress() {
                if ($scope.mapIsValidate == true) {
                    $scope.latitude = vm.map.markers[0].getPosition().lat();
                    $scope.longitude = vm.map.markers[0].getPosition().lng();
                    $scope.objectAddress.AddressStringLocation = $scope.latitude + '#' + $scope.longitude;
                    apiService.post($scope.baseURL + 'api/Address/', $scope.objectAddress,
                        objectAddressSucceded,
                        objectAddressFailed);
                    console.log("New Post!!!" + $scope.object);
                } else {
                    alert("Please Validate Your Address on Map.");
                }
            }

            function objectAddressSucceded(result) {
                console.log("Success" + result.data.Result);
                if ($scope.hasFileAddress) {
                    $scope.dzAddressMethods.processQueue();
                } else {
                    redirectBack();
                }
            }

            function objectAddressFailed(response) {
                console.log("Fail", response);
                $scope.error = response.data.Errors;
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

            $scope.masterAddressFileName = "address";
            //dropzone ;)
            $scope.dzAddressOptions = {
                url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.objectAddress.AddressImage + '&MasterFileName=address',
                paramName: 'photo',
                maxFilesize: '2',
                acceptedFiles: 'image/jpeg, images/jpg, image/png',
                addRemoveLinks: true,
                maxFiles: 1,
                autoProcessQueue: false,
                init: function () {
                    this.on("addedfile", function (file) {
                        $scope.hasFileAddress = true;
                        $scope.objectAddress.AddressImage = 'address-' + $scope.AddressID + ".jpg";
                        console.log($scope.hasFileAddress);
                    }),
                    this.on("processing", function (file) {
                        this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.objectAddress.AddressImage + '&MasterFileName=address';
                    });
                },
                removedfile: function (file) {
                    var _ref;
                    $scope.hasFileAddress = false;
                    console.log($scope.hasFileAddress);
                    console.log(file);
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                }
            };

            //Handle events for dropzone
            //Visit http://www.dropzonejs.com/#events for more events
            $scope.dzAddressCallbacks = {
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
            $scope.dzAddressMethods = {};
            $scope.removedfile = function () {
                $scope.dzAddressMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
                console.log('$scope.newFile');
            }
            //End dropzone ;)
            //Address End
        }]);
})(angular.module('gorgiasapp'));