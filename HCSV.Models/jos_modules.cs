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
    
    public partial class jos_modules
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int ordering { get; set; }
        public string position { get; set; }
        public long checked_out { get; set; }
        public System.DateTime checked_out_time { get; set; }
        public bool published { get; set; }
        public string module { get; set; }
        public int numnews { get; set; }
        public byte access { get; set; }
        public byte showtitle { get; set; }
        public string @params { get; set; }
        public short iscore { get; set; }
        public short client_id { get; set; }
        public string control { get; set; }
    }
}
