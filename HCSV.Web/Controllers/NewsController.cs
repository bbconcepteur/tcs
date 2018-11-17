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
            HttpContext.Application[HCSV.Core.Constants.Page.PAGE_TITLE] = Resources.GlobalResource.TIN_TCS;
            var contents = await UnitOfWork.NewsBusiness.GetContents(LanguageId, catId ?? 0, page ?? 1);
            return View(contents);
        }

        public async Task<ActionResult> Details(int? contentID)
        {
            var contents = await UnitOfWork.NewsBusiness.GetDetails(LanguageId, contentID ?? 0, DefaultLanguageId);
            string pageTitle = "";
            if (contents.Value != null)
            {
                pageTitle = contents.Value.title;
            }
            else
            {
                pageTitle = Resources.GlobalResource.KHONG_TIM_THAY_DU_LIEU;
            }
            HttpContext.Application[HCSV.Core.Constants.Page.PAGE_TITLE] = pageTitle;
            return View(contents);
        }
    }
}