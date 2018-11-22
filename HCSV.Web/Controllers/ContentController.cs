﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Business;

namespace HCSV.Web.Controllers
{
    public class ContentController : BaseController
    {
        // GET: News
        public async Task<ActionResult> Index(int? page, int? catId)
        {
            var contents = await UnitOfWork.NewsBusiness.GetContents(LanguageId, catId ?? 0, page ?? 1);
            return View(contents);
        }

        public async Task<ActionResult> Details(int? id)
        {
            var contents = await UnitOfWork.NewsBusiness.GetDetails(LanguageId, id ?? 0, DefaultLanguageId);
            return View(contents);
        }
    }
}