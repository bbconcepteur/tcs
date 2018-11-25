using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Business.Models;
using HCSV.Core.reCAPTCHA;

namespace HCSV.Web.Controllers
{
    public class Offer_AdviseController : BaseController
    {
        // GET: Offer_Advise
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        public async Task<ActionResult> Index(Feedback model)
        {
            if (ModelState.IsValid)
            {
                await UnitOfWork.FeedbackBusiness.Create(model);
            }
            return View();
        }
    }
}