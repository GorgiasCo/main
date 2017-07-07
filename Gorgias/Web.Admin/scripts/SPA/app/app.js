(function () {
    'use strict';

    var app = angular
        .module('heroesApp', ['ngRoute', 'datatables', 'LocalStorageModule', 'chieffancypants.loadingBar', 'thatisuday.dropzone', 'angularValidator', 'ui.bootstrap', 'ui.router', 'iso.directives', 'ngMap', 'ui.bootstrap.datetimepicker', 'angular-uuid'])
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider', '$stateProvider', '$urlRouterProvider'];
    function config($routeProvider, $locationProvider, $stateProvider, $urlRouterProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/layout/Profile/listProfile.html",
                controller: "listProfileController"
            })
            .when("/newsletter", {
                templateUrl: "/layout/Newsletter/listNewsletter.html",
                controller: "listNewsletterController"
            })
            .when("/dbBulk", {
                templateUrl: "/layout/DBTool/dbtoolAdd.html",
                controller: "addBulkController"
            })
            .when("/newsletter/add", {
                templateUrl: "/layout/Newsletter/addNewsletter.html",
                controller: "addNewsletterController"
            })
            .when("/newsletter/:newsletterid", {
                templateUrl: "/layout/Newsletter/viewNewsletter.html",
                controller: "viewNewsletterController"
            })
            .when("/newsletter/edit/:newsletterid", {
                templateUrl: "/layout/Newsletter/editNewsletter.html",
                controller: "editNewsletterController"
            })
            .when("/profileattribute", {
                templateUrl: "/layout/ProfileAttribute/listProfileAttribute.html",
                controller: "listProfileAttributeController"
            })
            .when("/profileattribute/add", {
                templateUrl: "/layout/ProfileAttribute/addProfileAttribute.html",
                controller: "addProfileAttributeController"
            })
            .when("/profileattribute/:attributeid/:profileid", {
                templateUrl: "/layout/ProfileAttribute/viewProfileAttribute.html",
                controller: "viewProfileAttributeController"
            })
            .when("/profileattribute/edit/:attributeid/:profileid", {
                templateUrl: "/layout/ProfileAttribute/editProfileAttribute.html",
                controller: "editProfileAttributeController"
            })
            .when("/profileattribute/edit/:attributeid/:profileid", {
                templateUrl: "/layout/ProfileAttribute/editProfileAttribute.html",
                controller: "editProfileAttributeController"
            })
            .when("/profile/profileattribute/profileattribute", {
                templateUrl: "/layout/profile/ProfileAttribute/listprofileProfileAttribute.html",
                controller: "listprofileProfileAttributeController"
            })
            .when("/address", {
                templateUrl: "/layout/Address/listAddress.html",
                controller: "listAddressController"
            })
            .when("/address/add", {
                templateUrl: "/layout/Address/addAddress.html",
                controller: "addAddressController"
            })
            .when("/address/:addressid", {
                templateUrl: "/layout/Address/viewAddress.html",
                controller: "viewAddressController"
            })
            .when("/address/edit/:addressid", {
                templateUrl: "/layout/Address/editAddress.html",
                controller: "editAddressController"
            })
            .when("/profile/address/address", {
                templateUrl: "/layout/profile/Address/listprofileAddress.html",
                controller: "listprofileAddressController"
            })
            .when("/addresstype", {
                templateUrl: "/layout/AddressType/listAddressType.html",
                controller: "listAddressTypeController"
            })
            .when("/addresstype/add", {
                templateUrl: "/layout/AddressType/addAddressType.html",
                controller: "addAddressTypeController"
            })
            .when("/addresstype/:addresstypeid", {
                templateUrl: "/layout/AddressType/viewAddressType.html",
                controller: "viewAddressTypeController"
            })
            .when("/addresstype/edit/:addresstypeid", {
                templateUrl: "/layout/AddressType/editAddressType.html",
                controller: "editAddressTypeController"
            })
            .when("/album", {
                templateUrl: "/layout/Album/listAlbum.html",
                controller: "listAlbumController"
            })
            .when("/album/add", {
                templateUrl: "/layout/Album/addAlbum.html",
                controller: "addAlbumController"
            })
            .when("/album/:albumid", {
                templateUrl: "/layout/Album/viewAlbum.html",
                controller: "viewAlbumController"
            })
            .when("/album/edit/:albumid", {
                templateUrl: "/layout/Album/editAlbum.html",
                controller: "editAlbumController"
            })
            .when("/profile/album/album", {
                templateUrl: "/layout/profile/Album/listprofileAlbum.html",
                controller: "listprofileAlbumController"
            })
            .when("/attribute", {
                templateUrl: "/layout/Attribute/listAttribute.html",
                controller: "listAttributeController"
            })
            .when("/attribute/add", {
                templateUrl: "/layout/Attribute/addAttribute.html",
                controller: "addAttributeController"
            })
            .when("/attribute/:attributeid", {
                templateUrl: "/layout/Attribute/viewAttribute.html",
                controller: "viewAttributeController"
            })
            .when("/attribute/edit/:attributeid", {
                templateUrl: "/layout/Attribute/editAttribute.html",
                controller: "editAttributeController"
            })
            .when("/category", {
                templateUrl: "/layout/Category/listCategory.html",
                controller: "listCategoryController"
            })
            .when("/category/add", {
                templateUrl: "/layout/Category/addCategory.html",
                controller: "addCategoryController"
            })
            .when("/category/:categoryid", {
                templateUrl: "/layout/Category/viewCategory.html",
                controller: "viewCategoryController"
            })
            .when("/category/edit/:categoryid", {
                templateUrl: "/layout/Category/editCategory.html",
                controller: "editCategoryController"
            })
            .when("/city", {
                templateUrl: "/layout/City/listCity.html",
                controller: "listCityController"
            })
            .when("/city/add", {
                templateUrl: "/layout/City/addCity.html",
                controller: "addCityController"
            })
            .when("/city/:cityid", {
                templateUrl: "/layout/City/viewCity.html",
                controller: "viewCityController"
            })
            .when("/city/edit/:cityid", {
                templateUrl: "/layout/City/editCity.html",
                controller: "editCityController"
            })
            .when("/connection", {
                templateUrl: "/layout/Connection/listConnection.html",
                controller: "listConnectionController"
            })
            .when("/connection/add", {
                templateUrl: "/layout/Connection/addConnection.html",
                controller: "addConnectionController"
            })
            .when("/connection/:profileid/:requestedprofileid/:requesttypeid", {
                templateUrl: "/layout/Connection/viewConnection.html",
                controller: "viewConnectionController"
            })
            .when("/connection/edit/:profileid/:requestedprofileid/:requesttypeid", {
                templateUrl: "/layout/Connection/editConnection.html",
                controller: "editConnectionController"
            })
            .when("/connection/edit/:profileid/:requestedprofileid/:requesttypeid", {
                templateUrl: "/layout/Connection/editConnection.html",
                controller: "editConnectionController"
            })
            .when("/profile/connection/connection", {
                templateUrl: "/layout/profile/Connection/listprofileConnection.html",
                controller: "listprofileConnectionController"
            })
            .when("/content", {
                templateUrl: "/layout/Content/listContent.html",
                controller: "listContentController"
            })
            .when("/content/add", {
                templateUrl: "/layout/Content/addContent.html",
                controller: "addContentController"
            })
            .when("/content/:contentid", {
                templateUrl: "/layout/Content/viewContent.html",
                controller: "viewContentController"
            })
            .when("/content/edit/:contentid", {
                templateUrl: "/layout/Content/editContent.html",
                controller: "editContentController"
            })
            .when("/country", {
                templateUrl: "/layout/Country/listCountry.html",
                controller: "listCountryController"
            })
            .when("/country/add", {
                templateUrl: "/layout/Country/addCountry.html",
                controller: "addCountryController"
            })
            .when("/country/:countryid", {
                templateUrl: "/layout/Country/viewCountry.html",
                controller: "viewCountryController"
            })
            .when("/country/edit/:countryid", {
                templateUrl: "/layout/Country/editCountry.html",
                controller: "editCountryController"
            })
            .when("/externallink", {
                templateUrl: "/layout/ExternalLink/listExternalLink.html",
                controller: "listExternalLinkController"
            })
            .when("/externallink/add", {
                templateUrl: "/layout/ExternalLink/addExternalLink.html",
                controller: "addExternalLinkController"
            })
            .when("/externallink/:linktypeid/:profileid", {
                templateUrl: "/layout/ExternalLink/viewExternalLink.html",
                controller: "viewExternalLinkController"
            })
            .when("/externallink/edit/:linktypeid/:profileid", {
                templateUrl: "/layout/ExternalLink/editExternalLink.html",
                controller: "editExternalLinkController"
            })
            .when("/externallink/edit/:linktypeid/:profileid", {
                templateUrl: "/layout/ExternalLink/editExternalLink.html",
                controller: "editExternalLinkController"
            })
            .when("/profile/externallink/externallink", {
                templateUrl: "/layout/profile/ExternalLink/listprofileExternalLink.html",
                controller: "listprofileExternalLinkController"
            })
            .when("/feature", {
                templateUrl: "/layout/Feature/listFeature.html",
                controller: "listFeatureController"
            })
            .when("/feature/add", {
                templateUrl: "/layout/Feature/addFeature.html",
                controller: "addFeatureController"
            })
            .when("/feature/:featureid", {
                templateUrl: "/layout/Feature/viewFeature.html",
                controller: "viewFeatureController"
            })
            .when("/feature/edit/:featureid", {
                templateUrl: "/layout/Feature/editFeature.html",
                controller: "editFeatureController"
            })
            .when("/featuredsponsor", {
                templateUrl: "/layout/FeaturedSponsor/listFeaturedSponsor.html",
                controller: "listFeaturedSponsorController"
            })
            .when("/featuredsponsor/add", {
                templateUrl: "/layout/FeaturedSponsor/addFeaturedSponsor.html",
                controller: "addFeaturedSponsorController"
            })
            .when("/featuredsponsor/:featureid/:profileid", {
                templateUrl: "/layout/FeaturedSponsor/viewFeaturedSponsor.html",
                controller: "viewFeaturedSponsorController"
            })
            .when("/featuredsponsor/edit/:featureid/:profileid", {
                templateUrl: "/layout/FeaturedSponsor/editFeaturedSponsor.html",
                controller: "editFeaturedSponsorController"
            })
            .when("/featuredsponsor/edit/:featureid/:profileid", {
                templateUrl: "/layout/FeaturedSponsor/editFeaturedSponsor.html",
                controller: "editFeaturedSponsorController"
            })
            .when("/profile/featuredsponsor/featuredsponsor", {
                templateUrl: "/layout/profile/FeaturedSponsor/listprofileFeaturedSponsor.html",
                controller: "listprofileFeaturedSponsorController"
            })
            .when("/industry", {
                templateUrl: "/layout/Industry/listIndustry.html",
                controller: "listIndustryController"
            })
            .when("/industry/add", {
                templateUrl: "/layout/Industry/addIndustry.html",
                controller: "addIndustryController"
            })
            .when("/industry/:industryid", {
                templateUrl: "/layout/Industry/viewIndustry.html",
                controller: "viewIndustryController"
            })
            .when("/industry/edit/:industryid", {
                templateUrl: "/layout/Industry/editIndustry.html",
                controller: "editIndustryController"
            })
            .when("/linktype", {
                templateUrl: "/layout/LinkType/listLinkType.html",
                controller: "listLinkTypeController"
            })
            .when("/linktype/add", {
                templateUrl: "/layout/LinkType/addLinkType.html",
                controller: "addLinkTypeController"
            })
            .when("/linktype/:linktypeid", {
                templateUrl: "/layout/LinkType/viewLinkType.html",
                controller: "viewLinkTypeController"
            })
            .when("/linktype/edit/:linktypeid", {
                templateUrl: "/layout/LinkType/editLinkType.html",
                controller: "editLinkTypeController"
            })
            .when("/message", {
                templateUrl: "/layout/Message/listMessage.html",
                controller: "listMessageController"
            })
            .when("/message/add", {
                templateUrl: "/layout/Message/addMessage.html",
                controller: "addMessageController"
            })
            .when("/message/:messageid", {
                templateUrl: "/layout/Message/viewMessage.html",
                controller: "viewMessageController"
            })
            .when("/message/edit/:messageid", {
                templateUrl: "/layout/Message/editMessage.html",
                controller: "editMessageController"
            })
            .when("/profile/message/message", {
                templateUrl: "/layout/profile/Message/listprofileMessage.html",
                controller: "listprofileMessageController"
            })
            .when("/profile", {
                templateUrl: "/layout/Profile/listProfile.html",
                controller: "listProfileController"
            })
            .when("/profile/add", {
                templateUrl: "/layout/Profile/addProfile.html",
                controller: "addProfileController"
            })
            .when("/profile/:profileid", {
                templateUrl: "/layout/Profile/viewProfile.html",
                controller: "viewProfileController"
            })
            .when("/profile/edit/:profileid", {
                templateUrl: "/layout/Profile/editProfile.html",
                controller: "editProfileController"
            })
            .when("/profile/profile/profile", {
                templateUrl: "/layout/profile/Profile/listprofileProfile.html",
                controller: "listprofileProfileController"
            })
            .when("/profilesocialnetwork", {
                templateUrl: "/layout/ProfileSocialNetwork/listProfileSocialNetwork.html",
                controller: "listProfileSocialNetworkController"
            })
            .when("/profilesocialnetwork/add", {
                templateUrl: "/layout/ProfileSocialNetwork/addProfileSocialNetwork.html",
                controller: "addProfileSocialNetworkController"
            })
            .when("/profilesocialnetwork/:socialnetworkid/:profileid", {
                templateUrl: "/layout/ProfileSocialNetwork/viewProfileSocialNetwork.html",
                controller: "viewProfileSocialNetworkController"
            })
            .when("/profilesocialnetwork/edit/:socialnetworkid/:profileid", {
                templateUrl: "/layout/ProfileSocialNetwork/editProfileSocialNetwork.html",
                controller: "editProfileSocialNetworkController"
            })
            .when("/profilesocialnetwork/edit/:socialnetworkid/:profileid", {
                templateUrl: "/layout/ProfileSocialNetwork/editProfileSocialNetwork.html",
                controller: "editProfileSocialNetworkController"
            })
            .when("/profile/profilesocialnetwork/profilesocialnetwork", {
                templateUrl: "/layout/profile/ProfileSocialNetwork/listprofileProfileSocialNetwork.html",
                controller: "listprofileProfileSocialNetworkController"
            })
            .when("/profiletag", {
                templateUrl: "/layout/ProfileTag/listProfileTag.html",
                controller: "listProfileTagController"
            })
            .when("/profiletag/add", {
                templateUrl: "/layout/ProfileTag/addProfileTag.html",
                controller: "addProfileTagController"
            })
            .when("/profiletag/:tagid/:profileid", {
                templateUrl: "/layout/ProfileTag/viewProfileTag.html",
                controller: "viewProfileTagController"
            })
            .when("/profiletag/edit/:tagid/:profileid", {
                templateUrl: "/layout/ProfileTag/editProfileTag.html",
                controller: "editProfileTagController"
            })
            .when("/profiletag/edit/:tagid/:profileid", {
                templateUrl: "/layout/ProfileTag/editProfileTag.html",
                controller: "editProfileTagController"
            })
            .when("/profile/profiletag/profiletag", {
                templateUrl: "/layout/profile/ProfileTag/listprofileProfileTag.html",
                controller: "listprofileProfileTagController"
            })
            .when("/profiletype", {
                templateUrl: "/layout/ProfileType/listProfileType.html",
                controller: "listProfileTypeController"
            })
            .when("/profiletype/add", {
                templateUrl: "/layout/ProfileType/addProfileType.html",
                controller: "addProfileTypeController"
            })
            .when("/profiletype/:profiletypeid", {
                templateUrl: "/layout/ProfileType/viewProfileType.html",
                controller: "viewProfileTypeController"
            })
            .when("/profiletype/edit/:profiletypeid", {
                templateUrl: "/layout/ProfileType/editProfileType.html",
                controller: "editProfileTypeController"
            })
            .when("/profile/profiletype/profiletype", {
                templateUrl: "/layout/profile/ProfileType/listprofileProfileType.html",
                controller: "listprofileProfileTypeController"
            })
            .when("/requesttype", {
                templateUrl: "/layout/RequestType/listRequestType.html",
                controller: "listRequestTypeController"
            })
            .when("/requesttype/add", {
                templateUrl: "/layout/RequestType/addRequestType.html",
                controller: "addRequestTypeController"
            })
            .when("/requesttype/:requesttypeid", {
                templateUrl: "/layout/RequestType/viewRequestType.html",
                controller: "viewRequestTypeController"
            })
            .when("/requesttype/edit/:requesttypeid", {
                templateUrl: "/layout/RequestType/editRequestType.html",
                controller: "editRequestTypeController"
            })
            .when("/socialnetwork", {
                templateUrl: "/layout/SocialNetwork/listSocialNetwork.html",
                controller: "listSocialNetworkController"
            })
            .when("/socialnetwork/add", {
                templateUrl: "/layout/SocialNetwork/addSocialNetwork.html",
                controller: "addSocialNetworkController"
            })
            .when("/socialnetwork/:socialnetworkid", {
                templateUrl: "/layout/SocialNetwork/viewSocialNetwork.html",
                controller: "viewSocialNetworkController"
            })
            .when("/socialnetwork/edit/:socialnetworkid", {
                templateUrl: "/layout/SocialNetwork/editSocialNetwork.html",
                controller: "editSocialNetworkController"
            })
            .when("/subscriptiontype", {
                templateUrl: "/layout/SubscriptionType/listSubscriptionType.html",
                controller: "listSubscriptionTypeController"
            })
            .when("/subscriptiontype/add", {
                templateUrl: "/layout/SubscriptionType/addSubscriptionType.html",
                controller: "addSubscriptionTypeController"
            })
            .when("/subscriptiontype/:subscriptiontypeid", {
                templateUrl: "/layout/SubscriptionType/viewSubscriptionType.html",
                controller: "viewSubscriptionTypeController"
            })
            .when("/subscriptiontype/edit/:subscriptiontypeid", {
                templateUrl: "/layout/SubscriptionType/editSubscriptionType.html",
                controller: "editSubscriptionTypeController"
            })
            .when("/tag", {
                templateUrl: "/layout/Tag/listTag.html",
                controller: "listTagController"
            })
            .when("/tag/add", {
                templateUrl: "/layout/Tag/addTag.html",
                controller: "addTagController"
            })
            .when("/tag/:tagid", {
                templateUrl: "/layout/Tag/viewTag.html",
                controller: "viewTagController"
            })
            .when("/tag/edit/:tagid", {
                templateUrl: "/layout/Tag/editTag.html",
                controller: "editTagController"
            })
            .when("/theme", {
                templateUrl: "/layout/Theme/listTheme.html",
                controller: "listThemeController"
            })
            .when("/theme/add", {
                templateUrl: "/layout/Theme/addTheme.html",
                controller: "addThemeController"
            })
            .when("/theme/:themeid", {
                templateUrl: "/layout/Theme/viewTheme.html",
                controller: "viewThemeController"
            })
            .when("/theme/edit/:themeid", {
                templateUrl: "/layout/Theme/editTheme.html",
                controller: "editThemeController"
            })
            .when("/user", {
                templateUrl: "/layout/User/listUser.html",
                controller: "listUserController"
            })
            .when("/user/add", {
                templateUrl: "/layout/User/addUser.html",
                controller: "addUserController"
            })
            .when("/user/:userid", {
                templateUrl: "/layout/User/viewUser.html",
                controller: "viewUserController"
            })
            .when("/user/edit/:userid", {
                templateUrl: "/layout/User/editUser.html",
                controller: "editUserController"
            })
            .when("/userprofile", {
                templateUrl: "/layout/UserProfile/listUserProfile.html",
                controller: "listUserProfileController"
            })
            .when("/userprofile/add", {
                templateUrl: "/layout/UserProfile/addUserProfile.html",
                controller: "addUserProfileController"
            })
            .when("/userprofile/:profileid/:userroleid/:userid", {
                templateUrl: "/layout/UserProfile/viewUserProfile.html",
                controller: "viewUserProfileController"
            })
            .when("/userprofile/edit/:profileid/:userroleid/:userid", {
                templateUrl: "/layout/UserProfile/editUserProfile.html",
                controller: "editUserProfileController"
            })
            .when("/userprofile/edit/:profileid/:userroleid/:userid", {
                templateUrl: "/layout/UserProfile/editUserProfile.html",
                controller: "editUserProfileController"
            })
            .when("/profile/userprofile/userprofile", {
                templateUrl: "/layout/profile/UserProfile/listprofileUserProfile.html",
                controller: "listprofileUserProfileController"
            })
            .when("/userrole", {
                templateUrl: "/layout/UserRole/listUserRole.html",
                controller: "listUserRoleController"
            })
            .when("/userrole/add", {
                templateUrl: "/layout/UserRole/addUserRole.html",
                controller: "addUserRoleController"
            })
            .when("/userrole/:userroleid", {
                templateUrl: "/layout/UserRole/viewUserRole.html",
                controller: "viewUserRoleController"
            })
            .when("/userrole/edit/:userroleid", {
                templateUrl: "/layout/UserRole/editUserRole.html",
                controller: "editUserRoleController"
            })
            .when("/albumtype", {
                templateUrl: "/layout/AlbumType/listAlbumType.html",
                controller: "listAlbumTypeController"
            })
            .when("/albumtype/add", {
                templateUrl: "/layout/AlbumType/addAlbumType.html",
                controller: "addAlbumTypeController"
            })
            .when("/albumtype/:albumtypeid", {
                templateUrl: "/layout/AlbumType/viewAlbumType.html",
                controller: "viewAlbumTypeController"
            })
            .when("/albumtype/edit/:albumtypeid", {
                templateUrl: "/layout/AlbumType/editAlbumType.html",
                controller: "editAlbumTypeController"
            })
            .when("/profile/profile/:id", {
                templateUrl: "/layout/Profile/viewAdminProfile.html",
                controller: "viewAdminProfileController"
            })
            .when("/profile/upload/:id", {
                templateUrl: "/layout/Profile/editAdminProfileUpload.html",
                controller: "editAdminProfileUploadController"
            })        
            .when("/login", {
                templateUrl: "/layout/login.html",
                controller: "loginController"
            })

            .when("/dashboardBETA", {
                templateUrl: "/layout/Dashboard/dashboardBETA.html"
                //controller:

            })

            .when("/stats", {
                templateUrl: "/layout/Dashboard/stats.html",
                //controller:
            })

            .when("/finance", {
                templateUrl: "/layout/Dashboard/finance.html",
                //controller: ""

            })

            //.when("/signup", {
            //    templateUrl: "/layout/signup.html",
            //    controller: "signupController"
            //})
            .otherwise({ redirectTo: "/" });
        $locationProvider.html5Mode(true);
    }

    
    //var serviceBase = 'http://gorgiasapi.azurewebsites.net/';
    //var serviceBase = 'http://localhost:43587/';
    //var serviceBase = 'http://gorgiasapp.azurewebsites.net/';
    var serviceBase = 'http://devgorgias.azurewebsites.net/';

    app.constant('ngAuthSettings', {
        apiServiceBaseUri: serviceBase,        
        clientId: 'ngAuthApp',
        cdn_images: 'https://gorgiasasia.blob.core.windows.net/images/',
        cdn_albums: 'https://gorgiasasia.blob.core.windows.net/albums/'
    });

    app.constant('ngAuthValueSettings', {                
        isAuthenticated: false        
    });



    app.config(function ($httpProvider, cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
        //cfpLoadingBarProvider.latencyThreshold = 500;
        $httpProvider.interceptors.push('authInterceptorService');
        // Sends this header with any AJAX request
        $httpProvider.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        // Send this header only in post requests. Specifies you are sending a JSON object
        //$httpProvider.defaults.headers.post['dataType'] = 'json'
        $httpProvider.defaults.headers.common = {};
        //$httpProvider.defaults.headers.post = {};
        $httpProvider.defaults.headers.put = {};
        $httpProvider.defaults.headers.patch = {};
    });

    app.run(['authService', '$location', 'ngAuthValueSettings', function (authService, $location, ngAuthValueSettings) {
        authService.fillAuthData();
        console.log(ngAuthValueSettings.isAuthenticated, 'authenticated');
    }]);

})();