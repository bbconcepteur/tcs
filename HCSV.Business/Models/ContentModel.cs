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

        public string name_URL { get; set; }

        public jos_content LEFT { get; set; }
        public jos_content RIGHT { get; set; }
        public jos_content MIDDLE { get; set; }
        public jos_content POS_1 { get; set; }
        public IEnumerable<jos_content> POS_1_LIST { get; set; }
        public jos_content POS_2 { get; set; }
        public IEnumerable<jos_content> POS_2_LIST { get; set; }

        public jos_content POS_3 { get; set; }
        public IEnumerable<jos_content> POS_3_LIST { get; set; }
        public jos_content POS_4 { get; set; }
        public IEnumerable<jos_content> POS_4_LIST { get; set; }
        public IEnumerable<jos_content> BANNER { get; set; }
        public IEnumerable<jos_content> NEWS { get; set; }
        

        public jos_content FOOTER { get; set; }
        public jos_content TOP { get; set; }
        public IEnumerable<jos_menu> LEFT_MENU { get; set; }

        public IEnumerable<jos_contact> CONTACTS { get; set; }
    }
}
