var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl']);

app.config(['growlProvider', function (growlProvider, $routeProvider) {
    growlProvider.globalTimeToLive(1500);
    growlProvider.globalPosition('bottom-right');
    growlProvider.globalDisableCountDown(true);
    growlProvider.globalDisableCloseButton(true);
}]);

app.controller('AdContactController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.ListContact = [];
    getallData();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    function getallData() {
        $http.get('/AdContact/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListContact = data.data;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});
