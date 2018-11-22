using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSV.Core
{
    public class Constants
    {
        public static class Page
        {
            public const string HOME_PAGE_TITLE = "HOME_PAGE_TITLE";
            public const string PAGE_BANNER = "PAGE_BANNER";
            public const string PAGE_TITLE = "PAGE_TITLE";
            public const string PAGE_BREADCRUMBS = "PAGE_BREADCRUMBS";
            public const string PARENT_MENU_TITLE = "PARENT_MENU_TITLE";
            public const string MENU_TITLE = "MENU_TITLE";
        }

        public  static class  TcsContentType
        {
            public const string MISSION_AREA = "MISSION_AREA";

            public const string VISION_AREA = "VISION_AREA";

            public const string VALUE_AREA = "VALUE_AREA";

            public const string CUSTOMER_NEWS = "CUSTOMER_NEWS";

            public const string TCS_NEWS = "TCS_NEWS";

            public const string INDUSTRIAL_NEWS = "INDUSTRIAL_NEWS";

            public const string MENU_TOP = "TOP_MENU";

            public const string MENU_BOTTOM = "BOTTOM_MENU";

            public const string MENU_LEFT = "LEFT_MENU";
        }

        public static class TranslateTable
        {
            public const string TBL_JOS_MENU = "jos_menu";
            public const string TBL_JOS_CONTENT = "jos_content";
            public const string TBL_JOS_CONTACT = "jos_contact";
        }

        public static class Session
        {
            public const string SESSION_LANGUAGE_ID = "LANGUAGE_ID";
            public const string SESSION_DEFAULT_LANGUAGE_ID = "DEFAULT_LANGUAGE_ID";
            public const string SESSION_MENU_ID = "MENU_ID";
            public const string SESSION_BANNER_MENU_PATH = "SESSION_BANNER_MENU_PATH";
            public const string SESSION_ERROR_CAPCHA = "ERROR_CAPTCHA";
            public const string SESSION_LOGIN_STATUS = "LOGIN_STATUS";
            public const string PARENT_MENU_TITLE = "PARENT_MENU_TITLE";
        }

        public const string WEB_HOME_PAGE = "Home";
        public const string WEB_SUPPORT_PAGE = "Support";
        public const string WEB_ABOUT_PAGE = "About";
        public const string WEB_SERVICE_PAGE = "Services";
        public const string WEB_OUR_TEAMS_PAGE = "OurTeams";
        public const string WEB_CLIENTS_PAGE = "Clients";

        public const string BUTTON_READ_MORE = "read more";

        public const string URL_SERVICES = "/Services/Index";
        public const string URL_CLIENTS_PARTNER = "/Clients/Index";


        public const string OLD_IMAGE_PATH = "src=\"";
        public const string OLD_IMAGE_PATH_1 = "src=\"../Upload/Images/"; //Do khac nhau ban Devexpress (13.0 va 14.0)
        public const string NEW_IMAGE_PATH = "src=\"../CPanel/Upload/Images/";

        public const string LANGUAGE_VIETNAM = "vi";
        public const string LANGUAGE_ENGLISH = "en";
        public const string LANGUAGE_JAPAN = "jp";

        public const string CONFIG_EMAIL = "CONFIG_EMAIL";

        public const int NUMBER_INVALID_INTEGER = -99999;

        public const int NUMBER_OF_CONTENTS_ON_A_PAGE = 10;

        public const string DATE_FORMAT_DD_MM_YYYY = "dd.MM.yyyy";

        public const string CSS_ACTIVE_TOP_MENU = " current section";

        //for web service
        public const string user_code = "user_code";
        public const string lg_codesgshdgsksasa = "lg_codesgshdgsksasa";
        public const string locale = "locale";
        public const string awbFirst = "awbFirst";
        public const string awbLast = "awbLast";
        public const string awbFst = "awbFst";
        public const string awbLst = "awbLst";
        public const string serie = "serie";

        public const string NAME_OF_CAPCHA = "clientCaptcha_Airlines";
    }
}
