(function (app) {
    'use strict';
    app.controller('profileDownloadAppController', ['$scope', '$route', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', 'notificationService', '$window', 'Page', 'isMobile', '$timeout', function ($scope, $route, $http, apiService, authService, ngAuthSettings, $location, notificationService, $window, Page, isMobile, $timeout) {

        $scope.DownloadProfileMessage = 'To Get my Micro-App, click on the \"Access to Private\" and follow instruction';
        $scope.ProfileURL = $route.current.params.id;
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
        $scope.isInstruction = false;
        $scope.accessToAppMessage = 'Access to Private';
        $scope.isNotValid = false;
        $scope.isApple = false;        

        $scope.timestamp = new Date().getUTCMilliseconds();

        console.log($route.current.params.name, $route.current.params.profileid, 'meta header ;)');
        Page.setTitle($route.current.params.name);
        Page.setProfile($route.current.params.profileid);

        function setupLocation() {
            if ($window.navigator.standalone == true || (screen.height - document.documentElement.clientHeight < 40) || $window.matchMedia('(display-mode: standalone)').matches) {
                if (isMobile.apple.device) {
                    $scope.isApple = true;
                    $scope.accessToAppMessage = 'Loading App...';
                    $timeout(function () {
                        console.log('it is apple phone la');
                        window.location = "https://itunes.apple.com/us/app/gorgias/id1193285323";
                    }, 5000);

                    window.location = "gorgias://profileid=" + $scope.ProfileID;
                } else if (isMobile.android.device) {
                    $scope.isApple = false;
                    $scope.accessToAppMessage = 'Loading App...';
                    $timeout(function () {
                        window.location = "https://play.google.com/store/apps/details?id=com.gorgias.app";
                    }, 2000);
                    console.log('it is android phone ya ;)');
                    //window.location = 'http://gorgias.com/app/' + $scope.ProfileID;
                    window.location = 'gorgias://app?profileid=' + $scope.ProfileID;
                } else {
                    $scope.accessToAppMessage = 'Access to Private';
                    //$location.path = 'yasser';
                    console.log('it is desktop ya ;)', $location.path);
                }
            } else {
                console.log(document.documentElement.clientHeight, screen.height, 'stand');
                console.log($window.navigator.standalone, 'stand');
                console.log($window.matchMedia('(display-mode: standalone)').matches, 'stand');
                $scope.accessToAppMessage = 'Access to Private App';
                //$location.path = 'yasser';
                //$location.path('gorgias://?id=3233').replace().reload(false);
                //location.replace("https://www.w3schools.com");
                //history.pushState(null, null, '/en/step2');

                console.log('standalone', $location.path);
            }
        }
        

        $scope.toggleInstruction = function () {
            $scope.isInstruction = !$scope.isInstruction;
        };

        loadProfile();

        function loadProfile() {
            apiService.get($scope.baseURL + 'api/Web/Profile/Low/App/' + $scope.ProfileURL, null,
            ProfileLoadCompleted,
            ProfileLoadFailed);
        }

        function ProfileLoadCompleted(response) {
            console.log(response);
            console.log(response.data.Result);
            $scope.Profile = response.data.Result;
            if ($scope.Profile != null) {
                $scope.isNotValid = false;
                $scope.ProfileID = $scope.Profile.ProfileID;
                //$scope.ProfileTypeID = $scope.Profile.ProfileTypeID;
                //$scope.SocialNetworks = response.data.Result.SocialNetworks;
                Page.setTitle($scope.Profile.ProfileFullname);
                Page.setProfile($scope.Profile.ProfileID);
                //notificationService.displaySuccess("Profile loaded");
                if (isMobile.apple.device) {
                    $scope.isApple = true;
                } else {
                    $scope.isApple = false;
                }
                setupLocation();
            } else {
                $scope.isNotValid = true;
            }
        }

        function ProfileLoadFailed(response) {
            //notificationService.displayError(response.data.Errors);
        }

    }]);
})(angular.module('gorgiasapp'));