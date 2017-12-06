using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Seller
    {
        public int SellerID { get; set; }
        public string GSTINNumber { get; set; }
        public string SellerName { get; set; }
        public string  Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PanCard { get; set; }

    }
}