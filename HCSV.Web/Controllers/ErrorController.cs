using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Error500()
        {
            var menus = UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId, DefaultLanguageId);
            return View(menus);
        }
        public ActionResult Error404()
        {
            var menus = UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId, DefaultLanguageId);
            return View(menus);
        }
        public ActionResult Error403()
        {
            var menus = UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId, DefaultLanguageId);
            return View(menus);
        }
    }
}