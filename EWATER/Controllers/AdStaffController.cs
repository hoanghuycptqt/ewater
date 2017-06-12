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
    public class AdStaffController : Controller
    {
        private AdStaffService objStaff;
        public AdStaffController()
        {
            this.objStaff = new AdStaffService();
        }
        // GET: AdStaff
        public ActionResult StaffView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<object> staffs = null;
            try
            {
                object[] parameters = {
                Count
            };
                staffs = objStaff.GetAll(parameters);
            }
            catch
            { }
            return Json(staffs.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Get Single Customer  
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            object staff = null;
            try
            {
                object[] parameters = {
                id
            };
                staff = this.objStaff.GetbyID(parameters);
            }
            catch
            { }
            return Json(staff, JsonRequestBehavior.AllowGet);
        }

        // POST: Save New Customer  
        [HttpPost]
        public JsonResult Insert(Staff model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.StaffName,
                    model.StaffEmail,
                    model.StaffPhone,
                    model.JobTitle
                };
                    result = objStaff.Insert(parameters);
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
        public JsonResult Update(Staff model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.StaffID,
                    model.StaffName,
                    model.StaffEmail,
                    model.StaffPhone,
                    model.JobTitle
                };
                    result = objStaff.Update(parameters);
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
        public JsonResult Delete(int id)
        {
            int result = 0;
            bool status = false;
            try
            {
                object[] parameters = {
                id
            };
                result = objStaff.Delete(parameters);
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