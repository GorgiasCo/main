(function (app) {
    'use strict';

    app.directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return parseInt(val, 10);
                });
                ngModel.$formatters.push(function (val) {
                    return '' + val;
                });
            }
        };
    });

    app.directive('sideBarAdmin', sideBarAdmin);

    function sideBarAdmin($timeout) {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/layout/directive/sideBarAdmin.html',
            link: function (scope, element, attr) {
                $timeout(function () {
                    (function ($) {

                        'use strict';
                        //alert("hi455");

                        var $items = $('.nav-main li.nav-parent');

                        function expand($li) {
                            $li.children('ul.nav-children').slideDown('fast', function () {
                                $li.addClass('nav-expanded');
                                $(this).css('display', '');
                                ensureVisible($li);
                            });
                        }

                        function collapse($li) {
                            $li.children('ul.nav-children').slideUp('fast', function () {
                                $(this).css('display', '');
                                $li.removeClass('nav-expanded');
                            });
                        }

                        function ensureVisible($li) {
                            var scroller = $li.offsetParent();
                            if (!scroller.get(0)) {
                                return false;
                            }

                            var top = $li.position().top;
                            if (top < 0) {
                                scroller.animate({
                                    scrollTop: scroller.scrollTop() + top
                                }, 'fast');
                            }
                        }

                        $items.find('> a').on('click', function (ev) {

                            var $anchor = $(this),
                                $prev = $anchor.closest('ul.nav').find('> li.nav-expanded'),
                                $next = $anchor.closest('li');

                            if ($anchor.prop('href')) {
                                var arrowWidth = parseInt(window.getComputedStyle($anchor.get(0), ':after').width, 10) || 0;
                                if (ev.offsetX > $anchor.get(0).offsetWidth - arrowWidth) {
                                    ev.preventDefault();
                                }
                            }

                            if ($prev.get(0) !== $next.get(0)) {
                                collapse($prev);
                                expand($next);
                            } else {
                                collapse($prev);
                            }
                        });


                    }).apply(this, [jQuery]);

                });
            }
        }
    }

})(angular.module('heroesApp'));