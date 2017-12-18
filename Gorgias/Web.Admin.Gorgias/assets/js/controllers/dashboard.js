'use strict';

/* Controllers */

angular.module('gorgiasapp')
    // Chart controller 
    .controller('DashboardCtrl', ['$scope', '$stateParams', '$http', 'apiService', 'ngAuthSettings', '$location', 'notificationService', '$anchorScroll', 'authService', '$translate',
        function ($scope, $stateParams, $http, apiService, ngAuthSettings, $location, notificationService, $anchorScroll, authService, $translate) {

            var vm = this;

            $scope.language = 'en';
            $scope.languages = ['en', 'zh', 'my'];
            $scope.updateLanguage = function () {
                $translate.use($scope.language);
            };

            $scope.language = $translate.proposedLanguage() || $translate.use()
            $scope.timestamp = new Date().getUTCMilliseconds();

            $scope.object = {};
            $scope.pSize = "250px";
            $scope.checked = true;

            $scope.checkedSlider = false;
            $scope.pSliderSize = "500px";

            console.log("DashboardCtrl wow loaded ;)");
            $scope.baseURL = ngAuthSettings.apiServiceBaseUri;

            $scope.ProfileID = authService.authentication.userID;// $route.current.params.id;


            loadAgencyProfiles();
            //loadProfile();

            //Web/Administration/Agency/

            function loadAgencyProfiles() {
                switch (authService.authentication.userRole) {
                    case "0":
                        apiService.get($scope.baseURL + 'api/Reports/Profiles/Current/' + authService.authentication.countryID + '/0', null,
                        AgencyProfilesLoadCompleted,
                        AgencyProfilesLoadFailed);
                        $scope.editLinkRole = 0;
                        console.log(authService.authentication.userID, 'WOWL Page successful login 0 Country Distributor ;)');
                        break;
                    default:
                        apiService.get($scope.baseURL + 'api/Reports/Profiles/Current/' + authService.authentication.userUserID + '/1', null,
                        AgencyProfilesLoadCompleted,
                        AgencyProfilesLoadFailed);
                        $scope.editLinkRole = 0;
                        console.log(authService.authentication.userID, 'WOWL Page successful login 0 Country Distributor ;) + authService.authentication.userID');
                        break;
                }
            }

            function AgencyProfilesLoadCompleted(response) {                
                $scope.Reports = response.data.Result;
                console.log(response, $scope.Reports, 'AGENCY');
            }

            function AgencyProfilesLoadFailed(response) {
                console.log(response);
            }

            $scope.plabels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
            $scope.pdata = [300, 500, 100];

            $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
            $scope.series = ['Series A', 'Series B'];
            $scope.data = [
              [65, 59, 80, 81, 56, 55, 40],
              [28, 48, 40, 19, 86, 27, 90]
            ];
            $scope.onClick = function (points, evt) {
                console.log(points, evt);
            };
            $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
            $scope.options = {
                scales: {
                    yAxes: [
                      {
                          id: 'y-axis-1',
                          type: 'linear',
                          display: true,
                          position: 'left'
                      },
                      {
                          id: 'y-axis-2',
                          type: 'linear',
                          display: true,
                          position: 'right'
                      }
                    ]
                }
            };


            //$scope.refreshTest = function(portlet) {
            //    console.log("Refreshing...");
            //    // Timeout to simulate AJAX response delay
            //    $timeout(function() {
            //        $(portlet).portlet({
            //            refresh: false
            //        });
            //    }, 2000);

            //}

            //$http.get('assets/js/api/charts.json').success(function(data) {
            //    $scope.widget_4_data = data.nvd3.productRevenue;
            //    $scope.widget_12_data = data.nvd3.line;
            //});

            //$http.get('assets/js/api/min_sales_chart.json').success(function(data) {
            //    $scope.widget_8_data = data.siteVisits;
            //    $scope.widget_7_data = data.premarket;
            //    $scope.widget_16_data = data.siteVisits;

            //});

            //// Widget-4
            //$scope.widget_4_options = {
            //    chart: {
            //        type: 'lineChart',
            //        x: function(d) {
            //            return d[0]
            //        },
            //        y: function(d) {
            //            return d[1] / 100
            //        },
            //        margin: {
            //            top: 60,
            //            right: -10,
            //            bottom: -10,
            //            left: -10
            //        },
            //        color: [
            //            $.Pages.getColor('success')

            //        ],
            //        useInteractiveGuideline: true,
            //        forceY: [0, 2],
            //        showLegend: false,
            //        transitionDuration: 500
            //    }
            //}

            //// Widget-8
            //$scope.widget_8_options = {
            //    chart: {
            //        type: 'lineChart',
            //        x: function(d) {
            //            return d[0]
            //        },
            //        y: function(d) {
            //            return d[1]
            //        },
            //        margin: {
            //            top: 10,
            //            right: -10,
            //            bottom: -13,
            //            left: -10
            //        },
            //        color: [
            //            '#000'

            //        ],
            //        showXAxis: false,
            //        showYAxis: false,
            //        showLegend: false,
            //        useInteractiveGuideline: false
            //    }
            //}

            //// Widget-7
            //$scope.widget_7_options = {
            //    chart: {
            //        type: 'lineChart',
            //        x: function(d) {
            //            return d[0]
            //        },
            //        y: function(d) {
            //            return d[1]
            //        },
            //        margin: {
            //            top: 10,
            //            right: -10,
            //            bottom: 20,
            //            left: -10
            //        },
            //        color: [
            //            '#fff'

            //        ],
            //        showXAxis: false,
            //        showYAxis: false,
            //        showLegend: false,
            //        useInteractiveGuideline: false
            //    }
            //}

            //// Widget-12
            //$scope.widget_12_options = {
            //    chart: {
            //        type: 'lineChart',
            //        x: function(d) {
            //            return d[0]
            //        },
            //        y: function(d) {
            //            return d[1]
            //        },
            //        margin: {
            //            left: 30,
            //            bottom: 35
            //        },
            //        color: [
            //            $.Pages.getColor('success'),
            //            $.Pages.getColor('danger'),
            //            $.Pages.getColor('primary'),
            //            $.Pages.getColor('complete'),

            //        ],
            //        xAxis: {
            //            tickFormat: function(d) {
            //                return d3.time.format('%a')(new Date(d))
            //            }
            //        },
            //        yAxis: {
            //            tickFormat: d3.format('d')
            //        },
            //        showLegend: false,
            //        useInteractiveGuideline: true,
            //    }
            //}

            //// Widget-16
            //$scope.widget_16_options = {
            //    chart: {
            //        type: 'lineChart',
            //        x: function(d) {
            //            return d[0]
            //        },
            //        y: function(d) {
            //            return d[1]
            //        },
            //        margin: {
            //            top: 10,
            //            right: -10,
            //            bottom: 10,
            //            left: -10
            //        },
            //        color: [
            //            '#27cebc'

            //        ],
            //        xAxis: {
            //            tickFormat: function(d) {
            //                return d3.time.format('%a')(new Date(d))
            //            }
            //        },
            //        yAxis: {
            //            tickFormat: d3.format('d')
            //        },
            //        showLegend: false,
            //        showXAxis: false,
            //        showYAxis: false,
            //        useInteractiveGuideline: true,
            //    }
            //}

            //// Widget-5
            //$scope.options1 = {
            //    renderer: 'bar'
            //};

            //$scope.series1 = [{
            //    name: 'Series 1',
            //    data: [{
            //        x: 0,
            //        y: 10
            //    }, {
            //        x: 1,
            //        y: 8
            //    }, {
            //        x: 2,
            //        y: 5
            //    }, {
            //        x: 3,
            //        y: 9
            //    }, {
            //        x: 4,
            //        y: 5
            //    }, {
            //        x: 5,
            //        y: 8
            //    }, {
            //        x: 6,
            //        y: 10
            //    }],
            //    color: $.Pages.getColor('danger')
            //}, {
            //    name: 'Series 2',
            //    data: [{
            //        x: 0,
            //        y: 0
            //    }, {
            //        x: 1,
            //        y: 2
            //    }, {
            //        x: 2,
            //        y: 5
            //    }, {
            //        x: 3,
            //        y: 1
            //    }, {
            //        x: 4,
            //        y: 5
            //    }, {
            //        x: 5,
            //        y: 2
            //    }, {
            //        x: 6,
            //        y: 0
            //    }],
            //    color: $.Pages.getColor('master-light')
            //}];


            //// Widget-14
            //var widget_14_seriesData = [
            //    [],
            //    [],
            //    []
            //];
            //var random = new Rickshaw.Fixtures.RandomData(50);
            //for (var i = 0; i < 50; i++) {
            //    random.addData(widget_14_seriesData);
            //}

            //$scope.widget_14_options = {
            //    renderer: 'area'

            //};

            //$scope.widget_14_series = [{
            //    data: widget_14_seriesData[0],
            //    color: $.Pages.getColor('success-light', .5),
            //    name: 'DB Server'
            //}, {
            //    data: widget_14_seriesData[1],
            //    color: $.Pages.getColor('master-light'),
            //    name: 'Web Server'
            //}];

            //$scope.widget_14_features = {
            //    yAxis: {
            //        tickFormat: function(y) {
            //            return y / 10;
            //        },
            //        orientation: 'right'
            //    }
            //}


            //// Widget-16
            //$scope.widget_16_data = [{
            //    "key": "Site visits",
            //    "values": [
            //        [100, 0],
            //        [150, 8],
            //        [200, 20],
            //        [250, 22],
            //        [300, 30],
            //        [350, 26],
            //        [400, 10]
            //    ]
            //}];

            //$scope.widget_16_xFunction = function() {
            //    return function(d) {
            //        return d[0];
            //    };
            //}
            //$scope.widget_16_yFunction = function() {
            //    return function(d) {
            //        return d[1];
            //    };
            //}
            //$scope.widget_16_colorFunction = function() {
            //    return function(d, i) {
            //        return "#27cebc"
            //    };
            //}

            //var widget_14_seriesData = [
            //    [],
            //    [],
            //    []
            //];
            //var random = new Rickshaw.Fixtures.RandomData(50);
            //for (var i = 0; i < 50; i++) {
            //    random.addData(widget_14_seriesData);
            //}

            //$scope.widget_14_options = {
            //    renderer: 'area'

            //};

            //$scope.widget_14_series = [{
            //    data: widget_14_seriesData[0],
            //    color: $.Pages.getColor('success-light', .5),
            //    name: 'DB Server'
            //}, {
            //    data: widget_14_seriesData[1],
            //    color: $.Pages.getColor('master-light'),
            //    name: 'Web Server'
            //}];

            //$scope.widget_14_features = {
            //    yAxis: {
            //        tickFormat: function(y) {
            //            return y / 10;
            //        },
            //        orientation: 'right'
            //    }
            //}

            //// Widget-15-chart2
            //var widget_15_2_seriesData = [
            //    [],
            //    [],
            //    []
            //];
            //var random = new Rickshaw.Fixtures.RandomData(40);
            //for (var i = 0; i < 40; i++) {
            //    random.addData(widget_15_2_seriesData);
            //}

            //$scope.widget_15_2_options = {
            //    renderer: 'bar'

            //};

            //$scope.widget_15_2_series = [{
            //    data: widget_15_2_seriesData[0],
            //    color: $.Pages.getColor('complete-light'),
            //    name: "New users"
            //}, {
            //    data: widget_15_2_seriesData[1],
            //    color: $.Pages.getColor('master-lighter'),
            //    name: "Returning users"
            //}];

            //$scope.widget_15_2_features = {}



            //// Manually Destroy LiveTile objects
            //$scope.$on('$destroy', function() {
            //    $('.live-tile').liveTile("destroy");
            //});


        }]);



angular.module('gorgiasapp')
    .directive('widget5Chart', function () {
        return {
            restrict: 'C',
            link: function (scope, el, attrs) {

                var container = '.widget-5-chart';

                var seriesData = [
                    [],
                    []
                ];
                var random = new Rickshaw.Fixtures.RandomData(7);
                for (var i = 0; i < 7; i++) {
                    random.addData(seriesData);
                }

                var graph = new Rickshaw.Graph({
                    element: document.querySelector(container),
                    renderer: 'bar',
                    series: [{
                        data: [{
                            x: 0,
                            y: 10
                        }, {
                            x: 1,
                            y: 8
                        }, {
                            x: 2,
                            y: 5
                        }, {
                            x: 3,
                            y: 9
                        }, {
                            x: 4,
                            y: 5
                        }, {
                            x: 5,
                            y: 8
                        }, {
                            x: 6,
                            y: 10
                        }],
                        color: $.Pages.getColor('danger')
                    }, {
                        data: [{
                            x: 0,
                            y: 0
                        }, {
                            x: 1,
                            y: 2
                        }, {
                            x: 2,
                            y: 5
                        }, {
                            x: 3,
                            y: 1
                        }, {
                            x: 4,
                            y: 5
                        }, {
                            x: 5,
                            y: 2
                        }, {
                            x: 6,
                            y: 0
                        }],
                        color: $.Pages.getColor('master-light')
                    }]

                });


                var MonthBarsRenderer = Rickshaw.Class.create(Rickshaw.Graph.Renderer.Bar, {
                    barWidth: function (series) {

                        return 7;
                    }
                });


                graph.setRenderer(MonthBarsRenderer);


                graph.render();


                $(window).resize(function () {
                    graph.configure({
                        width: $(container).width(),
                        height: $(container).height()
                    });

                    graph.render()
                });

                $(container).data('chart', graph);
            }
        };
    });

$('body').on('click', '.mapplic-pin', function (e) {
    e.preventDefault();
    var location = $(e.target).data('location');
    $('#mapplic').data().mapplic.goToLocation(location, 800);
});