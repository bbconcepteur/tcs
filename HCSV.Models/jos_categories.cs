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
    
    public partial class jos_categories
    {
        public int id { get; set; }
        public Nullable<int> old_id { get; set; }
        public int parent_id { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string image { get; set; }
        public Nullable<int> section { get; set; }
        public string image_position { get; set; }
        public string description { get; set; }
        public Nullable<bool> published { get; set; }
        public Nullable<long> checked_out { get; set; }
        public Nullable<System.DateTime> checked_out_time { get; set; }
        public string editor { get; set; }
        public Nullable<int> ordering { get; set; }
        public Nullable<byte> access { get; set; }
        public Nullable<int> count { get; set; }
        public string @params { get; set; }
        public Nullable<short> lang_id { get; set; }
    }
}
