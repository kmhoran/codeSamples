// Developped by Kevin
// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
// Bot Conversation Modal Controller
(function () {
    "use strict";

    angular.module(APPNAME)
        .controller('botConversationModalController', BotConversationModalController);

    // $uibModalInstance is coming from the UI Bootstrap library and is a reference to the
    //    modal window itself so we can work with it.
    // Items is the array passed in from the main controller above through the resolve property.
    BotConversationModalController.$inject = ['$scope', '$baseController', '$uibModalInstance'
    , '$botConversationService']

    function BotConversationModalController(
        $scope
        , $baseController
        , $uibModalInstance
        , $botConversationService) {

        var vm = this;


        // Inheritance
        vm.$scope = $scope;
        vm.$uibModalInstance = $uibModalInstance;
        vm.$botConversationService = $botConversationService;

        $baseController.merge(vm, $baseController);

        vm.notify = vm.$botConversationService.getNotifier($scope);

        // Properties
        vm.currentUserId = $('#PAGEUSER').val();
        vm.messageContent = null;
        vm.botMessages = null;

        // Methods
        vm.ok = _ok;
        vm.cancel = _cancel;
        vm.submit = _submit;
        vm.sendMessage = _sendMessage;

        // Start-up functions
        _getMessages();


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getMessages() {

            vm.$botConversationService.getByUserId(vm.currentUserId, _getBotMessageSuccess, _ajaxError);
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _submit() {
            // Submit message on enter key

            if (vm.messageContent) {
                vm.messageContent = this.messageContent;
                console.log('thisMessage:', vm.messageContent);

                _sendMessage();

                vm.messageContent = null;
            }
        };



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _sendMessage() {

            var messageObject;


            messageObject = {
                "senderId": vm.currentUserId,
                "content": vm.messageContent
            }

            vm.$botConversationService.insert(messageObject, _insertMessageSuccess, _ajaxError);

            vm.messageContent = null;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _getBotMessageSuccess(data) {
            var incoming = data.items;

            vm.notify(function () {
                vm.botMessages = incoming;
                console.log('GETTING BotMessages Success', vm.botMessages);
            });

            scrollSmoothToBottom("list_of_bot_messages");

            if (vm.botMessages.length <= 0) {
                var messageObject = {
                    "receiverId": vm.currentUserId,
                    "content": "Welcome to QuoteMule! Ask me any questions you might have and I'll look up the answer for you."
                }

                vm.$botConversationService.insert(messageObject, _insertMessageSuccess, _ajaxError);
            }
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _insertMessageSuccess(data) {

            vm.notify(function () {
                vm.submittedMessageItem = data;
                console.log('POSTING Messages Success', vm.submittedMessageItem);
                _getMessages();
            });

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function _ajaxError(jqXhr, error) {

            console.error("An error occured while accessing the server: ", error);
        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  $uibModalInstance is used to communicate and send data back to main controller
        function _ok() {
            console.log("editAddress.ok");

            // Dress up retun payload.
            delete vm.modalItems._Mtitle;
            delete vm.modalItems._modalType

            vm.modalItems.addressType = vm.selected.value;

            vm.$uibModalInstance.close(vm.modalItems);
        };

        //- If user cancels modal
        function _cancel() {
            console.log("editAddress.cancel");

            vm.$uibModalInstance.dismiss('cancel');
        };



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        function scrollSmoothToBottom(id) {
            var div = document.getElementById(id);
            $('#' + id).animate({
                scrollTop: div.scrollHeight - div.clientHeight
            }, 500);
        }
    }


})();