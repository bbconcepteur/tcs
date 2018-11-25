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
        jos_language_translation GetTranslatetion(long sourceId, string referentTable);

        List<jos_language_translation> GetTranslatetions(List<long> sourceId, string referentTable);
        List<jos_language_translation> GetTranslatetions(string referentTable);
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

        public jos_language_translation GetTranslatetion(long sourceId, string referentTable)
        {
            var translateContent = GetSingle(s => s.origin_id == sourceId &&
                                                  s.origin_id != s.reference_id
                                                  && referentTable.Equals(s.reference_table));

            return translateContent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="referentTable"></param>
        /// <returns></returns>
        public List<jos_language_translation> GetTranslatetions(List<long> sourceId, string referentTable)
        {
            var translateContent = GetMany(s => s.origin_id != s.reference_id && sourceId.Contains(s.origin_id) && referentTable.Equals(s.reference_table)).ToList();

            return translateContent;
        }

        public List<jos_language_translation> GetTranslatetions(string referentTable)
        {
            var translateContent = GetMany(s => s.origin_id != s.reference_id && referentTable.Equals(s.reference_table)).ToList();

            return translateContent;
        }
    }
}
