using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HCSV.Core;

namespace HCSV.Web
{
    public class GlobalConfig
    {
        public static void Initialization(System.Web.HttpApplicationState application)
        {
            application[Constants.Page.HOME_PAGE_TITLE] = "TCS - A joint venture between Vietnam Airlines, SASCO and SATS";
            application[Constants.Page.PAGE_TITLE] = "";
            application[Constants.Page.PAGE_BANNER] = "";
            application[Constants.SESSION_BANNER_MENU_PATH] = "";
        }
    }
}