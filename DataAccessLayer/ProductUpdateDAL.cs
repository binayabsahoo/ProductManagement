using ProductManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.DataAccessLayer
{
    public class ProductUpdateDAL
    {
        public int UpdateProduct(ProductListViewModel updateModel)
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ProductDAL"].ConnectionString;
            using (SqlConnection sqlconnection = new SqlConnection(connection))
            {
                using (SqlCommand sqlcommand = new SqlCommand("ProductSavefromMOdal", sqlconnection))
                {
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.Parameters.Add("@ProductID", SqlDbType.Int).Value = updateModel.ProductID;
                    sqlcommand.Parameters.Add("@Name",SqlDbType.NVarChar).Value=updateModel.ProductName;
                    sqlcommand.Parameters.Add("@Price", SqlDbType.Int).Value = updateModel.Price;
                    sqlcommand.Parameters.Add("@COD", SqlDbType.VarChar).Value = updateModel.CashOnDelevery;
                    sqlcommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = updateModel.SelectedCategory;
                    sqlcommand.Parameters.Add("@SelectedSellers", SqlDbType.VarChar).Value = updateModel.SelectedSellers;
                    sqlconnection.Open();
                    return sqlcommand.ExecuteNonQuery();
                }

            }
        }
    }
}