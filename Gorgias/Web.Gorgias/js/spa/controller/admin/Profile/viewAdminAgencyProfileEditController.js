(function (app) {
    'use strict';
    app.controller('viewAdminAgencyProfileEditController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', '$translate', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, $translate) {

        var vm = this;

        $scope.language = 'en';
        $scope.languages = ['en', 'zh', 'my'];
        $scope.updateLanguage = function () {
            $translate.use($scope.language);
        };

        $scope.language = $translate.proposedLanguage() || $translate.use()

        $scope.object = {};
        $scope.pSize = "250px";
        $scope.checked = true;

        $scope.checkedSlider = false;
        $scope.pSliderSize = "500px";

        console.log("viewAdminProfileController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.AddObject = insertObject;

        $scope.agencyProfileID = authService.authentication.userID;// $route.current.params.id;
        
        $scope.ProfileID = $route.current.params.id;
        $scope.AgencyProfileID = $route.current.params.aid;

        //Check Authorization ;)
        checkValidity();
        loadValidity();

        function checkValidity() {
            if (authService.authentication.userID == 0 || $scope.AgencyProfileID != authService.authentication.userID) {
                $location.url('/admin/login');
            }
            //Web/Administration/Agency/
            apiService.get($scope.baseURL + 'api/Web/Administration/Agency/' + $scope.AgencyProfileID + '/' + $scope.ProfileID, null,
            AgencyValidityLoadCompleted,
            AgencyValidityLoadFailed);
        }

        function AgencyValidityLoadCompleted(response) {
            if (response.status == 404) {
                $location.url('/admin/login');
            }
            console.log(response, 'success');
        }

        function AgencyValidityLoadFailed(response) {
            console.log(response, 'failed');
            $location.url('/admin/login');
        }

        function loadValidity() {
            apiService.get($scope.baseURL + 'api/web/validity/', null,
            ValidityLoadCompleted,
            ValidityLoadFailed);
        }

        function ValidityLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;
            checkValidity();
        }

        function ValidityLoadFailed(response) {
            checkValidity();
        }

        console.log($scope.ProfileID, 'log in now admin', authService.authentication.userID, 'auth', authService.authentication);

        $scope.ProfileID = $route.current.params.id;
        $scope.hasFile = false;

        $scope.checkedMenu = false;
        $scope.goToTop = goToTop;

        function goToTop() {
            $anchorScroll();
            console.log('Can ;)');
        }

        $scope.pageMenu = function () {
            $scope.checkedMenu = !$scope.checkedMenu;
        };

        $scope.toggleSlider = function () {
            $scope.checkedSlider = !$scope.checkedSlider;
            $location.url('/');
        };

        loadProfile();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Profile/ProfileID/' + $scope.ProfileID, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response.data.Result);
            $scope.object = response.data.Result;

            /* ---------------------------------------------------------------------------
		 * Chart
		 * --------------------------------------------------------------------------- */
            angular.element('.chart').waypoint({
                offset: '100%',
                triggerOnce: true,
                handler: function () {
                    var color = jQuery(this).attr('data-color');
                    jQuery(this).easyPieChart({
                        animate: 1000,
                        barColor: color,
                        lineCap: 'circle',
                        lineWidth: 8,
                        size: 140,
                        scaleColor: false,
                        trackColor: '#f8f8f8'
                    });
                }
            });

            angular.element('.blog_slider_ul').each(function () {

                // Init carouFredSel
                jQuery(this).carouFredSel({
                    circular: true,
                    responsive: true,
                    items: {
                        width: 380,
                        visible: {
                            min: 1,
                            max: 4
                        }
                    },
                    scroll: {
                        duration: 500,
                        easing: 'swing'
                    },
                    prev: {
                        button: function () {
                            return jQuery(this).closest('.blog_slider').find('.slider_prev');
                        }
                    },
                    next: {
                        button: function () {
                            return jQuery(this).closest('.blog_slider').find('.slider_next');
                        }
                    },
                    pagination: {
                        container: function () {
                            return jQuery(this).closest('.blog_slider').find('.slider_pagination');
                        }
                    },
                    auto: {
                        play: false,
                        timeoutDuration: 0,
                    },
                    swipe: {
                        onTouch: true,
                        onMouse: true,
                        onBefore: function () {
                            jQuery(this).find('a').addClass('disable');
                            jQuery(this).find('li').trigger('mouseleave');
                        },
                        onAfter: function () {
                            jQuery(this).find('a').removeClass('disable');
                        }
                    }
                });

                // Disable accidental clicks while swiping
                jQuery(this).on('click', 'a.disable', function () {
                    return false;
                });
            });

            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

        function insertObject() {
            apiService.post($scope.baseURL + 'api/Profile/ProfileID/' + $scope.ProfileID, $scope.object,
            updateProfileSucceded,
            updateProfileFailed);
            console.log("Post!!!" + $scope.object);
        }

        function updateProfileSucceded(result) {
            console.log("Success" + result.data.Result);
            if ($scope.hasFile) {
                //$scope.ProfileID = ProfileID;
                $scope.dzMethods.processQueue();
            } else {
                redirectBack();
            }
        }

        function updateProfileFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function redirectBack() {
            $location.url('/profile');
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
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
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
        $scope.dzMethods = {};
        $scope.removedfile = function () {
            $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
            console.log('$scope.newFile');
        }
        //End dropzone ;)



        loadIndustries()
        loadProfileTypes()
        loadThemes()
        loadSubscriptionTypes()

        $scope.signout = function () {
            authService.fillAuthData();
            authService.logOut();
            $location.url('/');
            console.log('good bye ;)');
        }

    }]);
})(angular.module('gorgiasapp'));