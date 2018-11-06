using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HCSV.Core;
using HCSV.Models;
using HCSV.Models.ViewModels;

namespace HCSV.Business.Business
{
    public interface IDashbroadBusiness
    {
    }
    public class DashbroadBusiness : IDashbroadBusiness
    {
        private readonly TCSEntities db;
        public DashbroadBusiness(TCSEntities db)
        {
            this.db = db;
        }

        
    }
}
