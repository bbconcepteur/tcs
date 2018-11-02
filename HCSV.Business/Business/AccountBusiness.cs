using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface IAccountBusiness : IRepository<jos_awb_users>
    {
    }
    public class AccountBusiness : Repository<jos_awb_users>, IAccountBusiness
    {
        public AccountBusiness(TCSEntities db) : base(db)
        {
            
        }

        public AccountBusiness()
        {
            db = new TCSEntities();
        }
    }
}
