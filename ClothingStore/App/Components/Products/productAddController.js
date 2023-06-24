
(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']

    function productAddController($scope, apiService, notificationService, $state) {
        $scope.product = {};

        $scope.chooseImage = () => {
            window.fileSelected = function (data) {
                $scope.$apply(() => {
                    $scope.product.Image_Url = data.path;
                })
            };
            window.open('/file-manager-elfinder', "Select", "menubar:0;");
        };

        $scope.ListCategoriesID = [];
        $scope.getCategory = () => {
            apiService.get('/api/Categories/get-all-category', null, (result) => {
                $scope.ListCategoriesID = result.data;
            }, () => {
                alert('Get all list categories failed');
            });
        };
        $scope.getCategory();

        $scope.addProduct = () => {
            apiService.post('/api/products/create-product', $scope.product, (result) => {
                notificationService.displaySuccess('Create product successfully');
                $state.go('product_list');
               
            }, (error) => {
                notificationService.displayError('Create product failed');
            });
        };
    };


})(angular.module('ClothingStoreProduct'));