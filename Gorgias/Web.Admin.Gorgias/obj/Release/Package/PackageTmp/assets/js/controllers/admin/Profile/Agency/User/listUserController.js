﻿(function (app) {
    'use strict';
    app.controller('listUserController', ['$scope', '$stateParams', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', '$filter', 'authService',
        function ($scope, $stateParams, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService, $filter, authService) {

            var vm = this;
            $scope.item = $stateParams.id;//$route.current.params.id;

            console.log("list User loaded ;)");

            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.pagesize = 2;
            $scope.Items = {};

            $scope.delete = deleteRow;

            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;
            $scope.CountryID = authService.authentication.countryID;

            vm.message = "hi";
            vm.dtInstance = {};

            checkValidity();
            //|| $scope.ProfileID != authService.authentication.userID
            function checkValidity() {
                if (authService.authentication.userID == 0 || authService.authentication.userRole != 0) {
                    $location.url('/access/login');
                }
            }

            //datatables
            $scope.dtColumns = [
                //here We will add .withOption('name','column_name') for send column name to the server
                DTColumnBuilder.newColumn("UserFullname", $filter('translate')('Fullname')).withOption('name', 'UserFullname').renderWith(viewHtml),
                DTColumnBuilder.newColumn("UserEmail", $filter('translate')('Email')).withOption('name', 'UserEmail'),
                DTColumnBuilder.newColumn("UserStatus", $filter('translate')('Status')).withOption('name', 'UserStatus'),
                DTColumnBuilder.newColumn("UserIsBlocked", $filter('translate')('IsBlocked')).withOption('name', 'UserIsBlocked'),
                DTColumnBuilder.newColumn("UserDateCreated", $filter('translate')('Register')).withOption('name', 'UserDateCreated').renderWith(viewHtmlDate),
                DTColumnBuilder.newColumn("UserDateConfirmed", $filter('translate')('Confirmed')).withOption('name', 'UserDateConfirmed').renderWith(viewHtmlDate),
                DTColumnBuilder.newColumn(null).withTitle($filter('translate')('Actions')).notSortable().renderWith(actionsHtml)
            ]

            $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
                function (sSource, aoData, fnCallback, oSettings) {
                    $http({
                        method: 'POST',
                        url: ngAuthSettings.apiServiceBaseUri + 'api/Users/Country/'+ $scope.CountryID +'/data',
                        data: {
                            start: aoData[3].value,
                            length: aoData[4].value,
                            draw: aoData[0].value,
                            order: aoData[2].value,
                            search: aoData[5].value,
                            columns: aoData[1].value
                        },
                        headers: {
                            'Content-type': 'application/json'
                        }
                    })
                    .then(function (result) {
                        console.log('success');
                        console.log(result)
                        $scope.Items = result.data.Result.data;
                        var records = {
                            draw: result.data.Result.draw,
                            recordsTotal: result.data.Result.recordsTotal,
                            recordsFiltered: result.data.Result.recordsFiltered,
                            data: result.data.Result.data
                        };
                        fnCallback(records);
                    });
                }
            )
            .withOption('processing', true) //for show progress bar
            .withOption('serverSide', true) // for server side processing
            .withPaginationType('full_numbers') // for get full pagination options // first / last / prev / next and page numbers
            .withDisplayLength(10) // Page size
            .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
            .withOption('createdRow', createdRow);

            function updateUserSucceded(result) {
                console.log("Success" + result.data.Result);
                vm.dtInstance._renderer.rerender();
                notificationService.displaySuccess('Deleted.');
            }

            function updateUserFailed(response) {
                console.log("Fail");
                notificationService.displayError(response.statusText);
                notificationService.displayError(response.data.Errors);
            }

            function deleteRow(item) {
                vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
                if (confirm($filter('translate')('DeleteNote'))) {
                    apiService.deleteItem($scope.baseURL + 'api/User/UserID/' + item, null,
                    updateUserSucceded,
                    updateUserFailed);
                    console.log('Deleted');
                } else {
                    console.log('Cant ;)');
                }
                //vm.dtInstance.reloadData();
            }
            function createdRow(row, data, dataIndex) {
                // Recompiling so we can bind Angular directive to the DT
                $compile(angular.element(row).contents())($scope);
            }
            function actionsHtml(data, type, full, meta) {
                console.log(data);
                return '<a class="btn btn-warning" href="/app/forms/user/' + full.UserID + '">' +
                    '   <i class="fa fa-edit"></i>' +
                    '</a>&nbsp;' +
                    '<button class="btn btn-danger" ng-click="delete(\'' + full.UserID + '\')">' +
                    '   <i class="fa fa-trash-o"></i>' +
                    '</button>';
            }
            function viewHtml(data, type, full, meta) {
                return '<a href="/app/forms/user/' + full.UserID + '">' + data + '</a>';
            }

            var myDateFormat = {
                angularjs: 'dd/MM/yyyy',        // My date format in angularjs way
                momentjs: 'DD/MM/YYYY',         // The same date format in momentjs way
            }

            function viewHtmlDate(data, type, full, meta) {
                return $filter('date')(data, myDateFormat.angularjs);
            }
            //end datatables ;)
        }]);
})(angular.module('gorgiasapp'));