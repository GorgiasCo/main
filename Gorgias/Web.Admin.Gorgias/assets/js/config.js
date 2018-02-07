/* ============================================================
 * File: config.js
 * Configure routing
 * ============================================================ */

//var serviceBase = 'https://gorgiasapi.azurewebsites.net/';
//var serviceBase = 'https://gorgiasapp.azurewebsites.net/';
var serviceBase = 'http://localhost:43587/';
//var serviceBase = 'http://gorgiasapp-v3.azurewebsites.net/';
//var serviceBase = 'http://apiigorgias.azurewebsites.net/';
//var serviceBase = 'http://gorgiasapp.azurewebsites.net/';


angular.module('gorgiasapp')
    .run(['authService', '$location', "$rootScope", "$window", "localStorageService",
        function (authService, $location, $rootScope, $window, localStorageService) {
            authService.fillAuthData();

            if (authService.authentication.userID == 0) {
                $location.url('/access/login');
            }

            $rootScope.previousState;
            $rootScope.currentState;
            $rootScope.$on('$stateChangeSuccess', function (ev, to, toParams, from, fromParams) {
                $rootScope.previousState = from.name;
                $rootScope.currentState = to.name;

                localStorageService.set('pageHistory', {from: from.name, fromParams: fromParams, to: to.name, toParams: toParams});

                console.log('Previous state:' + $rootScope.previousState, fromParams)
                console.log('Current state:' + $rootScope.currentState, toParams)
            });

            $rootScope.$on('$routeChangeSuccess', function (evt, absNewUrl, absOldUrl) {
                //$anchorScroll('top');
                //$rootScope.query = $.param(absNewUrl.params);
            });

        }]);

angular.module('gorgiasapp')
    .constant('ngAuthSettings', {
        apiServiceBaseUri: serviceBase,
        headercreative: '',
        clientId: 'ngAuthApp',
        cdn_images: 'https://gorgiasasia.blob.core.windows.net/images/',
        cdn_albums: 'https://gorgiasasia.blob.core.windows.net/albums/'
    });

angular.module('gorgiasapp')
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$locationProvider', '$httpProvider', '$translateProvider',

        function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $locationProvider, $httpProvider, $translateProvider) {
            $urlRouterProvider
                .otherwise('/app/dashboard');

            $locationProvider.html5Mode(true).hashPrefix('!');

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
                AddressPhoto: 'Address Photo',
                AddressName: 'Address Name',
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
                DescribeSomething: 'Write something interesting',
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
                Agency: 'Agency',
                ProfileNewHelp: 'Here you can create new profile. Short & long descriptions are important for user engagement. Web URL must be unique and catchy.',
                HelperAuthorTitle: 'VIA senior product manager',
                HelperAuthor: 'Alex @ Gorgias',
                UploadPhotoHere: 'Upload profile photo here.',
                ProfilePhoto: 'Profile Photo',
                GettingStarted: 'Getting started',
                ProfileListHelp: 'You can enable profile as Celeb profile by activating Is People. Each profile need to be confirmed to be displayed at Gorgias Main Page.',
                ProfilesInformation: 'Profiles Information',
                NewProfile: 'New Profile',
                UserNew: 'Email address must be unique for each user. User can sign in to control panel when it\'s status is active.',
                NewUser: 'New User',
                RequiredTitle: 'Please fill required information.',
                Block: 'Block',
                UserList: 'All users that need to access to control panel can be managed here. After create new user, they can recieved an email for activation. Email must be unique',
                UsersInformation: 'Users Information',
                EditUser: 'Edit User',
                UserProfileNew: 'You can assign many role to one profile such as Agency or Admin as profile ownership. Admin role is compulsory for each profile.',
                NewUserProfileAccess: 'New User Profile Access',
                UserRole: 'User Role',
                User: 'User',
                Profile: 'Profile',
                UserProfileList: 'All assigned user and profile can be managed here. To change user role, need to delete previous user role here, then assign new user to new profile with new user role.',
                ProfilesAccesses: 'Profiles Accesses',
                NewUserProfile: 'New User Profile',
                WelcometoProfileAdministration: 'Welcome to Profile Administration',
                Administration: 'Administration',
                SignOut: 'Sign Out',
                Allrightsreserved: 'All rights reserved.',
                Dashboard: 'Dashboard',
                AllProfiles: 'All Profiles',
                ProfilesSetting: 'Profile\'s Setting',
                AllUsers: 'All Users',
                ProfileAccess: 'Profile Access',
                NewAccess: 'New Access',
                AllProfilesAccesses: 'All Profiles Accesses',
                IsPeople: 'People',
                Status: 'Status',
                IsConfirmed: 'Confirmed',
                Deactive: 'Deactive',
                Active: 'Active',
                Restore: 'Restore',
                IsBlocked: 'Blocked',
                Register: 'Register',
                Confirmed: 'Confirmed',
                AlbumAvailability: 'Album Type',
                hasPublishedNewAwesome: " has published new awesome ",
                hasUpdatedAlbum: " has updated the album",
                hasPublishedNewAwesomeCandid: " has published awesome candid ",
                expiredInTitle: " expires in",
                candidNotificationTitle: "ICONIC Candid",
                updateNotificationTitle: " Update",
                AlbumHasComments: "Allow Comments",//允许评论 new V2

                NewStory: "Create new story",
                AddNewPhoto: "+ Gallery",
                AddNewText: "+ Text only",
                AddNewCTA: "+ Call to actions",
                NewStoryCategory: "Category",
                NewStoryDate: "Publish Date",
                NewStoryTopic: "Topic",
                NewStoryReadingLanguage: "Story Language",
                NewStoryAvailability: "Expiring in…",
                NewStoryContentRating: "Story Content Rating",
                NewStoryCanReview: "Able to review this story?",
                NewStoryStoryTitlePlaceHolder: "your story need title",
                NewStoryContentTitle: "Title",
                NewStoryContentURLCTAPlaceholder: "Your url must start with https://",
                NewStoryYouNeedAtLeastThreePhoto: "Title missing, choose a nice title for your Story and publish.\n Your story need at least 3 photos",
                NewStoryContinue: "Back",
                NewStoryCongratulationTitle: "Congratulation, your lovely story published. \n start sharing your story to get more HotSpot",
                NewStoryCongratulationNewStoryTitle: "Create new story",
                NewStoryCongratulationGoToMyProfileTitle: "Back",


                



            })
              .translations('zh', {
                  AllMyProfile: 'Profiles',
                  Administration: 'Administration',
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
                  AddressName: '地址名称',
                  AddressPhoto: '地址照片',
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
                  Agency: '機構',
                  ProfileNewHelp: '在這裡你可以創建新的檔案。 短和長的介紹對於用戶大=的參與很重要。 網址必須是獨一無二的。',
                  HelperAuthorTitle: '通過高級產品經理',
                  HelperAuthor: 'Alex @ Gorgias',
                  UploadPhotoHere: '在這裡上傳個人档案照片',
                  ProfilePhoto: '大头帖',
                  GettingStarted: '这里开始',
                  ProfileListHelp: '您可以通過激活“Is People”來啟用配置文件作為“姿雅”档案。 每個配置文件需要確認在Gorgias主頁上顯示。',
                  ProfilesInformation: '個人資料档案：“個人資料档案',
                  NewProfile: '新档案',
                  UserNew: '每個用戶的電子郵件地址必須是唯一的。 當用戶的狀態處於活動狀態時，用戶可以登錄控制面板。',
                  NewUser: '新用戶',
                  RequiredTitle: '請填寫所需的信息。',
                  Block: '塊',
                  UserList: '所有需要浏览控制面板的用戶都可以在這裡進行管理。 創建新用戶後，他們可以收到一封電子郵件進行激活。 電子郵件必須是唯一的',
                  UsersInformation: '用戶信息',
                  EditUser: '更改用戶',
                  UserProfileNew: '您可以將多個角色分配給一個配置文件，如机构或管理員作為档案文件所有權 管理員角色對於每個配置文件是強制性的。',
                  NewUserProfileAccess: '新用戶档案訪問',
                  UserRole: '用戶角色',
                  User: '用戶',
                  Profile: '個人档案',
                  UserProfileList: '這裡可以管理所有分配的用戶和档案文件。 要更改用戶角色，需要在此刪除以前的用戶角色，然後將新用戶分配給具有新用戶角色的新档案文件。',
                  ProfilesAccesses: '個人档案訪問',
                  NewUserProfile: '新用戶档案',
                  WelcometoProfileAdministration: '歡迎來到档案管理',
                  SignOut: '登出',
                  Allrightsreserved: '版權所有。',
                  Dashboard: '儀表板',
                  AllProfiles: '所有檔案',
                  ProfilesSetting: '個人档案設置',
                  AllUsers: '全部用戶',
                  ProfileAccess: '個人档案訪問',
                  NewAccess: '新訪問',
                  AllProfilesAccesses: '所有檔案訪問',
                  IsPeople: '姿雅',
                  Status: '狀態',
                  IsConfirmed: '確認',
                  Deactive: '去活',
                  Active: '活跃',
                  Restore: '恢復',
                  IsBlocked: '封鎖',
                  Register: '註冊',
                  Confirmed: '確認',
                  AlbumHasComments: "允许评论",
                  AlbumAvailability: '相簿类型',

                  hasPublishedNewAwesome: " PO了最新最棒的 ",
                  hasUpdatedAlbum: " 更新相簿",
                  hasPublishedNewAwesomeCandid: "PO了最新的【私密答】",
                  expiredInTitle: " 將過時在",
                  candidNotificationTitle: "Iconic 思密达",
                  updateNotificationTitle: " 更新",

                  NewStory: "發佈新的故事",
                  AddNewPhoto: "+ 我的相簿",
                  AddNewText: "+ 僅限文字",
                  AddNewCTA: "+ 行動呼籲 - CTA",
                  NewStoryCategory: "類別",
                  NewStoryDate: "發布日期",
                  NewStoryTopic: "Gorgias 主題",
                  NewStoryReadingLanguage: "故事語言",
                  NewStoryAvailability: "即將過期在…",
                  NewStoryContentRating: "故事內容評級",
                  NewStoryCanReview: "這個故事能下評論嗎？",
                  NewStoryStoryTitlePlaceHolder: "你的故事需要标题",
                  NewStoryContentTitle: "Title",
                  NewStoryContentURLCTAPlaceholder: "您的網址開頭必須以 https://",
                  NewStoryYouNeedAtLeastThreePhoto: "標題丟失，為您的故事選擇一個不錯的標題並發布.\n 你的故事至少需要3张相片",
                  NewStoryContinue: "再試一次",
                  NewStoryCongratulationTitle: "恭喜 你有趣的故事已發布。 \n 開始分享另一個故事以獲得更多的熱點",
                  NewStoryCongratulationNewStoryTitle: "發佈新的故事",
                  NewStoryCongratulationGoToMyProfileTitle: "返回",

              })
                .translations('my', {
                    Administration: 'Administration',
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
                    AddressPhoto: 'Alamat Foto',
                    AddressName: 'Nama Alamat',
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
                    Agency: 'Agensi',
                    ProfileNewHelp: 'Anda boleh membuat profil baru di sini. Deskripsi pendek & panjang adalah penting untuk penglibatan pengguna. URL Web mestilah unik dan menarik.',
                    HelperAuthorTitle: 'Via Pengurus Senior Produk',
                    HelperAuthor: 'Alex @ Gorgias',
                    UploadPhotoHere: 'Muat naik foto profil di sini.',
                    ProfilePhoto: 'Profil Foto',
                    GettingStarted: 'Bermuda',
                    ProfileListHelp: 'Anda boleh mendayakan profil sebagai profil Celeb dengan mengaktifkan ialah People. Setiap profil perlu disahkan untuk dipaparkan di Laman Utama Gorgias.',
                    ProfilesInformation: 'Maklumat Profail',
                    NewProfile: 'Profile Baru',
                    UserNew: 'Alamat e-mel mestilah unik untuk setiap pengguna. Pengguna boleh log masuk ke panel kawalan apabila statusnya aktif',
                    NewUser: 'Pengguna Baru',
                    RequiredTitle: 'Sila isikan maklumat yang diperlukan.',
                    Block: 'Blok',
                    UserList: 'Semua pengguna yang perlu mengakses ke panel kawalan boleh diuruskan di sini. Selepas mencipta pengguna baru, mereka boleh menerima e-mel untuk pengaktifan. E-mel mesti unik',
                    UsersInformation: 'Info Pengguna',
                    EditUser: 'Edit Pengguna',
                    UserProfileNew: 'Anda boleh memberikan banyak peranan kepada satu profil seperti Agensi atau Admin sebagai pemilikan profil. Peranan admin adalah wajib bagi setiap profil.',
                    NewUserProfileAccess: 'Akses Profil Pengguna Baru',
                    UserRole: 'Peranan Pengguna',
                    User: 'Pengguna',
                    Profile: 'Profil',
                    UserProfileList: 'Semua pengguna dan profil yang diberikan boleh diuruskan di sini. Untuk menukar peranan pengguna, perlu memadamkan peranan pengguna sebelumnya di sini, kemudian tetapkan pengguna baru ke profil baru dengan peranan pengguna baru.',
                    ProfilesAccesses: 'Profil Mengakses',
                    NewUserProfile: 'Profil Pengguna Baru',
                    WelcometoProfileAdministration: 'Selamat datang ke Pentadbiran Profil',
                    SignOut: 'Log Keluar',
                    Allrightsreserved: 'Hak cipta terpelihara.',
                    Dashboard: 'Papan Pemuka',
                    AllProfiles: 'Semua Profil',
                    ProfilesSetting: 'Tetapan Profil',
                    AllUsers: 'Semua pengguna',
                    ProfileAccess: 'Akses Profil',
                    NewAccess: 'Akses Baru',
                    AllProfilesAccesses: 'Semua Profil Mengakses',
                    IsPeople: 'People',
                    Status: 'Status',
                    IsConfirmed: 'Ditetapkan',
                    Deactive: 'Tidak aktif',
                    Active: 'Aktif',
                    Restore: 'Pulihkan',
                    IsBlocked: 'Disekat',
                    Register: 'Daftar',
                    Confirmed: 'Ditetapkan',
                    AlbumHasComments: "Memberi Komen",
                    AlbumAvailability: 'Jenis Album',

                    hasPublishedNewAwesome: " telah menerbitkan post baru ",
                    hasUpdatedAlbum: " telah update album baru",
                    hasPublishedNewAwesomeCandid: " telah update Candid baru ",
                    expiredInTitle: " akan tamat tempoh",
                    candidNotificationTitle: "Iconic Candid",
                    updateNotificationTitle: " Kemaskini",

                    NewStory: "Create new story",
                    AddNewPhoto: "+ Gallery",
                    AddNewText: "+ Text only",
                    AddNewCTA: "+ Call to actions",
                    NewStoryCategory: "Category",
                    NewStoryDate: "Publish Date",
                    NewStoryTopic: "Topic",
                    NewStoryReadingLanguage: "Story Language",
                    NewStoryAvailability: "Expiring in…",
                    NewStoryContentRating: "Story Content Rating",
                    NewStoryCanReview: "Able to review this story?",
                    NewStoryStoryTitlePlaceHolder: "your story need title",
                    NewStoryContentTitle: "Title",
                    NewStoryContentURLCTAPlaceholder: "Your url must start with https://",
                    NewStoryYouNeedAtLeastThreePhoto: "Title missing, choose a nice title for your Story and publish.\n Your story need at least 3 photos",
                    NewStoryContinue: "Back",
                    NewStoryCongratulationTitle: "Congratulation, your lovely story published. \n start sharing your story to get more HotSpot",
                    NewStoryCongratulationNewStoryTitle: "Create new story",
                    NewStoryCongratulationGoToMyProfileTitle: "Back",
                });

            $translateProvider.preferredLanguage('en');
            $translateProvider.useLocalStorage();


            $stateProvider
                .state('app', {
                    abstract: true,
                    url: "/app",
                    templateUrl: "tpl/app.html"
                })
                .state('app.dashboard', {
                    url: "/dashboard",
                    templateUrl: "tpl/admin/viewDashboard.html",//tpl/dashboard.html
                    controller: 'viewDashboardController',//DashboardCtrl
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'nvd3',
                                    'mapplic',
                                    'rickshaw',
                                    'metrojs',
                                    'sparkline',
                                    'skycons',
                                    'switchery'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load([
                                        'assets/js/controllers/admin/Profile/Dashboard/viewDashboardController.js'
//'assets/js/controllers/dashboard.js'
                                    ]);
                                });
                        }]
                    }
                })
                .state('app.landing', {
                    url: "/landing",
                    templateUrl: "tpl/admin/V2/profile/viewLandingPage.html",
                    controller: 'landingPageController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'nvd3',
                                    'mapplic',
                                    'rickshaw',
                                    'metrojs',
                                    'sparkline',
                                    'skycons',
                                    'switchery'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load([
                                        'assets/js/controllers/V2/Profile/landingPageController.js'
                                    ]);
                                });
                        }]
                    }
                })

            // Email app
            .state('app.email', {
                abstract: true,
                url: '/email',
                templateUrl: 'tpl/apps/email/email.html',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                                'menuclipper',
                                'wysihtml5'
                        ], {
                            insertBefore: '#lazyload_placeholder'
                        })
                            .then(function () {
                                return $ocLazyLoad.load([
                                    'assets/js/apps/email/service.js',
                                    'assets/js/apps/email/email.js'
                                ])
                            });
                    }]
                }
            })
            .state('app.email.inbox', {
                url: '/inbox/:emailId',
                templateUrl: 'tpl/apps/email/email_inbox.html'
            })
            .state('app.email.compose', {
                url: '/compose',
                templateUrl: 'tpl/apps/email/email_compose.html'
            })
            // Social app
            .state('app.social', {
                url: '/social',
                templateUrl: 'tpl/apps/social/social.html',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                                'isotope',
                                'stepsForm'
                        ], {
                            insertBefore: '#lazyload_placeholder'
                        })
                            .then(function () {
                                return $ocLazyLoad.load([
                                    'pages/js/pages.social.min.js',
                                    'assets/js/apps/social/social.js'
                                ])
                            });
                    }]
                }
            })
            //Calendar app
            .state('app.calendar', {
                url: '/calendar',
                templateUrl: 'tpl/apps/calendar/calendar.html',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                                'switchery',
                                'moment-locales',
                                'interact'
                        ], {
                            insertBefore: '#lazyload_placeholder'
                        })
                            .then(function () {
                                return $ocLazyLoad.load([
                                    'pages/js/pages.calendar.min.js',
                                    'assets/js/apps/calendar/calendar.js'
                                ])
                            });
                    }]
                }
            })
            .state('app.builder', {
                url: '/builder',
                template: '<div></div>',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'assets/js/controllers/builder.js',
                        ]);
                    }]
                }
            })

            .state('app.layouts', {
                url: '/layouts',
                template: '<div ui-view></div>'
            })
            .state('app.layouts.default', {
                url: '/default',
                templateUrl: 'tpl/layouts_default.html'
            })
            .state('app.layouts.secondary', {
                url: '/secondary',
                templateUrl: 'tpl/layouts_secondary.html'
            })
            .state('app.layouts.horizontal', {
                url: '/horizontal',
                templateUrl: 'tpl/layouts_horizontal.html'
            })
            .state('app.layouts.rtl', {
                url: '/rtl',
                controller: 'RTLCtrl',
                templateUrl: 'tpl/layouts_default.html',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'assets/js/controllers/rtl.js',
                        ]);
                    }]
                }
            })
            .state('app.layouts.columns', {
                url: '/columns',
                templateUrl: 'tpl/layouts_columns.html'
            })

            // Boxed app
            .state('boxed', {
                url: "/boxed",
                templateUrl: "tpl/app.boxed.html"
            })

            // UI Elements 
            .state('app.ui', {
                url: '/ui',
                template: '<div ui-view></div>'
            })
                .state('app.ui.color', {
                    url: '/color',
                    templateUrl: 'tpl/ui_color.html'
                })
                .state('app.ui.typo', {
                    url: '/typo',
                    templateUrl: 'tpl/ui_typo.html'
                })
                .state('app.ui.icons', {
                    url: '/icons',
                    templateUrl: 'tpl/ui_icons.html',
                    controller: 'IconsCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'sieve',
                                    'line-icons'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load([
                                        'assets/js/controllers/icons.js'
                                    ])
                                });
                        }]
                    }
                })
                .state('app.ui.buttons', {
                    url: '/buttons',
                    templateUrl: 'tpl/ui_buttons.html'
                })
                .state('app.ui.notifications', {
                    url: '/notifications',
                    templateUrl: 'tpl/ui_notifications.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'assets/js/controllers/notifications.js'
                            ]);
                        }]
                    }
                })
                .state('app.ui.story', {
                    url: '/story/new/:id',
                    templateUrl: 'tpl/admin/album/addnewalbum.html',
                    controller: 'addNewAlbumController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                   'select',
                                   'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/album/addNewAlbumController.js');
                                });
                        }]
                    }
                })
                .state('app.ui.progress', {
                    url: '/progress',
                    templateUrl: 'tpl/ui_progress.html'
                })
                .state('app.ui.tabs', {
                    url: '/tabs',
                    templateUrl: 'tpl/ui_tabs.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'tabcollapse'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            });
                        }]
                    }
                })
                .state('app.ui.sliders', {
                    url: '/sliders',
                    templateUrl: 'tpl/ui_sliders.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                'noUiSlider',
                                'ionRangeSlider'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            });
                        }]
                    }
                })
                .state('app.ui.treeview', {
                    url: '/treeview',
                    templateUrl: 'tpl/ui_treeview.html',
                    controller: 'TreeCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'navTree'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/treeview.js');
                                });
                        }]
                    }
                })
                .state('app.ui.nestables', {
                    url: '/nestables',
                    templateUrl: 'tpl/ui_nestable.html',
                    controller: 'NestableCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'nestable'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/nestable.js');
                                });
                        }]
                    }
                })

            // Form elements
//            'moment',
//'datepicker',
//'daterangepicker',
//'timepicker',
//'inputMask',
//'autonumeric',
//'wysihtml5',
//'summernote',
//'tagsInput',                      'switchery','typehead'
            .state('app.forms', {
                url: '/forms',
                template: '<div ui-view></div>'
            })
                .state('app.forms.elements', {
                    url: '/elements',
                    templateUrl: 'tpl/forms_elements.html',
                    controller: 'viewAdminProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/viewAdminProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.administrator', {
                    url: '/administrator/:id',
                    templateUrl: 'tpl/admin/viewAdminProfile.html',
                    controller: 'viewAdminProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/viewAdminProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.masteradministrator', {
                    url: '/master/administrator/:id',
                    templateUrl: 'tpl/admin/viewAdminProfile.html',
                    controller: 'viewMasterAdminProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/viewMasterAdminProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.administration', {
                    url: '/administration/',
                    templateUrl: 'tpl/admin/viewAdminAgencyProfile.html',
                    controller: 'viewAdminAgencyProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/viewAdminAgencyProfileController.js');
                                });
                        }]
                    }
                })
                 .state('app.forms.addnewalbum', {
                     url: '/album/new',
                     templateUrl: 'tpl/admin/album/addnewalbum.html',
                     controller: 'addNewAlbumController',
                     resolve: {
                         deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                             return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                             ], {
                                 insertBefore: '#lazyload_placeholder'
                             })
                                 .then(function () {
                                     return $ocLazyLoad.load('assets/js/controllers/admin/profile/album/addNewAlbumController.js');
                                 });
                         }]
                     }
                 })
                .state('app.forms.agency', {
                    url: '/agency/',
                    templateUrl: 'tpl/admin/viewAdminAgencyProfile.html',
                    controller: 'viewAdminAgencyProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/viewAdminAgencyProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.country', {
                    url: '/country/list',
                    templateUrl: 'tpl/admin/profile/listProfile.html',
                    controller: 'listProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/listProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.profilenew', {
                    url: '/profile/new',
                    templateUrl: 'tpl/admin/profile/addProfile.html',
                    controller: 'addProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/addProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.user', {
                    url: '/user/list',
                    templateUrl: 'tpl/admin/User/listUser.html',
                    controller: 'listUserController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/user/listUserController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.usernew', {
                    url: '/user/new',
                    templateUrl: 'tpl/admin/User/addUser.html',
                    controller: 'addUserController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/user/addUserController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.useredit', {
                    url: '/user/:id',
                    templateUrl: 'tpl/admin/User/editUser.html',
                    controller: 'editUserController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/user/editUserController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.commission', {
                    url: '/commission/list',
                    templateUrl: 'tpl/admin/ProfileCommission/listProfileCommission.html',
                    controller: 'listProfileCommissionController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/ProfileCommission/listProfileCommissionController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.commissionnew', {
                    url: '/commission/new',
                    templateUrl: 'tpl/admin/ProfileCommission/addProfileCommission.html',
                    controller: 'addProfileCommissionController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/ProfileCommission/addProfileCommissionController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.commissionedit', {
                    url: '/commission/:id',
                    templateUrl: 'tpl/admin/ProfileCommission/editProfileCommission.html',
                    controller: 'editProfileCommissionController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/ProfileCommission/editProfileCommissionController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.userprofile', {
                    url: '/user/profile/list',
                    templateUrl: 'tpl/admin/UserProfile/listUserProfile.html',
                    controller: 'listUserProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/userprofile/listUserProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.userprofilenew', {
                    url: '/user/profile/new',
                    templateUrl: 'tpl/admin/UserProfile/addUserProfile.html',
                    controller: 'addUserProfileController',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'select',
                                    'dropzone',
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/admin/profile/agency/userprofile/addUserProfileController.js');
                                });
                        }]
                    }
                })
                .state('app.forms.layouts', {
                    url: '/layouts',
                    templateUrl: 'tpl/forms_layouts.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'datepicker',
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/forms_layouts.js');
                                });
                        }]
                    }
                })
                .state('app.forms.wizard', {
                    url: '/wizard',
                    templateUrl: 'tpl/forms_wizard.html',
                    controller: 'FormWizardCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'wizard'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/forms_wizard.js');
                                });
                        }]
                    }
                })

            // Portlets
            .state('app.portlets', {
                url: '/portlets',
                templateUrl: 'tpl/portlets.html',
                controller: 'PortletCtrl',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'assets/js/controllers/portlets.js'
                        ]);
                    }]
                }
            })

            // Views
            .state('app.views', {
                url: '/views',
                templateUrl: 'tpl/views.html'
            })

            // Tables
            .state('app.tables', {
                url: '/tables',
                template: '<div ui-view></div>'
            })
                .state('app.tables.basic', {
                    url: '/basic',
                    templateUrl: 'tpl/tables_basic.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'dataTables'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/tables.js');
                                });
                        }]
                    }
                })
                .state('app.tables.dataTables', {
                    url: '/dataTables',
                    templateUrl: 'tpl/tables_dataTables.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'dataTables',
                                    'ui-grid'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/dataTables.js');
                                });
                        }]
                    }
                })

            // Maps
            .state('app.maps', {
                url: '/maps',
                template: '<div class="full-height full-width" ui-view></div>'
            })
                .state('app.maps.google', {
                    url: '/google',
                    templateUrl: 'tpl/maps_google_map.html',
                    controller: 'GoogleMapCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'google-map'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/google_map.js')
                                        .then(function () {
                                            return loadGoogleMaps();
                                        });
                                });
                        }]
                    }
                })
                .state('app.maps.vector', {
                    url: '/vector',
                    templateUrl: 'tpl/maps_vector_map.html',
                    controller: 'VectorMapCtrl',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'mapplic',
                                    'select'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/vector_map.js');
                                });
                        }]
                    }
                })

            // Charts
            .state('app.charts', {
                url: '/charts',
                templateUrl: 'tpl/charts.html',
                controller: 'ChartsCtrl',
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                                'nvd3',
                                'rickshaw',
                                'sparkline'
                        ], {
                            insertBefore: '#lazyload_placeholder'
                        })
                            .then(function () {
                                return $ocLazyLoad.load('assets/js/controllers/charts.js');
                            });
                    }]
                }
            })

            // Extras
            .state('app.extra', {
                url: '/extra',
                template: '<div ui-view></div>'
            })
                .state('app.extra.invoice', {
                    url: '/invoice',
                    templateUrl: 'tpl/extra_invoice.html'
                })
                .state('app.extra.blank', {
                    url: '/blank',
                    templateUrl: 'tpl/extra_blank.html'
                })
                .state('app.extra.gallery', {
                    url: '/gallery',
                    templateUrl: 'tpl/extra_gallery.html',
                    resolve: {
                        deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                            return $ocLazyLoad.load([
                                    'isotope',
                                    'codropsDialogFx',
                                    'metrojs',
                                    'owlCarousel',
                                    'noUiSlider'
                            ], {
                                insertBefore: '#lazyload_placeholder'
                            })
                                .then(function () {
                                    return $ocLazyLoad.load('assets/js/controllers/gallery.js');
                                });
                        }]
                    }
                })
                .state('app.extra.timeline', {
                    url: '/timeline',
                    templateUrl: 'tpl/extra_timeline.html'
                })
            // Extra - Others
            .state('access', {
                url: '/access',
                template: '<div class="full-height" ui-view></div>'
            })
                .state('access.404', {
                    url: '/404',
                    templateUrl: 'tpl/extra_404.html'
                })
                .state('access.500', {
                    url: '/500',
                    templateUrl: 'tpl/extra_500.html'
                })
                .state('access.login', {
                    url: '/login',
                    templateUrl: 'tpl/extra_login.html'
                })
                .state('access.register', {
                    url: '/register',
                    templateUrl: 'tpl/extra_register.html'
                })
                .state('access.lock_screen', {
                    url: '/lock_screen',
                    templateUrl: 'tpl/extra_lock_screen.html'
                })

        }



    ]);