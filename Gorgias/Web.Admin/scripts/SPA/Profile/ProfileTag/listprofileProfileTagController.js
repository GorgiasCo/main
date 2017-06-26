(function (app) {
    'use strict';
    app.controller('listprofileProfileTagController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', 'ngAuthSettings', '$location', 'notificationService', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder, ngAuthSettings, $location, notificationService) {

        var vm = this;        
        $scope.item = $route.current.params.id;        

        console.log("listprofile ProfileTag loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.Items = {};
        $scope.tags = [];
        $scope.insertTag = insertObject;        

        $scope.delete = deleteRow;
        $scope.ProfileID = $route.current.params.id;
        $scope.object = { TagName: "", ProfileTagStatus: false };
        $scope.object.ProfileID = Number($scope.ProfileID);
        $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

        vm.message = "hi";
        $scope.dtInstance = {};

        //datatables
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server
            DTColumnBuilder.newColumn("ProfileTagStatus", "Status").withOption('name', 'ProfileTagStatus').renderWith(viewHtml),                
            DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        ]

        $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
            function (sSource, aoData, fnCallback, oSettings) {
                $http({
                    method: 'POST',
                    url: ngAuthSettings.apiServiceBaseUri + 'api/ProfileTags/ProfileID/' + $scope.ProfileID + '/data',
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

        function updateProfileTagSucceded(result) {
            console.log("Success" + result.data.Result);
            //$scope.dtInstance._renderer.rerender();
            loadProfileTag();
            notificationService.displaySuccess('Deleted.');
        }

        function updateProfileTagFailed(response) {
            console.log("Fail");
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        function deleteRow(item) {
            vm.message = 'You are trying to remove the row: ' + JSON.stringify(item);
            if (confirm('Are you sure to delete?')) {
                apiService.deleteItem($scope.baseURL + 'api/ProfileTag/TagID/ProfileID/' + item.TagID + '/' + item.ProfileID, null,
                updateProfileTagSucceded,
                updateProfileTagFailed);
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
            return '<a class="btn btn-warning" href="/profiletag/edit/' + full.TagID + "/"  + full.ProfileID + '">' +
                '   <i class="fa fa-edit"></i>' +
                '</a>&nbsp;' +
                '<button class="btn btn-danger" ng-click="delete(\'' + full.TagID + "/"  + full.ProfileID + '\')">' +
                '   <i class="fa fa-trash-o"></i>' +
                '</button>';
        }
        function viewHtml(data, type, full, meta) {
            return '<a href="/profiletag/' + full.TagID + "/"  + full.ProfileID + '">'+ data + '</a>';
        }
        //end datatables ;)

        //Insert Tag ;)
        function insertObject() {
            console.log("Insert Tag");
            console.log($scope.object);
            apiService.post($scope.baseURL + 'api/ProfileTag/', $scope.object,
            addProfileTagSucceded,
            addProfileTagFailed);
            console.log("Post!!!" + $scope.object);
        }

        function addProfileTagSucceded(result) {
            console.log("Success");
            console.log(result.data.Result);
            $scope.object.TagName = "";
            loadProfileTag();
        }

        function addProfileTagFailed(response) {
            console.log("Fail");
            $scope.error = response.data.Errors;
            notificationService.displayError(response.statusText);
            notificationService.displayError(response.data.Errors);
        }

        //End of Insert Tag
        //Get all Tags
        function loadProfileTag() {
            apiService.get($scope.baseURL + 'api/ProfileTags/ProfileID/' + $scope.ProfileID + '/1/30/', null,
            ProfileTagLoadCompleted,
            ProfileTagLoadFailed);
        }

        function ProfileTagLoadCompleted(response) {
            console.log("Tags Me Loads ;)");
            console.log(response.data.Result);
            $scope.tags = response.data.Result.Items;
            notificationService.displaySuccess("ProfileTag loaded");
        }

        function ProfileTagLoadFailed(response) {
            notificationService.displayError(response.data.Errors);
        }
        //End of all Tags
        loadProfileTag();
    }]);
})(angular.module('heroesApp'));     