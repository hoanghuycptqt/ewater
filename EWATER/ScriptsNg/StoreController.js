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
app.factory("DataService", function () {


    // create shopping cart
    var myCart = new shoppingCart("myFormApp");

    // return data object with store and cart
    return {
        cart: myCart
    };
});

app.controller('StoreController', function ($scope, $http, $location, $window, $uibModal, growl, DataService) {
    $scope.productModel = {};
    $scope.custModel = {};
    $scope.ListProduct = null;
    $scope.phoneNumber = null;
    getallData();
    $scope.cart = DataService.cart;
    $scope.checkSelection = '1';
    $scope.validationFlag = '0';

    $scope.isCheckboxSelected = function (index) {
        return index === $scope.checkSelection;
    };

    $scope.cart.itemsChanged = function (e) {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    }
    //******=========Get All Customer=========******  
    function getallData() {
        $http.get('/ClientProduct/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListProduct = data.data;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };

    $scope.checkout = function (phoneNumber, custModel) {
        $scope.validationFlag = '1';
        var phoneCurrent = phoneNumber;
        var customerID = null;
        var date = new Date().YYYYMMDDHHMMSS();
        var orderID = date;
        var status = false;
        var orderDate = new Date();
        var dataOrder = new Object();
        if ($scope.checkSelection == "2") {
            $http.get('/AdCustomer/GetCustomerByPhone/' + custModel.PhoneNumber).then(successCallback, errorCallback);
            function successCallback(data) {
                customerID = data.data.CustomerID;
                $scope.phoneNumber = null;

                if (customerID != null) {
                    alert("Your phonenumber exits in my system");
                    return;
                } else {
                    $http(
           {
               method: 'POST',
               url: '/AdCustomer/Insert',
               async: false,
               data: custModel
           }).then(successCallback, errorCallback);

                    function successCallback(data) {
                        phoneCurrent = custModel.PhoneNumber;
                        $scope.custModel = {};

                        $http.get('/AdCustomer/GetCustomerByPhone/' + phoneCurrent).then(successCallback, errorCallback);

                        function successCallback(data) {
                            customerID = data.data.CustomerID;
                            $scope.phoneNumber = null;

                            if (customerID == null) {
                                alert("Hello! I am an alert box!!");
                                return;
                            }
                            dataOrder.OrderID = orderID;
                            dataOrder.CustomerID = customerID;
                            dataOrder.OrderDate = orderDate;
                            dataOrder.Status = status;
                            dataOrder.OrderItems = [];
                            var productItems = $scope.cart.items;
                            for (var i = 0; i < productItems.length; i++) {
                                var item = new Object();
                                item.OrderID = orderID;
                                item.ProductID = productItems[i].sku;
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
                                    $scope.cart.items = [];
                                    growl.success(data.data.message);
                                    alert("Thanks for your order. We will contact you soon");
                                    $window.location.href = '/ClientProduct';
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
            }
            function errorCallback(data) {
                growl.error("Có lỗi trong quá trình gọi xử lý đến server");
            }
        }     

        if ($scope.checkSelection == "1") {
            $http.get('/AdCustomer/GetCustomerByPhone/' + phoneCurrent).then(successCallback, errorCallback);

            function successCallback(data) {
                customerID = data.data.CustomerID;
                $scope.phoneNumber = null;

                if (customerID == null) {
                    alert("Your phonenumber not exits in my system");
                    return;
                }
                dataOrder.OrderID = orderID;
                dataOrder.CustomerID = customerID;
                dataOrder.OrderDate = orderDate;
                dataOrder.Status = status;
                dataOrder.OrderItems = [];
                var productItems = $scope.cart.items;
                for (var i = 0; i < productItems.length; i++) {
                    var item = new Object();
                    item.OrderID = orderID;
                    item.ProductID = productItems[i].sku;
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
                        $scope.cart.items = [];
                        growl.success(data.data.message);
                        alert("Thanks for your order. We will contact you soon");
                        $window.location.href = '/ClientProduct';
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

    $scope.base64ArrayBuffer = function (arrayBuffer) {
        var base64 = ''
        var encodings = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'

        var bytes = new Uint8Array(arrayBuffer)
        var byteLength = bytes.byteLength
        var byteRemainder = byteLength % 3
        var mainLength = byteLength - byteRemainder

        var a, b, c, d
        var chunk

        // Main loop deals with bytes in chunks of 3
        for (var i = 0; i < mainLength; i = i + 3) {
            // Combine the three bytes into a single integer
            chunk = (bytes[i] << 16) | (bytes[i + 1] << 8) | bytes[i + 2]

            // Use bitmasks to extract 6-bit segments from the triplet
            a = (chunk & 16515072) >> 18 // 16515072 = (2^6 - 1) << 18
            b = (chunk & 258048) >> 12 // 258048   = (2^6 - 1) << 12
            c = (chunk & 4032) >> 6 // 4032     = (2^6 - 1) << 6
            d = chunk & 63               // 63       = 2^6 - 1

            // Convert the raw binary segments to the appropriate ASCII encoding
            base64 += encodings[a] + encodings[b] + encodings[c] + encodings[d]
        }

        // Deal with the remaining bytes and padding
        if (byteRemainder == 1) {
            chunk = bytes[mainLength]

            a = (chunk & 252) >> 2 // 252 = (2^6 - 1) << 2

            // Set the 4 least significant bits to zero
            b = (chunk & 3) << 4 // 3   = 2^2 - 1

            base64 += encodings[a] + encodings[b] + '=='
        } else if (byteRemainder == 2) {
            chunk = (bytes[mainLength] << 8) | bytes[mainLength + 1]

            a = (chunk & 64512) >> 10 // 64512 = (2^6 - 1) << 10
            b = (chunk & 1008) >> 4 // 1008  = (2^6 - 1) << 4

            // Set the 2 least significant bits to zero
            c = (chunk & 15) << 2 // 15    = 2^4 - 1

            base64 += encodings[a] + encodings[b] + encodings[c] + '='
        }

        return base64
    }

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

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

