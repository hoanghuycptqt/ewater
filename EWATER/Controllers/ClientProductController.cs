using EWATER.Entity;
using EWATER.Models;
using EWATER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class ClientProductController : Controller
    {
        private ClientProductService objProduct;

        public ClientProductController()
        {
            this.objProduct = new ClientProductService();
        }
        // GET: ClientProduct
        public ActionResult ClientProductView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<Product> products = null;
            try
            {
                object[] parameters = {
                Count
            };
                products = objProduct.GetAll(parameters);

            }
            catch
            { }
            var listProduct = products.ToList();
            List<ProductList> list = new List<ProductList>();
            for (int i = 0; i < listProduct.Count; i++)
            {
                ProductList model = new ProductList();
                string base64String = Convert.ToBase64String(listProduct[i].Image, 0, listProduct[i].Image.Length);
                model.ProductID = listProduct[i].ProductID;
                model.ProductName = listProduct[i].ProductName;
                model.Price = listProduct[i].Price;
                model.Image = base64String;

                list.Add(model);
            }
            var jsonResult = Json(list, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}