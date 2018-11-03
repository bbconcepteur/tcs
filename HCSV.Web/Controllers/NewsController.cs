using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public async Task<ActionResult> Index(int? page,int? catId)
        {
            var contents = await UnitOfWork.NewsBusiness.GetContents(LanguageId, catId ?? 0, page ?? 1);
            return View(contents);
        }

        public async Task<ActionResult> Details(int? contentID)
        {
            var contents = await UnitOfWork.NewsBusiness.GetDetails(LanguageId, contentID ?? 0);
            return View(contents);
        }
    }
}