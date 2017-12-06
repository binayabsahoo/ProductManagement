using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace ProductManagement.Models
{
    [Table("tblProduct")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        //public int ProductNumbers { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }

        public string CashOnDelevery { get; set; }
        public Category Category { get; set; }
       // public Seller Seller { get; set; }

        
    }
}