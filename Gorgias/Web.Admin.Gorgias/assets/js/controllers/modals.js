'use strict';

/* Controllers */

angular.module('gorgiasapp')
    // Chart controller 
    .controller('ModalsCtrl', ['$scope', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', '$translate',
        function ($scope, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, $translate) {
            $scope.modal = {};
            $scope.modal.slideUp = "default";
            $scope.modal.stickUp = "default";

            $scope.toggleSlideUpSize = function () {
                var size = $scope.modal.slideUp;
                var modalElem = $('#modalSlideUp');
                if (size == "mini") {
                    $('#modalSlideUpSmall').modal('show')
                } else {
                    $('#modalSlideUp').modal('show')
                    if (size == "default") {
                        modalElem.children('.modal-dialog').removeClass('modal-lg');
                    } else if (size == "full") {
                        modalElem.children('.modal-dialog').addClass('modal-lg');
                    }
                }
            };

            $scope.stickUpSizeToggler = function () {
                var size = $scope.modal.stickUp;
                var modalElem = $('#myModal');
                if (size == "mini") {
                    $('#modalStickUpSmall').modal('show')
                } else {
                    $('#myModal').modal('show')
                    if (size == "default") {
                        modalElem.children('.modal-dialog').removeClass('modal-lg');
                    } else if (size == "full") {
                        modalElem.children('.modal-dialog').addClass('modal-lg');
                    }
                }
            };

            $scope.modalSlideLeft = function () {
                setTimeout(function () {
                    $('#modalSlideLeft').modal('show');
                }, 300);
            };

            $scope.fillSizeToggler = function () {
                $('#modalFillIn').modal('show');
                // Only for fillin modals so that the backdrop action is still there
                $('#modalFillIn').on('show.bs.modal', function (e) {
                    $('body').addClass('fill-in-modal');
                })
                $('#modalFillIn').on('hidden.bs.modal', function (e) {
                    $('body').removeClass('fill-in-modal');
                })

            };

            var vm = this;

            $scope.language = 'en';
            $scope.languages = ['en', 'zh', 'my'];
            $scope.updateLanguage = function () {
                $translate.use($scope.language);
            };

            $scope.language = $translate.proposedLanguage() || $translate.use()
            $scope.timestamp = new Date().getUTCMilliseconds();

            $scope.object = {};
            $scope.pSize = "250px";
            $scope.checked = true;

            $scope.checkedSlider = false;
            $scope.pSliderSize = "500px";

            console.log("viewAdminAgencyProfileController wow loaded ;)");
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

            $scope.AddObject = insertObject;

            $scope.ProfileID = authService.authentication.userID;// $route.current.params.id;

            $scope.contentIndex = 0;

            $scope.Contents = [];
            $scope.newContent = {
                ContentTitle: '',
                ContentURL: '',
                ContentType: '',
                ContentID: null,
            };


            $scope.addNewContent = function () {
                //var addedContent = {};
                //addedContent.ContentTitle = "it is me title ;)";
                //addedContent.ContentURL = "it is you URL ;)";
                //addedContent.ContentID = $scope.contentIndex + 1;
                //$scope.Contents.push(addedContent);
                //$scope.contentIndex = $scope.contentIndex + 1;
                //console.log('added', $scope.Contents);

                var modalInstance = $uibModal.open({
                    templateUrl: '/tpl/admin/Album/uploadPhoto.html',
                    controller: 'uploadPhotoController',
                    size: 'lg',

                });

                modalInstance.result.then(function (selectedItem) {
                    console.log(selectedItem);
                }, function () {
                    console.log('Modal dismissed at: ' + new Date());
                });

                //ModalService.showModal({
                //    templateUrl: "/tpl/admin/Album/uploadPhoto.html",
                //    controller: "uploadPhotoController",
                //    preClose: (modal) => { modal.element.modal('hide'); }
                //}).then(function (modal) {
                //    modal.element.modal();
                //    modal.close.then(function (result) {
                //        $scope.yesNoResult = result ? "You said Yes" : "You didn't say Yes";
                //        console.log(result, 'modal Close Result');
                //    });
                //});

            };






            $scope.properties = {
                // autoHeight:true,
                animateIn: 'fadeIn',
                lazyLoad: true,
                autoWidth: true,
                onDragged: function () {
                    console.log(event);
                }
            };

            function callback(event) {
                //var element = event.target;         // DOM element, in this example .owl-carousel
                var name = event.type;           // Name of the event, in this example dragged
                var namespace = event.namespace;      // Namespace of the event, in this example owl.carousel
                var items = event.item.count;     // Number of items
                var item = event.item.index;     // Position of the current item
                // Provided by the navigation plugin
                var pages = event.page.count;     // Number of pages
                var page = event.page.index;     // Position of the current page
                var size = event.page.size;      // Number of items per page
                console.log(name, namespace, items, item, pages, page, size, 'OWL ;)');
            };
            //Check Authorization ;)
            //checkValidity();
            //loadValidity();

            //|| $scope.ProfileID != authService.authentication.userID ;)
            function checkValidity() {
                if (authService.authentication.userID == 0) {
                    $location.url('/access/login');
                }
            }

            function loadValidity() {
                apiService.get($scope.baseURL + 'api/web/validity/', null,
                ValidityLoadCompleted,
                ValidityLoadFailed);
            }

            function ValidityLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.object = response.data.Result;
                //checkValidity();
            }

            function ValidityLoadFailed(response) {
                //checkValidity();
            }

            console.log($scope.ProfileID, 'log in now admin', authService.authentication.userID, 'auth', authService.authentication);

            $scope.ProfileID = $stateParams.id;//$route.current.params.id;
            $scope.hasFile = false;

            $scope.checkedMenu = false;
            $scope.goToTop = goToTop;
            $scope.editLinkRole = 0;

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

            //loadAgencyProfiles();
            //loadProfile();

            //Web/Administration/Agency/

            function loadAgencyProfiles() {
                switch (authService.authentication.userRole) {
                    case "3":
                        apiService.get($scope.baseURL + 'api/Web/UserProfile/Agency/' + authService.authentication.userID, null,
                        AgencyProfilesLoadCompleted,
                        AgencyProfilesLoadFailed);
                        $scope.editLinkRole = 3;
                        console.log(authService.authentication.userID, 'WOWL Page successful login 3 Invite without Profile ;)');
                        break;
                    case "5":
                        apiService.get($scope.baseURL + 'api/Web/UserProfile/Agency/' + authService.authentication.userID, null,
                        AgencyProfilesLoadCompleted,
                        AgencyProfilesLoadFailed);
                        $scope.editLinkRole = 5;
                        console.log(authService.authentication.userID, 'WOWL Page successful login 4 Agency without Profile ;)');
                        break;
                    case "0":
                        apiService.get($scope.baseURL + 'api/Web/Administration/Country/' + authService.authentication.userID, null,
                        AgencyProfilesLoadCompleted,
                        AgencyProfilesLoadFailed);
                        $scope.editLinkRole = 0;
                        console.log(authService.authentication.userID, 'WOWL Page successful login 0 Country Distributor ;)');
                        break;
                    default:
                        $location.path('/access/login/');
                }
            }

            function AgencyProfilesLoadCompleted(response) {
                $scope.Profiles = response.data.Result;
                console.log(response, $scope.Profiles, 'AGENCY');
            }

            function AgencyProfilesLoadFailed(response) {
                console.log(response);
            }

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
                //angular.element('.chart').waypoint({
                //    offset: '100%',
                //    triggerOnce: true,
                //    handler: function () {
                //        var color = jQuery(this).attr('data-color');
                //        jQuery(this).easyPieChart({
                //            animate: 1000,
                //            barColor: color,
                //            lineCap: 'circle',
                //            lineWidth: 8,
                //            size: 140,
                //            scaleColor: false,
                //            trackColor: '#f8f8f8'
                //        });
                //    }
                //});

                //angular.element('.blog_slider_ul').each(function () {

                //    // Init carouFredSel
                //    jQuery(this).carouFredSel({
                //        circular: true,
                //        responsive: true,
                //        items: {
                //            width: 380,
                //            visible: {
                //                min: 1,
                //                max: 4
                //            }
                //        },
                //        scroll: {
                //            duration: 500,
                //            easing: 'swing'
                //        },
                //        prev: {
                //            button: function () {
                //                return jQuery(this).closest('.blog_slider').find('.slider_prev');
                //            }
                //        },
                //        next: {
                //            button: function () {
                //                return jQuery(this).closest('.blog_slider').find('.slider_next');
                //            }
                //        },
                //        pagination: {
                //            container: function () {
                //                return jQuery(this).closest('.blog_slider').find('.slider_pagination');
                //            }
                //        },
                //        auto: {
                //            play: false,
                //            timeoutDuration: 0,
                //        },
                //        swipe: {
                //            onTouch: true,
                //            onMouse: true,
                //            onBefore: function () {
                //                jQuery(this).find('a').addClass('disable');
                //                jQuery(this).find('li').trigger('mouseleave');
                //            },
                //            onAfter: function () {
                //                jQuery(this).find('a').removeClass('disable');
                //            }
                //        }
                //    });

                //    // Disable accidental clicks while swiping
                //    jQuery(this).on('click', 'a.disable', function () {
                //        return false;
                //    });
                //});

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

            function uuidv4() {
                return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                    (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
                )
            }

            function getName() {
                var d = new Date();
                var n = d.getTime();
                return uuidv4() + n + '.jpg';
            }

            console.log(uuidv4(), 'uuid ;)', getName());

            $scope.masterFileName = "profile";

            var maxImageWidth = 800;
            var maxImageHeight = 800;
            var isImageValidation = false;

            //dropzone ;)
            $scope.dzOptions = {
                url: 'https://api.gorgias.com/api/images/name?ImageName=hottest-' + $scope.imagename + '&MasterFileName=album',
                paramName: 'photo',
                maxFilesize: '10',
                acceptedFiles: 'image/jpeg, images/jpg, image/png',
                addRemoveLinks: true,
                previewsContainer: false,
                clickable: '#addnewPhoto',
                maxFiles: 5,
                autoProcessQueue: true,
                init: function () {
                    this.on("addedfile", function (file) {
                        //$scope.hasFile = true;
                        //$scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/albums/hottest-' + $scope.imagename;
                        //console.log($scope.imagename, 'addedFile init ;)');
                    }),
                    this.on("processing", function (file) {
                        $scope.imagename = getName();

                        this.options.url = $scope.baseURL + 'api/images/name?ImageName=hottest-' + $scope.imagename + '&MasterFileName=album';
                        console.log($scope.imagename, file, 'processing ;)');
                    });
                    this.on("thumbnail", function (file) {
                        // Do the dimension checks you want to do
                        if (isImageValidation) {
                            if (file.width > maxImageWidth || file.height > maxImageHeight) {
                                console.log(file.width, file.height, 'hi its image ;)');
                                file.rejectDimensions();
                            }
                            else {
                                console.log(file.width, file.height, 'hi accept its image ;)');
                                file.acceptDimensions();
                            }
                        } else {
                            console.log(file.width, file.height, 'image ;)');
                            file.acceptDimensions();
                        }
                    });
                },
                accept: function (file, done) {
                    file.acceptDimensions = done;
                    file.rejectDimensions = function () { done("Invalid dimension."); };
                    // Of course you could also just put the `done` function in the file
                    // and call it either with or without error in the `thumbnail` event
                    // callback, but I think that this is cleaner.
                },
                addedfile: function (file) {
                    //file.previewTemplate = $(this.options.previewTemplate);
                    //console.log(file.previewTemplate, 'file.previewTemplate');
                    //file.previewTemplate.find(".filename span").text(file.name);
                    //file.previewTemplate.find(".details").append($("<div class=\"size\">" + (this.filesize(file.size)) + "</div>"));

                    //console.log(file.previewTemplate.find('img').attr('src'));
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
                    console.log(file, 'addedfile dzCallbacks');
                    $scope.newFile = file;
                },
                'success': function (file, xhr) {
                    console.log('success ;)');
                    console.log(file, xhr);

                    var addedContent = {};
                    addedContent.ContentTitle = file.width + '-' + file.height + "*" + xhr.Result[0].FileUrl;
                    addedContent.ContentURL = xhr.Result[0].FileUrl;
                    addedContent.ContentID = $scope.contentIndex + 1;
                    addedContent.ContentDimenssion = file.width + '-' + file.height;
                    $scope.Contents.push(addedContent);
                    $scope.contentIndex = $scope.contentIndex + 1;
                    console.log('added', $scope.Contents, xhr.Result[0].FileUrl);
                },
                'uploading': function (file, xhr) {
                    console.log('uploading ;)');
                    console.log(file, xhr);
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
            //loadProfileTypes()
            //loadThemes()
            //loadSubscriptionTypes()

            $scope.signout = function () {
                authService.fillAuthData();
                authService.logOut();
                $location.url('/');
                console.log('good bye ;)');
            }



        }]);