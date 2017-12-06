using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagement.DataAccessLayer;
using ProductManagement.Models;
using ProductManagement.ViewModels;
using System.IO;

namespace ProductManagement.Controllers//this is for controller
{
    public class ProductController : Controller
    {
        ProductDAL dbOBJ = new ProductDAL();

        public List<Category> GetCategories()
        {
            return (from data in dbOBJ.CatagoriesTable select data).ToList();
        }

        public List<Seller> GetSellers()
        {
            //return (dbOBJ.SellerTable.Select(x => new Seller {SellerName = x.SellerName,SellerID = x.SellerID })).ToList();

            //return (from data in dbOBJ.SellerTable select new  {data.SellerID,data.SellerName }).ToList();

            //return (from data in dbOBJ.SellerTable select new Seller { SellerName = data.SellerName, SellerID = data.SellerID }).ToList();

            return (from data in dbOBJ.SellerTable select data).ToList();

        }

        public List<ProductGrid> GetGridData()
        {
            return (from data in dbOBJ.ProductsTable
                    select new ProductGrid
                    {
                        ProductID = data.ProductID,
                        ProductName = data.ProductName,
                        Price = data.Price,
                        CashOnDelevery = data.CashOnDelevery
                    }).ToList();
        }

        public Product GetProductDetailsForEdit(int ProductID)
        {
            return (from data in dbOBJ.ProductsTable where data.ProductID == ProductID select data).FirstOrDefault();
        }
        public List<SellerInfo> GetProductSellers(int ProductID)
        {
            return (from data in dbOBJ.sellerInfoTable where data.Product.ProductID == ProductID select data).ToList();
        }

        public ActionResult Products()
        {

            dbOBJ.CatagoriesTable.ToList();
            dbOBJ.ProductsTable.ToList();
            dbOBJ.SellerTable.ToList();
            dbOBJ.sellerInfoTable.ToList();

            var productModel = new ProductListViewModel();
            productModel.CatagoryList = GetCategories();
            productModel.SellerList = GetSellers();


            Product productobj = new Product();


            return View("ViewAddProduct", productModel);

        }


        [HttpPost]
        public ActionResult Create(ProductListViewModel listproductObj)
        {
            //ProductListViewModel listproductObj = new ProductListViewModel();
            Product productObj = new Product();
            var fileName = String.Empty;
            List<ProductGrid> gridInfo = new List<ProductGrid>();
            try
            {
                // null chr=eck
                if (listproductObj.productImage != null && listproductObj.productImage.ContentLength > 0)
                {
                    //var fileName = Path.GetFileName(file.FileName);
                    fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(listproductObj.productImage.FileName);
                    var uploadUrl = Server.MapPath("~/Upload");
                    listproductObj.productImage.SaveAs(Path.Combine(uploadUrl, fileName));
                }

                // todo - check existing product

                productObj.ProductName = listproductObj.ProductName;
                //var CheckProductName = dbOBJ.ProductsTable.Where(x => x.ProductName == productObj.ProductName).FirstOrDefault();
                if (dbOBJ.ProductsTable.Any(x => x.ProductName == productObj.ProductName))
                {
                    return View("ExistingProductInfo");
                }
                else
                {

                    productObj.CashOnDelevery = listproductObj.CashOnDelevery;
                    int catId = Convert.ToInt32(listproductObj.SelectedCategory);
                    productObj.Category = dbOBJ.CatagoriesTable.Where(x => x.CategoryID == catId).FirstOrDefault();
                    productObj.Price = listproductObj.Price;
                    productObj.ImagePath = fileName;
                    Product product = dbOBJ.ProductsTable.Add(productObj);

                    foreach (var sellerId in listproductObj.SelectedSellers)
                    {
                        SellerInfo sellerInfoObj = new SellerInfo();
                        sellerInfoObj.Seller = dbOBJ.SellerTable.Where(x => x.SellerID == sellerId).FirstOrDefault();
                        sellerInfoObj.Product = product;
                        dbOBJ.sellerInfoTable.Add(sellerInfoObj);
                    }

                    dbOBJ.SaveChanges();

                    listproductObj.CatagoryList = GetCategories();
                    listproductObj.SellerList = GetSellers();

                    //listproductObj.gridinfo = GetGridData();

                    gridInfo = GetGridData();


                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Data svaed Failure";
            }
            return View("ProductGrid", gridInfo);
        }

        public ActionResult ShowProductGrid()
        {
            List<ProductGrid> gridInfo = new List<ProductGrid>();
            gridInfo = GetGridData();
            return View("ProductGrid", gridInfo);
        }

        public ActionResult EditProduct(int ProductID)
        {
            ProductListViewModel listViewModelObj = new ProductListViewModel();
            listViewModelObj.ProductID = ProductID;
            var productdetails = GetProductDetailsForEdit(ProductID);
            listViewModelObj.ProductName = productdetails.ProductName;

            listViewModelObj.CatagoryList = GetCategories();
            listViewModelObj.SelectedCategory = productdetails.Category.CategoryID.ToString();
            listViewModelObj.Price = productdetails.Price;
            listViewModelObj.SellerList = GetSellers();
            listViewModelObj.SelectedSellers = GetProductSellers(ProductID).Select(x => x.Seller.SellerID).ToArray();
            //listVieeModelObj.SelectedSellers=productdetails.s

            listViewModelObj.CashOnDelevery = productdetails.CashOnDelevery;
            listViewModelObj.ImagePath = productdetails.ImagePath;
            return PartialView("PartialAddProduct", listViewModelObj);
        }

        public ActionResult DeleteProduct(int ProductID)
        {
            // var duplicates = dbOBJ.sellerInfoTable.AsEnumerable().GroupBy(r => r.Product.ProductID).Where(gr => gr.Count() > 1).Select(g => g.Key);
            var duplicates = dbOBJ.sellerInfoTable.Count(x => x.Product.ProductID == ProductID);

            for (int item = 0; item < duplicates; item++)
            {
                var deleteSellerInfo = dbOBJ.sellerInfoTable.Where(x => x.Product.ProductID == ProductID).FirstOrDefault();

                if (deleteSellerInfo != null)
                {
                    dbOBJ.sellerInfoTable.Remove(deleteSellerInfo);
                    dbOBJ.SaveChanges();
                }
            }


            var deleteProduct = dbOBJ.ProductsTable.Where(x => x.ProductID == ProductID).FirstOrDefault();
            if (deleteProduct != null)
            {
                dbOBJ.ProductsTable.Remove(deleteProduct);
                dbOBJ.SaveChanges();
            }

            List<ProductGrid> gridInfo = new List<ProductGrid>();
            gridInfo = GetGridData();
            return View("ProductGrid", gridInfo);
        }
        [HttpPost]
        public ActionResult UpdateProduct(ProductListViewModel updateModel)
        {
            ProductUpdateDAL updateObj = new ProductUpdateDAL();
            int numberOFRows = updateObj.UpdateProduct(updateModel);
            if (numberOFRows > 0)
            {
                return Json(new { success = true, responseText = " Data updated successfuly " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = " Data has not been Updated " }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}