(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', '$rootScope', '$q'];

    function apiService($http, $location, $rootScope, $q) {

        var deferred = $q.defer();

        var service = {
            get: get,
            post: post,
            getWithPromise: getWithPromise
        };

        

        function getWithPromise(url) {
            return $http.get(url)
                    .then(function (result) {                        
                        // promise is fulfilled
                        deferred.resolve(result.data);
                        // promise is returned
                        return deferred.promise;
                    }, function (error) {
                        if (error.status == '401') {
                            //notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            // the following line rejects the promise 
                            deferred.reject(error);
                            // promise is returned
                            return deferred.promise;
                        }
                    });
        }

        function get(url, config, success, failure) {
            return $http.get(url, config)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            //notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status == '401') {
                            //notificationService.displayError('Authentication required.');
                            $rootScope.previousState = $location.path();
                            $location.path('/login');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        return service;
    }

})(angular.module('heroesApp'));