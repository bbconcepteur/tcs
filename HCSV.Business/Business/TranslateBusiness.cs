using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Core;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface ITranslateBusiness : IRepository<jos_language_translation>
    {
        jos_language_translation GetTranslatetion(long langId, int sourceId, string referentTable);

        jos_content GetNewsTranslatetion(long langId, int sourceId);

        jos_menu GetMenuTranslatetion(long langId, int sourceId);

        /*jos_links GetLinkTranslatetion(long langId, int sourceId, string referentTable);*/
    }

    public class TranslateBusiness : Repository<jos_language_translation>, ITranslateBusiness
    {
        public TranslateBusiness(TCSEntities db) : base(db)
        {

        }

        public TranslateBusiness()
        {
            db = new TCSEntities();
        }

        public jos_language_translation GetTranslatetion(long langId, int sourceId, string referentTable)
        {
            var translateContent = GetSingle(s => s.language_id == langId
                                                  && s.origin_id == sourceId &&
                                                  s.origin_id != s.reference_id
                                                  && referentTable.Equals(s.reference_table));
            
            return translateContent;
        }

        public jos_content GetNewsTranslatetion(long langId, int sourceId)
        {
            var translateContent = GetTranslatetion(langId, sourceId, Constants.TranslateTable.TBL_JOS_CONTENT);
            var news =
                db.jos_content.AsNoTracking()
                    .FirstOrDefault(s => s.lang_id == langId && s.state == 1 && s.id == translateContent.reference_id);
            return news;
        }

        public jos_menu GetMenuTranslatetion(long langId, int sourceId)
        {
            var translateContent = GetTranslatetion(langId, sourceId, Constants.TranslateTable.TBL_JOS_MENU);
            var menu =
                db.jos_menu.AsNoTracking()
                    .FirstOrDefault(s => s.lang_id == langId && s.published == 1 && s.id == translateContent.reference_id);
            return menu;
        }

        /*public jos_links GetLinkTranslatetion(long langId, int sourceId, string referentTable)
        {
            var translateContent = GetTranslatetion(langId, sourceId, Constants.TranslateTable.TBL_JOS_CONTACT);
        }*/
    }
}
