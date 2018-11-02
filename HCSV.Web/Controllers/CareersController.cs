using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class CareersController : BaseController
    {
        // GET: Careers
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(int? id)
        {
            return View();
        }
    }
}