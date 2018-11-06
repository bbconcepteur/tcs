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
            var menus = UnitOfWork.MenuBusiness.GetTopMenu(LanguageId);

            return PartialView("~/Views/Shared/_Menu.cshtml", menus);
        }

        public ActionResult BottomMenu()
        {
            var menus = UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId);

            return PartialView("~/Views/Shared/_Footer.cshtml", menus);
        }

    }
}