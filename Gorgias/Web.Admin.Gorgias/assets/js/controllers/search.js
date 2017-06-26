'use strict';

/* Controllers */

angular.module('gorgiasapp')

.controller('SearchCtrl', ['$scope', function($scope) {
    $scope.liveSearch = function() {
        console.log("Live search for: " + $scope.search.query);
    }


}]);