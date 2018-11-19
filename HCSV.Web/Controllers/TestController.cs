using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HCSV.Core.reCAPTCHA;

namespace HCSV.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult Index(bool captchaValid)
        {
            if (ModelState.IsValid)
            {
                String sss = "";
            }
            return View();
        }
    }
}