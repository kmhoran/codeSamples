// Developped by Kevin
(function () {
    "use strict";

    angular.module(APPNAME)
        .config(["$routeProvider", "$locationProvider",
            function ($routeProvider, $locationProvider) {

            $routeProvider.when('/', {
                templateUrl: '/Scripts/app/Addresses/Templates/primaryAddressBook.html',
                controller: 'addressbookController',
                controllerAs: 'AB'
            }).when('/office', {
                templateUrl: '/Scripts/app/Addresses/Templates/officeAddressBook.html',
                controller: 'addressbookController',
                controllerAs: 'OAB'
            }).when('/warehouse', {
                templateUrl: '/Scripts/app/Addresses/Templates/warehouseAddressBook.html',
                controller: 'addressbookController',
                controllerAs: 'WAB'
            }).when('/projectSite', {
                templateUrl: '/Scripts/app/Addresses/Templates/projectSiteAddressBook.html',
                controller: 'addressbookController',
                controllerAs: 'PAB'
            });

            $locationProvider.html5Mode(false).hashPrefix('');

        }]);

})();



//.when('/primary', {
//    templateUrl: '/Scripts/app/Addresses/Templates/primaryAddressBook.html',
//    controller: 'addressbookController',
//    controllerAs: 'AB'
//})