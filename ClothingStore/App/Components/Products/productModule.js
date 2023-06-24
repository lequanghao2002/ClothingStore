
(function () {
    angular.module('ClothingStoreProduct', ['ClothingStoreCommon']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product_list', {
                url: '/product_list',
                parent: 'base',
                templateUrl: '/App/Components/products/productListView.html',
                controller: 'productListController'
            })
            .state('product_add', {
                url: '/product_add',
                parent: 'base',
                templateUrl: '/App/Components/products/productAddView.html',
                controller: 'productAddController'
            })
            .state('product_edit', {
                url: '/product_edit/:id',
                parent: 'base',
                templateUrl: '/App/Components/products/productEditView.html',
                controller: 'productEditController'
            })
    }
})();