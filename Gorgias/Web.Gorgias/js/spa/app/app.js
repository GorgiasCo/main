(function () {
    'use strict';
    //'pageslide-directive7','angular-loading-bar','chieffancypants.loadingBar','ngMaterial'
    var app = angular
        .module('gorgiasapp', ['ngRoute', 'LocalStorageModule', 'ngCookies', 'angularValidator', 'ui.bootstrap', 'chieffancypants.loadingBar', 'checklist-model', 'ngTagsInput', 'pageslide-directive', 'geolocation', 'ngMap', 'thatisuday.dropzone', 'angularModalService', 'angular-carousel', 'slickCarousel', 'angular-preload-image', 'ngAnimate', 'cgBusy', 'ismobile', 'pascalprecht.translate', 'angular-uuid', 'ngLocationUpdate'])
        .config(config);

    app.value('cgBusyDefaults', {
        message: 'Loading',
        backdrop: true,
        templateUrl: '/layout/loading/custom-template.html',
        delay: 300,
        minDuration: 500,
        wrapperClass: 'cg-busy cg-busy-animation'
    });

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/layout/main.html",
                controller: "mainController"
            })
            .when("/terms-of-use", {
                templateUrl: "/layout/termOfUse.html",
            })
            .when("/about-us", {
                templateUrl: "/layout/aboutUsGorgias.html",
            })
            .when("/contact-us", {
                templateUrl: "/layout/contactUsGorgias.html",
            })
            .when("/admin/profile/:id", {
                templateUrl: "/layout/admin/viewAdminProfile.html",
                controller: "viewAdminProfileController"
            })
            .when("/admin/agency/profile/edit/:id/:aid", {
                templateUrl: "/layout/admin/viewAdminAgencyProfileEdit.html",
                controller: "viewAdminAgencyProfileEditController"
            })
            .when("/admin/agency/profile/:id", {
                templateUrl: "/layout/admin/viewAdminAgencyProfile.html",
                controller: "viewAdminAgencyProfileController"
            })
            .when("/:id/about", {
                templateUrl: "/layout/about.html",
                controller: "aboutController"
            })
            .when("/:id/location", {
                templateUrl: "/layout/location.html",
                controller: "locationController"
            })
            .when("/:id", {
                templateUrl: "/layout/web.html",
                controller: "webController"
            })
            .when("/app/:id", {
                templateUrl: "/layout/downloadapp.html",
                controller: "profileDownloadAppController"
            })
            .when("/app/:name/:profileid/:id", {
                templateUrl: "/layout/downloadapp.html",
                controller: "profileDownloadAppController"
            })
            .when("/:id/contact", {
                templateUrl: "/layout/contact.html",
                controller: "contactController"
            })
            .when("/:id/gallery", {
                templateUrl: "/layout/gallery.html",
                controller: "galleryController"
            })
            .when("/:id/slideshow/:cid", {
                templateUrl: "/layout/slideshow.html",
                controller: "slideshowController"
            })
            .when("/web/gallery/:id", {
                templateUrl: "/layout/gallery.html",
                controller: "galleryController"
            })
            .when("/account/reset/:userId", {
                templateUrl: "/layout/resetpassword.html",
                controller: "resetpasswordController"
            })
            .when("/account/confirmation/:userId", {
                templateUrl: "/layout/emailconfirmation.html",
                controller: "emailconfirmationController"
            })
            .when("/admin/login/", {
                templateUrl: "/layout/mainLogin.html",
                controller: "mainLoginController"
            })
            .when("/login", {
                templateUrl: "/layout/login.html",
                controller: "loginController"
            })
            //            .when("/signup", {
            //                templateUrl: "/layout/signup.html",
            //                controller: "signupController"
            //            })
            .otherwise({ redirectTo: "/" });
        $locationProvider.html5Mode(true);
    }

    //var serviceBase = 'https://gorgiasapi.azurewebsites.net/';
    var serviceBase = 'https://gorgiasapp.azurewebsites.net/';
    //var serviceBase = 'http://localhost:43587/';
    //var serviceBase = 'http://devgorgias.azurewebsites.net/';

    app.constant('ngAuthSettings', {
        apiServiceBaseUri: serviceBase,
        headercreative: '',
        clientId: 'ngAuthApp',
        cdn_images: 'https://gorgiasasia.blob.core.windows.net/images/',
        cdn_albums: 'https://gorgiasasia.blob.core.windows.net/albums/'
    });

    //, cfpLoadingBarProvider
    app.config(function ($httpProvider, cfpLoadingBarProvider, $translateProvider) {
        cfpLoadingBarProvider.includeSpinner = false;
        cfpLoadingBarProvider.latencyThreshold = 1;

        //cfpLoadingBarProvider.parentSelector = '#loading-bar-container';
        //cfpLoadingBarProvider.spinnerTemplate = '<div class="spinnerLoading"><div class="spinner"></div></div>';
        //cfpLoadingBarProvider.spinnerTemplate = '<div class="spinnerLoading"><div class="sk-cube-grid"><div class="sk-cube sk-cube1"></div><div class="sk-cube sk-cube2"></div><div class="sk-cube sk-cube3"></div><div class="sk-cube sk-cube4"></div><div class="sk-cube sk-cube5"></div><div class="sk-cube sk-cube6"></div><div class="sk-cube sk-cube7"></div><div class="sk-cube sk-cube8"></div><div class="sk-cube sk-cube9"></div></div></div>';
        $httpProvider.interceptors.push('authInterceptorService');
        $httpProvider.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        // Send this header only in post requests. Specifies you are sending a JSON object
        //$httpProvider.defaults.headers.post['dataType'] = 'json'
        //$httpProvider.defaults.headers.common['Content-Type'] = 'application/json';
        $httpProvider.defaults.headers.common = {};
        $httpProvider.defaults.headers.post = {};
        $httpProvider.defaults.headers.put = {};
        $httpProvider.defaults.headers.patch = {};

        $translateProvider.translations('en', {
            AllMyProfile: 'Profiles',
            Fullname: 'Fullname',
            DateCreated: 'Date Created',
            Description: 'Description',
            View: 'View',
            Like: 'Like',
            WebURL: 'Web URL',
            ShortDescription: 'Short Description',
            Email: 'Email',
            ProfileType: 'Profile Type',
            Theme: 'Theme',
            Subscription: 'Subscription',
            Submit: 'Submit',
            Cancel: 'Cancel',
            Address: 'Address',
            Name: 'Name',
            Tel: 'Tel',
            Fax: 'Fax',
            ZipCode: 'Zip Code',
            City: 'City',
            AddressType: 'Address Type',
            ValidatingAddress: 'Validating Address',
            ValidateAddress: 'Validate Address',
            ValidationNote: 'Please just click on validate before submit your address. Then submit it.',
            Reset: 'Reset',
            Albums: 'Albums',
            Album: 'Album',
            MyProfile: 'My Profile',
            AboutMe: 'About Me',
            AlbumInformation: 'Album Information',
            Category: 'Category',
            AlbumCover: 'Album Cover',
            AlbumImages: 'Album Images',
            Actions: 'Actions',
            Section: 'Section',
            Content: 'Content',
            AboutMeSection: 'About Me Section',
            Note: 'Note',
            DescribeSomething: 'Describe Something',
            Tags: 'Tags',
            PrimaryTag: 'Primary Tag',
            Add: 'Add',
            DeleteNote: 'Are you sure to delete?',
            SocialConnection: 'Social Connection',
            SocialNetwork: 'Social Network',
            SocialNetworkInformation: 'Social Network Information',
            Link: 'Link',
            CoverImage: 'Cover Image',
            MenuSlider: 'Menu Slider',
            MicroAppCover: 'Micro-App Cover',
            MicroAppSplashScreen: 'Micro-App Splash Screen',
            GorgiasCopyrights: 'Gorgias Copyrights',
            AddressInformation: 'Address Information',
            Update: 'Update',
            MyProfileInformation: 'My Profile Information',
            Delete: 'Delete',
            Edit: 'Edit',
            MyWebImages: 'Screen Images',

        })
          .translations('zh', {
              AllMyProfile: 'Profiles',
              Fullname: '标志性之名',
              DateCreated: '创建日期',
              Description: '详细介绍',
              View: '浏览',
              Like: '赞',
              WebURL: '网址',
              ShortDescription: '简介',
              Email: '电邮',
              ProfileType: '标志类型',
              Theme: '主题',
              Subscription: '订购',
              Submit: '提交',
              Cancel: '取消',
              Address: '地址',
              Name: '名',
              Tel: '电话',
              Fax: '传真',
              ZipCode: '邮政编码',
              City: '城市',
              AddressType: '地址类型',
              ValidatingAddress: '验证地址',
              ValidationNote: '请在提交您的地址之前点击验证, 然后提交',
              ValidateAddress: '验证地址',
              Reset: '重启',
              Albums: '相册',
              Album: '相册',
              MyProfile: '我的',
              AboutMe: '关于我的',
              AlbumInformation: '相册简介',
              Category: '类别',
              AlbumCover: '相册封面',
              AlbumImages: '相册图片',
              Actions: '行动',
              Section: '标题',
              Content: '内容',
              AboutMeSection: '关于',
              Note: '注意',
              DescribeSomething: '描述',
              Tags: '标签',
              PrimaryTag: '主标签',
              Add: '添加',
              DeleteNote: '确定删除?',
              SocialConnection: '社交联系',
              SocialNetwork: '社交网联',
              SocialNetworkInformation: '社交网联信息',
              Link: '网址',
              CoverImage: '封面图',
              MenuSlider: '菜单滑出块',
              MicroAppCover: '微型APP封面',
              MicroAppSplashScreen: '微型APP闪屏',
              GorgiasCopyrights: 'Gorgias版权',
              AddressInformation: '地址信息',
              Update: '立即更新',
              MyProfileInformation: '我的资料与信息',
              Delete: '删除',
              Edit: '编辑',
              MyWebImages: '屏幕图片',
          })
            .translations('my', {
                AllMyProfile: 'Profiles',
                Fullname: 'Nama Penuh',
                DateCreated: 'Tarikh Daftar',
                Description: 'Deskripsi',
                View: 'View',
                Like: 'Like',
                WebURL: 'Alamat web',
                ShortDescription: 'Deskripsi ringkas',
                Email: 'E-mel',
                ProfileType: 'Jenis profil',
                Theme: 'Tema',
                Subscription: 'Langgangan',
                Submit: 'Hantar',
                Cancel: 'Batal',
                Address: 'Alamat',
                Name: 'Nama',
                Tel: 'Telefon',
                Fax: 'Fax',
                ZipCode: 'Poskod',
                City: 'bandar',
                AddressType: 'Jenis alamat',
                ValidatingAddress: '',
                ValidationNote: 'Sila mengesahkan alamat sebelum teruskan',
                ValidateAddress: 'Sahkan Alamat',
                Reset: 'menetapkan semula',
                Albums: 'Album',
                Album: 'Album',
                MyProfile: 'Profil Saya',
                AboutMe: 'Mengenai Saya',
                AlbumInformation: 'Maklumat Album',
                Category: 'Kategori',
                AlbumCover: 'Lampiran Album',
                AlbumImages: 'Imej Album',
                Actions: 'Tindakan',
                Section: 'Seksyen',
                Content: 'Kandungan',
                AboutMeSection: 'Seksyen mengenai saya',
                Note: 'Nota',
                DescribeSomething: 'Menggambarkan sesuatu',
                Tags: 'Tags',
                PrimaryTag: 'Tag Utama',
                Add: 'Tambah',
                DeleteNote: 'Anda Pasti?',
                SocialConnection: 'Rangkaian Sosial',
                SocialNetwork: 'Rangkaian Sosial',
                SocialNetworkInformation: 'Informasi Rangkaian Sosial',
                Link: 'Link',
                CoverImage: 'Lampiran Gambar',
                MenuSlider: 'Slider Menu',
                MicroAppCover: 'Lampiran Mikro-App',
                MicroAppSplashScreen: 'Skrin Utama Mikro-App',
                GorgiasCopyrights: 'Hakcipta Gorgias',
                AddressInformation: 'Maklumat Alamat',
                Update: 'Ubah',
                MyProfileInformation: 'My Profile Information',
                Delete: 'Memadamkan',
                Edit: 'Edit',
                MyWebImages: 'Imej Web Saya',

            });

        $translateProvider.preferredLanguage('en');
        $translateProvider.useLocalStorage();

    });

    app.run(['authService', '$location', "$rootScope", "$window", '$anchorScroll', '$route',
        function (authService, $location, $rootScope, $window, $anchorScroll, $route) {
            authService.fillAuthData();

            $rootScope.$on('$routeChangeSuccess', function (evt, absNewUrl, absOldUrl) {
                $anchorScroll('top');
                //$rootScope.query = $.param(absNewUrl.params);
            });

            var original = $location.path;
            $location.path = function (path, reload) {
                if (reload === false) {
                    var lastRoute = $route.current;
                    var un = $rootScope.$on('$locationChangeSuccess', function () {
                        $route.current = lastRoute;
                        un();
                    });
                }
                return original.apply($location, [path]);
            };

        }]);

})(jQuery);