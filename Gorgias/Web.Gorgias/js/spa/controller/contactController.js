(function (app) {
    'use strict';
    app.controller('contactController', ['$scope', '$route', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', 'ModalService', 'trialService', function ($scope, $route, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, ModalService, trialService) {

        var vm = this;
        $scope.object = {};
        $scope.Profile = {};
        $scope.SubscribeMode = 'Subscribe';
        $scope.RequestedProfileFullname = 'You need to login.';

        $scope.redirectToSocialMedia = RedirectToSocialMedia;
        $scope.submitForm = submit;
        ngAuthSettings.headercreative = 'header-creative';

        console.log("contactController loaded ;)");
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        //$scope.ProfileID = $route.current.params.id;
        $scope.ProfileURL = $route.current.params.id;

        $scope.goToTop = goToTop;

        function goToTop() {
            $anchorScroll();
        }

        loadProfile();

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
                        submit();
                        console.log('before Result');
                        console.log(result, 'modal result');
                    } else {
                        console.log(result, 'modal result false');
                    }
                });
            });
        }

        function showReportModal() {
            ModalService.showModal({
                templateUrl: "/layout/modal/contactReport.html",
                controller: "contactReportController"
            }).then(function (modal) {
                // The modal object has the element built, if this is a bootstrap modal
                // you can call 'modal' to show it, if it's a custom modal just show or hide
                // it as you need to.
                modal.element.modal();
                console.log("show modal");

                modal.close.then(function (result) {
                    
                });
            });
        }

        function RedirectToSocialMedia(item) {
            if (trialService.getSocialNetworkTrial()) {
                trialService.showDownloadWebAppModal();
                console.log('modal');
            } else {
                $window.location.href = item;
            }
        }

        setupSendButton();

        function setupSendButton() {
            if (authService.authentication.userID > 0) {
                $scope.RequestedProfileFullname = authService.authentication.userFullName;
                $scope.sendButton = 'Send';
            } else {
                $scope.RequestedProfileFullname = 'You need to login.';
                $scope.sendButton = 'Login';
            }
        }

        function submit(isValid) {
            if (!trialService.getContactTrial) {
                if (isValid) {
                    console.log(authService.authentication, 'post Contact Validation');
                    if (authService.authentication.userID > 0) {
                        if (authService.authentication.userIsConfirmed != 'True') {
                            console.log('Cant Send Message');
                            showReportModal();
                        } else {
                            console.log('Can Send Message ;)');
                            $scope.object.MessageStatus = 0;
                            $scope.object.ProfileID = $scope.ProfileID;
                            $scope.object.RequestedProfileID = authService.authentication.userID;
                            apiService.postAuth($scope.baseURL + 'api/web/profile/contact', $scope.object,
                            addMessageSucceded,
                            addMessageFailed, contactAuth);
                            console.log("Post!!!" + $scope.object);
                        }
                    } else {
                        $scope.object.MessageStatus = 0;
                        $scope.object.ProfileID = $scope.ProfileID;
                        $scope.object.RequestedProfileID = authService.authentication.userID;
                        apiService.postAuth($scope.baseURL + 'api/web/profile/contact', $scope.object,
                        addMessageSucceded,
                        addMessageFailed, contactAuth);
                        console.log("Post!!!" + $scope.object);
                    }
                }
            } else {
                trialService.showDownloadWebAppModal();
            }
        }

        function contactAuth() {
            showLoginModal();
        }

        function addMessageSucceded(response){
            console.log("Post!!!", $scope.object);
            setupSendButton();
            $scope.object = {};
        }

        function addMessageFailed(response) {
            console.log(response);
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
            console.log($scope.Profile, 'Hi');
            if ($scope.Profile.RequestedProfileID > 0) {
                $scope.SubscribeMode = "Unsubscribed";
            } else {
                $scope.SubscribeMode = "Subscribe";
            }
            console.log($scope.SubscribeMode, 'Hi');
            $scope.ProfileID = $scope.Profile.ProfileID;
            $scope.ProfileTypeID = $scope.Profile.ProfileTypeID;
            $scope.SocialNetworks = response.data.Result.SocialNetworks;
            notificationService.displaySuccess("Profile loaded");
        }

        function ProfileLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }

    }]);
})(angular.module('gorgiasapp'));