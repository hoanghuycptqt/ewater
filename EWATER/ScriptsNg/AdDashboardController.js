var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl', 'chart.js']);

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
app.controller('AdDashboardController', function ($scope, $http, $location, $window, $uibModal, growl) {
   
    $scope.isDisabled = true;
    $scope.isDisabledEdit = false;
    $scope.orderModel = {};
    $scope.productID = null;
    $scope.orderModelAdd = {};
    $scope.ListOrder = null;
    $scope.ListStaff = null;
    $scope.ListProduct = null;
    $scope.Status = [{ value: true, name: 'Processed' }, { value: false, name: 'Pending' }];
    getallData();
    getallStaff();
    getallProduct();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;

    $scope.FuncDisable = function () {
        $scope.isDisabled = false;
        $scope.isDisabledEdit = true;
    }
    //******=========Get All Customer=========******  
    function getallData() {
        $http.get('/AdDashboard/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListOrder = data.data;

        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

    function getallStaff() {
        $http.get('/AdDashboard/GetAllStaff').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListStaff = data.data;

        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

    function getallProduct(ListProduct) {
        $http.get('/AdDashboard/GetAllProduct').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListProduct = data.data;

        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

    $scope.updateOrder = function(orderModel) {

        $http(
        {
            method: 'POST',
            url: '/AdDashboard/UpdateOrder',
            data: orderModel
        }).then(successCallback, errorCallback);

        function successCallback(data) {
            if (data.data.success === true) {
                $scope.orderModel = null;
                getallData();
                $scope.isDisabled = true;
                $scope.isDisabledEdit = false;
                growl.success(data.data.message);
            }
            else {
                growl.error(data.data.message);
            }
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

    function addOrder(orderNew) {
        var orderModelAdd = orderNew.orderModelAdd;
        var OrderItems = orderNew.OrderItems;
        var CheckSelected = orderNew.CheckSelected;
        var phoneCurrent = orderModelAdd.ExitsPhone;
        var customerID = null;
        var date = new Date().YYYYMMDDHHMMSS();
        var orderID = date;
        var status = false;
        var orderDate = new Date();
        var dataOrder = new Object();
        if (CheckSelected == "2") {
            $http(
           {
               method: 'POST',
               url: '/AdCustomer/Insert',
               async: false,
               data: orderModelAdd
           }).then(successCallback, errorCallback);

            function successCallback(data) {
                phoneCurrent = orderModelAdd.PhoneNumber;

                $http.get('/AdCustomer/GetCustomerByPhone/' + phoneCurrent).then(successCallback, errorCallback);

                function successCallback(data) {
                    customerID = data.data.CustomerID;
                    

                    if (customerID == null) {
                        alert("Hello! I am an alert box!!");
                        return;
                    }
                    dataOrder.OrderID = orderID;
                    dataOrder.CustomerID = customerID;
                    dataOrder.OrderDate = orderDate;
                    dataOrder.Status = status;
                    dataOrder.OrderItems = [];
                    var productItems = OrderItems;
                    for (var i = 0; i < productItems.length; i++) {
                        var item = new Object();
                        item.OrderID = orderID;
                        item.ProductID = productItems[i].id;
                        item.Price = productItems[i].price;
                        item.Quantity = productItems[i].quantity;
                        dataOrder.OrderItems.push(item);
                    }
                    $http(
                   {
                       method: 'POST',
                       url: '/ClientOrder/CreateOrder',
                       async: false,
                       data: dataOrder
                   }).then(successCallback, errorCallback);

                    function successCallback(data) {
                        if (data.data.success === true) {
                            getallData();
                            growl.success(data.data.message);
                        }
                        else {
                            growl.error(data.data.message);
                        }
                    }
                    function errorCallback(data) {
                        growl.error("Có lỗi trong quá trình gọi xử lý đến server");
                    }

                }
                function errorCallback(data) {
                    growl.error("Có lỗi trong quá trình gọi xử lý đến server");
                }
            }
            function errorCallback(data) {
                growl.error("Có lỗi trong quá trình gọi xử lý đến server");
            }

        }

        if (CheckSelected == "1") {
            $http.get('/AdCustomer/GetCustomerByPhone/' + phoneCurrent).then(successCallback, errorCallback);

            function successCallback(data) {
                customerID = data.data.CustomerID;

                if (customerID == null) {
                    alert("Your phonenumber not exits in my system");
                    return;
                }
                dataOrder.OrderID = orderID;
                dataOrder.CustomerID = customerID;
                dataOrder.OrderDate = orderDate;
                dataOrder.Status = status;
                dataOrder.OrderItems = [];
                var productItems = OrderItems;
                for (var i = 0; i < productItems.length; i++) {
                    var item = new Object();
                    item.OrderID = orderID;
                    item.ProductID = productItems[i].id;
                    item.Price = productItems[i].price;
                    item.Quantity = productItems[i].quantity;
                    dataOrder.OrderItems.push(item);
                }
                $http(
               {
                   method: 'POST',
                   url: '/ClientOrder/CreateOrder',
                   async: false,
                   data: dataOrder
               }).then(successCallback, errorCallback);

                function successCallback(data) {
                    if (data.data.success === true) {
                        getallData();
                        growl.success(data.data.message);
                    }
                    else {
                        growl.error(data.data.message);
                    }
                }
                function errorCallback(data) {
                    growl.error("Có lỗi trong quá trình gọi xử lý đến server");
                }

            }
            function errorCallback(data) {
                growl.error("Có lỗi trong quá trình gọi xử lý đến server");
            }
        }

    };

    $scope.addOrder = function (ListProduct, ListStaff) {
        
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'AddOrder',
            resolve: {
                oldCust: function () {
                    return $scope.oldCust = ListProduct;
                },
                oldCust1: function () {
                    return $scope.oldCust1 = ListStaff;
                }
            }
        });

        modalInstance.result.then(function (orderNew) {
            addOrder(orderNew);
        }, function () {
            //error
        });
    };

    $scope.deleteOrder = function (orderModel) {

        var modalInstance = $uibModal.open({
            templateUrl: 'myModalDelete.html',
            controller: 'DeleteOrder'
        });

        modalInstance.result.then(function () {
            var id = orderModel.OrderID.toString();
            $http.delete('/ClientOrder/Delete/' + id).then(successCallback, errorCallback);

            function successCallback(data) {
                $scope.errors = [];
                if (data.data.success === true) {
                    getallData();
                    growl.success(data.data.message);
                }
                else {
                    growl.error(data.data.message);
                }
            }
            function errorCallback(data) {
                growl.error("Có lỗi trong quá trình gọi xử lý đến server");
            }
        }, function () {
            //error
        });

    };
    $scope.labels = [];
    $scope.data = [];
    getYear();
    $scope.chartYear = function () {
        getYear();      
    }
    function getYear() {
        var year = new Date().getFullYear();
        $scope.labels = [];
        $scope.data = [];
        $http.get('/AdDashboard/GetYearChart/' + year).then(successCallback, errorCallback);

        function successCallback(data) {
            var listMonth = data.data;
            var dataMonth = [];
            for (var i = 0; i < 12; i++) {
                $scope.labels.push(listMonth[i].Month);
                dataMonth.push(listMonth[i].Sum);
            }
            $scope.data.push(dataMonth);
            $scope.options = {
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Year: ' + year
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'VNĐ'
                        }
                    }]
                }
            };
            $scope.datasetOverride = [{
                label: "Sales",
                backgroundColor: window.chartColors.blue,
                borderColor: window.chartColors.red,
                fill: false,
            }];
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    }

    $scope.chartMonth = function () {
        var d = new Date();
        var yearmonth = d.getFullYear().toString() + (d.getMonth() + 1).toString();
        $scope.labels = [];
        $scope.data = [];
        $http.get('/AdDashboard/GetMonthChart/' + yearmonth).then(successCallback, errorCallback);

        function successCallback(data) {
            var listDays = data.data;
            var dataMonth = [];
            for (var i = 0; i < listDays.length; i++) {
                $scope.labels.push(listDays[i].Days);
                dataMonth.push(listDays[i].Sum);
            }
            $scope.data.push(dataMonth);
            $scope.options = {
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month: ' + (d.getMonth() + 1)
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'VNĐ'
                        }
                    }]
                }
            };
            $scope.datasetOverride = [{
                label: "Sales",
                backgroundColor: window.chartColors.blue,
                borderColor: window.chartColors.red,
                fill: false,
            }];
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    }
    
   
    

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.controller('AddOrder', function ($scope, $uibModalInstance, oldCust, oldCust1) {
    $scope.action = "Thêm";
    $scope.ListProduct = oldCust;
    $scope.ListStaff = oldCust1;
    $scope.AddProduct = null;
    $scope.OrderItems = [];
    $scope.checkSelection = '1';
    $scope.AddProductToTable = function (AddProduct) {
        var edit = false;
        var size = Object.size($scope.ListProduct);
        var sizeItem = Object.size($scope.OrderItems);
        var name = "";
        var price = 0;
        for (var i = 0; i < size; i++) {
            if (AddProduct.ProductID == $scope.ListProduct[i].ProductID) {
                name = $scope.ListProduct[i].ProductName;
                price = $scope.ListProduct[i].Price;
                break;
            }
        }
        for (var i = 0; i < sizeItem; i++) {
            if (AddProduct.ProductID == $scope.OrderItems[i].id) {
                $scope.OrderItems[i].quantity = AddProduct.Quantity;
                edit = true;
                break;
            }
        }
        if (edit == false) {
            $scope.OrderItems.push({ 'id': AddProduct.ProductID, 'name': name,'price':price, 'quantity': AddProduct.Quantity });
        }  
        $scope.AddProduct = null;
    };
    $scope.TotalPrice = function () {
        var total = 0;
        var sizeItem = Object.size($scope.OrderItems);
        for (var i = 0; i < sizeItem; i++) {
            var item = $scope.OrderItems[i];
            total += toNumber(item.quantity * item.price);
        }
        return total;
    }
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close({ orderModelAdd: $scope.orderModelAdd, OrderItems: $scope.OrderItems, CheckSelected: $scope.checkSelection });
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };

    function toNumber (value) {
    value = value * 1;
    return isNaN(value) ? 0 : value;
    }

    $scope.isCheckboxSelected = function (index) {
        return index === $scope.checkSelection;
    };
});

app.controller('DeleteOrder', function ($scope, $uibModalInstance) {
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close();
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

Object.size = function(obj) {
    var size = 0, key;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) size++;
    }
    return size;
};

Object.defineProperty(Date.prototype, 'YYYYMMDDHHMMSS', {
    value: function () {
        function pad2(n) {  // always returns a string
            return (n < 10 ? '0' : '') + n;
        }

        return this.getFullYear() +
               pad2(this.getMonth() + 1) +
               pad2(this.getDate()) +
               pad2(this.getHours()) +
               pad2(this.getMinutes()) +
               pad2(this.getSeconds());
    }
});


