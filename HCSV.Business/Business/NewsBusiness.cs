using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Core;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface INewsBusiness : IRepository<jos_content>
    {
        Task<jos_content> GetContents(long langId, int categoryId, int pageNumber);

        Task<jos_content> GetDetails(long langId, int id);

        Task<jos_content> GetAbout(long langId);
    }
    public class NewsBusiness : Repository<jos_content>, INewsBusiness
    {
        public NewsBusiness()
        {
            db = new TCSEntities();
        }

        public NewsBusiness(TCSEntities db) : base(db) { }

        public Task<jos_content> GetContents(long langId, int categoryId, int pageNumber)
        {
            return Task.Run(() =>
            {
                jos_content content = new jos_content();
                var objCategory = db.jos_categories.AsNoTracking().FirstOrDefault(x => x.id == categoryId) ?? new jos_categories();
                content.name_page = objCategory.name;

                var lstContents = GetMany(x => (x.lang_id == langId) && (x.catid == categoryId) && (x.state == 1)).OrderByDescending(y => y.publish_up).ToList();

                List<jos_content> lstResutOfContent = new List<jos_content>();

                if (lstContents.Any())
                {
                    //Get Paging                
                    pageNumber = (pageNumber == 0 ? 1 : pageNumber);

                    int i = 0;

                    int intTotalOfContent = lstContents.Count();
                    int intPageTotal = (intTotalOfContent / Constants.NUMBER_OF_CONTENTS_ON_A_PAGE) + ((intTotalOfContent % Constants.NUMBER_OF_CONTENTS_ON_A_PAGE > 0) ? 1 : 0);
                    content.PAGING_LINK = new List<string>();
                    string strCss = "";
                    for (i = 1; i <= intPageTotal; i++)
                    {
                        if (i == pageNumber) strCss = " class=\"current\"";
                        else strCss = "";
                        content.PAGING_LINK.Add(String.Format("<li{0}><a href=\"?catID={1}&page={2}\"><strong>{3}</strong></a></li>", strCss, categoryId, i, i));
                    }

                    int intBegin = (pageNumber - 1) * Constants.NUMBER_OF_CONTENTS_ON_A_PAGE + 1;
                    int intEnd = pageNumber * Constants.NUMBER_OF_CONTENTS_ON_A_PAGE;
                    //reset value
                    i = 0;

                    //get Contents
                    foreach (var item in lstContents)
                    {
                        i++;
                        if ((intBegin <= i) && (i <= intEnd))
                        {
                            lstResutOfContent.Add(item);
                        }
                    }
                }
                content.NEWS = lstResutOfContent;

                return content;
            });


        }

        public Task<jos_content> GetDetails(long langId, int id)
        {
            return Task.Run(() =>
            {
                int defaultLanguage = 0;
                var objLang = db.jos_languages.AsNoTracking().FirstOrDefault(x => x.default_status == 1);
                if (objLang != null)
                {
                    defaultLanguage = (int)objLang.lang_id;
                }
                else defaultLanguage = Constants.NUMBER_INVALID_INTEGER;

                jos_content objContent = GetSingle(x => (x.id == id) && (x.state == 1));

                if (objContent != null)
                {
                    if (objContent.lang_id == langId) //in case of News
                    {
                        return objContent;
                    }
                    else if ((langId != defaultLanguage) && (objContent.lang_id == defaultLanguage)) //in case of not be News
                    {
                        objContent = db.jos_content.AsNoTracking().Join(db.jos_language_translation.AsNoTracking(), C => C.id, L => L.reference_id, (C, L) => new { C, L })
                                                           .Where(x => (x.L.origin_id != x.L.reference_id) && (x.L.origin_id == objContent.id)
                                                               && (Constants.TBL_JOS_CONTENT.Equals(x.L.reference_table))).Select(y => y.C).FirstOrDefault();
                    }
                }

                return objContent;
            });
        }

        public Task<jos_content> GetAbout(long langId)
        {
            return null;
        }
    }
}
