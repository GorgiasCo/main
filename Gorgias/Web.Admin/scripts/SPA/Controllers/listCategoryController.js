(function (app) {
    'use strict';
    app.controller('listCategoryController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings) {

        var vm = this;
        $scope.item = $route.current.params.id;

        console.log("list Category loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};

        $scope.edit = edit;
        $scope.delete = deleteRow;

        vm.message = "hi";
        vm.dtInstance = {};

        //datatables
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server
            DTColumnBuilder.newColumn("CategoryName", "Name").withOption('name', 'CategoryName'),
            DTColumnBuilder.newColumn("CategoryStatus", "Status").withOption('name', 'CategoryStatus'),
            DTColumnBuilder.newColumn("CategoryDescription", "Description").withOption('name', 'CategoryDescription'),
            DTColumnBuilder.newColumn("CategoryParentID", "ParentID").withOption('name', 'CategoryParentID'),
            DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        ]

        $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
            function (sSource, aoData, fnCallback, oSettings) {
                $http({
                    method: 'POST',
                    url: ngAuthSettings.apiServiceBaseUri + 'api/Categories/data',
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

        function edit(item) {
            console.log(item);
            vm.message = 'You are trying to edit the row: ' + JSON.stringify(item);
            // Edit some data and call server to make changes...
            // Then reload the data so that DT is refreshed
            vm.dtInstance.reloadData();
        }
        function deleteRow(item) {
            vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
            // Delete some data and call server to make changes...
            // Then reload the data so that DT is refreshed
            vm.dtInstance.reloadData();
        }
        function createdRow(row, data, dataIndex) {
            // Recompiling so we can bind Angular directive to the DT
            $compile(angular.element(row).contents())($scope);
        }
        function actionsHtml(data, type, full, meta) {
            $scope.Items[data.CategoryID] = data;
            console.log(data);
            return '<button class="btn btn-warning" ng-click="edit(Items[' + data.CategoryID + '])">' +
                '   <i class="fa fa-edit"></i>' +
                '</button>&nbsp;' +
                '<button class="btn btn-danger" ng-click="delete(Items[' + data.CategoryID + '])" )"="">' +
                '   <i class="fa fa-trash-o"></i>' +
                '</button>';
        }
        //end datatables ;)
    }]);
})(angular.module('heroesApp'));