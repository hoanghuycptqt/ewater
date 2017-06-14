using EWATER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class AdContactController : Controller
    {
        private ContactService objContact;
        public AdContactController()
        {
            this.objContact = new ContactService();
        }
        // GET: AdContact
        public ActionResult AdContactView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<object> contacts = null;
            try
            {
                object[] parameters = {
                Count
            };
                contacts = objContact.GetAll(parameters);
            }
            catch
            { }
            return Json(contacts.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}