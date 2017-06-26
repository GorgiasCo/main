(function (app) {
    'use strict';

    app.factory('trialService', trialService);

    app.factory('Page', function () {
        var title = 'ICONIC Gorgias';
        var profileID = 'apple';
        return {
            title: function () { return title; },
            profile: function () { return profileID; },
            setProfile: function (newProfile) { profileID = newProfile; console.log('Pageammmmmmm', profileID); },
            setTitle: function (newTitle) { title = newTitle; console.log('Pageammmmmmm', title);}
        };
    });

    trialService.$inject = ['ModalService', '$window'];

    function trialService(ModalService, $window) {

        var service = {
            getTrial: getTrial,
            getFeaturedTrial: getFeaturedTrial,
            getTagTrial: getTagTrial,
            getAlbumTrial: getAlbumTrial,
            getContactTrial: getContactTrial,
            getLoadMoreTrial: getLoadMoreTrial,
            getMyProfileTrial: getMyProfileTrial,
            getDownloadTrial: getDownloadTrial,
            getSocialNetworkTrial: getSocialNetworkTrial,
            getToGalleryTrial: getToGalleryTrial,
            getMainPageActivity: getMainPageActivity,
            getLoginTrial: getLoginTrial,
            showDownloadAppModal: showDownloadAppModal,
            showDownloadWebAppModal: showDownloadWebAppModal
        };

        function getLoginTrial() {
            console.log('Trial ;) getLoginTrial', false);
            return true;
        }

        function getMainPageActivity() {
            console.log('Trial ;) getMainPageActivity', false);
            return true;
        }

        function getSocialNetworkTrial() {
            console.log('Trial ;) getSocialNetworkTrial', false);
            return true;
        }

        function getToGalleryTrial() {
            console.log('Trial ;) getToGalleryTrial', false);
            return true;
        }

        function getMyProfileTrial() {
            console.log('Trial ;) getMyProfileTrial', false);
            return false;
        }

        function getTrial() {
            console.log('Trial ;) getTerial', 1);
            return true;
        }

        function getFeaturedTrial() {
            console.log('Trial ;) getFeaturedTrial', 1);
            return true;
        }

        function getTagTrial() {
            console.log('Trial ;) getTagTrial', 1);
            return true;
        }

        function getAlbumTrial() {
            console.log('Trial ;) getAlbumTrial', 1);
            return true;
        }

        function getContactTrial() {
            console.log('Trial ;) getContactTrial', 1);
            return true;
        }

        function getLoadMoreTrial() {
            console.log('Trial ;) getLoadMoreTrial', 1);
            return true;
        }

        function getDownloadTrial() {
            console.log('Trial ;) getLoadMoreTrial', 1);
            return true;
        }

        function showDownloadAppModal() {
            ModalService.showModal({
                templateUrl: "/layout/modal/downloadapp.html",
                controller: "downloadAppController"
            }).then(function (modal) {
                // The modal object has the element built, if this is a bootstrap modal
                // you can call 'modal' to show it, if it's a custom modal just show or hide
                // it as you need to.
                modal.element.modal();
                console.log("show modal");

                modal.close.then(function (result) {
                    if (result) {
                        //loadProfileTypes();
                        $window.location.href = 'https://play.google.com/store/apps/details?id=com.gorgias.app'

                        console.log('before Result');
                        console.log(result, 'modal result');
                    } else {
                        console.log(result, 'modal result false');
                    }
                });
            });
        }

        function showDownloadWebAppModal() {
            ModalService.showModal({
                templateUrl: "/layout/modal/downloadWebApp.html",
                controller: "downloadAppController"
            }).then(function (modal) {
                // The modal object has the element built, if this is a bootstrap modal
                // you can call 'modal' to show it, if it's a custom modal just show or hide
                // it as you need to.
                modal.element.modal();
                console.log("show modal");

                modal.close.then(function (result) {
                    if (result) {
                        //loadProfileTypes();
                        $window.location.href = 'https://play.google.com/store/apps/details?id=com.gorgias.app'

                        console.log('before Result');
                        console.log(result, 'modal result');
                    } else {
                        console.log(result, 'modal result false');
                    }
                });
            });
        }

        return service;
    }

})(angular.module('gorgiasapp'));