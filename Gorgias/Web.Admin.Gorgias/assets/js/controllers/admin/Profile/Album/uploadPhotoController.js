(function (app) {
    'use strict';
    app.controller('uploadPhotoController', ['$scope', 'ngAuthSettings', function ($scope, ngAuthSettings) {

        $scope.close = function (result) {
            console.log('uploadPhotoController', result);
            //close(result,500); // close, but give 500ms for bootstrap to animate
        };

        $scope.fromContrller = "Hiiiiiiiiiiiiiiii ;)";

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.masterFileName = "profile";

        $scope.imagename = "sdas";

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
                    $scope.imagename = 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    $scope.object.ProfileImage = 'https://gorgiasasia.blob.core.windows.net/images/' + 'profile-' + $scope.ProfileID + '.' + file.name.split(".")[1];
                    console.log($scope.hasFile);
                }),
                this.on("processing", function (file) {
                    this.options.url = $scope.baseURL + 'api/images/name?ImageName=' + $scope.imagename + '&MasterFileName=profile';
                });
            },
            addedfile: function (file) {
                //file.previewTemplate = $(this.options.previewTemplate);
                //console.log(file.previewTemplate, 'file.previewTemplate');
                //file.previewTemplate.find(".filename span").text(file.name);
                //file.previewTemplate.find(".details").append($("<div class=\"size\">" + (this.filesize(file.size)) + "</div>"));

                //console.log(file.previewTemplate.find('img').attr('src'));

                //var addedContent = {};
                //addedContent.ContentTitle = "it is me title ;)";
                //addedContent.ContentURL = "it is you URL ;)";
                //addedContent.ContentID = $scope.contentIndex + 1;
                //$scope.Contents.push(addedContent);
                //$scope.contentIndex = $scope.contentIndex + 1;
                //console.log('added', $scope.Contents);

            },
            removedfile: function (file) {
                var _ref;
                //$scope.hasFile = false;
                //console.log($scope.hasFile);
                console.log(file);
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            }
        };

        //Handle events for dropzone
        //Visit http://www.dropzonejs.com/#events for more events
        $scope.dzCallbacks = {
            'addedfile': function (file) {
                console.log(file);
                //$scope.newFile = file;
            },
            'success': function (file, xhr) {
                console.log('success ;)');
                console.log(file, xhr);
                //redirectBack();
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

    }]);
})(angular.module('gorgiasapp'));