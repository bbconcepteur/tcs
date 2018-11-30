using System;
using System.Collections.Generic;
using HCSV.Core;

namespace HCSV.Models.ViewModels
{
    public class Menu
    {
        public  string Name { get; set; }

        public  string Url { get; set; }

        public  long ParentId { get; set; }

        public  int Id { get; set; }

        public  int LangId { get; set; }

        public int Level { get; set; }

        public int OrdNumber { get; set; }

        public List<Menu> Childrens { get; set; } 

        public bool HasChildren { get { return Childrens != null && Childrens.Count > 0; } }

        public MenuType MenuType { get; set; }
    }
}
