using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Business.Models;
using HCSV.Core;
using HCSV.Core.reCAPTCHA;

namespace HCSV.Web.Controllers
{
    public class AirlineController : BaseController
    {
        // GET: Airline
        public ActionResult Statistics_default()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [CaptchaValidator]
        public async Task<ActionResult> Statistics(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await UnitOfWork.AccountBusiness.Login(model, Constants.LoginType.USER_TYPE_AIRLINE);
                if (loginResult != null)
                {
                    if (loginResult.status != 1)
                    {
                        //bblock user
                        return RedirectToAction("Statistics_default", model);
                    }
                    return View();
                }
            }
            return RedirectToAction("Statistics_default", model);
        }
    }
}