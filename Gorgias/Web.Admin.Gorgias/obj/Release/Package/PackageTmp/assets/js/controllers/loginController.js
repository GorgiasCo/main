//(function (app) {
//    'use strict';
'use strict';
angular.module('gorgiasapp')
    .controller('loginController', ['$scope', '$stateParams', '$http', 'apiService', 'authService', 'ngAuthSettings', '$location', 'notificationService', 
        function ($scope, $stateParams, $http, apiService, authService, ngAuthSettings, $location, notificationService) {

            $scope.username = '';
            $scope.forgetMessageResult = '';
            $scope.registerMessageResult = 'Email is already used. Try other email.';

            $scope.successRegistration = false;

            $scope.registerModel = {
                'UserFullname': '',
                'UserName': '',
                'Password': '',
                'ConfirmPassword': '',
                'ProfileTypeID': 0
            };

            $scope.loginmode = 1;

            $scope.toggleLayout = function (item) {
                if (item == 3) {
                    loadProfileTypes();
                }
                $scope.loginmode = item;
            }

            $scope.forgetusername = '';

            $scope.forget = {
                'forgetusername': ''
            };

            $scope.submitForgetForm = function (isValid) {

                // check to make sure the form is completely valid
                if (isValid) {
                    console.log($scope.forget.forgetusername, 'hellooowww');
                    apiService.get($scope.baseURL + 'api/Account/Forget/' + $scope.forget.forgetusername, null,
                    forgetPasswordLoadCompleted,
                    forgetPasswordLoadFailed);
                } else {
                    alert('our form is so so so amazing');
                }

            };

            function forgetPasswordLoadCompleted(response) {
                console.log(response.data.Result);
                if (response.data.Result == false) {
                    $scope.forgetMessageResult = 'Your email is not valid';
                } else {
                    $scope.forgetMessageResult = 'Done, reset link has been sent now to your email.';
                }
            }

            function forgetPasswordLoadFailed(response) {
                $scope.forgetMessageResult = 'Ops, There is an issue, please try later.';
            }

            $scope.submitRegisterForm = function (isValid) {

                $scope.registerModel.ConfirmPassword = $scope.registerModel.Password;

                // check to make sure the form is completely valid
                if (isValid) {
                    authService.saveRegistration($scope.registerModel).then(function (response) {
                        console.log(response, 'successful Register');
                        $scope.successRegistration = true;
                    },
                     function (err) {
                         console.log(err, 'Register form error');
                         console.log(err.data.Errors, 'Register form error');
                         $scope.registerMessageResults = err.data.Errors;
                     });
                } else {
                    alert('our form is so so so amazing submitRegisterForm');
                }

            };

            $scope.submitForm = function (isValid) {
                if (isValid) {
                    console.log('Open Login');
                    $scope.login();
                } else {
                    alert('our form is so so so amazing');
                }
            };


            $scope.close = function () {
                console.log('Close Login');
                $scope.successRegistration = false;
                //close(false); // close, but give 500ms for bootstrap to animate
            };

            $scope.login = function () {
                console.log('Open Login');
                //close($scope.username); // close, but give 500ms for bootstrap to animate
            };

            ngAuthSettings.headercreative = 'header-creative';

            console.log("webController loaded ;)");
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

            $scope.ProfileID = $stateParams.id;//$route.current.params.id;

            function contact() {
                $location.url('/web/contact/' + $scope.ProfileID);
            }

            //dropzone ;)
            $scope.dzOptions = {
                url: $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile',
                paramName: 'photo',
                maxFilesize: '10',
                acceptedFiles: 'image/jpeg, images/jpg, image/png',
                addRemoveLinks: true,
                maxFiles: 1,
                autoProcessQueue: false,
                init: function () {
                    this.on("addedfile", function (file) {
                        $scope.hasFile = true;
                        console.log($scope.hasFile);
                    }),
                    this.on("processing", function (file) {
                        $scope.imagename = 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
                        $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.jpg';// + file.name.split(".")[1];
                        this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile';
                    });
                },
                removedfile: function (file) {
                    var _ref;
                    $scope.hasFile = false;
                    console.log($scope.hasFile);
                    console.log(file);
                    return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
                }
            };

            //Handle events for dropzone
            //Visit http://www.dropzonejs.com/#events for more events
            $scope.dzCallbacks = {
                'addedfile': function (file) {
                    console.log(file);
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    $scope.newFile = file;
                },
                'success': function (file, xhr) {
                    console.log('success ;)');
                    console.log(file, xhr);
                    redirectBack();
                },
            };


            //Apply methods for dropzone
            //Visit http://www.dropzonejs.com/#dropzone-methods for more methods
            $scope.dzMethods = {};
            $scope.removedfile = function () {
                $scope.dzMethods.removeFile($scope.newFile); //We got $scope.newFile from 'addedfile' event callback
                console.log('$scope.newFile');
            }
            //End dropzone ;)

            function loadProfileTypes() {
                apiService.get($scope.baseURL + 'api/ProfileTypes', null,
                profileTypesLoadCompleted,
                profileTypesLoadFailed);
            }

            function profileTypesLoadCompleted(response) {
                console.log(response.data.Result);
                $scope.ProfileTypes = response.data.Result;
                notificationService.displaySuccess("ProfileTypes Loaded");
            }

            function profileTypesLoadFailed(response) {
                notificationService.displayError(response.data.Errors);
            }

            /*Login f(x)*/
            $scope.loginData = {
                userName: "",
                password: "",
                useRefreshTokens: false
            };

            $scope.message = "";

            $scope.login = function () {
                authService.login($scope.loginData).then(function (response) {
                    console.log(response, 'WOWL successful login');
                    console.log(authService.authentication.userID, 'WOWL successful login', authService.authentication.userRole);
                    //close(true); // close, but give 500ms for bootstrap to animate
                    //$location.path('/yass');
                    switch (authService.authentication.userRole) {
                        case "1":
                            $location.path('/app/forms/administrator/' + authService.authentication.userID);
                            console.log(authService.authentication.userID, 'WOWL successful login 1 Brand or Agency And Celeb Profile ;)');
                            break;
                        case "3":
                            $location.path('/app/dashboard');
                            console.log(authService.authentication.userID, 'WOWL successful login 3 Invitie people ;)'); // forms/invitie/
                            break;
                        case "5":
                            $location.path('/app/dashboard');
                            console.log(authService.authentication.userID, 'WOWL successful login 4 Agency without Profile ;)'); // forms/agency/
                            break;
                        case "0":
                            $location.path('/app/dashboard');
                            console.log(authService.authentication.userID, 'WOWL successful login 0 Country Distributor ;)'); // forms/administration/
                            break;
                    }
                    //if (authService.authentication.userRole == 1) {
                    //    $location.path('/admin/profile/' + authService.authentication.userID);
                    //} else {
                    //    $location.path('/admin/agency/profile/' + authService.authentication.userID);
                    //}                
                },
                 function (err) {
                     console.log(err, 'login form error');
                     $scope.loginMessage = err.error_description;
                 });
            };

            $scope.authExternalProvider = function (provider) {

                var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

                var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                            + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                            + "&redirect_uri=" + redirectUri;
                window.$windowScope = $scope;

                var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
            };

            $scope.authCompletedCB = function (fragment) {

                $scope.$apply(function () {

                    if (fragment.haslocalaccount == 'False') {

                        authService.logOut();

                        authService.externalAuthData = {
                            provider: fragment.provider,
                            userName: fragment.external_user_name,
                            externalAccessToken: fragment.external_access_token
                        };

                        $location.path('/associate');

                    }
                    else {
                        //Obtain access token and redirect to orders
                        var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                        authService.obtainAccessToken(externalData).then(function (response) {

                            $location.path('/detail/4');

                        },
                     function (err) {
                         $scope.message = err;
                     });
                    }

                });
            }

        }]);
//})(angular.module('gorgiasapp'));