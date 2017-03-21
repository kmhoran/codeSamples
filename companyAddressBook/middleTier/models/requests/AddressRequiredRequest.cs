using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Requests
{
    public class AddressRequiredRequest
    {
        [Required]
        public int CompanyId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Address1 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Slug { get; set; }

        [Required]
        public int AddressType { get; set; }
    }
}