//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCSV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class jos_contact
    {
        public int id { get; set; }
        public int id_parent { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public Nullable<long> modified_by { get; set; }
        public string name { get; set; }
        public string ext_tel { get; set; }
        public string email { get; set; }
        public int order { get; set; }
        public int lang_id { get; set; }
        public string department_manager { get; set; }
        public string phone { get; set; }
        public string hotline { get; set; }
        public string title_of_manager { get; set; }
        public bool published { get; set; }
    }
}
