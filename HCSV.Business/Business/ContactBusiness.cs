using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Business.Models;
using HCSV.Core;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface IContactBusiness : IRepository<jos_contact>
    {
        Task<ContactModel> GetContacts(long langId, long defaullangId, int contentId);
    }
    public class ContactBusiness : Repository<jos_contact>, IContactBusiness
    {
        public ContactBusiness()
        {
            db = new TCSEntities();
        }

        public ContactBusiness(TCSEntities db) : base(db)
        {

        }
        public Task<ContactModel> GetContacts(long langId, long defaullangId, int contentId)
        {
            return Task.Run(() =>
            {
                var model = new ContactModel();

                INewsBusiness newsBusiness = new NewsBusiness(db);
                ITranslateBusiness translateBusiness = new TranslateBusiness(db);
                model.Contact = newsBusiness.GetDetail(langId, contentId, defaullangId);


                var defaultContacts = GetMany(s => s.lang_id == defaullangId && s.published).OrderByDescending(y => y.order).ToList();

                var defaultId = defaultContacts.Select(s => (long)s.id).ToList();
                var translateContacts = translateBusiness.GetTranslatetions(defaultId, Constants.TranslateTable.TBL_JOS_CONTACT).Select(s => s.reference_id).ToList();
                model.ListContacts = GetMany(s => translateContacts.Contains(s.id) && s.lang_id == langId).ToList();

                return model;
            });
        }
    }
}
