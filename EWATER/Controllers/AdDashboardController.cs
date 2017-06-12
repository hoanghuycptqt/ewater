using EWATER.Common;
using EWATER.Models;
using EWATER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class AdDashboardController : Controller
    {
        private AdDashboardService objOrder;
        public AdDashboardController()
        {
            this.objOrder = new AdDashboardService();
        }
        // GET: AdDashboard
        public ActionResult DashboardView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<object> orders = null;
            try
            {
                object[] parameters = {
                Count
            };
                orders = objOrder.GetAll(parameters);
            }
            catch(Exception ex)
            { ex.ToString(); }
           
            return Json(orders.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetYearChart(string date)
        {
            int Count = 10;
            IEnumerable<object> orders = null;
            try
            {
                object[] parameters = {
                date
            };
                orders = objOrder.GetYearChart(parameters);
            }
            catch (Exception ex)
            { ex.ToString(); }

            return Json(orders.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMonthChart(string date)
        {
            int year = Int16.Parse(date.Substring(0, 4));
            int month = Int16.Parse(date.Substring(4, date.Length-4));
            IEnumerable<object> orders = null;
            try
            {
                object[] parameters = {
                month,
                year
            };
                orders = objOrder.GetMonthChart(parameters);
            }
            catch (Exception ex)
            { ex.ToString(); }

            return Json(orders.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllStaff()
        {
            int Count = 10;
            IEnumerable<object> staffs = null;
            try
            {
                object[] parameters = {
                Count
            };
                staffs = objOrder.GetAllStaff(parameters);
            }
            catch (Exception ex)
            { ex.ToString(); }

            return Json(staffs.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllProduct()
        {
            int Count = 10;
            IEnumerable<object> products = null;
            try
            {
                object[] parameters = {
                Count
            };
                products = objOrder.GetAllProduct(parameters);
            }
            catch (Exception ex)
            { ex.ToString(); }

            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateOrder(Order model)
        {
            int result = 0;
            bool status = false;

                try
                {
                    object[] parameters = {
                        model.OrderID,
                        model.StaffID,
                        model.Status
                };
                    result = objOrder.Update(parameters);
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
            
            return Json(new
            {
                success = status,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray(),
                message = Constants.UpdateFail
            });
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}