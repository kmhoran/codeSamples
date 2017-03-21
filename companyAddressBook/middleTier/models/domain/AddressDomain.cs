using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class AddressDomain
    {
        public int AddressId { get; set; }

        public int CompanyId { get; set; }

        public DateTime Date { get; set; }

        public string Address1 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Slug { get; set; }

        public int AddressType { get; set; }
    }
}