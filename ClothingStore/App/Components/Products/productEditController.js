
(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams']

    function productEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.product = {};

        $scope.chooseImage = () => {
            window.fileSelected = function (data) {
                $scope.$apply(() => {
                    $scope.product.image_Url = data.path;
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
        console.log($stateParams.id);
        $scope.loadProductById = () => {
            apiService.get('/api/products/get-product-by-id/' + $stateParams.id, null, (result) => {
                $scope.product = result.data;

            }, (error) => {
                notificationService.displayError('Không load product được');
            });
        };
        $scope.loadProductById();

        $scope.updateProduct = () => {
            
            apiService.put('/api/products/update-product/' + $stateParams.id, $scope.product, (result) => {
                notificationService.displaySuccess('Update product successfully');
                $state.go('product_list')
            }, (error) => {
                notificationService.displayError('Update product failed');
            });
        };
    };


})(angular.module('ClothingStoreProduct'));