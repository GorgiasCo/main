
(function (app) {
    'use strict';

    app.directive('scrollHeader', scrollHeader);

    function scrollHeader($window) {
        return {
            restrict: 'A',
            scope: {
                tolerance: '='
            },
            link: function ($scope, element, attrs) {

                var prevScroll = {
                    val: null,
                    direction: null
                },
                  directionChangedVal,
                  resizeListener,
                  scrollListener,
                  isListeningWindowEvents;

                resizeListener = function () {
                    prevScroll.val = $window.scrollY;
                };

                scrollListener = function () {

                    var scroll = {
                        val: $window.scrollY,
                        direction: $window.scrollY > prevScroll.val ? 'down' : 'up'
                    },
                      headerHeight = 75;

                    if (isOutOfDocumentBounds(scroll.val)) {
                        return;
                    }

                    if (scroll.direction === 'up') {
                        if (!inUpTolerance(scroll.val)) {
                            clearTolerance();
                            showHeader();
                        }

                    } else if (scroll.direction === 'down') {
                        if (!inDownTolerance(scroll.val)) {
                            clearTolerance();
                            hideHeader(headerHeight);
                        }
                    }

                    prevScroll = {
                        val: scroll.val,
                        direction: scroll.direction
                    };
                };

                initListenWindowEvents();

                function showHeader() {
                    element.css('transform', 'translateY(0)');
                }

                function hideHeader(headerHeight) {
                    element.css('transform', 'translateY(' + (-1 * headerHeight) + 'px)');
                }

                function initListenWindowEvents() {
                    if (!isListeningWindowEvents) {
                        isListeningWindowEvents = true;
                        angular.element($window).on('resize', resizeListener);
                        angular.element($window).on('scroll', scrollListener);
                    }
                    initHeader();
                }

                function destroyListenWindowEvents() {
                    isListeningWindowEvents = false;
                    angular.element($window).off('resize', resizeListener);
                    angular.element($window).off('scroll', scrollListener);
                }

                function initHeader() {
                    prevScroll = {
                        val: 0,
                        direction: null
                    };
                    directionChangedVal = null;
                    showHeader();
                }

                function inUpTolerance(scrollVal) {

                    if (prevScroll.direction === 'down') {
                        directionChangedVal = scrollVal;
                    }

                    return scrollVal > 0 &&
                      ((directionChangedVal - scrollVal) <= $scope.tolerance.up);
                }

                function inDownTolerance(scrollVal) {

                    if (prevScroll.direction === 'up') {
                        directionChangedVal = scrollVal;
                    }

                    return ($window.innerHeight + $window.scrollY < getDocumentHeight()) &&
                      ((scrollVal - directionChangedVal) <= $scope.tolerance.down);
                }

                function clearTolerance() {
                    directionChangedVal = null;
                }

                function getDocumentHeight() {
                    return Math.max(
                      document.body.scrollHeight, document.documentElement.scrollHeight,
                      document.body.offsetHeight, document.documentElement.offsetHeight,
                      document.body.clientHeight, document.documentElement.clientHeight
                    );
                }

                function isOutOfDocumentBounds(scrollVal) {
                    return scrollVal < 0 || ($window.innerHeight + $window.scrollY > getDocumentHeight());
                }
            }
        };
    }
})(angular.module('gorgiasapp'));