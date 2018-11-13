using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HCSV.Business.Models;
using HCSV.Core;
using HCSV.Models;
using HCSV.Models.ViewModels;

namespace HCSV.Business.Business
{
    public interface IDashbroadBusiness
    {
        Task<DashbroadModel> GetDashbroad(long langId);
    }
    public class DashbroadBusiness : IDashbroadBusiness
    {
        private readonly TCSEntities db;
        public DashbroadBusiness(TCSEntities db)
        {
            this.db = db;
        }

        public Task<DashbroadModel> GetDashbroad(long langId)
        {
            return Task.Run(() =>
            {
                var returnResult = new DashbroadModel();

                var newsBusiness = new NewsBusiness(db);

                returnResult.MissionArea =
                    newsBusiness.GetSingle(s => s.lang_id == langId && s.content_type == Constants.TcsContentType.MISSION_AREA) ??
                    new jos_content();

                returnResult.ValueArea = newsBusiness.GetSingle(s => s.lang_id == langId && s.content_type == Constants.TcsContentType.VALUE_AREA) ??
                    new jos_content();

                returnResult.VisionArea = newsBusiness.GetSingle(s => s.lang_id == langId && s.content_type == Constants.TcsContentType.VISION_AREA) ??
                    new jos_content();

                returnResult.TcsNews = newsBusiness.GetTopNews(langId, Constants.TcsContentType.TCS_NEWS, 4);

                returnResult.CustomerNews = newsBusiness.GetTopNews(langId, Constants.TcsContentType.CUSTOMER_NEWS, 4);

                returnResult.IndustrialNews = newsBusiness.GetTopNews(langId, Constants.TcsContentType.INDUSTRIAL_NEWS, 4);

                return returnResult;
            });
        }
    }
}
