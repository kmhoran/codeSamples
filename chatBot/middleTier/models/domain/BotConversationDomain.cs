using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class BotConversationDomain
    {
        public int ConversationId { get; set; }

        public string BotId { get; set; }

        public string BotUrl { get; set; }

        public string BotFullName { get; set; }

        public string BotCompanyName { get; set; }
    }
}