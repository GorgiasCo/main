(function (app) {
    'use strict';

    app.factory('myService', MyService);

    MyService.$inject = ['$q', '$http'];

    function MyService($q, $http) {
        var data;

        var service = {
            getData: getData
        };

        return service;

        //////////////////////////////////////

        function getData(refresh) {
            if (refresh || !data) {
                return $http.get('your_source').then(function (data) {
                    this.data = data;
                    return data;
                })
            }
            else {
                var deferrer = $q.defer();
                deferrer.resolve(data);
                return deferrer.promise;
            }
        }
    }

}(angular.module('heroesApp')));