// ANGULAR SERVICE 
(function () {
    "use strict";

    angular.module(APPNAME)
    .factory('$botConversationService', botConversationServiceFactory);

    botConversationServiceFactory.$inject = ['$baseService', '$sabio'];

	// $baseService and $sabio are utility services that include many handy jQuery functions like .merge() 
    function botConversationServiceFactory($baseService, $sabio) {

        var serviceToAdd = sabio.services.botConversation;

        var newService = $baseService.merge(true, {}, serviceToAdd, $baseService);

        return newService;
    }
})();