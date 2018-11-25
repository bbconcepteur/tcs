using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HCSV.Business;
using HCSV.Models.ViewModels;

namespace HCSV.Web.Controllers
{
    public class SitemapController : BaseController
    {
        // GET: Sitemap
        public ActionResult Index()
        {
            var menus = new List<Menu>();
            menus.AddRange(UnitOfWork.MenuBusiness.GetTopMenu(LanguageId, DefaultLanguageId));
            menus.AddRange(UnitOfWork.MenuBusiness.GetBottomMenu(LanguageId, DefaultLanguageId));
            return View(menus);
        }
    }
}