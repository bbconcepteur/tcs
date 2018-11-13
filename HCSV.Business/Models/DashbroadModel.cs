using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCSV.Models;

namespace HCSV.Business.Models
{
    public class DashbroadModel
    {
        public jos_content MissionArea { get; set; }

        public jos_content VisionArea { get; set; }

        public jos_content ValueArea { get; set; }

        public List<jos_content> CustomerNews { get; set; } = new List<jos_content>();

        public List<jos_content> TcsNews { get; set; } = new List<jos_content>();

        public List<jos_content> IndustrialNews { get; set; } = new List<jos_content>();

    }
}
