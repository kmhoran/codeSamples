using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteMuleBot1.Domain
{
    public class FaqQueryDomain
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<FaqTagDomain> Tags { get; set; }
    }
}