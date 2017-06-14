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
app.controller('AdStaffController', function ($scope, $http, $location, $window, $uibModal, growl) {
    $scope.staffModel = {};
    $scope.ListStaff = null;
    getallData();
    $scope.currentPage = 1;
    $scope.itemsPerPage = 10;
    //******=========Get All Customer=========******  
    function getallData() {
        $http.get('/AdStaff/GetAllData').then(successCallback, errorCallback);

        function successCallback(data) {
            $scope.ListStaff = data.data;
        }
        function errorCallback(data) {
            growl.error("Có lỗi trong quá trình gọi xử lý đến server");
        }
    };
    //******=========Get Single Customer=========******  
   
    //******=========Save Customer=========******  
    function saveStaff(staffModel) {
        $http(
        {
            method: 'POST',
            url: '/AdStaff/Insert',
            data: staffModel
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
    function updateStaff(newStaff) {

        $http(
        {
            method: 'POST',
            url: '/AdStaff/Update',
            data: newStaff
        }).then(successCallback, errorCallback);

        function successCallback(data) {
            if (data.data.success === true) {
                $scope.staffModel = null;
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
    $scope.deleteStaff = function (staffModel) {

        var modalInstance = $uibModal.open({
            templateUrl: 'myModalDelete.html',
            controller: 'DeleteStaff'
        });

        modalInstance.result.then(function () {
            $http.delete('/AdStaff/Delete/' + staffModel.StaffID).then(successCallback, errorCallback);

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

    $scope.updateStaff = function (staffModel) {

        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'UpdateStaff',
            resolve: {
                oldStaff: function () {
                    return $scope.oldStaff = staffModel;
                }
            }
        });

        modalInstance.result.then(function (newStaff) {
            updateStaff(newStaff);
        }, function () {
            //error
        });
    };

    $scope.addStaff = function () {
        var modalInstance = $uibModal.open({
            templateUrl: 'myModal.html',
            controller: 'AddStaff'
        });

        modalInstance.result.then(function (newStaff) {
            saveStaff(newStaff);
        }, function () {
            //error
        });
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.controller('UpdateStaff', function ($scope, $uibModalInstance, oldStaff) {
    $scope.action = "Lưu";
    $scope.staffModel = oldStaff;
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.staffModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('AddStaff', function ($scope, $uibModalInstance) {
    $scope.action = "Thêm";
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close($scope.staffModel);
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});

app.controller('DeleteStaff', function ($scope, $uibModalInstance) {
    $scope.ok = function () {
        //it close the modal and sends the result to controller
        $uibModalInstance.close();
    };
    $scope.cancel = function () {
        //it dismiss the modal 
        $uibModalInstance.dismiss('cancel');
    };
});
