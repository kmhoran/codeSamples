using QuoteMuleBot1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteMuleBot1.Domain
{
    public class DirectAddressLuis
    {
        public string query { get; set; }
        public Topscoringintent topScoringIntent { get; set; }
        public Intent[] intents { get; set; }
        public object[] entities { get; set; }
    }

}