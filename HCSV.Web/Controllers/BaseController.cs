using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HCSV.Business;
using HCSV.Core;
using HCSV.Models;

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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Request.QueryString["menu"] != null)
            {
                long menuId = -1;
                long.TryParse(Request["menu"], out menuId);
                /*if (Session[Constants.SESSION_MENU_ID] != null)
                {
                    //Muc dich de tranh query qua nhieu vao db
                    var sessionMenuId = long.Parse(Session[Constants.SESSION_MENU_ID].ToString());
                    if (menuId == sessionMenuId)
                    {
                        HttpContext.Application[Constants.Page.PAGE_BANNER] = Session[Constants.SESSION_BANNER_MENU_PATH].ToString();
                        HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = Session[Constants.Page.PARENT_MENU_TITLE].ToString();
                        return;
                    }
                }*/

                if (menuId > 0)
                {
                    Session[Constants.SESSION_MENU_ID] = menuId;
                    var menu = UnitOfWork.MenuBusiness.GetMenuById(LanguageId, menuId);
                    if (menu != null)
                    {
                        var parentMenu = UnitOfWork.MenuBusiness.GetMenuById(LanguageId, menu.parent);
                        if (parentMenu != null)
                        {
                            HttpContext.Application[Constants.Page.PAGE_BANNER] = parentMenu.@params;
                            HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = parentMenu.name;
                            Session[Constants.SESSION_BANNER_MENU_PATH] = parentMenu.@params;
                            Session[Constants.Page.PARENT_MENU_TITLE] = parentMenu.name;
                        }
                        HttpContext.Application[Constants.Page.MENU_TITLE] = menu.name;
                    }
                }
                else
                {
                    // index
                    HttpContext.Application[Constants.Page.PAGE_BANNER] = "<img src='/Content/BoostrapTemplate/img/header-bg.jpg' />";
                    HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = "Home page";
                    HttpContext.Application[Constants.Page.MENU_TITLE] = "Index";
                }


                // for test
                HttpContext.Application[Constants.Page.PAGE_BANNER] = "<img src='/Content/BoostrapTemplate/img/header-bg.jpg' />";
            }
            else
            {
                // index
                HttpContext.Application[Constants.Page.PAGE_BANNER] = "<img src='/Content/BoostrapTemplate/img/header-bg.jpg' />";
                HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = "Home page";
                HttpContext.Application[Constants.Page.MENU_TITLE] = "Index";
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