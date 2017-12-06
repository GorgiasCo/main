(function (app) {
    'use strict';
    app.controller('addNewAlbumController', ['$scope', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', '$translate', 'localStorageService', '$state', '$filter',
        function ($scope, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, $translate, localStorageService, $state, $filter) {
            $scope.ProfileIsConfirmed = false;
            //$scope.ProfileID = 1011;

            var historyPage = localStorageService.get('pageHistory');
            console.log('add new ;)', historyPage);

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
            $scope.cancel = redirectBack;
            //$scope.ProfileID = authService.authentication.userID;// $route.current.params.id;

            $scope.contentIndex = 0;

            $scope.Contents = [];
            $scope.newContent = {
                ContentTitle: '',
                ContentURL: '',
                ContentType: '',
                ContentID: null,
            };

            $scope.modal = {};
            $scope.modal.slideUp = "default";
            $scope.modal.stickUp = "default";
            $scope.errorStoryNumberOfPhotoTitle = $filter('translate')('NewStoryYouNeedAtLeastThreePhoto');

            $scope.errorStory = 0;

            $scope.resetNewStory = function () {
                //setTimeout(function () {
                //    $('#modalSlideUpSmallCongratulation').modal('hide');                    
                //}, 300);

                //setTimeout(function () {                    
                //    $state.reload();
                //}, 300);
                $scope.Contents = [];
                $scope.errorStory = 0;
                $('#modalSlideUpSmallCongratulation').modal('hide');
            };

            $scope.toggleSlideUpSize = function () {
                $scope.errorStory = 1;
                //var size = $scope.modal.slideUp;
                //var modalElem = $('#modalSlideUp');
                //if (size == "mini") {
                //    $('#modalSlideUpSmall').modal('show');
                //} else {
                //    $('#modalSlideUp').modal('show');
                //    if (size == "default") {
                //        modalElem.children('.modal-dialog').removeClass('modal-lg');
                //    } else if (size == "full") {
                //        modalElem.children('.modal-dialog').addClass('modal-lg');
                //    }
                //}
            };

            $scope.modalCongratulation = function () {
                $('#modalSlideUpSmallCongratulation').modal('show');
            };

            $scope.stickUpSizeToggler = function () {
                $scope.errorStory = 1;

                //var size = $scope.modal.stickUp;

                $('#modalSlideUpSmall').modal('show');
                //$('#modalSlideLeft').modal('show');


                console.log('stickUpSizeToggler', $('#modalStickUpSmall'));

                //var modalElem = $('#myModal');
                //if (size == "mini") {
                //    $('#modalStickUpSmall').modal('show')
                //} else {
                //    $('#myModal').modal('show')
                //    if (size == "default") {
                //        modalElem.children('.modal-dialog').removeClass('modal-lg');
                //    } else if (size == "full") {
                //        modalElem.children('.modal-dialog').addClass('modal-lg');
                //    }
                //}
            };

            $scope.modalSlideLeft = function () {
                setTimeout(function () {
                    $('#modalSlideLeft').modal('show');
                }, 300);
            };

            $scope.fillSizeToggler = function () {
                console.log('fillSizeToggler');
                console.log('fillSizeToggler', $('#modalFillIn').modal());
                $('#modalFillIn').modal();
                // Only for fillin modals so that the backdrop action is still there
                $('#modalFillIn').on('show.bs.modal', function (e) {
                    $('body').addClass('fill-in-modal');
                })
                $('#modalFillIn').on('hidden.bs.modal', function (e) {
                    $('body').removeClass('fill-in-modal');
                })

            }

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

            //ToDO
            $scope.ProfileID = $stateParams.id;//$route.current.params.id;
            console.log($scope.ProfileID, 'log in now admin', authService.authentication.userID, 'auth', authService.authentication);

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

            loadProfile();

            function loadProfile() {
                apiService.get($scope.baseURL + 'api/Profile/ProfileID/' + $scope.ProfileID, null,
                ProfileLoadCompleted,
                ProfileLoadFailed);
            }

            function ProfileLoadCompleted(response) {
                console.log(response.data.Result, 'ProfileLoadCompleted profile loaded');
                $scope.ProfileData = response.data.Result;
                $scope.ProfileIsConfirmed = response.data.Result.ProfileIsConfirmed;

                loadStorySettings();
                loadContentTypes();

                notificationService.displaySuccess("Profile loaded");
            }

            function ProfileLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
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
                //$location.url('/profile');
                console.log('redirect Back ;)', historyPage);

                //$scope.resetNewStory();

                setTimeout(function () {
                    $('#modalSlideUpSmallCongratulation').modal('hide');
                }, 300);

                setTimeout(function () {
                    if (historyPage.from != "" && historyPage.to !== historyPage.from) {
                        $state.go(historyPage.from, historyPage.fromParams);
                    } else {
                        $state.go('app.forms.masteradministrator', historyPage.toParams);
                    }
                }, 300);
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
                    this.on("complete", function (file) {
                        this.removeAllFiles(true);
                    }),
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
                    addedContent.ContentTitle = null;
                    addedContent.ContentURL = xhr.Result[0].FileUrl;
                    addedContent.ContentDimension = file.width + '-' + file.height;
                    addedContent.ContentTypeID = 1;
                    addedContent.ContentGeoLocation = null;
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
            };
            //End dropzone ;)

            $scope.regx = /https:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
            $scope.searchKeywordURL = $scope.baseURL + "api/Mobile/V2/Categories/Search/";

            var todayDate = new Date();

            var dd = todayDate.getDate();
            var mm = todayDate.getMonth() + 1; //January is 0!
            var yyyy = todayDate.getFullYear();

            $scope.today = yyyy + '-' + mm + '-' + dd;
            $scope.object.AlbumDatePublish = todayDate;//$scope.today;

            console.log($scope.today, 'Today');

            $scope.addNewTextContent = function () {
                var addedContent = {};
                addedContent.ContentTitle = null;
                addedContent.ContentURL = null;
                //addedContent.ContentID = $scope.contentIndex + 1;
                addedContent.ContentDimenssion = null;
                addedContent.ContentTypeID = 3;
                addedContent.ContentGeoLocation = null;
                $scope.Contents.push(addedContent);
            };

            $scope.addNewCTAContent = function () {
                var addedContent = {};
                addedContent.ContentTitle = null;
                addedContent.ContentURL = null;
                //addedContent.ContentID = $scope.contentIndex + 1;
                addedContent.ContentDimenssion = null;
                addedContent.ContentTypeID = 4;
                addedContent.ContentGeoLocation = null;
                $scope.Contents.push(addedContent);
            };

            function insertObject() {
                if ($scope.ProfileIsConfirmed) {
                    console.log($scope.Category, 'before action ;)');
                    //Need to be three 3 but now it is 0
                    if ($scope.Category != undefined && $scope.Contents.length >= 0) {
                        console.log($scope.object, $scope.Category, 'added ;)');

                        $scope.object.Contents = $scope.Contents;

                        let categoryName = $scope.Category.title != undefined ? $scope.Category.originalObject.KeyName : $scope.Category.originalObject;

                        let newAlbumData = {
                            AlbumName: $scope.Contents[0].ContentTitle,
                            AlbumStatus: true,
                            AlbumCover: $scope.Contents[0].ContentURL,
                            CategoryID: $scope.object.TopicID != undefined ? $scope.object.TopicID : 29,
                            ProfileID: $scope.ProfileID,
                            AlbumDatePublish: $scope.object.AlbumDatePublish,
                            AlbumView: 0,
                            AlbumAvailability: $scope.object.AlbumAvailability,
                            AlbumHasComment: $scope.object.AlbumHasComment != undefined ? $scope.object.AlbumHasComment : false,
                            AlbumReadingLanguageCode: $scope.object.AlbumReadingLanguageCode,
                            AlbumRepostValue: null,
                            AlbumRepostRequest: null,
                            AlbumRepostAttempt: null,
                            AlbumPrice: null,
                            AlbumIsTokenAvailable: null,
                            AlbumPriceToken: null,
                            ContentRatingID: $scope.object.ContentRatingID,
                            AlbumParentID: $scope.object.AlbumParentID,
                            Topic: { CategoryName: categoryName, CategoryID: null },//Why???? ;)
                            Contents: $scope.Contents
                            // [
                            // {
                            // "ContentTitle":"hello WOW from fiddler4 h6",
                            // "ContentURL":"https://gorgiasasia.blob.core.windows.net/images/content-20161106233839-pic(4).jpg",
                            // "ContentGeoLocation":null,
                            // "ContentDimension":"800-600",
                            // "ContentTypeID":1
                            // }]
                        };

                        console.log(newAlbumData, 'newalbumdata added ;)');

                        InsertNewStory(newAlbumData);

                    } else {
                        $scope.stickUpSizeToggler();
                    }
                } else {
                    if ($scope.Contents.length >= 3) {
                        console.log($scope.object, 'added ;)');

                        $scope.object.Contents = $scope.Contents;

                        let newAlbumData = {
                            AlbumName: $scope.Contents[0].ContentTitle,
                            AlbumStatus: true,
                            AlbumCover: $scope.Contents[0].ContentURL,
                            CategoryID: $scope.object.TopicID != '' ? $scope.object.TopicID : 29,
                            ProfileID: $scope.ProfileID,
                            AlbumDatePublish: $scope.object.AlbumDatePublish,
                            AlbumView: 0,
                            AlbumAvailability: $scope.object.AlbumAvailability,
                            AlbumHasComment: $scope.object.AlbumHasComment != undefined ? $scope.object.AlbumHasComment : false,
                            AlbumReadingLanguageCode: $scope.object.AlbumReadingLanguageCode,
                            AlbumRepostValue: null,
                            AlbumRepostRequest: null,
                            AlbumRepostAttempt: null,
                            AlbumPrice: null,
                            AlbumIsTokenAvailable: null,
                            AlbumPriceToken: null,
                            ContentRatingID: $scope.object.ContentRatingID,
                            AlbumParentID: $scope.object.AlbumParentID,
                            Topic: null,//Why???? ;)
                            Contents: $scope.Contents
                            // [
                            // {
                            // "ContentTitle":"hello WOW from fiddler4 h6",
                            // "ContentURL":"https://gorgiasasia.blob.core.windows.net/images/content-20161106233839-pic(4).jpg",
                            // "ContentGeoLocation":null,
                            // "ContentDimension":"800-600",
                            // "ContentTypeID":1
                            // }]
                        };

                        console.log(newAlbumData, 'newalbumdata added ;)');

                        InsertNewStory(newAlbumData);

                    } else {
                        $scope.stickUpSizeToggler();
                    }
                }
            }

            function InsertNewStory(data) {
                apiService.post($scope.baseURL + 'api/Mobile/V2/Album/New/Topic/', data,
                insertNewStoryCompleted,
                insertNewStoryFailed);
            }

            function insertNewStoryCompleted(response) {
                console.log(response, 'insertNewStoryCompleted');
                sendNotification(response.data.Result.AlbumID);
            }

            function insertNewStoryFailed(error) {
                console.log(error, 'insertNewStoryFailed');
            }

            function loadStorySettings() {
                apiService.get($scope.baseURL + 'api/Mobile/V2/Story/Settings/' + $scope.ProfileID + '/13/' + $scope.ProfileIsConfirmed, null,
                StorySettingsLoadCompleted,
                StorySettingsLoadFailed);
            }

            function StorySettingsLoadCompleted(response) {
                console.log(response.data.Result);

                $scope.Languages = response.data.Result[0].SettingCollection;
                $scope.Topics = response.data.Result[1].SettingCollection;
                $scope.ContentRatings = response.data.Result[2].SettingCollection;
                $scope.Availabilities = prepareAvailableAvailabilities(response.data.Result[3].SettingCollection);
                $scope.Categories = response.data.Result[1].SettingCollection;

                notificationService.displaySuccess("StorySettingsLoadCompleted loaded");
            }

            function prepareAvailableAvailabilities(availabilities) {
                var results = [];
                availabilities.forEach(function (item) {
                    if (item.KeyExtra == null) {
                        results.push(item);
                    }
                });
                return results;
            }

            function StorySettingsLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            function loadContentTypes() {
                apiService.get($scope.baseURL + 'api/Mobile/V2/Content/Types/3', null,
                contentTypesLoadCompleted,
                contentTypesLoadFailed);
            }

            function contentTypesLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.ContentTypes = response.data.Result;
                notificationService.displaySuccess("StorySettingsLoadCompleted loaded");
            }

            function contentTypesLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            $scope.prepareAvailability = function (data) {

                var item = data.KeyID;
                console.log(data, 'Availability');

                if (data.KeyExtra != null) {
                    return;
                }

                var hours = (item / (60)).toFixed(0);

                var days = (item / (60 * 24)).toFixed(0);
                var result = null;

                if (item < 60) {
                    result = item > 1 ? item + ' mins' : item + ' min';
                } else if (hours < 24) {
                    result = hours > 1 ? hours + ' hours' : hours + ' hour';
                } else {
                    result = days > 1 ? days + ' days' : days + ' day';
                }

                return result;
            }

            function sendNotification(albumID) {
                var notificationData = {
                    body: $scope.ProfileData.ProfileFullname + " has published new story",
                    title: "New Story",
                    albumid: albumID,
                    ProfileFullname: $scope.ProfileData.ProfileFullname,
                    channelid: "ch" + $scope.ProfileID,
                    ProfileID: $scope.ProfileID,
                    //NotificationType: 'Story',
                    //canValidate: false,
                };

                apiService.post($scope.baseURL + 'api/Web/Notification/V2/', notificationData,
                    sendNotificationLoadCompleted,
                    sendNotificationLoadFailed);
            }

            function sendNotificationLoadCompleted(response) {
                $scope.modalCongratulation();
                $scope.errorStory = 0;
                console.log(response.data.Result, 'Notification');
            }

            function sendNotificationLoadFailed(response) {
                console.log(response, 'Notification');
            }
            //$scope.modalCongratulation();
            //$scope.stickUpSizeToggler();

        }]);
})(angular.module('gorgiasapp'));