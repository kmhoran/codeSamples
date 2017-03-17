using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class BotConversationIdResponseDomain
    {
        public string ConversationId { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string StreamUrl { get; set; }
    }
}