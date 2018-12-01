using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HCSV.Web.Controllers
{
    public class ServicesController : BaseController
    {
        // GET: Services
        public async Task<ActionResult> Index(int? page, int? catId)
        {
            //HttpContext.Application[HCSV.Core.Constants.Page.PAGE_TITLE] = Resources.GlobalResource.TIN_TCS;
            var contents = await UnitOfWork.NewsBusiness.GetServices(LanguageId, DefaultLanguageId, catId ?? 0);
            return View(contents);
        }

        public async Task<ActionResult> Tab(int? catId)
        {
            var contents = await UnitOfWork.NewsBusiness.GetServices(LanguageId, DefaultLanguageId, catId ?? 0);
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