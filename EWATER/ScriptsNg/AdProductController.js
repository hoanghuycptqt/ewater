var app = angular.module('myFormApp', ['ui.bootstrap', 'angular-growl', 'ngFileUpload']);

app.config(['growlProvider', function (growlProvider) {
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
app.controller('AdProductController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.productModel = {};
    $scope.ListProduct = null;
    getallData();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    //******=========Get All Customer=========******  
    function getallData() {
        $http.get('/AdProduct/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListProduct = data.data;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };
    //******=========Get Single Customer=========******  
   
    //******=========Save Customer=========******  
    function saveCustomer(productModel) {
        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function (value, key) {
                dataAsFormData.append(key, value);
            });
            return dataAsFormData;
        };

        $http(
        {
            method: 'POST',
            url: '/AdProduct/Insert',
            data: getModelAsFormData(productModel),
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
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
        getallData();

    };
    //******=========Edit Customer=========******  
    function updateProduct(newProduct) {
        var getModelAsFormData = function (data) {
            var dataAsFormData = new FormData();
            angular.forEach(data, function (value, key) {
                dataAsFormData.append(key, value);
            });
            return dataAsFormData;
        };
        $http(
        {
            method: 'POST',
            url: '/AdProduct/Update',
            data: getModelAsFormData(newProduct),
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(successCallback, errorCallback);

        function successCallback(data) {
            if (data.data.success === true) {
                $scope.productModel = null;
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
    };
    //******=========Delete Customer=========******  
    $scope.deleteProduct = function (productModel) {

        var modalInstance = $uibModal.open({
            templateUrl: 'myModalDelete.html',
            controller: 'DeleteProduct'
        });

        modalInstance.result.then(function () {
            $http.delete('/AdProduct/Delete/' + productModel.ProductID).then(successCallback, errorCallback);

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

    $scope.updateProduct = function (productModel) {
        
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'UpdateProduct',
            resolve: {
                oldProduct: function () {
                    return $scope.oldProduct = productModel;
                }
            }
        });

        modalInstance.result.then(function (newProduct) {
            updateProduct(newProduct);
        }, function () {
            //error
        });
    };

    $scope.addProduct = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'AddProduct'
        });

        modalInstance.result.then(function (newProduct) {
            saveCustomer(newProduct);
        }, function () {
            //error
        });
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.controller('UpdateProduct', function ($scope, $uibModalInstance, oldProduct) {
    $scope.action = "Lưu";
    $scope.productModel = oldProduct;
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.productModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('AddProduct', function ($scope, $uibModalInstance) {
    $scope.action = "Thêm";
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.productModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('DeleteProduct', function ($scope, $uibModalInstance) {
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close();
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});



