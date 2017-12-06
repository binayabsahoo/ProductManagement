using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    [Table("tblSellerInfo")]
    public class SellerInfo
    {
        public int SellerInfoID { get; set; }
        public Product Product { get; set; }
        public Seller Seller { get; set; }
    }
}