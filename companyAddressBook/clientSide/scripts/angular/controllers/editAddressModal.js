// Developped by Kevin
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Edit Address Modal Controller
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('addressEditModalController', AddressEditModalController);

    // $uibModalInstance is coming from the UI Bootstrap library and is a reference to the
    //    modal window itself so we can work with it.
    // Items is the array passed in from the main controller above through the resolve property.
    AddressEditModalController.$inject = ['$scope', '$baseController', '$uibModalInstance', 'items']

    function AddressEditModalController(
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
        vm.modalItems = items;
        vm.modalTilte = vm.modalItems._Mtitle;
        
        vm.propertyTypes = [
            {name: "Choose...", value:0},
            { name: "Primary", value: 1 },
            { name: "Office", value: 2 },
            { name: "Warehouse", value: 3 },
            { name: "Project Site", value: 4 }
        ]

        if (vm.modalItems._modalType == "edit" || vm.modalItems._modalType == "add") {

            vm.selected = vm.propertyTypes[vm.modalItems.addressType];
        }
        
        else {
        	vm.selected = vm.propertyTypes[0];
        }
        

        // Methods
        vm.ok = _ok;
        vm.cancel = _cancel;

        //  $uibModalInstance is used to communicate and send data back to main controller
        function _ok() {
            console.log("editAddress.ok");

            // Dress up retun payload.
            delete vm.modalItems._Mtitle;
            delete vm.modalItems._modalType

            vm.modalItems.addressType = vm.selected.value;

            vm.$uibModalInstance.close(vm.modalItems);
        };

        function _cancel() {
            console.log("editAddress.cancel");

            vm.$uibModalInstance.dismiss('cancel');
        };
    }
})();