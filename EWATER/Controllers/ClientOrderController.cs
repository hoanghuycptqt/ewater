using EWATER.Common;
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
    public class ClientOrderController : Controller
    {
        private ClientOrderService objOrder;

        public ClientOrderController()
        {
            this.objOrder = new ClientOrderService();
        }
        // GET: ClientOrder
        public ActionResult ClientOrderView()
        {
            return View();
        }     

        [HttpPost]
        public JsonResult CreateOrder(OrderEntity model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.OrderID,
                    model.CustomerID,
                    model.OrderDate,
                    model.Status,
                    model.OrderItems
                };
                    result = objOrder.InsertOrder(parameters);
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

        [HttpGet]
        public JsonResult GetbyOrderID(string phoneNumber)
        {
            object order = null;
            try
            {
                object[] parameters = {
                phoneNumber
            };
                order = this.objOrder.GetbyID(parameters);
            }
            catch
            { }
            return Json(order, JsonRequestBehavior.AllowGet);
        }

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
                result = objOrder.Delete(parameters);
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