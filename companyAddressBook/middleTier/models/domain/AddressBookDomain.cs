using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class AddressBookDomain
    {
        public List<AddressDomain> Primary { get; set; }

        public List<AddressDomain> Office { get; set; }

        public List<AddressDomain> Warehouse { get; set; }

        public List<AddressDomain> ProjectSite { get; set; }
    }
}