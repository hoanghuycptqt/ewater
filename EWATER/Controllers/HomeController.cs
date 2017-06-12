using EWATER.Models;
using EWATER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomeView()
        {
            return View();
        }

    }
}