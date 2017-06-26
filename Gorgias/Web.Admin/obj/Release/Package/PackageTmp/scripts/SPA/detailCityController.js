(function (app) {
    'use strict';
    app.controller('detailCityController', ['$scope', '$route', '$http', '$compile', 'apiService', 'DTOptionsBuilder', 'DTColumnBuilder', function ($scope, $route, $http, $compile, apiService, DTOptionsBuilder, DTColumnBuilder) {

        var vm = this;
        $scope.Heroes = "SALAM";
        $scope.genre = { Name: "Yasser Nasser", ID: 0 };
        $scope.error = "hi salam hallo";
        $scope.account = [];

        console.log("detail loaded ;)");

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pagesize = 2;
        $scope.items = {};

        $scope.search = search;
        $scope.edit = edit;
        $scope.delete = deleteRow;

        vm.message = "hi";

        function search(page) {
            page = page || 0;

            $scope.loadingMovies = true;

            var config = {
                params: {
                    page: page,
                    pageSize: $scope.pagesize = 2
                }
            };

            apiService.get('http://localhost:43587/api/cities/' + page + "/" + $scope.pagesize, null,
            addMovieSucceded,
            addMovieFailed);
            console.log("paging !!!" + page);
        }

        var currentID = $route.current.params.id;


        //apiService.get('/api/cities/1/10', config, addMovieSucceded, addMovieFailed);
        //console.log("add triggered !!!");

        //apiService.get('/api/countries/1/10', config, addMovieSucceded, addMovieFailed);
        //console.log("add triggered !!!");

        //apiService.get('/api/genres/GetCountry/tune/1/2', config, addMovieSucceded, addMovieFailed);
        //console.log("add triggered !!!");

        //apiService.get('/api/genres/GetCountry/future/1/2', config, addMovieSucceded, addMovieFailed);
        //console.log("add triggered !!!");

        //if (typeof currentID == 'undefined') {
        //    apiService.getWithPromise('api/genres/GetValues/777').then(
        //                function (result) {
        //                    // promise was fullfilled (regardless of outcome)
        //                    // checks for information will be peformed here
        //                    $scope.account = result;
        //                    console.log(result);
        //                },
        //                function (error) {
        //                    // handle errors here
        //                    console.log(error.statusText);
        //                }
        //            );
        //    console.log($route.current.params.id)
        //} else {
        //var url = 'api/city/' + $route.current.params.id;
        var url = 'api/cities/1/2';
        vm.dtInstance = {};

        //datatables
        $scope.dtColumns = [
            //here We will add .withOption('name','column_name') for send column name to the server 
            DTColumnBuilder.newColumn("CityID", "ID").withOption('name', 'CityID'),
            DTColumnBuilder.newColumn("CityName", "Name").withOption('name', 'CityName'),
            DTColumnBuilder.newColumn("CityStatus", "Status").withOption('name', 'CityStatus'),
            DTColumnBuilder.newColumn("CountryID", "CountryID").withOption('name', 'CountryID'),
            DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable().renderWith(actionsHtml)
        ]

        //$scope.dtOptions = DTOptionsBuilder.newOptions().withOption('ajax', {
        //    dataSrc: function (json) {
        //        console.log('SUCCESS' + json)
        //        return json.Result.data
        //    },
        //    url: "/api/cities/data",
        //    type: "POST"
        //})

        $scope.dtOptions = DTOptionsBuilder.newOptions().withFnServerData(
            function (sSource, aoData, fnCallback, oSettings) {
                $http({
                    method: 'POST',
                    url: 'http://localhost:43587/api/cities/data',
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
        .withDisplayLength(2) // Page size
        .withOption('aaSorting', [0, 'asc']) // for default sorting column // here 0 means first column
        .withOption('createdRow', createdRow);

        //.withOption('ajax', function (data, callback, settings) {
        //    // make an ajax request using data.start and data.length
        //    console.log('success');
        //    console.log(data)
        //    $http.post('/api/cities/data', data).success(function (res) {
        //        // map your server's response to the DataTables format and pass it to
        //        // DataTables' callback
        //        console.log('success');
        //        console.log(res)
        //        callback({
        //            draw: res.Result.draw,
        //            recordsTotal: res.Result.recordsTotal,
        //            recordsFiltered: res.Result.recordsFiltered,
        //            data: res.Result.data
        //        });
        //    });
        //})
        function edit(person) {
            console.log(person);
            vm.message = 'You are trying to edit the row: ' + JSON.stringify(person);
            // Edit some data and call server to make changes...
            // Then reload the data so that DT is refreshed
            vm.dtInstance.reloadData();
        }
        function deleteRow(person) {
            if (confirm("sure to delete")) {
                console.log(person);
                vm.message = 'You are trying to remove the row: ' + JSON.stringify(person);
                // Delete some data and call server to make changes...
                // Then reload the data so that DT is refreshed
                vm.dtInstance.reloadData();
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



        //$scope.search(1);
        //apiService.get(url,config,
        //    addMovieSucceded,
        //    addMovieFailed);
        //console.log("add triggered !!!");

        function addMovieSucceded(result) {
            //notificationService.displaySuccess($scope.genre.Name + ' has been submitted to Home Cinema');
            console.log("add triggered 3>" + result.data.Result);
            $scope.account = result.data.Result.Items;
            //$scope.items = result.data.Result.Items;                

            $scope.items = result.data;

            $scope.page = result.data.Result.Page;
            $scope.pagesCount = result.data.Result.TotalPages;
            $scope.totalCount = result.data.Result.TotalCount;
            //$scope.movie = response.data;
            //if (movieImage) {
            //    fileUploadService.uploadImage(movieImage, $scope.movie.ID, redirectToEdit);
            //}
            //else
            //    redirectToEdit();
        }

        function addMovieFailed(response) {
            console.log("add triggered !!! Fail");
            //console.log(response);
            $scope.error = response.statusText;
            //notificationService.displayError(response.statusText);
        }

        //    apiService.getWithPromise(url).then(
        //    function (result) {
        //        // promise was fullfilled (regardless of outcome)
        //        // checks for information will be peformed here
        //        $scope.account = result;
        //        console.log(result);
        //    },
        //    function (error) {
        //        // handle errors here
        //        console.log(error.statusText);
        //    }
        //);
        //console.log('Second ;)' + $route.current.params.id + '**** ' + url )
        //}

    }]);
})(angular.module('heroesApp'));