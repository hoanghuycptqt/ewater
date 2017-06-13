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
app.controller('AdCustomerController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.custModel = {};
    $scope.ListCustomer = [];
    getallData();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;

   
    //******=========Get All Customer=========******  
    function getallData() {
        $http.get('/AdCustomer/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListCustomer = data.data;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };
    //******=========Get Single Customer=========******  
    function getCustomer(custModel) {

        $http.get('/AdCustomer/GetbyID/' + custModel.CustomerID).then(successCallback, errorCallback);
        function successCallback(data) {
            $scope.custModel = data.data;
            getallData();
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };
    //******=========Save Customer=========******  
    function saveCustomer(custModel) {
        $http(
        {
            method: 'POST',
            url: '/AdCustomer/Insert',
            data: custModel
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
    function updateCustomer(newCust) {

        $http(
        {
            method: 'POST',
            url: '/AdCustomer/Update',
            data: newCust
        }).then(successCallback, errorCallback);

        function successCallback(data) {
            if (data.data.success === true) {
                $scope.custModel = null;
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
    $scope.deleteCustomer = function (custModel) {

        var modalInstance = $uibModal.open({
            templateUrl: 'myModalDelete.html',
            controller: 'DeleteCustomer'
        });

        modalInstance.result.then(function () {
            var id = custModel.CustomerID.toString();
            $http.delete('/AdCustomer/Delete/' + id).then(successCallback, errorCallback);

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

    $scope.updateCust = function (custModel) {
        getCustomer(custModel);
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'UpdateCustomer',
            resolve: {
                oldCust: function () {
                    return $scope.oldCust = custModel;
                }
            }
        });

        modalInstance.result.then(function (newCust) {
            updateCustomer(newCust);
        }, function () {
            //error
        });
    };

    $scope.addCust = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'AddCustomer'
        });

        modalInstance.result.then(function (newCust) {
            saveCustomer(newCust);
        }, function () {
            //error
        });
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.controller('UpdateCustomer', function ($scope, $uibModalInstance, oldCust) {
    $scope.action = "Lưu";
    $scope.custModel = oldCust;
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.custModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('AddCustomer', function ($scope, $uibModalInstance) {
    $scope.action = "Thêm";
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.custModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('DeleteCustomer', function ($scope, $uibModalInstance) {
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close();
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});
