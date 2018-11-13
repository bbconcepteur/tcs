using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;
using PagedList;

namespace HCSV.Business.Models
{
    public class ContentModel<T> where T : class
    {
        public T Value { get; set; }

        public IPagedList<T> ListValues { get; set; }

        public List<string> PageLink { get; set; }

        public string PageName { get; set; }
        
    }
}
