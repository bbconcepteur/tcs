using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HCSV.Business.Models;
using HCSV.Models;

namespace HCSV.Web.Controllers
{
    public class ContactsController : BaseController
    {
        // GET: Contacts
        public  async  Task<ActionResult> Index(int? contentID)
        {
            var contactModel = await UnitOfWork.ContactBusiness.GetContacts(LanguageId, DefaultLanguageId, contentID ?? 0);
            return View(contactModel);
        }
    }
}