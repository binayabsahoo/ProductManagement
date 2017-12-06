using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductManagement.Models;
using System.Data.Entity.Infrastructure;
//using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProductManagement.DataAccessLayer
{
    public class ProductDAL : DbContext
    {
        
        public  DbSet<Category> CatagoriesTable { get; set; }
        public DbSet<Product> ProductsTable { get; set; }

        public DbSet<Seller> SellerTable { get; set; }

        public DbSet<SellerInfo> sellerInfoTable { get; set; }

        ////protected override void OnModelCreating(DbModelBuilder modelBuilder)
        ////{
        ////    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        ////}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().ToTable("TblCategory");
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}