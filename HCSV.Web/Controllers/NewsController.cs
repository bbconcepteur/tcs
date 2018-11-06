using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HCSV.Web.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public async Task<ActionResult> Index(int? page,int? catId)
        {
            HttpContext.Application[HCSV.Core.Constants.PAGE_TITLE] = Resources.GlobalResource.TIN_TCS;
            var contents = await UnitOfWork.NewsBusiness.GetContents(LanguageId, catId ?? 0, page ?? 1);
            return View(contents);
        }

        public async Task<ActionResult> Details(int? contentID)
        {
            var contents = await UnitOfWork.NewsBusiness.GetDetails(LanguageId, contentID ?? 0);
            HttpContext.Application[HCSV.Core.Constants.PAGE_TITLE] = contents.Value.title;
            return View(contents);
        }
    }
}