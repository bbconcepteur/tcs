using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Business.Models;
using HCSV.Core;
using HCSV.Models;
using PagedList;

namespace HCSV.Business.Business
{
    public interface INewsBusiness : IRepository<jos_content>
    {
        Task<ContentModel<jos_content>> GetContents(long langId, int categoryId, int pageNumber);

        Task<ContentModel<jos_content>> GetServices(long langId, long defaultLang, int categoryId);

        Task<ContentModel<jos_content>> GetContents(long langId, string contentType, int pageNumber);

        Task<ContentModel<jos_content>> GetDetails(long langId, int id, long defaultLang);

        jos_content GetDetail(long langId, int id, long defaultLang);

        List<jos_content> GetTopNews(long langId, string contentType, int takeNumberOfRow);
    }
    public class NewsBusiness : Repository<jos_content>, INewsBusiness
    {
        public NewsBusiness()
        {
            db = new TCSEntities();
        }

        public NewsBusiness(TCSEntities db) : base(db) { }

        public Task<ContentModel<jos_content>> GetContents(long langId, int categoryId, int pageNumber)
        {
            return Task.Run(() =>
            {
                var content = new ContentModel<jos_content>();
                var objCategory = db.jos_categories.AsNoTracking().FirstOrDefault(x => x.id == categoryId) ?? new jos_categories();
                content.PageName = objCategory.name;
                var lstContents = GetMany(x => (x.lang_id == langId) && (x.catid == categoryId) && (x.state == 1)).OrderByDescending(y => y.publish_up).ToList();
                content.ListValues = lstContents.ToPagedList(pageNumber, 10);
                return content;
            });
        }

        public Task<ContentModel<jos_content>> GetServices(long langId, long defaultLang, int categoryId)
        {
            return Task.Run(() =>
            {
                var content = new ContentModel<jos_content>();
                var defaultContents = GetMany(x => (x.lang_id == defaultLang) && (x.catid == categoryId) && (x.state == 1)).OrderByDescending(y => y.publish_up).ToList();

                if (langId != defaultLang)
                {
                    var defaultIds = defaultContents.Select(s => s.id).ToList();
                    ITranslateBusiness translate = new TranslateBusiness(db);

                    var transContents =
                        translate.GetTranslatetions(defaultIds, Constants.TranslateTable.TBL_JOS_CONTENT)
                            .Select(s => (long) s.reference_id)
                            .ToList();

                    var currentContents = GetMany(s => s.lang_id == langId && transContents.Contains(s.id)).ToList();
                    content.ListValues = currentContents.ToPagedList(1, 99999);
                }
                else
                {
                    content.ListValues = defaultContents.ToPagedList(1, 99999);
                }
                return content;
            });
        }

        public Task<ContentModel<jos_content>> GetContents(long langId, string contentType, int pageNumber)
        {
            return Task.Run(() =>
            {
                var content = new ContentModel<jos_content>();

                var lstContents = GetMany(x => (x.lang_id == langId) && (x.content_type == contentType) && (x.state == 1)).OrderByDescending(y => y.publish_up).ToList();
                content.ListValues = lstContents.ToPagedList(pageNumber, 10);

                //TODO: kha nang phai thay doi cho nay nua
                if (lstContents.Count > 0)
                {
                    var objCategory = db.jos_categories.AsNoTracking().FirstOrDefault(x => x.id == lstContents[0].catid) ?? new jos_categories();
                    content.PageName = objCategory.name;
                }
                return content;
            });
        }

        /// <summary>
        /// Get new details
        /// </summary>
        /// <param name="langId">Current language</param>
        /// <param name="id">News id</param>
        /// <param name="defaultLang">Default language</param>
        /// <returns></returns>
        public Task<ContentModel<jos_content>> GetDetails(long langId, int id, long defaultLang)
        {
            return Task.Run(() =>
            {
                var contentDetail = new ContentModel<jos_content>();
                var objContent = GetSingle(x => (x.id == id) && (x.state == 1));
                if (objContent != null)
                {
                    // Trường hợp language
                    if (defaultLang != langId)
                    {
                        var translate = new TranslateBusiness(db);
                        var translateContent = translate.GetTranslatetion(objContent.id,
                            Constants.TranslateTable.TBL_JOS_CONTENT);

                        if (translateContent!= null)
                        {
                            objContent = GetSingle(x => (x.id == translateContent.reference_id) && x.lang_id == langId);
                        }
                    }
                }

                contentDetail.Value = objContent;
                return contentDetail;
            });
        }

        public jos_content GetDetail(long langId, int id, long defaultLang)
        {
            var objContent = GetSingle(x => (x.id == id) && (x.state == 1));
            if (objContent != null)
            {
                // Trường hợp language
                if (defaultLang != langId)
                {
                    var translate = new TranslateBusiness(db);
                    var translateContent = translate.GetTranslatetion(objContent.id,
                        Constants.TranslateTable.TBL_JOS_CONTENT);

                    if (translateContent != null)
                    {
                        objContent = GetSingle(x => (x.id == translateContent.reference_id) && x.lang_id == langId);
                    }
                }
            }
            return objContent;
        }

        public List<jos_content> GetTopNews(long langId, string contentType, int takeNumberOfRow)
        {
            return GetMany(x => x.lang_id == langId && (x.content_type == contentType) && (x.state == 1))
                        .OrderByDescending(y => y.publish_up)
                        .Take(takeNumberOfRow)
                        .ToList();
        }

    }
}
