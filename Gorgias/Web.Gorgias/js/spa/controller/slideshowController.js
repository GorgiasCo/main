(function (app) {
    'use strict';
    app.controller('slideshowController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$timeout', 'authService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $timeout, authService) {

        $scope.albumID = $route.current.params.cid;
        $scope.ProfileURL = $route.current.params.id;
        $scope.SubscribeMode = "Subscribe";

        console.log('Khodaya Mamnonam' + $scope.ProfileURL + '******' + $scope.albumID);
        $scope.contents = [];

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        loadContents();
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
            if ($scope.Profile.RequestedProfileID > 0) {
                $scope.SubscribeMode = "Unsubscribed";
            } else {
                $scope.SubscribeMode = "Subscribe";
            }
            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadContents() {
            apiService.get($scope.baseURL + 'api/Web/Profile/Album/' + $scope.albumID, null,
            ContentsLoadCompleted,
            ContentsLoadFailed);
        }

        function ContentsLoadCompleted(response) {
            console.log('contents');
            console.log(response);
            console.log(response.data.Result);
            $scope.contents = response.data.Result;
            $timeout(function () { $scope.callRevolutionInit(); }, 1000);
            notificationService.displaySuccess("Slideshow loaded");
        }

        function ContentsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        $scope.callRevolutionInit = function () {
            var tpj = jQuery;
            tpj.noConflict();

            var revapi50;
            tpj(document).ready(function () {
                if (tpj("#rev_slider_50_1").revolution == undefined) {
                    revslider_showDoubleJqueryError("#rev_slider_50_1");
                } else {
                    revapi50 = tpj("#rev_slider_50_1").show().revolution({
                        sliderType: "carousel",
                        sliderLayout: "fullscreen",
                        dottedOverlay: "none",
                        delay: 9000,
                        navigation: {
                            keyboardNavigation: "off",
                            keyboard_direction: "horizontal",
                            onHoverStop: "off",
                        },
                        carousel: {
                            maxRotation: 5,
                            vary_rotation: "off",
                            minScale: 15,
                            vary_scale: "off",
                            horizontal_align: "center",
                            vertical_align: "center",
                            fadeout: "on",
                            vary_fade: "on",
                            maxVisibleItems: 3,
                            infinity: "off",
                            space: -80,
                            stretch: "off"
                        },
                        responsiveLevels: [1240, 1024, 778, 480],
                        gridwidth: [1024, 900, 778, 480],
                        gridheight: [868, 768, 960, 720],
                        lazyType: "smart",
                        shadow: 0,
                        spinner: "off",
                        stopLoop: "on",
                        stopAfterLoops: 0,
                        stopAtSlide: 1,
                        shuffle: "off",
                        autoHeight: "off",
                        fullScreenAlignForce: "off",
                        fullScreenOffsetContainer: "",
                        fullScreenOffset: "",
                        disableProgressBar: "on",
                        hideThumbsOnMobile: "off",
                        hideSliderAtLimit: 0,
                        hideCaptionAtLimit: 0,
                        hideAllCaptionAtLilmit: 0,
                        debugMode: false,
                        fallbacks: {
                            simplifyAll: "off",
                            nextSlideOnWindowFocus: "off",
                            disableFocusListener: false,
                        }
                    });
                }
            }); /*ready*/
        }
       
        
    }]);
})(angular.module('gorgiasapp'));