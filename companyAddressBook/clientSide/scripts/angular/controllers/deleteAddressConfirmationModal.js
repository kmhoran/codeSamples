// Developped by Kevin
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Delete Address Confirmation Modal
// Modal Controller
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('addressDeleteModalController', AddressDeleteModalController);

    // $uibModalInstance is coming from the UI Bootstrap library and is a reference to the
    //    modal window itself so we can work with it.
    // Items is the array passed in from the main controller above through the resolve property.
    AddressDeleteModalController.$inject = ['$scope', '$baseController', '$uibModalInstance', 'items']

    function AddressDeleteModalController(
        $scope
        , $baseController
        , $uibModalInstance
        , items) {

        var vm = this;


        // Inheritance
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        $baseController.merge(vm, $baseController);

        // Properties
        vm.items = items;

        // Methods
        vm.ok = _ok;
        vm.cancel = _cancel;

        //  $uibModalInstance is used to communicate and send data back to main controller
        function _ok() {
            console.log("Delete confirmed.");

            vm.$uibModalInstance.close(vm.items.addressId);
        };

        function _cancel() {
            console.log("Delete cancelled");

            vm.$uibModalInstance.dismiss('cancel');
        };
    }
})();