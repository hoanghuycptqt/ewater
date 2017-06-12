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
            IEnumerable<object> products = null;
            try
            {
                object[] parameters = {
                Count
            };
                products = objProduct.GetAll(parameters);

            }
            catch
            { }
            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}