var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl', 'daterangepicker']);

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

app.filter('sortDate', function () {
    return function (items,date) {
        var arrayToReturn = [];
        for (var i = 0; i < items.length; i++) {
            var dateCurent = items[i].OrderDate;
            if (dateCurent > date.startDate && dateCurent < date.endDate) {
                arrayToReturn.push(items[i]);
            }
        }
        return arrayToReturn;
    };
});
app.controller('AdReportController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.reportModel = {};
    $scope.ListReport = [];
    getallData();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 50;
    $scope.date = {
        startDate: moment().subtract(29, "days"),
        endDate: moment()
    };

    $scope.opts = {
        locale: {
            applyClass: 'btn-green',
            applyLabel: "Apply",
            fromLabel: "From",
            toLabel: "To",
            cancelLabel: 'Cancel',
            customRangeLabel: 'Custom Range'          
        },
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    };

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
                    ListReportFinal.OrderDate = new Date(parseInt(data.data[i].OrderDate.slice(6, 19)));
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

