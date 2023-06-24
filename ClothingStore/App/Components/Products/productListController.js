/// <reference path="../assets/admin/libs/angular-1.8.2/angular.js" />

(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox']

    function productListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.listproducts = [];

        $scope.page = 0;
        $scope.pageSize = 2;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.num = 0;

        $scope.listCategories = [];
        $scope.getListCategory = () => {
            apiService.get('/api/Categories/get-all-category', null, (result) => {
                $scope.listCategories = result.data;
            }, () => {
                //alert('Get all list categories failed');
            });
        };
        $scope.getListCategory();

        $scope.getListproducts = getListproducts;
        function getListproducts(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: $scope.pageSize
                }
            };

            apiService.get('/api/products/get-all-product', config, (result) => {
                $scope.listproducts = result.data.list;
                $scope.page = result.data.page;
                $scope.num = result.data.count;
                $scope.pagesCount = result.data.pagesCount;
                $scope.totalCount = result.data.totalCount;

                if ($scope.num == 0) {
                    $scope.showTo = 0;
                }
                else {
                    $scope.showTo = ($scope.page * $scope.pageSize + 1);
                }
                $scope.showFrom = ($scope.page * $scope.pageSize) + $scope.num;

                if ($scope.showFrom % $scope.pageSize == 1) {
                    $scope.showEnd = true;
                }
                else {
                    $scope.showEnd = false;
                }

             
            }, () => {
               
            });
        }
        $scope.getListproducts();

        $scope.deleteProduct = (id) => {
            $ngBootbox.confirm('Do you want to delete product with id = ' + id).then(() => {
                apiService.delete('/api/products/delete-product/' + id, null, () => {
                    notificationService.displaySuccess('Delete product successfully');
                    $scope.getListproducts();
                }, () => {
                    notificationService.displayError('Delete product failed');
                })
            });
        };
    };


})(angular.module('ClothingStoreProduct'));