(function (app) {
    'use strict';
    app.controller('locationController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'NgMap', '$timeout', 'authService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, NgMap, $timeout, authService) {

        var vm = this;
        $scope.object = {};
        $scope.locations = [];
        $scope.addressTypes = [];
        $scope.addressMode = [];
        $scope.checkedMenu = false;
        $scope.AddressTypeID = 0;
        $scope.SubscribeMode = "Subscribe";

        $scope.maps = [];

        $scope.mycallback = function (map) {
            $scope.mapUpdateCountry(map);
            console.log('first ;) loaded map');
        };

        $scope.reRednerMap = function () {
            $timeout(function () {
                angular.forEach($scope.maps, function (index) {
                    google.maps.event.trigger(index, 'resize');
                });
            }, 100);
        }

        $scope.$on('mapInitialized', function (evt, evtMap) {
            $scope.map = evtMap;
            //$scope.mapUpdateCountry(0);
            console.log('first ;) 333 loaded map');
            $scope.maps.push(evtMap);

            //$scope.marker = new google.maps.Marker({ position: evt.latLng, map: $scope.map });
            console.log('map ;)', evtMap);
        });

        //$scope.$watch("locations", function (value) {//I change here
        //    var val = value || null;
        //    if (val)
        //        $scope.mapUpdateCountry(0);
        //});


        //NgMap.getMap().then(function (map) {
        //    google.maps.event.addListener(map, "idle", function () {
        //        google.maps.event.trigger(map, 'resize');
        //        console.log('map ;) resized');
        //    });
        //});

        $scope.accordion = {
            current: null
        };

        $scope.mapUpdateCountry = function (mapId) {
            var self = this;
            var address = $scope.locations[mapId].Addresses[0];

            console.log('Help - ' + address.AddressName);
            vm.map = NgMap.initMap(address.AddressName);
            console.log('$scope.map 777777----', vm.map)

            console.log('$scope.map 777777--' + mapId + '----' + map);
            google.maps.event.trigger(map, 'resize');

            $timeout(function () {
                NgMap.getMap(address.AddressName).then(function (response) {
                    // Initialize the Google map
                    google.maps.event.trigger(response, 'resize');
                    //vm.map = NgMap.initMap('323-74-4870');
                    $scope.$apply();
                    //$scope.$applyAsync();
                    console.log('$scope.map 434343--' + address.AddressName + '----' + response);
                });
            });
        }

        $scope.mapUpdate = function (mapId) {

            var self = this;
            //NgMap.getMap().then(function (map) {
            //    google.maps.event.addListener(map, "idle", function () {
            //        google.maps.event.trigger(map, 'resize');
            //    });
            //    $scope.$apply();
            //});
            console.log('Help - ' + mapId);
            vm.map = NgMap.initMap(mapId);
            console.log('$scope.map 434343----', vm.map)

            console.log('$scope.map 434343--' + mapId + '----' + map);
            google.maps.event.trigger(map, 'resize');

            $timeout(function () {
                // the code you want to run in the next digest            
                NgMap.getMap(mapId).then(function (response) {
                    // Initialize the Google map
                    google.maps.event.trigger(response, 'resize');
                    //vm.map = NgMap.initMap('323-74-4870');
                    $scope.$apply();
                    console.log('$scope.map 434343--' + mapId + '----' + response);
                });
            });



            //NgMap.getMap().then(function (map) {
            //    google.maps.event.trigger(map, 'resize');
            //});

            //NgMap.getMap('323-74-4870').then(function () {
            //    // Initialize the Google map
            //    vm.map = NgMap.initMap('323-74-4870');
            //    console.log('$scope.map 2---' + mapId + '----' + vm.map)
            //});

            //vm.map = NgMap.initMap(mapId);
        }

        $scope.close = function () {
            close(500); // close, but give 500ms for bootstrap to animate
        };

        $scope.pageMenu = function () {
            $scope.checkedMenu = !$scope.checkedMenu;
        };

        ngAuthSettings.headercreative = 'header-creative';

        $scope.pageSize = 15;
        $scope.pageNumber = 0;
        $scope.addressTypesLoaded = false;
        $scope.goToTop = goToTop;
        $scope.addressTypeSliderNumber = 5;

        function goToTop() {
            $anchorScroll();
        }

        $scope.slickMenu = {
            autoplay: false,
            infinite: false,
            dot: false,
            slidesToShow: $scope.addressTypeSliderNumber,
            slidesToScroll: 2,
            arrows: false,
            variableWidth: true,
            method: {}
        };

        $scope.initMap = function (mapId) {
            if ($scope.mapIsValidate == false) {
                vm.map = NgMap.initMap(mapId);
                console.log('vm.map ' + mapId, vm.map);
                $scope.mapIsValidate = true;
            }
        }

        console.log("webController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //$scope.ProfileID = $route.current.params.id;
        $scope.ProfileURL = $route.current.params.id;

        loadAddressTypes();
        loadLocations();

        $scope.sort = function (item) {
            console.log('AddressTypeID->' + $scope.AddressTypeID + '<--' + item);
            $scope.AddressTypeID = item;
            loadLocations(1);
        }

        function loadLocations(type) {
            if (type == 0) {
                apiService.get($scope.baseURL + 'api/Web/Profile/Addresses/' + $scope.ProfileURL + '/0', null,
                LocationLoadCompleted,
                LocationLoadFailed);
            } else {
                apiService.get($scope.baseURL + 'api/Web/Profile/Addresses/' + $scope.ProfileURL + '/' + $scope.AddressTypeID, null,
                LocationLoadCompleted,
                LocationLoadFailed);
            }
        }

        function LocationLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.locations = response.data.Result;
            $scope.addressTypesLoaded = true;
            //$scope.reRednerMap();
            notificationService.displaySuccess("Profile loaded");
        }

        function LocationLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadAddressTypes() {
            apiService.get($scope.baseURL + 'api/Web/AddressTypes', null,
            AddressTypesLoadCompleted,
            AddressTypesLoadFailed);
        }

        function AddressTypesLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.addressTypes = response.data.Result;
            notificationService.displaySuccess("Profile loaded");
        }

        function AddressTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        loadProfile();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Web/Profile/Low/' + $scope.ProfileURL + '/' + authService.authentication.userID, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.Profile = response.data.Result.Profile;
            $scope.ProfileID = $scope.Profile.ProfileID;
            $scope.ProfileTypeID = $scope.Profile.ProfileTypeID;
            console.log($scope.ProfileTypeID, 'ProfileTypeID');
            if ($scope.Profile.RequestedProfileID > 0) {
                $scope.SubscribeMode = "Unsubscribed";
            } else {
                $scope.SubscribeMode = "Subscribe";
            }
            $scope.SocialNetworks = response.data.Result.SocialNetworks;
            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function contact() {
            $location.url('/' + $scope.ProfileURL + '/contact/');
        }
    }]);
})(angular.module('gorgiasapp'));