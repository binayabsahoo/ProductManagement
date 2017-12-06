using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class ProductGrid
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string CashOnDelevery { get; set; }
        
    }
}