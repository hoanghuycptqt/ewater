using EWATER.Models;
using EWATER.Services;
using EWATER.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class AdCustomerController : Controller
    {
        private AdCustomerService objCust;
        public AdCustomerController()
        {
            this.objCust = new AdCustomerService();
        }

        public ActionResult CustomerView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<object> customers = null;
            try
            {
                object[] parameters = {
                Count
            };
                customers = objCust.GetAll(parameters);
            }
            catch
            { }
            return Json(customers.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Get Single Customer  
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            object customer = null;
            try
            {
                object[] parameters = {
                id
            };
                customer = this.objCust.GetbyID(parameters);
            }
            catch
            { }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // POST: Save New Customer  
        [HttpPost]
        public JsonResult Insert(Customer model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.CustomerName,
                    model.CustomerEmail,
                    model.PhoneNumber,
                    model.Address,
                    model.Ward
                };
                    result = objCust.Insert(parameters);
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

        // POST: Update Existing Customer  
        [HttpPost]
        public JsonResult Update(Customer model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.CustomerID,
                    model.CustomerName,
                    model.CustomerEmail,
                    model.PhoneNumber,
                    model.Address,
                    model.Ward
                };
                    result = objCust.Update(parameters);
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
        // DELETE: Delete Customer  
        [HttpDelete]
        public JsonResult Delete(string phoneNumber)
        {
            int result = 0;
            bool status = false;
            try
            {
                object[] parameters = {
                phoneNumber
            };
                result = objCust.Delete(parameters);
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
        [HttpGet]
        public JsonResult GetCustomerByPhone(string phoneNumber)
        {
            object customer = null;
            try
            {
                object[] parameters = {
                phoneNumber
            };
                customer = this.objCust.GetByPhone(parameters);
            }
            catch
            { }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}