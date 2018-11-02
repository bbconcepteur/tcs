using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HCSV.Business;
using HCSV.Core;

namespace HCSV.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork UnitOfWork = new UnitOfWork();
        private long _languageId;
        protected long LanguageId
        {
            get
            {
                if (Session[Constants.SESSION_LANGUAGE_ID] != null)
                {
                    _languageId = int.Parse(Session[Constants.SESSION_LANGUAGE_ID].ToString());
                }
                else
                {
                    const string cultureName = "vi"; //default vietnamese
                    _languageId = UnitOfWork.LanguageBusiness.GetLanguageId(cultureName);
                    Session[Constants.SESSION_LANGUAGE_ID] = _languageId;
                }
                
                return _languageId;
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName;
            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                cultureName = "vi-vn";//default vietnamese
                cultureCookie = new HttpCookie("_culture");
                cultureCookie.Value = cultureName.ToLower();
                cultureCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cultureCookie);
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(CultureInfo.GetCultureInfo("en-US"));
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = info;
            return base.BeginExecuteCore(callback, state);
        }
    }
}