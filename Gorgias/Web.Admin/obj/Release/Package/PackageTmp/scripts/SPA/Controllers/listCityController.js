﻿(function (app) {
    'use strict';
    app.controller('listCityController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.item = $route.current.params.id;

        console.log("list City loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        $scope.edit = edit;
        $scope.delete = deleteRow;

        $scope.dtInstance = {};

        vm.message = "hi";
        vm.dtInstance = {};

        //datatables
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server
            DTColumnBuilder.newColumn("CityName", "Name").withOption('name', 'CityName'),
            DTColumnBuilder.newColumn("CityStatus", "Status").withOption('name', 'CityStatus'),
            DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        ]

        $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
            function (sSource, aoData, fnCallback, oSettings) {
                $http({
                    method: 'POST',
                    url: ngAuthSettings.apiServiceBaseUri + 'api/Cities/data',
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

        function updateCitySucceded(result) {
            console.log("Success" + result.data.Result);
            $scope.dtInstance._renderer.rerender();
            notificationService.displaySuccess('Deleted.');
        }

        function updateCityFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function edit(item) {
            console.log(item);
            vm.message = 'You are trying to edit the row: ' + JSON.stringify(item);
            $location.url('city/edit/' + item.CityID);
            // Edit some data and call server to make changes...
            // Then reload the data so that DT is refreshed
            $scope.dtInstance.reloadData();
        }
        function deleteRow(item) {
            if (confirm()) {
                apiService.deleteItem($scope.baseURL + 'api/City/CityID/' + item.CityID, null,
                updateCitySucceded,
                updateCityFailed);
                console.log('Deleted');
            } else {
                console.log('Cant ;)');
            }
        }
        function createdRow(row, data, dataIndex) {
            // Recompiling so we can bind Angular directive to the DT
            $compile(angular.element(row).contents())($scope);
        }
        function actionsHtml(data, type, full, meta) {
            $scope.Items[data.CityID] = data;
            console.log(data);
            return '<button class="btn btn-warning" ng-click="edit(Items[' + data.CityID + '])">' +
                '   <i class="fa fa-edit"></i>' +
                '</button>&nbsp;' +
                '<button class="btn btn-danger" ng-click="delete(Items[' + data.CityID + '])" )"="">' +
                '   <i class="fa fa-trash-o"></i>' +
                '</button>';
        }
        //end datatables ;)
    }]);
})(angular.module('heroesApp'));