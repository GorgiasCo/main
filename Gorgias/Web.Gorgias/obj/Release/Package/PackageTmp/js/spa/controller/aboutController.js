(function (app) {
    'use strict';
    app.controller('aboutController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', 'trialService', '$window', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, trialService, $window) {

        var vm = this;

        $scope.object = {};
        $scope.albums = [];
        $scope.connections = [];
        $scope.abouts = [];
        $scope.checkedMenu = false;
        $scope.SubscribeMode = "Subscribe";

        $scope.timestamp = new Date().getUTCMilliseconds();

        $scope.close = function () {
            close(500); // close, but give 500ms for bootstrap to animate
        };

        $scope.pageMenu = function () {
            $scope.checkedMenu = !$scope.checkedMenu;
        };

        ngAuthSettings.headercreative = 'header-creative';

        $scope.pageSize = 10;
        $scope.pageNumber = 0;

        $scope.pageSizeOfficial = 10;
        $scope.pageNumberOfficial = 0;

        $scope.loadmore = loadLatestAlbums;
        $scope.contact = contact;
        $scope.goToTop = goToTop;
        $scope.redirectToSlideshow = RedirectToSlideshow;
        $scope.redirectToSocialMedia = RedirectToSocialMedia;

        console.log("webController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.ProfileID = 0;// = $route.current.params.id;
        $scope.ProfileURL = $route.current.params.id;

        //List Load More ;)
        $scope.finishedList = true;
        $scope.finishedListConnection = true;

        //loadProfile();
        //loadLatestAlbums();
        //loadConnections();
        loadAbout();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Web/Profile/' + $scope.ProfileID, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadAbout() {
            apiService.get($scope.baseURL + 'api/Web/About/Page/' + $scope.ProfileURL + '/' + authService.authentication.userID + '/10/1', null,
            AboutLoadCompleted,
            AboutLoadFailed);
        }

        function AboutLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.abouts = response.data.Result.Abouts;
            $scope.albums = response.data.Result.Albums;
            $scope.Profile = response.data.Result.Profile;
            console.log($scope.Profile, 'Images');
            $scope.ProfileID = $scope.Profile.ProfileID;
            $scope.ProfileTypeID = $scope.Profile.ProfileTypeID;
            if ($scope.Profile.RequestedProfileID > 0) {
                $scope.SubscribeMode = "Unsubscribed";
            } else {
                $scope.SubscribeMode = "Subscribe";
            }
            $scope.SocialNetworks = response.data.Result.SocialNetworks;

            console.log($scope.Profile.ProfileImage, $scope.Profile.ProfileTypeID, 'ProfileImage');
            $scope.ready = true;
            notificationService.displaySuccess("About loaded");
        }

        function AboutLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function RedirectToSlideshow(item) {
            if (trialService.getAlbumTrial()) {
                trialService.showDownloadWebAppModal();
                console.log('modal');
            } else {
                $location.url('/' + $scope.ProfileURL + '/slideshow/' + item);
            }            
        }

        function RedirectToSocialMedia(item) {
            if (trialService.getSocialNetworkTrial()) {
                trialService.showDownloadWebAppModal();
                console.log('modal');
            } else {
                $window.location.href = item;
            }
        }

        function loadLatestAlbums() {
            $scope.pageNumber += 1;
            console.log($scope.pageNumber, 'album number page');
            if ($scope.pageNumber < 1) {
                apiService.get($scope.baseURL + 'api/Web/Profile/albums/latest/' + $scope.ProfileID + '/' + $scope.pageNumber + '/' + $scope.pageSize, null,
                            AlbumsLoadCompleted,
                            AlbumsLoadFailed);
            } else {
                if (trialService.getToGalleryTrial()) {
                    $location.url('/' + $scope.ProfileURL + '/gallery/');
                } else {
                    apiService.get($scope.baseURL + 'api/Web/Profile/albums/latest/' + $scope.ProfileID + '/' + $scope.pageNumber + '/' + $scope.pageSize, null,
                                AlbumsLoadCompleted,
                                AlbumsLoadFailed);
                }
            }
        }

        function AlbumsLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1 || response.data.Result.length < $scope.pageSize) {
                $scope.finishedList = false;
            }

            $scope.albums.push.apply($scope.albums, response.data.Result);
            console.log($scope.albums, $scope.ProfileID, 'loadmorealbum');
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function AlbumsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function contact() {
            $location.url('/' + $scope.ProfileURL + '/contact/');
        }

        function goToTop() {
            $anchorScroll();
        }

    }]);
})(angular.module('gorgiasapp'));