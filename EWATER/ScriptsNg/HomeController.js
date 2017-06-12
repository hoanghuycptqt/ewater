var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl']);

app.config(['growlProvider', function (growlProvider, $routeProvider) {
    growlProvider.globalTimeToLive(1500);
    growlProvider.globalPosition('bottom-right');
    growlProvider.globalDisableCountDown(true);
    growlProvider.globalDisableCloseButton(true);
}]);

app.controller('HomeController', function ($scope, $http, $location, $window, $uibModal, growl) {
    

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

