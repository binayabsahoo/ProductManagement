using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    [Table("tblCategory")]
    public class Category
    {        
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}