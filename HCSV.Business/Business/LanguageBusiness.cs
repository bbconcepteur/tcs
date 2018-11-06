using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;
using HCSV.Core;

namespace HCSV.Business.Business
{
    public interface ILanguageBusiness : IRepository<jos_languages>
    {
        long GetLanguageId(string cultureName);

        int GetDefaultLanguage();
    }

    public class LanguageBusiness : Repository<jos_languages>, ILanguageBusiness
    {
        public LanguageBusiness(TCSEntities db) : base(db)
        {

        }

        public LanguageBusiness()
        {
            db = new TCSEntities();
        }

        public long GetLanguageId(string cultureName)
        {
            return (GetSingle(s => s.sef.Equals(cultureName)) ?? new jos_languages()).lang_id;
        }

        public int GetDefaultLanguage()
        {
            var objLang = GetSingle(x => x.default_status == 1);
            if (objLang != null)
            {
                return (int)objLang.lang_id;
            }
            return Constants.NUMBER_INVALID_INTEGER;
        }
    }
}
