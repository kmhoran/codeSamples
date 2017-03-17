using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteMuleBot1.Domain
{
    public class FaqTagDomain
    {
        public int Id { get; set; }

        public int QueryId { get; set; }

        public string Name { get; set; }
    }
}