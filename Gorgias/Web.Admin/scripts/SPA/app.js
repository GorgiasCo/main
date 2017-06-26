(function () {
    'use strict';

    var app = angular
        .module('heroesApp', ['ngRoute', 'datatables', 'LocalStorageModule', 'angular-loading-bar', 'thatisuday.dropzone', 'angularValidator'])
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/layout/heroesview.html",
                controller: "mainController"
            })
             .when("/Address", {
                 templateUrl: "/layout/Address/listAddress.html",
                 controller: "listAddressController"
             })
            .when("/Address/add", {
                templateUrl: "/layout/Address/addAddress.html",
                controller: "addAddressController"
            })
            .when("/Address/edit/:id", {
                templateUrl: "/layout/Address/editAddress.html",
                controller: "editAddressController"
            })
            .when("/AddressType", {
                templateUrl: "/layout/AddressType/listAddressType.html",
                controller: "listAddressTypeController"
            })
            .when("/AddressType/add", {
                templateUrl: "/layout/AddressType/addAddressType.html",
                controller: "addAddressTypeController"
            })
            .when("/AddressType/edit/:id", {
                templateUrl: "/layout/AddressType/editAddressType.html",
                controller: "editAddressTypeController"
            })
            .when("/Album", {
                templateUrl: "/layout/Album/listAlbum.html",
                controller: "listAlbumController"
            })
            .when("/Album/add", {
                templateUrl: "/layout/Album/addAlbum.html",
                controller: "addAlbumController"
            })
            .when("/Album/edit/:id", {
                templateUrl: "/layout/Album/editAlbum.html",
                controller: "editAlbumController"
            })
            .when("/Attribute", {
                templateUrl: "/layout/Attribute/listAttribute.html",
                controller: "listAttributeController"
            })
            .when("/Attribute/add", {
                templateUrl: "/layout/Attribute/addAttribute.html",
                controller: "addAttributeController"
            })
            .when("/Attribute/edit/:id", {
                templateUrl: "/layout/Attribute/editAttribute.html",
                controller: "editAttributeController"
            })
            .when("/Category", {
                templateUrl: "/layout/Category/listCategory.html",
                controller: "listCategoryController"
            })
            .when("/Category/add", {
                templateUrl: "/layout/Category/addCategory.html",
                controller: "addCategoryController"
            })
            .when("/Category/edit/:id", {
                templateUrl: "/layout/Category/editCategory.html",
                controller: "editCategoryController"
            })
            .when("/City", {
                templateUrl: "/layout/City/listCity.html",
                controller: "listCityController"
            })
            .when("/City/add", {
                templateUrl: "/layout/City/addCity.html",
                controller: "addCityController"
            })
            .when("/City/edit/:id", {
                templateUrl: "/layout/City/editCity.html",
                controller: "editCityController"
            })
            .when("/Connection", {
                templateUrl: "/layout/Connection/listConnection.html",
                controller: "listConnectionController"
            })
            .when("/Connection/add", {
                templateUrl: "/layout/Connection/addConnection.html",
                controller: "addConnectionController"
            })
            .when("/Connection/edit/:id", {
                templateUrl: "/layout/Connection/editConnection.html",
                controller: "editConnectionController"
            })
            .when("/Content", {
                templateUrl: "/layout/Content/listContent.html",
                controller: "listContentController"
            })
            .when("/Content/add", {
                templateUrl: "/layout/Content/addContent.html",
                controller: "addContentController"
            })
            .when("/Content/edit/:id", {
                templateUrl: "/layout/Content/editContent.html",
                controller: "editContentController"
            })
            .when("/Country", {
                templateUrl: "/layout/Country/listCountry.html",
                controller: "listCountryController"
            })
            .when("/Country/add", {
                templateUrl: "/layout/Country/addCountry.html",
                controller: "addCountryController"
            })
            .when("/Country/edit/:id", {
                templateUrl: "/layout/Country/editCountry.html",
                controller: "editCountryController"
            })
            .when("/ExternalLink", {
                templateUrl: "/layout/ExternalLink/listExternalLink.html",
                controller: "listExternalLinkController"
            })
            .when("/ExternalLink/add", {
                templateUrl: "/layout/ExternalLink/addExternalLink.html",
                controller: "addExternalLinkController"
            })
            .when("/ExternalLink/edit/:id", {
                templateUrl: "/layout/ExternalLink/editExternalLink.html",
                controller: "editExternalLinkController"
            })
            .when("/Feature", {
                templateUrl: "/layout/Feature/listFeature.html",
                controller: "listFeatureController"
            })
            .when("/Feature/add", {
                templateUrl: "/layout/Feature/addFeature.html",
                controller: "addFeatureController"
            })
            .when("/Feature/edit/:id", {
                templateUrl: "/layout/Feature/editFeature.html",
                controller: "editFeatureController"
            })
            .when("/FeaturedSponsor", {
                templateUrl: "/layout/FeaturedSponsor/listFeaturedSponsor.html",
                controller: "listFeaturedSponsorController"
            })
            .when("/FeaturedSponsor/add", {
                templateUrl: "/layout/FeaturedSponsor/addFeaturedSponsor.html",
                controller: "addFeaturedSponsorController"
            })
            .when("/FeaturedSponsor/edit/:id", {
                templateUrl: "/layout/FeaturedSponsor/editFeaturedSponsor.html",
                controller: "editFeaturedSponsorController"
            })
            .when("/Industry", {
                templateUrl: "/layout/Industry/listIndustry.html",
                controller: "listIndustryController"
            })
            .when("/Industry/add", {
                templateUrl: "/layout/Industry/addIndustry.html",
                controller: "addIndustryController"
            })
            .when("/Industry/edit/:id", {
                templateUrl: "/layout/Industry/editIndustry.html",
                controller: "editIndustryController"
            })
            .when("/LinkType", {
                templateUrl: "/layout/LinkType/listLinkType.html",
                controller: "listLinkTypeController"
            })
            .when("/LinkType/add", {
                templateUrl: "/layout/LinkType/addLinkType.html",
                controller: "addLinkTypeController"
            })
            .when("/LinkType/edit/:id", {
                templateUrl: "/layout/LinkType/editLinkType.html",
                controller: "editLinkTypeController"
            })
            .when("/Message", {
                templateUrl: "/layout/Message/listMessage.html",
                controller: "listMessageController"
            })
            .when("/Message/add", {
                templateUrl: "/layout/Message/addMessage.html",
                controller: "addMessageController"
            })
            .when("/Message/edit/:id", {
                templateUrl: "/layout/Message/editMessage.html",
                controller: "editMessageController"
            })
            .when("/Profile", {
                templateUrl: "/layout/Profile/listProfile.html",
                controller: "listProfileController"
            })
            .when("/Profile/add", {
                templateUrl: "/layout/Profile/addProfile.html",
                controller: "addProfileController"
            })
            .when("/Profile/edit/:id", {
                templateUrl: "/layout/Profile/editProfile.html",
                controller: "editProfileController"
            })
            .when("/ProfileSocialNetwork", {
                templateUrl: "/layout/ProfileSocialNetwork/listProfileSocialNetwork.html",
                controller: "listProfileSocialNetworkController"
            })
            .when("/ProfileSocialNetwork/add", {
                templateUrl: "/layout/ProfileSocialNetwork/addProfileSocialNetwork.html",
                controller: "addProfileSocialNetworkController"
            })
            .when("/ProfileSocialNetwork/edit/:id", {
                templateUrl: "/layout/ProfileSocialNetwork/editProfileSocialNetwork.html",
                controller: "editProfileSocialNetworkController"
            })
            .when("/ProfileTag", {
                templateUrl: "/layout/ProfileTag/listProfileTag.html",
                controller: "listProfileTagController"
            })
            .when("/ProfileTag/add", {
                templateUrl: "/layout/ProfileTag/addProfileTag.html",
                controller: "addProfileTagController"
            })
            .when("/ProfileTag/edit/:id", {
                templateUrl: "/layout/ProfileTag/editProfileTag.html",
                controller: "editProfileTagController"
            })
            .when("/ProfileType", {
                templateUrl: "/layout/ProfileType/listProfileType.html",
                controller: "listProfileTypeController"
            })
            .when("/ProfileType/add", {
                templateUrl: "/layout/ProfileType/addProfileType.html",
                controller: "addProfileTypeController"
            })
            .when("/ProfileType/edit/:id", {
                templateUrl: "/layout/ProfileType/editProfileType.html",
                controller: "editProfileTypeController"
            })
            .when("/RequestType", {
                templateUrl: "/layout/RequestType/listRequestType.html",
                controller: "listRequestTypeController"
            })
            .when("/RequestType/add", {
                templateUrl: "/layout/RequestType/addRequestType.html",
                controller: "addRequestTypeController"
            })
            .when("/RequestType/edit/:id", {
                templateUrl: "/layout/RequestType/editRequestType.html",
                controller: "editRequestTypeController"
            })
            .when("/SocialNetwork", {
                templateUrl: "/layout/SocialNetwork/listSocialNetwork.html",
                controller: "listSocialNetworkController"
            })
            .when("/SocialNetwork/add", {
                templateUrl: "/layout/SocialNetwork/addSocialNetwork.html",
                controller: "addSocialNetworkController"
            })
            .when("/SocialNetwork/edit/:id", {
                templateUrl: "/layout/SocialNetwork/editSocialNetwork.html",
                controller: "editSocialNetworkController"
            })
            .when("/SubscriptionType", {
                templateUrl: "/layout/SubscriptionType/listSubscriptionType.html",
                controller: "listSubscriptionTypeController"
            })
            .when("/SubscriptionType/add", {
                templateUrl: "/layout/SubscriptionType/addSubscriptionType.html",
                controller: "addSubscriptionTypeController"
            })
            .when("/SubscriptionType/edit/:id", {
                templateUrl: "/layout/SubscriptionType/editSubscriptionType.html",
                controller: "editSubscriptionTypeController"
            })
            .when("/Tag", {
                templateUrl: "/layout/Tag/listTag.html",
                controller: "listTagController"
            })
            .when("/Tag/add", {
                templateUrl: "/layout/Tag/addTag.html",
                controller: "addTagController"
            })
            .when("/Tag/edit/:id", {
                templateUrl: "/layout/Tag/editTag.html",
                controller: "editTagController"
            })
            .when("/Theme", {
                templateUrl: "/layout/Theme/listTheme.html",
                controller: "listThemeController"
            })
            .when("/Theme/add", {
                templateUrl: "/layout/Theme/addTheme.html",
                controller: "addThemeController"
            })
            .when("/Theme/edit/:id", {
                templateUrl: "/layout/Theme/editTheme.html",
                controller: "editThemeController"
            })
            .when("/User", {
                templateUrl: "/layout/User/listUser.html",
                controller: "listUserController"
            })
            .when("/User/add", {
                templateUrl: "/layout/User/addUser.html",
                controller: "addUserController"
            })
            .when("/User/edit/:id", {
                templateUrl: "/layout/User/editUser.html",
                controller: "editUserController"
            })
            .when("/UserProfile", {
                templateUrl: "/layout/UserProfile/listUserProfile.html",
                controller: "listUserProfileController"
            })
            .when("/UserProfile/add", {
                templateUrl: "/layout/UserProfile/addUserProfile.html",
                controller: "addUserProfileController"
            })
            .when("/UserProfile/edit/:id", {
                templateUrl: "/layout/UserProfile/editUserProfile.html",
                controller: "editUserProfileController"
            })
            .when("/UserRole", {
                templateUrl: "/layout/UserRole/listUserRole.html",
                controller: "listUserRoleController"
            })
            .when("/UserRole/add", {
                templateUrl: "/layout/UserRole/addUserRole.html",
                controller: "addUserRoleController"
            })
            .when("/UserRole/edit/:id", {
                templateUrl: "/layout/UserRole/editUserRole.html",
                controller: "editUserRoleController"
            })
            .when("/login", {
                templateUrl: "/layout/login.html",
                controller: "loginController"
            })
            .when("/signup", {
                templateUrl: "/layout/signup.html",
                controller: "signupController"
            })
            //.when("/detail/:id", {
            //    templateUrl: "/layout/detail.html",
            //    controller: "detailCityController"
            //})
            //.when("/addresstype/add", {
            //    templateUrl: "/layout/edit/addAddressType.html",
            //    controller: "addAddressTypeController"
            //})
            //.when("/album/add", {
            //    templateUrl: "/layout/edit/addAlbum.html",
            //    controller: "addAlbumController"
            //})
            //.when("/category", {
            //    templateUrl: "/layout/list/listCategory.html",
            //    controller: "listCategoryController"
            //})
            //.when("/category/add", {
            //    templateUrl: "/layout/edit/editCategory.html",
            //    controller: "editCategoryController"
            //})
            //.when("/category/edit/:id", {
            //    templateUrl: "/layout/edit/editCategory.html",
            //    controller: "editCategoryController"
            //})
            //.when("/city", {
            //    templateUrl: "/layout/edit/listCity.html",
            //    controller: "listCityController"
            //})
            //.when("/city/add", {
            //    templateUrl: "/layout/edit/addCity.html",
            //    controller: "addCityController"
            //})
            //.when("/city/edit/:id", {
            //    templateUrl: "/layout/edit/editCity.html",
            //    controller: "editCityController"
            //})
            //.when("/login", {
            //    templateUrl: "/layout/login.html",
            //    controller: "loginController"
            //})
            //            .when("/signup", {
            //                templateUrl: "/layout/signup.html",
            //                controller: "signupController"
            //            })
            .otherwise({ redirectTo: "/" });
        $locationProvider.html5Mode(true);
    }

    var serviceBase = 'http://localhost:43587/';
    //var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
    app.constant('ngAuthSettings', {
        apiServiceBaseUri: serviceBase,
        clientId: 'ngAuthApp'
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(['authService', '$location', function (authService, $location) {
        authService.fillAuthData();
    }]);

})();