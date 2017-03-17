using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class BotApiActivitiesDomain
    {
        public Activity[] Activities { get; set; }
        public string Watermark { get; set; }
    }

    public class Activity
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelId { get; set; }
        public From From { get; set; }
        public Conversation Conversation { get; set; }
        public string Text { get; set; }
        public string ReplyToId { get; set; }
    }

    public class From
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Conversation
    {
        public string Id { get; set; }
    }

}