using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Business.Models;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface IFeedbackBusiness : IRepository<jos_feedback>
    {
        Task Create(Feedback feed);
    }
    public class FeedbackBusiness : Repository<jos_feedback>, IFeedbackBusiness
    {
        public FeedbackBusiness()
        {
            db = new TCSEntities();
        }

        public FeedbackBusiness(TCSEntities db) : base(db)
        {
            
        }

        public Task Create(Feedback feed)
        {
            return Task.Run(() =>
            {
                var feedback = new jos_feedback()
                {
                    Name = feed.Name,
                    Created = DateTime.Now,
                    Email = feed.Email,
                    Message = feed.Message,
                    Subject = feed.Subject
                };
                Create(feedback);
                db.SaveChanges();
            });
        }
    }

}
