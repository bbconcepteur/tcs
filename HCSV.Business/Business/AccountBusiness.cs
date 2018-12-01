using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Business.Models;
using HCSV.Core;
using HCSV.Models;

namespace HCSV.Business.Business
{
    public interface IAccountBusiness : IRepository<jos_awb_users>
    {
        Task<jos_awb_users> Login(LoginModel model, string loginType);

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

        public Task<jos_awb_users> Login(LoginModel model, string loginType)
        {
            return Task.Run(() =>
            {
                if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                    return null;
                string password = Utils.GetMd5(model.Password);
                var user =
                    GetSingle(
                        s =>
                            model.UserName.ToLower().Equals(s.user_name.ToLower()) &&
                            password.ToUpper().Equals(s.user_passwd.ToUpper()));
                return user;
            });
        }
    }
}
