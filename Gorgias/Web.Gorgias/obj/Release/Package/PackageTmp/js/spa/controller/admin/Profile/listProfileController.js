(function (app) {
    'use strict';
    app.controller('listProfileController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        var vm = this;
        $scope.item = $route.current.params.id;

        console.log("list Profile loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};

        $scope.delete = deleteRow;
        $scope.updateStatus = updateStatus;

        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        vm.message = "hi";
        vm.dtInstance = {};

        //datatables
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server
            //DTColumnBuilder.newColumn("ProfileEmail", "Email").withOption('name', 'ProfileEmail'),
            DTColumnBuilder.newColumn("ProfileFullname", "Fullname").withOption('name', 'ProfileFullname').renderWith(viewHtml),
            DTColumnBuilder.newColumn("ProfileIsPeople", "IsPeople").withOption('name', 'ProfileIsPeople').renderWith(viewHtmlUpdateStatus),
            DTColumnBuilder.newColumn("ProfileIsDeleted", "IsDeleted").withOption('name', 'ProfileIsDeleted').renderWith(viewHtmlUpdateIsDeleted),
            //DTColumnBuilder.newColumn("ProfileDateCreated", "DateCreated").withOption('name', 'ProfileDateCreated'),
            //DTColumnBuilder.newColumn("ProfileView", "View").withOption('name', 'ProfileView'),
            //DTColumnBuilder.newColumn("ProfileLike", "Like").withOption('name', 'ProfileLike'),
            DTColumnBuilder.newColumn("ProfileStatus", "Status").withOption('name', 'ProfileStatus').renderWith(viewHtmlUpdateProfileStatus),
            DTColumnBuilder.newColumn("ProfileIsConfirmed", "IsConfirmed").withOption('name', 'ProfileIsConfirmed').renderWith(viewHtmlUpdatePeopleStatus),
            DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        ]

        $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
            function (sSource, aoData, fnCallback, oSettings) {
                $http({
                    method: 'POST',
                    url: ngAuthSettings.apiServiceBaseUri + 'api/Profiles/data',
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

        function updateProfileSucceded(result) {
            console.log("Success" + result.data.Result);
            vm.dtInstance._renderer.rerender();
            notificationService.displaySuccess('Deleted.');
        }

        function updateProfileFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function updateStatus(item, status, mode) {
            console.log('Item->' + item + '---->' + status);

            $scope.objectUpdate = { UpdateMode: mode, Status: status }

            apiService.post($scope.baseURL + 'api/Profile/Confirm/ProfileID/' + item, $scope.objectUpdate,
                updateProfileSucceded,
                updateProfileFailed);
        }

        function deleteRow(item) {
            vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/Profile/ProfileID/' + item, null,
                updateProfileSucceded,
                updateProfileFailed);
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
            return '<a class="btn btn-warning" href="/profile/edit/' + full.ProfileID + '">' +
                '   <i class="fa fa-edit"></i>' +
                '</a>&nbsp;' +
                '<button class="btn btn-danger" ng-click="delete(\'' + full.ProfileID + '\')">' +
                '   <i class="fa fa-trash-o"></i>' +
                '</button>';
        }

        function viewHtmlUpdateStatus(data, type, full, meta) {
            if (full.ProfileIsPeople == true) {
                return '<button class="btn btn-danger" ng-click="updateStatus(' + full.ProfileID + ',false,2)">Deactive</button>';
            } else {
                return '<button class="btn btn-success" ng-click="updateStatus(' + full.ProfileID + ',true,2)">Active</button>';
            }
        }

        function viewHtmlUpdatePeopleStatus(data, type, full, meta) {
            if (full.ProfileIsConfirmed == true) {
                return '<button class="btn btn-danger" ng-click="updateStatus(' + full.ProfileID + ',false,1)">Deactive</button>';
            } else {
                return '<button class="btn btn-success" ng-click="updateStatus(' + full.ProfileID + ',true,1)">Active</button>';
            }
        }

        function viewHtmlUpdateProfileStatus(data, type, full, meta) {
            if (full.ProfileStatus == true) {
                return '<button class="btn btn-danger" ng-click="updateStatus(' + full.ProfileID + ',false,3)">Deactive</button>';
            } else {
                return '<button class="btn btn-success" ng-click="updateStatus(' + full.ProfileID + ',true,3)">Active</button>';
            }
        }

        function viewHtmlUpdateIsDeleted(data, type, full, meta) {
            if (full.ProfileIsDeleted == true) {
                return '<button class="btn btn-success" ng-click="updateStatus(' + full.ProfileID + ',false,4)">Restore</button>';
            } else {
                return '<button class="btn btn-danger" ng-click="updateStatus(' + full.ProfileID + ',true,4)">Delete</button>';
            }
        }        

        function viewHtml(data, type, full, meta) {
            return '<a href="/profile/profile/' + full.ProfileID + '">' + data + '</a>';
        }
        //end datatables ;)
    }]);
})(angular.module('heroesApp'));