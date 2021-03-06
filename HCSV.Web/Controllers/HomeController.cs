﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Core;

namespace HCSV.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            var model = await UnitOfWork.DashbroadBussiness.GetDashbroad(LanguageId);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SwitchLanguage()
        {
            // Validate input
            string culture = CultureHelper.GetImplementedCulture(Request.QueryString["lang"]);
            Session[Constants.Session.SESSION_LANGUAGE_ID] = UnitOfWork.LanguageBusiness.GetLanguageId(Request.QueryString["lang"]);
            Session[Constants.Session.SESSION_CURRENT_CULTURE] = culture;

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires.AddDays(30);// = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cookie);

            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return RedirectToAction("Index", "home");
            }
        }
    }
}