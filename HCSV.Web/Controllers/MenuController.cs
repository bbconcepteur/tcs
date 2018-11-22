using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Models.ViewModels;

namespace HCSV.Web.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        
        public ActionResult TopMenu()
        {
            var menus = UnitOfWork.MenuBusiness.GetTopMenu(LanguageId, DefaultLanguageId);

            return PartialView("~/Views/Shared/_Menu.cshtml", menus);
        }

        public ActionResult BottomMenu()
        {
            var menus = UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId, DefaultLanguageId);

            return PartialView("~/Views/Shared/_Footer.cshtml", menus);
        }

        public ActionResult BottomLink()
        {
            var menus = UnitOfWork.MenuBusiness.GetLinkMenu(LanguageId);
            return PartialView("~/Views/Shared/_Module_Logo_Footer.cshtml", menus);
        }

    }
}