/* ============================================================
 * File: app.js
 * Configure global module dependencies. Page specific modules
 * will be loaded on demand using ocLazyLoad
 * ============================================================ */

'use strict';

angular.module('gorgiasapp', [
    'ui.router',
    'ui.utils',
    'oc.lazyLoad',
    'LocalStorageModule', 'ngCookies', 'pascalprecht.translate', 'angularModalService', 'angularValidator', 'ngMap', 'chart.js',
    'angular-uuid',
    'angular-preload-image',
    'ui.bootstrap',
    'datatables'
]);
 