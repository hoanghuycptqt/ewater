using EWATER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EWATER.Controllers
{
    public class AdReportController : Controller
    {
        private AdReportService objReport;
        public AdReportController()
        {
            this.objReport = new AdReportService();
        }
        // GET: AdReport
        public ActionResult ReportView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10;
            IEnumerable<object> report = null;
            try
            {
                object[] parameters = {
                Count
            };
                report = objReport.GetAll(parameters);
            }
            catch
            { }
            return Json(report.ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}