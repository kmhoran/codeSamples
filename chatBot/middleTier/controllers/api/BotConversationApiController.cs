using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/systemConversation")]
    public class BotConversationApiController : ApiController
    {
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route(), HttpPost]
        public async Task<HttpResponseMessage> InsertMessage(MessageInsertRequest model)

        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool result = await BotConversationService.GetBotResponse(model);

            var response = new ItemResponse<bool> { Item = result };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [Route("{UserId}"), HttpGet]
        public HttpResponseMessage GetMessageByUserId([FromUri]MessageInsertRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<MessageDomain> MessageList = BotConversationService.GetMessagesByUserId(model.UserId);

            var response = new ItemsResponse<MessageDomain> { Items = MessageList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }

}
