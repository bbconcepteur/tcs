using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;

namespace HCSV.Business.Models
{
    public class ContactModel
    {
        public jos_content Contact { get; set; }

        public  List<jos_contact> ListContacts { get; set; } 
    }
}
