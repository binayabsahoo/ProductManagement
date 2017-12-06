using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.ViewModels
{
    public class ProductListViewModel
    {
        public List<Category> CatagoryList { get; set; }

        public List<Seller> SellerList { get; set; }
        public List<Product> ProductList { get; set; }
        

        //public Product products { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        //public int ProductNumbers { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }

        public string CashOnDelevery { get; set; }
        public Category Category { get; set; }
        public HttpPostedFileBase productImage { get; set; }
        public string SelectedCategory { get; set; }
        public int[] SelectedSellers { get; set; }
        public string SelectedSellersinString { get; set; }
    }
}