using System;
using System.Globalization;
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
        private long _defaultLangId;
        protected long LanguageId
        {
            get
            {
                if (Session[Constants.Session.SESSION_LANGUAGE_ID] != null)
                {
                    _languageId = int.Parse(Session[Constants.Session.SESSION_LANGUAGE_ID].ToString());
                }
                else
                {
                    string cultureName = "vi"; //default vietnamese
                    _languageId = UnitOfWork.LanguageBusiness.GetLanguageId(cultureName);
                    cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
                    Session[Constants.Session.SESSION_CURRENT_CULTURE] = cultureName;
                    Session[Constants.Session.SESSION_LANGUAGE_ID] = _languageId;
                }

                return _languageId;
            }
        }

        protected long DefaultLanguageId
        {
            get
            {
                if (Session[Constants.Session.SESSION_DEFAULT_LANGUAGE_ID] != null)
                {
                    _defaultLangId = int.Parse(Session[Constants.Session.SESSION_DEFAULT_LANGUAGE_ID].ToString());
                }
                else
                {
                    _defaultLangId = UnitOfWork.LanguageBusiness.GetDefaultLanguage();
                    Session[Constants.Session.SESSION_DEFAULT_LANGUAGE_ID] = _defaultLangId;
                }
                return _defaultLangId;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            HttpContext.Application[Constants.Page.PAGE_TITLE] = HttpContext.Application[Constants.Page.HOME_PAGE_TITLE];

            if (Request.RawUrl.Equals("/") || Request.RawUrl.ToLower().Contains("/home/"))
            {
                // index
                HttpContext.Application[Constants.Page.PAGE_BANNER] = "/Content/BoostrapTemplate/img/header-bg.jpg";
                HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = "Home page";
                HttpContext.Application[Constants.Page.MENU_TITLE] = "Index";
            }
            else
            {
                long menuId = Constants.NUMBER_INVALID_INTEGER;
                long sessionMenuId = Constants.NUMBER_INVALID_INTEGER;
                if (Session[Constants.Session.SESSION_MENU_ID] != null)
                {
                    //Muc dich de tranh query qua nhieu vao db
                    sessionMenuId = long.Parse(Session[Constants.Session.SESSION_MENU_ID].ToString());
                }
                if (Request.QueryString["menu"] != null)
                {
                    long.TryParse(Request["menu"], out menuId);
                }

                if (menuId == sessionMenuId && sessionMenuId != Constants.NUMBER_INVALID_INTEGER)
                {
                    if (Session[Constants.Session.SESSION_BANNER_MENU_PATH] != null)
                        HttpContext.Application[Constants.Page.PAGE_BANNER] = Session[Constants.Session.SESSION_BANNER_MENU_PATH].ToString();
                    if (Session[Constants.Session.PARENT_MENU_TITLE] != null)
                        HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = Session[Constants.Session.PARENT_MENU_TITLE].ToString();
                    return;
                }

                if (menuId > 0)
                {
                    Session[Constants.Session.SESSION_MENU_ID] = menuId;
                    var menu = UnitOfWork.MenuBusiness.GetMenuById(LanguageId, DefaultLanguageId, menuId);
                    if (menu != null)
                    {
                        var parentMenu = UnitOfWork.MenuBusiness.GetMenuById(LanguageId, DefaultLanguageId, menu.parent);
                        if (parentMenu != null)
                        {
                            HttpContext.Application[Constants.Page.PAGE_BANNER] = parentMenu.@params;
                            HttpContext.Application[Constants.Page.PARENT_MENU_TITLE] = parentMenu.name;
                            Session[Constants.Session.SESSION_BANNER_MENU_PATH] = parentMenu.@params;
                            Session[Constants.Page.PARENT_MENU_TITLE] = parentMenu.name;
                        }
                        HttpContext.Application[Constants.Page.MENU_TITLE] = menu.name;
                    }
                }
                else
                {
                    HttpContext.Application[Constants.Page.PAGE_BANNER] = "/Media/Images/themes/orange/images/banner.jpg";
                    Session[Constants.Session.SESSION_BANNER_MENU_PATH] = "/Media/Images/themes/orange/images/banner.jpg";
                }
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
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(CultureInfo.GetCultureInfo(cultureName));
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = info;
            return base.BeginExecuteCore(callback, state);
        }
    }
}