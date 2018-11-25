using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class ClientsController : BaseController
    {
        // GET: Clients
        public ActionResult Index()
        {

            var links = UnitOfWork.MenuBusiness.GetLinkMenu(LanguageId);
            return View(links);
        }
    }
}