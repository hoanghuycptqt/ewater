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
    public class ContactClientController : Controller
    {
        private ContactService objContact;
        public ContactClientController()
        {
            this.objContact = new ContactService();
        }
        // GET: ContactClient
        public ActionResult ContactClientView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Send(ContactClient model)
        {
            int result = 0;
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = {
                    model.Name,
                    model.Phone,
                    model.Email,
                    model.Content
                };
                    result = objContact.Insert(parameters);
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
    }
}