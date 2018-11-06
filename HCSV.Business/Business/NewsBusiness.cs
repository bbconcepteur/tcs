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

        Task<ContentModel<jos_content>> GetDetails(long langId, int id);

        Task<jos_content> GetAbout(long langId);
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

        public Task<ContentModel<jos_content>> GetDetails(long langId, int id)
        {
            return Task.Run(() =>
            {
                var contentDetail = new ContentModel<jos_content>();
                var objContent = GetSingle(x => (x.id == id) && (x.state == 1) && x.lang_id == langId);
                contentDetail.Value = objContent;
                return contentDetail;
            });
        }

        public Task<jos_content> GetAbout(long langId)
        {
            return null;
        }
    }
}
