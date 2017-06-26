/* ============================================================
 * Directive: csSelect
 * AngularJS directive for SelectFx jQuery plugin
 * https://github.com/codrops/SelectInspiration
 * ============================================================ */


angular.module('gorgiasapp')
    .directive('csSelect', function ($compile) {
        return {
            restrict: 'A',            
            link: function (scope, element, attrs) {
                if (!window.SelectFx) return;

                var newElement = angular.element('<div class="cs-wrapper"></div>');
                element.wrap($compile(newElement)(scope));
                new SelectFx(element[0]);
            }
        };
    });