(function (app) {
    'use strict';
    app.controller('mainController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$interval', '$window', 'geolocation', 'ModalService', 'Carousel', '$anchorScroll', 'authService', 'trialService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $interval, $window, geolocation, ModalService, Carousel, $anchorScroll, authService, trialService) {
        $scope.checkedads = false;

        ngAuthSettings.headercreative = '';
        $scope.ArchiveFeaturesLoaded = false;
        $scope.webProfileLoaded = false;
        $scope.sliderProfileLoaded = false;
        $scope.isProfileLoaded = false;
        $scope.primaryTagsAll = true;

        $scope.timestamp = new Date().getUTCMilliseconds();

        $scope.Carousel = Carousel;
        $scope.checkedads = false;
        $scope.isLandscapeSupported = false;
        $scope.Industries = [];
        $scope.isAdmin = false;
        $scope.signinTitle = 'Sign In';

        var vm = this;
        $scope.mainPage = 1;
        $scope.trialCounter = 0;
        $scope.object = {};
        $scope.Profiles = {};
        $scope.profileSearch = {
            'ProfileTypeID': '',
            'ProfileID': '',
            'OrderType': 1,
            'Location': '',
            'Tags': [],
            'Industries': [],
            'CountryID': '',
            'PageNumber': 1,
            'PageSize': 10,
            'SubscriptionTypeID': ''
        };

        $scope.webs = [];
        $scope.apps = [];
        $scope.sliders = [];

        $scope.MainEntities = [];

        $scope.pageSizeWeb = 15;
        $scope.pageSizeApp = 12;
        $scope.pageSizeSlider = 18;

        $scope.pageNumberWeb = 1;
        $scope.pageNumberApp = 1;
        $scope.pageNumberSlider = 1;

        $scope.pageSizeOfficial = 10;
        $scope.pageNumberOfficial = 0;

        $scope.loadmoreWeb = loadWeb;
        $scope.loadmoreApp = loadApp;
        $scope.loadmoreSlider = loadSlider;
        $scope.contact = contact;
        $scope.sort = sort;
        $scope.sortCountry = sortCountry;
        $scope.profileSetup = profileSetup;
        $scope.pageToggle = pageToggle;
        $scope.redirectToSocialMedia = RedirectToSocialMedia;

        $scope.isDesktop = true;
        $scope.isFeatured = false;
        /*Slider Functions*/
        $scope.loadAlbums = loadAlbums;

        $scope.isMyProfiles = false;
        $scope.isMainProfile = true;

        console.log("mainController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //List Load More ;)
        $scope.finishedWebList = true;
        $scope.finishedAppList = true;
        $scope.finishedSliderList = true;

        $scope.countryLabel = 'All';

        $scope.MyProfileTrial = trialService.getMyProfileTrial();
        $scope.TagTrial = trialService.getTagTrial();

        if (trialService.getFeaturedTrial()) {
            $scope.mainPage = 2;
        } else {
            loadFeatures();
        }

        loadMainEntities();
        //ProfileSearch();

        $scope.tags = [];

        $scope.profileSearch.OrderType = '1';

        $scope.tagsSort = [];
        $scope.industryAll = true;

        $scope.downloadApp = function () {
            $location.url('/app/' + $scope.webProfile.ProfileURL);
            //if (trialService.getTrial()) {
            //    trialService.showDownloadAppModal();
            //}
        }

        $scope.PrimaryTagsAll = function () {
            // iterate over your whole list
            $scope.primaryTagsAll = true;
            $scope.profileSearch.Tags = [];
            angular.forEach($scope.PrimaryTags, function (contact) {
                contact.selected = false;
            });
            setupTags();
            sort();
        }

        $scope.PrimaryTagSort = function () {
            $scope.primaryTagsAll = false;
            setupTags();
            sort();
        }

        $scope.IndustryAll = function () {
            // iterate over your whole list
            $scope.industryAll = true;
            $scope.profileSearch.Industries = [];
            angular.forEach($scope.Industries, function (contact) {
                contact.selected = false;
            });
            sort();
        }

        $scope.IndustrySort = function () {
            $scope.industryAll = false;
            sort();
        }

        $scope.CountryAll = function () {
            $scope.profileSearch.CountryID = null;
            $scope.countryLabel = 'All';
            sort();
        }

        $scope.$watchCollection('tags', function () {
            console.log($scope.tags, 'Yasser Tags');
            console.log($scope.tagsSort, 'Yasser Tags');
            //$scope.profileSearch.Tags = $scope.tagsSort;
            console.log($scope.profileSearch, 'Yasser Tags -');
            $scope.tagsSort = $scope.tags.map(function (item) {
                return item['TagID'];
            });
            console.log($scope.tagsSort, 'Yasser Tags :)');
            setupTags();
            ProfileSearch();
            console.log($scope.profileSearch.Tags, 'Yasser PP');
        });

        $scope.loadTags = function ($query) {
            return $http.get($scope.baseURL + 'api/Web/Tags/', { cache: true }).then(function (response) {
                var tags = response.data.Result;
                return tags.filter(function (tag) {
                    return tag.TagName.toLowerCase().indexOf($query.toLowerCase()) != -1;
                });
            });
        }

        $scope.onTagRemoved = function ($tag) {
            $scope.tagsSort.splice($scope.tagsSort.indexOf($tag), 1);
        }

        $scope.onTagAdded = function ($tag) {
            $scope.tagsSort.push($tag.TagID);
        }

        function pageToggle(item) {
            if (!trialService.getFeaturedTrial()) {
                $scope.mainPage = item;
                $scope.checkedads = false;
                $anchorScroll();
            }            
        }

        function setupMyProfiles() {
            if ($scope.isMyProfiles == false) {
                $scope.isMyProfiles = true;
                $scope.isMainProfile = false;
                $scope.isProfileLoaded = true;
                $scope.profileSearch.ProfileID = authService.authentication.userID;
            } else {
                $scope.isMyProfiles = false;
                $scope.isMainProfile = true;
                $scope.isProfileLoaded = false;
                $scope.profileSearch.ProfileID = '';
            }
            setupLoginTitle();
            resetPageNumbers();
            ProfileSearch();
        }

        function profileSetup() {
            if (authService.authentication.userID > 0) {
                setupMyProfiles();
                console.log('Log Out');
            } else {
                ModalService.showModal({
                    templateUrl: "/layout/modal/login.html",
                    controller: "loginController"
                }).then(function (modal) {
                    modal.element.modal();
                    console.log("show modal");
                    modal.close.then(function (result) {
                        if (result) {
                            setupMyProfiles();
                            console.log(result, 'modal result');
                        } else {
                            console.log(result, 'modal result false');
                        }
                    });
                });
            }
        }

        $scope.signIn = function () {
            //if (!trialService.getLoginTrial()) {
                authService.fillAuthData();
                if (authService.authentication.userID > 0) {
                    $scope.isAdmin = false;
                    $scope.signinTitle = 'Sign In';
                    authService.logOut();
                    $scope.isMyProfiles = true;
                    $scope.isMainProfile = false;
                    $scope.profileSearch.ProfileID = '';
                    resetPageNumbers();
                    setupMyProfiles();
                    console.log('Log Out');
                } else {
                    showLoginModal();
                }
            //} else {
            //    trialService.showDownloadAppModal();
            //}
        }

        setupLoginTitle();

        function setupLoginTitle() {
            authService.fillAuthData();
            if (authService.authentication.userID > 0) {
                $scope.isAdmin = true;
                $scope.signinTitle = 'Sign Out';
                $scope.adminProfileID = authService.authentication.userID;
                console.log('Log Out title');
            } else {
                $scope.isAdmin = false;
                $scope.signinTitle = 'Sign In';
                $scope.adminProfileID = null;
                console.log('Log in title');
            }
        }

        function showLoginModal() {
            ModalService.showModal({
                templateUrl: "/layout/modal/login.html",
                controller: "loginController"
            }).then(function (modal) {
                // The modal object has the element built, if this is a bootstrap modal
                // you can call 'modal' to show it, if it's a custom modal just show or hide
                // it as you need to.
                modal.element.modal();
                console.log("show modal");

                modal.close.then(function (result) {
                    if (result) {
                        $scope.isAdmin = true;
                        $scope.signinTitle = 'Sign Out';
                        $scope.adminProfileID = authService.authentication.userID;
                        console.log('before Result');
                        console.log(result, 'modal result');
                    } else {
                        $scope.isAdmin = false;
                        $scope.signinTitle = 'Sign In';
                        $scope.adminProfileID = null;
                        console.log(result, 'modal result false');
                    }
                });
            });
        }

        function resetPageNumbers() {
            $scope.pageNumberWeb = 1;
            $scope.pageNumberApp = 1;
            $scope.pageNumberSlider = 1;
            //List Load More ;)
            $scope.finishedWebList = true;
            $scope.finishedAppList = true;
            $scope.finishedSliderList = true;
            //Reset SubscriptionID
            $scope.profileSearch.SubscriptionTypeID = null;
        }

        function setupTags() {
            $scope.profileSearch.Tags = [];

            angular.forEach($scope.PrimaryTags, function (contact) {
                if (contact.selected) {
                    $scope.profileSearch.Tags.push(contact.TagID);
                }
            });

            angular.forEach($scope.tagsSort, function (contact) {
                $scope.profileSearch.Tags.push(contact);
            });

            console.log($scope.profileSearch.Tags, 'Yasser PP');
        }

        function sortCountry(index) {
            console.log(index, 'Country Index ;)', $scope.Countries[index].CountryShortName);
            $scope.countryLabel = $scope.Countries[index].CountryShortName;
            sort();
        }

        function RedirectToSocialMedia(item) {
            if (trialService.getSocialNetworkTrial()) {
                trialService.showDownloadAppModal();
                console.log('modal');
            } else {
                $window.location.href = item;
            }
        }

        function trialMonitoring() {
            $scope.trialCounter += 1;
            if (trialService.getMainPageActivity) {
                if ($scope.trialCounter % 3 == 0) {
                    trialService.showDownloadAppModal();
                }
            }
        }

        function sort() {
            trialMonitoring();
            resetPageNumbers();
            console.log($scope.profileSearch.CountryID, '+CountryID ;)');
            //console.log($scope.profileSearch.IndustryID, '+IndustryID ;)');
            console.log($scope.profileSearch.Tags, 'Yasser PP');
            console.log('OrderType -> ;)' + $scope.profileSearch.OrderType);

            $scope.profileSearch.Industries = [];
            angular.forEach($scope.Industries, function (contact) {
                if (contact.selected) {
                    $scope.profileSearch.Industries.push(contact.IndustryID);
                }
            });

            //setupTags();

            console.log($scope.profileSearch.Industries, 'Yasser Industries ;)');

            if ($scope.profileSearch.OrderType == 4) {
                geolocation.getLocation().then(function (data) {
                    $scope.coords = { lat: data.coords.latitude, long: data.coords.longitude };
                    console.log(data.coords.latitude + '---' + data.coords.longitude);
                    $scope.profileSearch.Location = 'POINT(' + data.coords.longitude + ' ' + data.coords.latitude + ')';
                    console.log($scope.profileSearch);
                    ProfileSearch();
                });
            } else {
                ProfileSearch();
            }
        }

        function ProfileSearch() {
            //$scope.myPromise = $http.get($scope.baseURL + 'api/Web/Tags/', { cache: true }).then(function (response) {
            //    var tags = response.data.Result;
            //    return tags.filter(function (tag) {
            //        return tag.TagName.toLowerCase().indexOf($query.toLowerCase()) != -1;
            //    });
            //});$scope.myPromise = 

            //$scope.promise = $http.get('http://httpbin.org/delay/3');

            console.log($scope.profileSearch, 'Yasser Tags -');

            $scope.promise = apiService.post($scope.baseURL + 'api/web/profiles/fresh/', $scope.profileSearch,
                profileSearchSucceded,
                profileSearchFailed);
            console.log($scope.promise, 'Hi Yasser');
        }

        function profileSearchSucceded(response) {

            console.log('main profile success');
            console.log(response.data.Result);
            $scope.Profiles = response.data.Result;
            console.log($scope.Profiles.Webs.length);
            notificationService.displaySuccess("Profile Search loaded");

            if ($scope.Profiles.Webs.length < 1 || $scope.Profiles.Webs.length < $scope.pageSizeWeb) {
                $scope.finishedWebList = false;
            }

            if ($scope.Profiles.Apps.length < 1 || $scope.Profiles.Apps.length < $scope.pageSizeApp) {
                $scope.finishedAppList = false;
            }

            if ($scope.Profiles.Sliders.length < 1 || $scope.Profiles.Sliders.length < $scope.pageSizeSlider) {
                $scope.finishedSliderList = false;
            }
            $scope.profilesEmptyLoaded = false;
            if ($scope.Profiles.Webs.length == 0 && $scope.Profiles.Apps.length == 0 && $scope.Profiles.Sliders.length == 0) {
                $scope.profilesEmptyLoaded = true;
            }
        }

        function profileSearchFailed(response) {
            console.log('main profile Failed');
            notificationService.displayError(response.data.Errors);
        }


        $scope.distance = function (item) {
            return item / 1000;
        };

        //loadWeb();
        //loadApp();
        //loadSlider();

        function loadFeatures() {
            apiService.get($scope.baseURL + 'api/web/Features/', null,
            FeaturesLoadCompleted,
            FeaturesLoadFailed);
        }

        function FeaturesLoadCompleted(response) {
            console.log('Features', response.data.Result);

            if (response.data.Result.Tags.length > 3) {
                $scope.MainTags = response.data.Result.Tags.slice(0, 3);
                $scope.ProfileTags = response.data.Result.Tags.slice(3, response.data.Result.Tags.length - 1);
            } else {
                $scope.MainTags = response.data.Result.Tags;
            }

            $scope.ProfileProfile = response.data.Result.Profile;
            getReachText($scope.ProfileProfile.ProfileTypeID);

            $scope.SocialNetworks = response.data.Result.SocialNetworks;
            $scope.ExternalLinks = response.data.Result.ExternalLinks;
            $scope.CurrentFeature = response.data.Result.CurrentFeature[0];
            console.log($scope.CurrentFeature, 'current feature ;)');
            $scope.ArchiveFeatures = response.data.Result.ArchiveFeatures;
            $scope.ArchiveFeaturesLoaded = true;
        }

        function FeaturesLoadFailed(response) {

        }

        $scope.featureDetail = loadFeatureDetail;

        function loadFeatureDetail(item) {
            apiService.get($scope.baseURL + 'api/web/Feature/' + item, null,
            FeatureDetailLoadCompleted,
            FeatureDetailLoadFailed);
        }

        function FeatureDetailLoadCompleted(response) {
            console.log('Features', response.data.Result);

            if (response.data.Result.Tags.length > 3) {
                $scope.MainTags = response.data.Result.Tags.slice(0, 3);
                $scope.ProfileTags = response.data.Result.Tags.slice(3, response.data.Result.Tags.length - 1);
            } else {
                $scope.MainTags = response.data.Result.Tags;
            }

            $scope.ProfileProfile = response.data.Result.Profile;
            getReachText($scope.ProfileProfile.ProfileTypeID);

            $scope.SocialNetworks = response.data.Result.SocialNetworks;
            $scope.ExternalLinks = response.data.Result.ExternalLinks;
            $scope.CurrentFeature = response.data.Result.CurrentFeature[0];
            console.log($scope.CurrentFeature, 'current feature ;)');
        }

        function FeatureDetailLoadFailed(response) {

        }

        function loadWeb() {
            if (trialService.getLoadMoreTrial) {
                trialService.showDownloadAppModal();
            } else {
                $scope.pageNumberWeb += 1;
                $scope.profileSearch.PageNumber = $scope.pageNumberWeb;
                $scope.profileSearch.PageSize = $scope.pageSizeWeb;
                $scope.profileSearch.SubscriptionTypeID = 1;
                apiService.post($scope.baseURL + 'api/web/profiles/', $scope.profileSearch,
                WebLoadCompleted,
                WebLoadFailed);
            }            
        }

        function WebLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1 || response.data.Result.length < $scope.pageSizeWeb) {
                $scope.finishedWebList = false;
            }

            $scope.Profiles.Webs.push.apply($scope.Profiles.Webs, response.data.Result);
            console.log($scope.Profiles.Webs);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function WebLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadApp() {
            if (trialService.getLoadMoreTrial) {
                trialService.showDownloadAppModal();
            } else {
                $scope.pageNumberApp += 1;
                $scope.profileSearch.PageNumber = $scope.pageNumberApp;
                $scope.profileSearch.PageSize = $scope.pageSizeApp;
                $scope.profileSearch.SubscriptionTypeID = 2;
                apiService.post($scope.baseURL + 'api/web/profiles/', $scope.profileSearch,
                AppLoadCompleted,
                AppLoadFailed);
            }            
        }

        function AppLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1 || response.data.Result.length < $scope.pageSizeApp) {
                $scope.finishedAppList = false;
            }

            $scope.Profiles.Apps.push.apply($scope.Profiles.Apps, response.data.Result);
            console.log($scope.Profiles.Apps);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function AppLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadSlider() {
            if (trialService.getLoadMoreTrial) {
                trialService.showDownloadAppModal();
            } else {
                $scope.pageNumberSlider += 1;
                $scope.profileSearch.PageNumber = $scope.pageNumberSlider;
                $scope.profileSearch.PageSize = $scope.pageSizeSlider;
                $scope.profileSearch.SubscriptionTypeID = 3;
                apiService.post($scope.baseURL + 'api/web/profiles/', $scope.profileSearch,
                SliderLoadCompleted,
                SliderLoadFailed);
            }            
        }

        function SliderLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1 || response.data.Result.length < $scope.pageSizeSlider) {
                $scope.finishedSliderList = false;
            }

            $scope.Profiles.Sliders.push.apply($scope.Profiles.Sliders, response.data.Result);
            console.log($scope.Profiles.Sliders);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function SliderLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadProfileTypes() {
            apiService.get($scope.baseURL + 'api/Web/ProfileTypes/', null,
            ProfileTypesLoadCompleted,
            ProfileTypesLoadFailed);
        }

        function ProfileTypesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.ProfileTypes = response.data.Result;
            notificationService.displaySuccess("Profile Type loaded");
        }

        function ProfileTypesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        /*Optimized ;)*/
        function loadMainEntities() {
            console.log("Load Entities ;)");
            apiService.get($scope.baseURL + 'api/Web/MainEntities/', null,
            MainEntitiesLoadCompleted,
            MainEntitiesLoadFailed);
        }

        function MainEntitiesLoadCompleted(response) {
            console.log("Entities ;)");
            console.log(response.data.Result);
            $scope.Industries = response.data.Result.Industry;
            $scope.ProfileTypes = response.data.Result.ProfileType;
            $scope.Countries = response.data.Result.Country;
            $scope.PrimaryTags = response.data.Result.Tags;
            notificationService.displaySuccess("MainEntities loaded");
            console.log($scope.Industries, 'Yasser');
        }

        function MainEntitiesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        /*End Optimized ;)*/


        function loadIndustries() {
            apiService.get($scope.baseURL + 'api/Web/Industries/', null,
            IndustriesLoadCompleted,
            IndustriesLoadFailed);
        }

        function IndustriesLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Industries = response.data.Result;
            notificationService.displaySuccess("Industries loaded");
        }

        function IndustriesLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadTags() {
            apiService.get($scope.baseURL + 'api/Web/Tags/', null,
            TagsLoadCompleted,
            TagsLoadFailed);
        }

        function TagsLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.Tags = response.data.Result;
            notificationService.displaySuccess("Industries loaded");
        }

        function TagsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        
        function contact() {
            $location.url('/web/contact/' + $scope.ProfileID);
        }

        $window.onscroll = function () {
            $scope.scrollPos = document.body.scrollTop || document.documentElement.scrollTop || 0;
            $scope.$apply(); //or simply $scope.$digest();
            console.log('scroll ;))))))');
            //alert('Open7');
        };

        $scope.checked = false;
        $scope.checkedWeb = false;
        $scope.checkedads = false;

        $scope.size = '500px';

        /*Tags Animation*/
        $scope.isTags = true;
        $scope.isTagsSearch = false;

        $scope.showTags = function () {
            $scope.isTags = false;
            $scope.isTagsSearch = true;
        }

        $scope.hiddenTags = function () {
            $scope.isTags = true;
            $scope.isTagsSearch = false;
        }


        $scope.toggleFeatured = function () {
            $scope.isFeatured = !$scope.isFeatured;
        }

        $scope.closeSlider = function () {
            $scope.checked = !$scope.checked;
        }

        $scope.closeWebSlider = function () {
            $scope.checkedWeb = !$scope.checkedWeb
            $location.url('/' + $scope.webProfile.ProfileURL);
        }

        $scope.closeAdSlider = function () {
            $scope.checkedads = false;
        }

        $scope.industryOff = false;
        $scope.industryToggle = function () {
            $scope.industryOff = !$scope.industryOff
        }

        //Get Reach Text
        function getReachText(item) {
            console.log(item, 'item reach');
            switch (item) {
                case 1:
                    $scope.ReachUS = "HER";
                    break;
                case 2:
                    $scope.ReachUS = "HIM";
                    break;
                default:
                    $scope.ReachUS = "US";
            }
        }

        //Get Reach Text
        function getSliderReachText(item) {
            console.log(item, 'item reach');
            switch (item) {
                case 1:
                    $scope.SliderReachUS = "HER";
                    break;
                case 2:
                    $scope.SliderReachUS = "HIM";
                    break;
                default:
                    $scope.SliderReachUS = "US";
            }
        }


        //Prepare Web Slider Info
        $scope.toggleWeb = function (item) {
            trialMonitoring();
            $scope.webProfileLoaded = false;
            apiService.get($scope.baseURL + 'api/web/Profile/web/URL/' + item, null,
                WebProfileLoadCompleted,
                WebProfileLoadFailed);
        }

        $scope.adsProfile = {};

        function WebProfileLoadCompleted(response) {
            if (response.data.Result.Profile != null) {
                $scope.webProfile = response.data.Result.Profile;
                $scope.adsProfile = $scope.webProfile;
                console.log($scope.webProfile, 'Theme');

                getReachText($scope.webProfile.ProfileTypeID);
                $scope.webExternalLinks = response.data.Result.ExternalLinks;
                $scope.webSocialNetworks = response.data.Result.SocialNetworks;

                if ($scope.webSocialNetworks.length > 0) {
                    $scope.websocialNetworksLoaded = true;
                }

                $scope.webMainTags = response.data.Result.PrimaryTags;
                $scope.webTags = response.data.Result.Tags;
                
                notificationService.displaySuccess("Profile loaded");
                $scope.isloaded = true;
                if ($scope.webExternalLinks.length > 0) {
                    $scope.webexternalLinksLoaded = true;
                }
                $scope.webProfileLoaded = true;
                $scope.checkedWeb = !$scope.checkedWeb
                $scope.checkedads = false;
            }
        }

        function WebProfileLoadFailed(response) {
            console.log(response);
        }

        $scope.mockRouteChange = function () {
            $scope.$broadcast('$locationChangeStart');
        }

        $scope.onopen = function () {
            alert('Open');
        }

        $scope.onclose = function () {
            if ($scope.mainPage == 2 && ($scope.webProfileLoaded || $scope.sliderProfileLoaded)) {
                $scope.checkedads = true;
                console.log("hehe i was one at this size ;)")
                $scope.$apply();
            }            
        }

        $scope.featuredSliderNumber = 3;


        var w = angular.element($window);
        $scope.pSize = 0;
        $scope.pWebSize = 0;

        $scope.pAdsSize = '220px';
        $scope.checkedads = false;

        $scope.windowWidth = $window.innerWidth;

        $scope.showMenu = function () {
            return ($scope.checked || $scope.checkedWeb);
        }

        if ($scope.windowWidth > 500) {
            $scope.isDesktop = true;
            $scope.isFeatured = true;
        } else {
            $scope.isDesktop = false;
            $scope.isFeatured = false;
        }

        $window.addEventListener('orientationchange', function () {
            var position = $window.matchMedia("(orientation: portrait)");
            if ($scope.windowWidth < 600) {
                if (!position.matches) {
                    console.log('its not supported ;)');
                    $scope.isLandscapeSupported = true;
                } else {
                    console.log('something Changed');
                    $scope.isLandscapeSupported = false;
                    $route.reload();
                }
            }
        }, false);

        console.log($window.matchMedia("(orientation: portrait)"));
        var position = $window.matchMedia("(orientation: portrait)");
        if ($scope.windowWidth > 600) {
            $scope.pSize = '370px';
            $scope.pAdsSize = '19.1%';//220px
            $scope.pWebSize = '35.6%';//500px
            $scope.featuredSliderNumber = 3;
            console.log('1000');
            console.log($scope.windowWidth);
            console.log($scope.pSize);
            $scope.isLandscapeSupported = false;
        } else if ($scope.windowWidth < 600 && position.matches) {
            $scope.pSize = (parseInt($scope.windowWidth) - parseInt(30)) + 'px';
            $scope.pWebSize = (parseInt($scope.windowWidth) - parseInt(30)) + 'px';
            $scope.pAdsSize = '0px';
            $scope.featuredSliderNumber = 1;
            console.log('300');
            console.log(parseInt($scope.windowWidth) - parseInt(15));
            console.log($scope.pSize);
            $scope.isLandscapeSupported = false;
        } else {
            console.log('its not supported ;)');
            $scope.isLandscapeSupported = true;
            //$route.reload();
        }

        $scope.slickFeatured = {
            autoplay: false,
            infinite: false,
            slidesToShow: $scope.featuredSliderNumber,
            slidesToScroll: 1,
            arrows: false,
            method: {}
        };

        $scope.$watch(
          function () {
              return $window.innerWidth;
          },
          function (value) {
              $scope.windowWidth = value;

          },
          true
        );

        w.bind('resize', function () {
            $scope.$apply();
        });


        /*Slider Settings*/

        $scope.toggle = function (item) {
            console.log(item);
            $scope.checkedads = false;
            $scope.selectedProfile = item;
            $scope.showTab(1);
            $scope.sliderProfileLoaded = false;
            loadSliderProfile(item);
            loadAlbumSet(4);
            $scope.loadAbout(0);
            $scope.loadAlbums(0);
            $scope.About = 0;
            $scope.checked = true; //!$scope.checked;
        }

        $scope.mainSlider = 4;
        $scope.isAllOpen = false;
        $scope.aboutExpandTitle = "expand all";

        $scope.showTab = function (item) {
            $scope.mainSlider = item;
            console.log(item);
        }

        $scope.goContact = function () {
            $scope.mainSlider = 4;
            console.log($scope.mainSlider, 'go contact', $scope.checked);
        }

        $scope.expandAll = function () {
            if ($scope.isAllOpen == true) {
                $scope.isAllOpen = false;
                $scope.aboutExpandTitle = "expand all";
            } else {
                $scope.isAllOpen = true;
                $scope.aboutExpandTitle = "close all";
            }
            console.log($scope.isAllOpen);
        }

        $scope.loadAbout = function (item) {
            $scope.aboutTab = item;
            if (item == 0) {
                loadAbout();
            } else {
                loadConnections();
            }
            console.log(item);
        }

        $scope.slickMenu = {
            autoplay: false,
            infinite: false,
            dot: true,
            slidesToShow: $scope.featuredSliderNumber,
            slidesToScroll: 2,
            arrows: false,
            variableWidth: true,
            method: {}
        };

        //Slider ;)
        loadCategories();
        $scope.categoriesLoaded = false;

        function loadSliderProfile(item) {
            $scope.sliderProfileLoaded = false;
            apiService.get($scope.baseURL + 'api/Web/Profile/Slider/' + item, null,
            SliderProfileLoadCompleted,
            SliderProfileLoadFailed);
        }

        function SliderProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result, 'Slider Profile ;)');
            $scope.sliderProfile = response.data.Result.Profile;
            $scope.adsProfile = $scope.sliderProfile;

            $scope.sliderProfileTags = response.data.Result.Tags;
            $scope.sliderProfileSocialNetworks = response.data.Result.SocialNetworks;
            $scope.sliderProfileConnections = response.data.Result.Connections;

            if (response.data.Result.Tags.length > 2) {
                $scope.sliderProfileMainTags = response.data.Result.Tags.slice(0, 1);
                $scope.sliderProfileTags = response.data.Result.Tags.slice(1, response.data.Result.Tags.length - 1);
            } else {
                $scope.sliderProfileMainTags = response.data.Result.Tags;
            }

            getSliderReachText($scope.sliderProfile.ProfileTypeID);

            $scope.sliderProfileLoaded = true;
            notificationService.displaySuccess("Profile loaded");
        }

        function SliderProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
            $scope.sliderProfileLoaded = false;
        }

        function loadAlbumSet(item) {
            apiService.get($scope.baseURL + 'api/Web/Profile/AlbumSet/' + item, null,
            AlbumSetLoadCompleted,
            AlbumSetLoadFailed);
        }

        function AlbumSetLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.sliderProfileAlbumSets = response.data.Result;
            $scope.sliderProfileAlbumSetsLoaded = true;
            notificationService.displaySuccess("Profile loaded");
        }

        function AlbumSetLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
            $scope.sliderProfileAlbumSetsLoaded = false;
        }

        function loadCategories() {
            if (!$scope.categoriesLoaded) {
                apiService.get($scope.baseURL + 'api/Web/Categories', null,
                CategoryLoadCompleted,
                CategoryLoadFailed);
            }
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
            $scope.categoriesLoaded = false;
        }

        $scope.selectedProfile = 1;

        function loadAlbums(loadMode) {
            if (loadMode > 0) {
                apiService.get($scope.baseURL + 'api/Web/Profile/albums/' + $scope.selectedProfile + '/1/' + loadMode + '/1/33', null,
                    AlbumsLoadCompleted,
                    AlbumsLoadFailed);
            } else {
                apiService.get($scope.baseURL + 'api/Web/Profile/albums/' + $scope.selectedProfile + '/1/100', null,
                    AlbumsLoadCompleted,
                    AlbumsLoadFailed);
            }
        }

        function AlbumsLoadCompleted(response) {
            console.log(response.data.Result);
            //if (response.data.Result.length < 1 || response.data.Result.length < $scope.pageSize) {
            //    $scope.finishedList = false;
            //}
            $scope.sliderAlbums = response.data.Result;
            console.log($scope.sliderAlbums);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("sliderAlbums loaded");
        }

        function AlbumsLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadAbout() {
            apiService.get($scope.baseURL + 'api/Web/Profile/About/' + $scope.selectedProfile, null,
            AboutLoadCompleted,
            AboutLoadFailed);
        }

        function AboutLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.abouts = response.data.Result;
            $scope.sliderProfileLoaded = true;
            notificationService.displaySuccess("Profile loaded");
        }

        function AboutLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function loadConnections() {
            $scope.pageNumberOfficial += 1;
            apiService.get($scope.baseURL + 'api/Web/Profile/Connections/' + $scope.selectedProfile + '/0/1/2/1/100', null,
            ConnectionLoadCompleted,
            ConnectionLoadFailed);
        }

        function ConnectionLoadCompleted(response) {
            console.log(response.data.Result);
            if (response.data.Result.length < 1) {
                $scope.finishedListConnection = false;
            }
            $scope.connections = response.data.Result;
            console.log($scope.connections);
            console.log(response.data.Result.length);
            notificationService.displaySuccess("Profile loaded");
        }

        function ConnectionLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        console.log('nasserjoon');
        console.log($scope.checkedads);
        /*End of Slider Settings ;)*/

    }]);
})(angular.module('gorgiasapp'));