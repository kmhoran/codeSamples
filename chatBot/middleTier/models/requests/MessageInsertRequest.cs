using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
	// Model to be used for both person-to-person-to-pperson
	// and person-to-bot messaging
    public class MessageInsertRequest
    {
        public string UserId { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public int ConversationId { get; set; }
    }
}