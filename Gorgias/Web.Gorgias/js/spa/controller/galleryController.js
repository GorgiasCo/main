(function (app) {
    'use strict';
    app.controller('galleryController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$window', '$anchorScroll', 'authService', 'trialService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $window, $anchorScroll, authService, trialService) {

        var vm = this;
        $scope.object = {};
        $scope.albums = [];
        $scope.connections = [];
        $scope.abouts = [];
        $scope.categories = [];
        $scope.isloaded = false;
        $scope.SubscribeMode = 'Subscribe';
        //$scope.isGalleryLoaded = false;
        $scope.checkedMenu = false;

        $scope.close = function () {
            close(500); // close, but give 500ms for bootstrap to animate
        };

        $scope.pageMenu = function () {
            $scope.checkedMenu = !$scope.checkedMenu;
            //alert("I like the book!");
        };

        $scope.pageSize = 15;
        $scope.pageNumber = 1;
        ngAuthSettings.headercreative = 'header-creative';

        $scope.pageSizeOfficial = 10;
        $scope.pageNumberOfficial = 0;

        $scope.loadmore = LoadMore;
        $scope.sortOrder = sortOrder;
        $scope.sort = Sort;
        $scope.contact = contact;
        $scope.goToTop = goToTop;
        $scope.redirectToSlideshow = RedirectToSlideshow;
        $scope.redirectToSocialMedia = RedirectToSocialMedia;

        function goToTop() {
            $anchorScroll();
        }

        console.log("webController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.CategoryID = 0; //$route.current.params.cid;
        //$scope.ProfileID = $route.current.params.id;
        $scope.ProfileURL = $route.current.params.id;

        $scope.Order = { 'OrderType' :1};
        $scope.OrderType = 1;
        $scope.categoriesLoaded = false;

        //loadCategories();

        //List Load More ;)
        $scope.finishedList = true;

        loadCategories();
        loadAlbums(false);
        loadProfile();

        $scope.$watch('OrderType', function (newVal) {
            sortOrder(newVal);
            console.log('Watch OrderType new val', newVal);
        });

        function sortOrder(item) {
            $scope.Order.OrderType = item;
            $scope.OrderType = item;
            Sort($scope.CategoryID);
        }

        function Sort(item) {
            $scope.finishedList = true;
            $scope.pageNumber = 1;
            $scope.CategoryID = item;
            console.log($scope.CategoryID, 'CategoryID Mammnonam Khoda');
            console.log($scope.Order.OrderType + 'Órder');
            //LayoutSetup();
            loadAlbums(false);
        }

        function LoadMore() {
            $scope.pageNumber += 1;
            loadAlbums(true);
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

        function loadAlbums(loadMode) {
            //$scope.isGalleryLoaded = false;
            //$scope.isLoaded = false;
            if (loadMode) {
                if (trialService.getToGalleryTrial()) {
                    trialService.showDownloadWebAppModal();
                } else {
                    apiService.get($scope.baseURL + 'api/web/gallery/page/' + $scope.ProfileURL + '/' + $scope.CategoryID + '/' + $scope.OrderType + '/' + $scope.pageSize + '/' + $scope.pageNumber, null,
                        AlbumsLoadMoreCompleted,
                        AlbumsLoadMoreFailed);
                }               
            } else {
                $scope.isloaded = false;
                apiService.get($scope.baseURL + 'api/web/gallery/page/' + $scope.ProfileURL + '/' + $scope.CategoryID + '/' + $scope.OrderType + '/' + $scope.pageSize + '/1', null,
                    AlbumsLoadCompleted,
                    AlbumsLoadFailed);
            }
        }

        function AlbumsLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.Albums.length < 1 || response.data.Result.Albums.length < $scope.pageSize) {
                $scope.finishedList = false;
                //$scope.isGalleryLoaded = false;
            }

            $scope.isloaded = true;
            $scope.albums = response.data.Result.Albums;
            //$scope.categories = response.data.Result.Categories;
            //$scope.categoriesLoaded = true;

            console.log($scope.albums);
            console.log(response.data.Result.Albums.length, 'lenght');
            notificationService.displaySuccess("Profile loaded");
            console.log($scope.isloaded, '$scope.isloaded');
            //if (typeof response.data.Result.Albums.length === undefined) {
            //    $scope.finishedList = false;
            //    $scope.isGalleryLoaded = false;
            //}
        }

        function AlbumsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
            $scope.isloaded = false;
            //$scope.isGalleryLoaded = false;
        }


        function AlbumsLoadMoreCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.Albums.length < 1 || response.data.Result.Albums.length < $scope.pageSize) {
                $scope.finishedList = false;
                //$scope.isGalleryLoaded = false;
            }

            //$scope.isGalleryLoaded = true;
            $scope.albums.push.apply($scope.albums, response.data.Result.Albums);
            console.log($scope.albums);
            console.log(response.data.Result.Albums.length);
            notificationService.displaySuccess("Profile loaded");
            //if (typeof response.data.Result.Albums.length === undefined) {
            //    $scope.finishedList = false;
            //    $scope.isGalleryLoaded = false;
            //}
        }

        function AlbumsLoadMoreFailed(response) {
            notificationService.displayError(response.data.Errors);
            //$scope.isGalleryLoaded = false;
        }

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
            $scope.SocialNetworks = response.data.Result.SocialNetworks;
            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadCategories() {
            apiService.get($scope.baseURL + 'api/Web/Categories', null,
            CategoryLoadCompleted,
            CategoryLoadFailed);
        }

        function CategoryLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.categories = response.data.Result;
            $scope.categoriesLoaded = true;
            notificationService.displaySuccess("Profile loaded");
        }

        function CategoryLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function contact() {
            $location.url('/' + $scope.ProfileURL + '/contact/');
        }


        var w = angular.element($window);
        $scope.pSize = 0;
        $scope.windowWidth = $window.innerWidth;

        if ($scope.windowWidth > 500) {
            $scope.pSize = '500px';
            $scope.featuredSliderNumber = 5;
            console.log('1000');
            console.log($scope.windowWidth);
            console.log($scope.pSize);
        } else {
            $scope.pSize = (parseInt($scope.windowWidth) - parseInt(15)) + 'px';
            $scope.featuredSliderNumber = 2;
            console.log('300');
            console.log(parseInt($scope.windowWidth) - parseInt(15));
            console.log($scope.pSize);
        }

        console.log($scope.featuredSliderNumber, 'featuredSliderNumber');
        $scope.slickMenu = {
            autoplay: false,
            infinite: false,
            dot: false,
            slidesToShow: $scope.featuredSliderNumber,
            slidesToScroll: 3,
            arrows: false,
            variableWidth: true,
            method: {}
        };

    }]);
})(angular.module('gorgiasapp'));