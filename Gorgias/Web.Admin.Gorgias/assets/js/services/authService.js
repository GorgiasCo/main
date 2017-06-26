//(function (app) {
//    'use strict';
'use strict';
angular.module('gorgiasapp')
    .factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings',
        function ($http, $q, localStorageService, ngAuthSettings) {

            var serviceBase = ngAuthSettings.apiServiceBaseUri;
            var authServiceFactory = {};

            var _authentication = {
                isAuth: false,
                userName: "",
                userID: 0,
                userUserID:0,
                userProfileTypeID: 0,
                userProfileURL: "",
                userIsConfirmed: '',
                userIsPeople: '',
                useRefreshTokens: true,
                userRole: 0,
                countryID: 0,
            };

            var _externalAuthData = {
                provider: "",
                userName: "",
                externalAccessToken: ""
            };

            var _saveRegistration = function (registration) {

                _logOut();

                return $http.post(serviceBase + 'api/account/register', registration, { headers: { 'Content-Type': 'application/json' } }).then(function (response) {
                    return response;
                });

            };

            var _login = function (loginData) {

                var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

                if (loginData.useRefreshTokens) {
                    data = data + "&client_id=" + ngAuthSettings.clientId;
                    //console.log(data = data + "&client_id=" + ngAuthSettings.clientId);
                }

                var deferred = $q.defer();

                $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                    console.log('Mamnonam khoda', response['.expires'], new Date().toUTCString());
                    //if (response['.expires'] > new Date().toUTCString()) {
                    //    console.log('its valid ;)');                
                    //} else {
                    //    console.log('its nooooooot valid anymore ;)');
                    //}
                    console.log(response, 'wow Login');
                    if (response.UserRole == 1) {
                        _authentication.isAuth = true;
                        _authentication.userName = response.userName;
                        _authentication.userFullName = response.userFullName;
                        _authentication.userID = response.userID;
                        _authentication.userProfileURL = response.profileURL;
                        _authentication.userProfileTypeID = response.profileTypeID;
                        _authentication.userIsConfirmed = response.profileIsConfirmed;
                        _authentication.userIsPeople = response.profileIsPeople;
                        _authentication.userRole = response.UserRole;
                        _authentication.userUserID = response.userUserID;
                        _authentication.countryID = response.countryID;

                        if (loginData.useRefreshTokens) {
                            localStorageService.set('authorizationData', { userID: _authentication.userID, userFullName: _authentication.userFullName, userIsConfirmed: _authentication.userIsConfirmed, userIsPeople: _authentication.userIsPeople, userProfileURL: _authentication.userProfileURL, userProfileTypeID: _authentication.userProfileTypeID, token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true, userRole: response.UserRole, userUserID: response.userUserID, countryID: response.countryID });
                            _authentication.useRefreshTokens = true;
                        }
                        else {
                            localStorageService.set('authorizationData', { userID: _authentication.userID, userFullName: _authentication.userFullName, userIsConfirmed: _authentication.userIsConfirmed, userIsPeople: _authentication.userIsPeople, userProfileURL: _authentication.userProfileURL, userProfileTypeID: _authentication.userProfileTypeID, token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false, userRole: response.UserRole, userUserID: response.userUserID, countryID: response.countryID });
                            _authentication.useRefreshTokens = false;
                        }
                    } else {
                        _authentication.isAuth = true;
                        _authentication.userName = response.userName;
                        _authentication.userID = response.userID;
                        _authentication.userRole = response.UserRole;
                        _authentication.userFullName = response.userFullName;
                        _authentication.userUserID = response.userUserID;
                        _authentication.countryID = response.countryID;

                        if (loginData.useRefreshTokens) {
                            localStorageService.set('authorizationData', { userID: _authentication.userID, token: response.access_token, userName: loginData.userName, userFullName: _authentication.userFullName, refreshToken: response.refresh_token, useRefreshTokens: true, userRole: response.UserRole, userUserID: response.userUserID, countryID: response.countryID });
                            _authentication.useRefreshTokens = true;
                        }
                        else {
                            localStorageService.set('authorizationData', { userID: _authentication.userID, token: response.access_token, userName: loginData.userName, userFullName: _authentication.userFullName, refreshToken: "", useRefreshTokens: false, userRole: response.UserRole, userUserID: response.userUserID, countryID: response.countryID });
                            _authentication.useRefreshTokens = false;
                        }
                    }


                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

                return deferred.promise;

            };

            var _logOut = function () {

                localStorageService.remove('authorizationData');

                _authentication.isAuth = false;
                _authentication.userName = "";
                _authentication.userID = 0;
                _authentication.userProfileURL = "";
                _authentication.userProfileTypeID = 0;
                _authentication.userIsConfirmed = '';
                _authentication.userIsPeople = '';
                _authentication.userFullName = '';
                _authentication.useRefreshTokens = false;
                _authentication.userUserID = 0;
                _authentication.countryID = 0;

            };

            var _fillAuthData = function () {

                var authData = localStorageService.get('authorizationData');
                if (authData) {
                    _authentication.isAuth = true;
                    _authentication.userName = authData.userName;
                    _authentication.userID = authData.userID;
                    _authentication.userProfileURL = authData.userProfileURL;
                    _authentication.userProfileTypeID = authData.userProfileTypeID;
                    _authentication.userIsConfirmed = authData.userIsConfirmed;
                    _authentication.userIsPeople = authData.userIsPeople;
                    _authentication.useRefreshTokens = authData.useRefreshTokens;
                    _authentication.userFullName = authData.userFullName;
                    _authentication.userUserID = authData.userUserID;
                    _authentication.countryID = authData.countryID;
                    _authentication.userRole = authData.userRole;
                }

            };

            var _refreshToken = function () {
                var deferred = $q.defer();

                var authData = localStorageService.get('authorizationData');

                if (authData) {

                    if (authData.useRefreshTokens) {

                        var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

                        localStorageService.remove('authorizationData');

                        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                            localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

                            deferred.resolve(response);

                        }).error(function (err, status) {
                            _logOut();
                            deferred.reject(err);
                        });
                    }
                }

                return deferred.promise;
            };

            var _obtainAccessToken = function (externalData) {

                var deferred = $q.defer();

                $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.useRefreshTokens = false;

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

                return deferred.promise;

            };

            var _registerExternal = function (registerExternalData) {

                var deferred = $q.defer();

                $http.post(serviceBase + 'api/account/registerexternal', registerExternalData).success(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.useRefreshTokens = false;

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });

                return deferred.promise;

            };

            authServiceFactory.saveRegistration = _saveRegistration;
            authServiceFactory.login = _login;
            authServiceFactory.logOut = _logOut;
            authServiceFactory.fillAuthData = _fillAuthData;
            authServiceFactory.authentication = _authentication;
            authServiceFactory.refreshToken = _refreshToken;

            authServiceFactory.obtainAccessToken = _obtainAccessToken;
            authServiceFactory.externalAuthData = _externalAuthData;
            authServiceFactory.registerExternal = _registerExternal;

            return authServiceFactory;
        }]);
//})(angular.module('gorgiasapp'));