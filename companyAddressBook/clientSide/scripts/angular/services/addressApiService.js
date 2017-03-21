// Address Service
// Developped by Kevin Horan
(function () {
    "use strict";

    angular.module(APPNAME)
        .factory('$addressService', AddressServiceFactory);

    AddressServiceFactory.$inject = ['$baseService', '$sabio'];

    function AddressServiceFactory($baseService, $sabio) {

        var serviceCopy = sabio.services.addresses;

        //  merge the jQuery object with the angular base service to simulate inheritance
        var newService = $baseService.merge(true, {}, serviceCopy, $baseService);


        return newService;
    }
})();