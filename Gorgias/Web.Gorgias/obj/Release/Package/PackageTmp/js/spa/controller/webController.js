(function (app) {
    'use strict';
    app.controller('webController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', 'trialService', '$window', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, trialService, $window) {

        var vm = this;
        $scope.object = {};
        $scope.albums = [];
        $scope.connections = [];
        $scope.abouts = [];
        $scope.checkedMenu = false;
        $scope.isloaded = false;
        $scope.SubscribeMode = "Subscribe";

        $scope.timestamp = new Date().getUTCMilliseconds();

        $scope.downloadApp = function () {
            if (trialService.getTrial()) {
                //trialService.showDownloadAppModal();
                window.location = "http://gorgias.azurewebsites.net/app/" + $scope.Profile.ProfileURL;
            }
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

        $scope.pageSizeOfficial = 10;
        $scope.pageNumberOfficial = 0;

        $scope.loadmore = loadLatestAlbums;
        $scope.contact = contact;
        $scope.goToTop = goToTop;
        $scope.redirectToSlideshow = RedirectToSlideshow;
        $scope.redirectToSocialMedia = RedirectToSocialMedia;

        console.log("webController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //$scope.ProfileID = $route.current.params.id;
        $scope.ProfileURL = $route.current.params.id;

        //List Load More ;)
        $scope.finishedList = true;
        $scope.finishedListConnection = true;

        loadProfile();
        //loadLatestAlbums();
        //loadConnections();
        //loadAbout();

        function loadProfile() {
            $scope.isloaded = false;
            $scope.promise = apiService.get($scope.baseURL + 'api/Web/Profile/URL/' + $scope.ProfileURL + '/' + authService.authentication.userID, null,
                                ProfileLoadCompleted,
                                ProfileLoadFailed);
        }

        $scope.albumLoaded = false;
        $scope.connectionsLoaded = false;
        $scope.aboutsLoaded = false;
        $scope.externalLinksLoaded = false;
        $scope.socialNetworksLoaded = false;
        $scope.allLoaded = false;

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            if (response.data.Result.Profile != null) {
                $scope.Profile = response.data.Result.Profile;
                $scope.ProfileTypeID = $scope.Profile.ProfileTypeID;
                $scope.ProfileID = $scope.Profile.ProfileID;

                if ($scope.Profile.RequestedProfileID > 0) {
                    $scope.SubscribeMode = "Unsubscribed";
                } else {
                    $scope.SubscribeMode = "Subscribe";
                }

                if (response.data.Result.About.length > 0) {
                    $scope.abouts.push(response.data.Result.About[0]);
                }
                
                $scope.albums = response.data.Result.Albums;
                $scope.connections = response.data.Result.Connections;
                $scope.ExternalLinks = response.data.Result.ExternalLinks;
                $scope.SocialNetworks = response.data.Result.SocialNetworks;

                if ($scope.SocialNetworks.length > 0) {
                    $scope.socialNetworksLoaded = true;
                }

                $scope.MainTags = response.data.Result.PrimaryTags;
                $scope.Tags = response.data.Result.Tags;

                notificationService.displaySuccess("Profile loaded");
                $scope.isloaded = true;
                if ($scope.ExternalLinks.length > 0) {
                    $scope.externalLinksLoaded = true;
                }
                if ($scope.ProfileID > 0) {
                    $scope.profileLoaded = true;
                }
                if ($scope.albums.length > 0) {
                    $scope.albumLoaded = true;
                }
                if ($scope.abouts.length > 0) {
                    $scope.aboutsLoaded = true;
                }
                if ($scope.connections.length > 0) {
                    $scope.connectionsLoaded = true;
                }
                $scope.allLoaded = true;
            } else {
                $location.url('/');
            }
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

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
            $scope.isloaded = false;
            //$location.url('/');
        }

        function loadAbout() {
            apiService.get($scope.baseURL + 'api/Web/Profile/About/' + $scope.ProfileID, null,
            AboutLoadCompleted,
            AboutLoadFailed);
        }

        function AboutLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.abouts = response.data.Result;
            notificationService.displaySuccess("Profile loaded");
        }

        function AboutLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadLatestAlbums() {
            $scope.pageNumber += 1;
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
            console.log($scope.albums);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function AlbumsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadConnections() {
            $scope.pageNumberOfficial += 1;
            apiService.get($scope.baseURL + 'api/Web/Profile/Connections/' + $scope.ProfileID + '/0/1/2/' + $scope.pageNumberOfficial + '/' + $scope.pageSizeOfficial, null,
            ConnectionLoadCompleted,
            ConnectionLoadFailed);
        }

        function ConnectionLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1) {
                $scope.finishedListConnection = false;
            }
            $scope.connections.push.apply($scope.connections, response.data.Result);
            console.log($scope.connections);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function ConnectionLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function contact() {
            $location.url('/' + $scope.ProfileURL + '/contact/');
        }

        function goToTop() {
            $anchorScroll();
        }

        //// slider settings object set to scope.
        //$scope.slider = {
        //    sliderType: "standard",
        //    sliderLayout: "auto",
        //    responsiveLevels: [1920, 1024, 778, 480],
        //    gridwidth: [1930, 1240, 778, 480],
        //    gridheight: [768, 768, 960, 720],
        //    autoHeight: "off",
        //    minHeight: "",
        //    fullScreenOffsetContainer: "",
        //    fullScreenOffset: "",
        //    delay: 9000,
        //    disableProgressBar: "on",
        //    startDelay: "",
        //    stopAfterLoops: "",
        //    stopAtSlide: "",
        //    viewPort: {},
        //    lazyType: "none",
        //    dottedOverlay: "none",
        //    shadow: 0,
        //    spinner: "off",
        //    hideAllCaptionAtLilmit: 0,
        //    hideCaptionAtLimit: 0,
        //    hideSliderAtLimit: 0,
        //    debugMode: false,
        //    extensions: "",
        //    extensions_suffix: "",
        //    fallbacks: {
        //        simplifyAll: "off",
        //        disableFocusListener: false
        //    },
        //    parallax: {
        //        type: "scroll",
        //        origo: "enterpoint",
        //        speed: 400,
        //        levels: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50]
        //    },
        //    carousel: {},
        //    navigation: {
        //        keyboardNavigation: "off",
        //        keyboard_direction: "horizontal",
        //        mouseScrollNavigation: "off",
        //        onHoverStop: "on",
        //        touch: {
        //            touchenabled: "on",
        //            swipe_treshold: 75,
        //            swipe_min_touches: 1,
        //            drag_block_vertical: false,
        //            swipe_direction: "horizontal"
        //        },
        //        tabs: {
        //            style: "zeus",
        //            enable: true,
        //            width: 150,
        //            height: 30,
        //            min_width: 100,
        //            wrapper_padding: 0,
        //            wrapper_color: "transparent",
        //            wrapper_opacity: "0",
        //            tmp: "<span class=\"tp-tab-title\">{{title}}</span>",
        //            visibleAmount: 3,
        //            hide_onmobile: true,
        //            hide_under: 480,
        //            hide_onleave: false,
        //            hide_delay: 200,
        //            direction: "horizontal",
        //            span: true,
        //            position: "inner",
        //            space: 1,
        //            h_align: "left",
        //            v_align: "top",
        //            h_offset: 30,
        //            v_offset: 30
        //        }
        //    },
        //    jsFileLocation: "",
        //    visibilityLevels: [1240, 1024, 778, 480],
        //    hideThumbsOnMobile: "off"
        //};

        

    }]);
})(angular.module('gorgiasapp'));