using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
	// Model to be used for person-to-person 
	// and person-to-bot messaging
    public class MessageDomain
    {
        public int ConversationId { get; set; }

        public int MessageId { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public string SenderFullName { get; set; }

        public string ReceiverFullName { get; set; }

        public string SenderUrl { get; set; }

        public string ReceiverUrl { get; set; }

        public bool IsRead { get; set; }
    }
}