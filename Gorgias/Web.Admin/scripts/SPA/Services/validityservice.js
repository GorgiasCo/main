(function (app) {
    'use strict';

    app.factory('validityService', validityService);

    validityService.$inject = ['authService'];

    function validityService(authService) {

        var service = {
            getValidity: getValidity
        };

        function getValidity() {
            console.log('from service', authService.authentication.isAuth);
            return authService.authentication.isAuth;
        }

        return service;
    }

})(angular.module('heroesApp'));