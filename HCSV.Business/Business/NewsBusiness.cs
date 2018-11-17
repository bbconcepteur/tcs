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

        Task<ContentModel<jos_content>> GetContents(long langId, string contentType, int pageNumber);

        Task<ContentModel<jos_content>> GetDetails(long langId, int id, long? defaultLang = null);

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

        public Task<ContentModel<jos_content>> GetDetails(long langId, int id, long? defaultLang = null)
        {
            return Task.Run(() =>
            {
                var contentDetail = new ContentModel<jos_content>();
                var objContent = GetSingle(x => (x.id == id) && (x.state == 1) && x.lang_id == langId);

                // Trường hợp không tìm thấy tin => đổi language
                if (objContent == null)
                {
                    var translateContent =
                        db.jos_language_translation.AsNoTracking().FirstOrDefault(s => s.language_id == langId
                        && (s.origin_id == id || s.reference_id == id) && s.origin_id != s.reference_id
                        && Constants.TranslateTable.TBL_JOS_CONTENT.Equals(s.reference_table));
                    if (translateContent != null)
                    {
                        objContent = GetSingle(x => (x.id == translateContent.reference_id) && (x.state == 1) && x.lang_id == langId);
                    }
                }

                contentDetail.Value = objContent;
                return contentDetail;
            });
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
