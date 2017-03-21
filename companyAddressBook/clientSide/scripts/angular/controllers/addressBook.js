// Addresses Widget
// Developped by Kevin
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('addressbookController', AddressbookController);

    AddressbookController.$inject = ['$scope', '$baseController'
        , '$addressService', '$uibModal', '$googleMapService','$route' ,'$alertService']

    function AddressbookController(
        $scope
        , $baseController
        , $addressService
        , $uibModal
        , $googleMapService
        , $route
        , $alertService) {

        var vm = this;

        // Injection
        vm.$scope = $scope;
        vm.$addressService = $addressService;
        vm.$uibModal = $uibModal;
        vm.$gMaps = $googleMapService;
        vm.$route = $route;
        vm.alertService = $alertService;
    	// Properties
        vm.addressbook = null;
        vm.primaryExists = false;
        vm.officeExists = false;
        vm.warehouseExists = false;
        vm.projectSiteExists = false;
        vm.tabs = [
            { link: "#/", title: "Primary", "class": "active" },
            { link: "#/office", title: "Office", "class": "" },
            { link: "#/warehouse", title: "Warehouse", "class": "" },
            { link: "#/projectSite", title: "Project Site", "class": "" }
        ];


        // Methods
        vm.openEditModal = _openEditModal;
        vm.openAddAddressModal = _openAddAddressModal;
        vm.editAddress = _editAddress;
        vm.insertAddress = _insertAddress;
        vm.openDeleteAddress = _openDeleteAddressModal;
        vm.deleteAddress = _deleteAddress;
        vm.plotAddressOnMap = _plotLatLngOnMap;
        vm.plotAllAddresses = _plotAllAddresses;
        vm.setActiveTab = _setActiveTab;
     


        $baseController.merge(vm, $baseController);

        vm.notify = vm.$addressService.getNotifier($scope);


        // Startup functions
        _getAddressbook();

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getAddressbook() {
            vm.$addressService.getAddressBookByCompanyId(sabio.p.companyId, _recieveData,
                _onError);

        };



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _editAddress(payload) {
            console.log("sending edit address request.");

            if (payload.addressId != null && payload.addressId != 0) {

                vm.$addressService.editById(payload.addressId, payload, _onUpdateSuccess, _onError);
            }
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _insertAddress(payload) {
            console.log("sending insert address request.");

            vm.$addressService.insert(payload, _onReloadSuccess, _onError);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _deleteAddress(addressId) {
            console.log("sending insert address request.");

            vm.$addressService.delete(addressId, _onUpdateSuccess, _onError);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _plotLatLngOnMap(latLng) {

            // Those darn inconsistencies
            if (latLng.latitude) {
                vm.$gMaps.markLatLng(latLng.latitude, latLng.longitude);
            }
            if (latLng.lat) {
                vm.$gMaps.markLatLng(latLng.lat, latLng.lng);
            }
            


        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _plotAllAddresses() {
            var locationArray = [];

            for (var propertyType in vm.addressbook) {
                if (vm.addressbook[propertyType] != null
                    && vm.addressbook[propertyType].length > 0) {

                    var property = vm.addressbook[propertyType];

                    for (var i = 0; i < property.length ; i++) {

                        var location = {
                            settings:null,
                            latLng: {
                                lat: property[i].latitude,
                                lng: property[i].longitude
                            }
                        };

                        locationArray.push(location);
                    }
                }
            }

            console.log(locationArray);
            if (locationArray.length == 1) {
                _plotLatLngOnMap(locationArray[0].latLng);
            } else {
                vm.$gMaps.plotLocationArray(locationArray);
            }
            
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _recieveData(data) {
            vm.notify(function () {
                vm.addressbook = data.item;

                vm.primaryExists = (vm.addressbook.primary != null);
                vm.officeExists = (vm.addressbook.office != null);
                vm.warehouseExists = (vm.addressbook.warehouse != null);
                vm.projectSiteExists = (vm.addressbook.projectSite != null);

                // Marks principal address.
                //for (var propertyType in vm.addressbook) {
                //    if (vm.addressbook[propertyType] != null
                //        && vm.addressbook[propertyType].length > 0) {
                //        var location = vm.addressbook[propertyType][0]
                //        vm.$gMaps.markLatLng(location.latitude, location.longitude);
                //        break;
                //    }
                //}

                // Mark all addresses
                vm.plotAllAddresses();
              
            });
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _onUpdateSuccess(data) {
            console.log("HTTP success: ", data);
           
            // Refresh addressbook
            _getAddressbook();
        }

        function _onReloadSuccess() {
            $route.reload();
        };

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _onError(data) {
            console.debug("An error occured: ", data);
                vm.alertService.error('Google Maps does not recognize the address');
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        function _setActiveTab(tab) {
            for (var i = 0; i < vm.tabs.length; i++) {
                vm.tabs[i]["class"] = ""
            }

            tab["class"] = "active";
        };


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _openEditModal(addressItem) {

            addressItem._Mtitle = "Edit Address";
            addressItem._modalType = "edit";

            var modalInstance = vm.$uibModal.open({
                animation: true,

                // This tells it what html template to use.
                // It must exist in a script tag OR external file.
                templateUrl: '/Scripts/app/Addresses/Templates/editAddressModal.html',

                // This controller must exist & be registered with angular in order to work.
                controller: 'addressEditModalController as AdrEMC',
                size: 'md',

                // Anything in resolve can be injected into the modal controller as shown below
                resolve: {
                    items: function () {
                        return addressItem;
                    }
                }
            });

            //  when the modal closes it returns a promise
            modalInstance.result.then(function (modalItems) {

           
                //  If the user closed the modal by clicking Save.
                console.log("returned from modal: ", modalItems);

                vm.editAddress(modalItems);

             


            }, function () {

                //  If the user closed the modal by clicking cancel.
                console.log('Modal dismissed at: ' + new Date());
            });
        } // End _openEditModal


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _openAddAddressModal(addressObject) {

        	
          
        	var toSend = {

                _Mtitle: "Add Address",
                _modalType: "add",
                companyId: sabio.p.companyId,
                address1: "",
                city: "",
                state: "",
                zipCode: "",
                addressType: addressObject.addressType
            }
               

            var modalInstance = vm.$uibModal.open({
                animation: true,

                // This tells it what html template to use.
                // It must exist in a script tag OR external file.
                templateUrl: '/Scripts/app/Addresses/Templates/editAddressModal.html',

                // This controller must exist & be registered with angular in order to work.
                controller: 'addressEditModalController as AdrEMC',
                size: 'md',

                // Anything in resolve can be injected into the modal controller as shown below
                resolve: {
                    items: function () {
                    	return toSend;
                    }
                }
            });

            //  when the modal closes it returns a promise
            modalInstance.result.then(function (modalItems) {

                //  If the user closed the modal by clicking Save.
                console.log("returned from modal: ", modalItems);

                vm.insertAddress(modalItems);

            }, function () {

                //  If the user closed the modal by clicking cancel.
                console.log('Modal dismissed at: ' + new Date());
            });
        } // End _openAddModal



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _openDeleteAddressModal(address) {
            console.log("open delete modal.");
            var modalInstance = vm.$uibModal.open({
                animation: true,

                // This tells it what html template to use.
                // It must exist in a script tag OR external file.
                templateUrl: '/Scripts/app/Addresses/Templates/confirmDeleteAddressModal.html',

                // This controller must exist & be registered with angular in order to work.
                controller: 'addressDeleteModalController as AdrDMC',
                size: 'md',

                // Anything in resolve can be injected into the modal controller as shown below
                resolve: {
                    items: function () {
                        return address;
                    }
                }
            });

            //  when the modal closes it returns a promise
            modalInstance.result.then(function (addressId) {

                //  If the user closed the modal by clicking Save.
                console.log("Id of address to delete: ", addressId);

                vm.deleteAddress(addressId);

            }, function () {

                //  If the user closed the modal by clicking cancel.
                console.log('Modal dismissed at: ' + new Date());
            });
        } // End _openDeleteAddressModal

    } // END ADDRESSBOOK CONTROLLER
})();