using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class GuidesController : BaseController
    {
        // GET: Guides
        public async Task<ActionResult> Index(int? catId)
        {
            //HttpContext.Application[HCSV.Core.Constants.Page.PAGE_TITLE] = Resources.GlobalResource.TIN_TCS;
            var contents = await UnitOfWork.NewsBusiness.GetServices(LanguageId, DefaultLanguageId, catId ?? 0);
            return View(contents);
        }
    }
}