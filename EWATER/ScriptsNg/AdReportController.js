var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl']);

app.config(['growlProvider', function (growlProvider, $routeProvider) {
    growlProvider.globalTimeToLive(1500);
    growlProvider.globalPosition('bottom-right');
    growlProvider.globalDisableCountDown(true);
    growlProvider.globalDisableCloseButton(true);
}]);
app.filter('vndFormat', function () {
    return function (input) {
        return input + ' \u20ab';
    };
});
app.controller('AdReportController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.reportModel = {};
    $scope.ListReport = [];
    getallData();

    function getallData() {
        $http.get('/AdReport/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            var ListReportAll = [];
            var OrderIDOld = '';
            for (var i = 0; i < data.data.length; i++) {
                if (data.data[i].OrderID != OrderIDOld) {
                    var ListReportFinal = new Object();
                    ListReportFinal.OrderID = data.data[i].OrderID;
                    ListReportFinal.CustomerName = data.data[i].CustomerName;
                    ListReportFinal.PhoneNumber = data.data[i].PhoneNumber;
                    ListReportFinal.Address = data.data[i].Address;
                    ListReportFinal.StaffName = data.data[i].StaffName;
                    ListReportFinal.ListProduct = [];
                    for (var j = 0; j < data.data.length; j++) {
                        if (ListReportFinal.OrderID == data.data[j].OrderID) {
                            var item = new Object();
                            item.ProductName = data.data[j].ProductName;
                            item.Quantity = data.data[j].Quantity;
                            item.Price = data.data[j].Price;
                            ListReportFinal.ListProduct.push(item);
                        }
                    }
                    ListReportAll.push(ListReportFinal);
                    OrderIDOld = data.data[i].OrderID;
                }               
            }
            $scope.ListReport = ListReportAll;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

