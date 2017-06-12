using EWATER.Common;
using EWATER.Entity;
using EWATER.Models;
using EWATER.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class AdProductController : Controller
    {
        private AdProductService objProduct;
        public AdProductController()
        {
            this.objProduct = new AdProductService();
        }
        // GET: AdProduct
        public ActionResult ProductView()
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

        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            object product = null;
            try
            {
                object[] parameters = {
                id
            };
                product = this.objProduct.GetbyID(parameters);
            }
            catch
            { }
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(ProductEntity model)
        {
            int result = 0;
            bool status = false;
            byte[] image = new byte[model.Image.ContentLength];
            model.Image.InputStream.Read(image, 0, model.Image.ContentLength);

            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                        model.ProductName,
                        model.Price,
                        image


                };
                    result = objProduct.Insert(parameters);
                    if (result == 1)
                    {
                        status = true;
                        return Json(new
                        {
                            success = status,
                            message = Constants.AddSuccess
                        });
                    }
                }
                catch
                { }
            }
            return Json(new
            {
                success = status,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray(),
                message = Constants.AddFail
            });
        }

        [HttpPost]
        public JsonResult Update(ProductEntity model)
        {
            int result = 0;
            bool status = false;           

            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters;
                    if (model.Image != null)
                    {
                        byte[] image = new byte[model.Image.ContentLength];
                        model.Image.InputStream.Read(image, 0, model.Image.ContentLength);
                        parameters = new object[] {
                            model.ProductID,
                            model.ProductName,
                            model.Price,
                            image
                         };
                    }
                    else
                    {
                        parameters = new object[] {
                            model.ProductID,
                            model.ProductName,
                            model.Price
                         };
                    }

                    result = objProduct.Update(parameters);
                    if (result == 1)
                    {
                        status = true;
                        return Json(new
                        {
                            success = status,
                            message = Constants.UpdateSuccess
                        });
                    }
                }
                catch
                { }
            }
            return Json(new
            {
                success = status,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray(),
                message = Constants.UpdateFail
            });
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            int result = 0;
            bool status = false;
            try
            {
                object[] parameters = {
                id
            };
                result = objProduct.Delete(parameters);
                if (result == 1)
                {
                    status = true;
                    return Json(new
                    {
                        success = status,
                        message = Constants.DeleteSuccess
                    });
                }
            }
            catch
            { }
            return Json(new
            {
                success = status,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray(),
                message = Constants.DeleteFail
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}