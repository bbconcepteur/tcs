
using System;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Commons
{
    public class ConstURL
    {        
        public static string URL_CONTENT_EDIT = "/Modules/ContentEdit";
        public static string URL_CONTENT_VIEW = "/Modules/ContentList";
        public static string URL_CONTENT_TRANSLATION = "/Modules/ContentTranslationLang";

        public static string URL_SECTION_VIEW = "/Modules/SectionList";                    
        public static string URL_SECTION_EDIT = "/Modules/SectionEdit";                    

        public static string URL_CATEGORY_EDIT = "/Modules/CategoryEdit";
        public static string URL_CATEGORY_VIEW = "/Modules/CategoryList";
        
        public static string URL_MENU_LIST = "/Modules/FunctionMenusList";
        public static string URL_MENU_EDIT = "/Modules/FunctionMenusEdit";

        public static string URL_PARTNER_LIST = "/Modules/PartnersLinkList";
        public static string URL_PARTNER_EDIT = "/Modules/PartnerLinkEdit";
        public static string URL_PARTNER_TRANSLATION = "/Modules/PartnerTranslationLang";

        public static string URL_CONTACT_EDIT = "/Modules/ContactEdit";
        public static string URL_CONTACT_VIEW = "/Modules/ContactList";
        public static string URL_CONTACT_TRANSLATION = "/Modules/ContactTranslationLang";

        public static string URL_CHANGE_PASSWORD = "/Modules/ChangePassword";

        public static string URL_CHECK_PERMISSION = "/CheckPermission";
        public static string URL_SIGN_IN = "/SignIn.aspx?returnUrl={0}";

        public static string URL_FUNCTION_EDIT = "/Modules/FunctionEdit";
        public static string URL_FUNCTION_VIEW = "/Modules/FunctionList";

        public static string URL_LANGUAGE_EDIT = "/Modules/LanguageEdit";
        public static string URL_LANGUAGE_LIST = "/Modules/LanguageList";

        public static string URL_COMMENT_EDIT = "/Modules/CommentEdit";
        public static string URL_COMMENT_VIEW = "/Modules/CommentList";
    }

    public class ConstTable
    {
        public static string TBL_JOS_MENU = "jos_menu";
        public static string TBL_JOS_CONTENT = "jos_content";
        public static string TBL_JOS_COMMENT = "jos_components";
        public static string TBL_JOS_LINKS = "jos_links";
        public static string TBL_JOS_CONTACT = "jos_contact";
    }

    public class ConstValues
    {
        public static string SESSION_SECTION = "SESSION_SECTION";
        public static string SESSION_CONTENT = "SESSION_CONTENT";
        public static string SESSION_COMMENT = "SESSION_COMMENT";
        public static string SESSION_CONTACT = "SESSION_CONTACT";
        public static string SESSION_CATEGORY = "SESSION_CATEGORY";        
        public static string SESSION_MENU = "SESSION_MENU";
        public static string SESSION_PARTNER = "SESSION_PARTNER";
        public static string SESSION_LANGUAGE = "SESSION_LANGUAGE"; 

        public static string SUPPER_ADMIN = "ADMIN";
        public static string CONFIG_EMAIL = "CONFIG_EMAIL";


        public static string LANGUAGE_VIETNAMESE = "vi_VN";
        public static string LANGUAGE_ENGLISH = "en_US";
    }
    public class SessionForFindingContent
    {
        public string ID_CATEGORY = "";
        public string ID_LANGUAGE = "";
        public string ID_CONTENT = "";
        public string ID_MENU = "";        
    }

    public class SessionForFindingContact
    {
        public string ID_CONTACT = "";
        public string ID_LANGUAGE = "";
    }
    public class SessionForFindingComment
    {
        public string ID_COMMENT = "";
        public string ID_LANGUAGE = "";
    }
    public class SessionForFindingLanguage
    {
        public string ID_LANGUAGE = "";

    }

    public class SessionForFindingPartner
    {
        public string ID_PARTNER = "";
        public string ID_LANGUAGE = "";

    }

    public class SessionForFindingFunction
    {
        public string ID_FUNCTION = "";
    }
    
    public class SessionForFindingCategory
    {
        public string ID_CATEGORY = "";
        public string ID_LANGUAGE = "";

    }

    public class SessionForFindingSection                                                    //added
    {
        public string ID_SECTION = "";
        public string ID_LANGUAGE = "";
    }

    public class SessionForFindingMenu
    {
        public string ID_MENU = "";
        public string ID_MENU_EDIT = "";
        public string ID_MENU_TYPE = "";
        public string ID_LANGUAGE = "";


    }

    public class CommonFuncs
    {
        public static string POSITION_NONE = "";
        public static string POSITION_LEFT = "LEFT";
        public static string POSITION_RIGHT = "RIGHT";
        public static string POSITION_MIDDLE = "MIDDLE";
        public static string POSITION_POS_1 = "POS_1";
        public static string POSITION_POS_2 = "POS_2";
        public static string POSITION_POS_3 = "POS_3";
		public static string POSITION_POS_4 = "POS_4";
        public static string POSITION_TOP = "TOP";
        public static string POSITION_FOOTER = "FOOTER";
        public static string POSITION_BANNER = "BANNER";

        //BEGIN: Const for TYPE_OF_PAGE
        public static string TYPE_OF_PAGE_NEWS = "NEWS";
        public static string TYPE_OF_PAGE_CONTENT = "CONTENTS";
        public static string TYPE_OF_PAGE_FAQ = "FAQ";        
        public static string TYPE_OF_PAGE_HOME = "HOME_PAGE";
        public static string TYPE_OF_PAGE_LINK = "LINK";

        public static string TYPE_OF_PAGE_SERVICE_CONTENT = "SERVICE_CONTENT";
        public static string TYPE_OF_PAGE_SERVICE_TAB = "SERVICE_TAB";

        public static string TYPE_OF_PAGE_GUIDES = "GUIDES";
        public static string TYPE_OF_PAGE_FEEDBACK = "FEEDBACK";
        public static string TYPE_OF_PAGE_CONTACT = "CONTACT";
        public static string TYPE_OF_PAGE_SITEMAP = "SITEMAP";
        public static string TYPE_OF_PAGE_STATISTICS = "STATISTICS";
        

        public static string TYPE_OF_PAGE_OTHERS = "OTHERS";        
        //END: Const for TYPE_OF_PAGE

        public static string BLANK_ITEM_VALUE = "BLANK_ITEM_VALUE";
        public static string BLANK_ITEM_TITLE = "---Choose the list---";

        public static int NUMBER_INVALID_INTEGER = -99999;

        public static string LINK_MENU_TO_CATEGORY_NEWS = "/News/Index?catID={0}&menu={1}";
        public static string LINK_MENU_TO_CONTENT_NEWS = "/News/Details?contentID={0}&menu={1}";

        public static string RECYCLE_BIN_CATEGORY_TITLE = "Menus";
        public static string RECYCLE_BIN_CONTENT_TITLE = "Contents";

        //Image Path
        public static string OLD_IMAGE_PATH = "src=\"";
        public static string NEW_IMAGE_PATH = "src=\"../Upload/Images/"; //Do khac nhau ban Devexpress (13.0 va 14.0)        
        
        //VINH
        public static string CATEGORY_BLANK_ITEM_VALUE = "XXXXXXXXXXXXXXX";
        public static string[] getListOfPosition()
        {
            string[] lstPosition = new string[11];
            lstPosition[0] = POSITION_NONE;
            lstPosition[1] = POSITION_LEFT;
            lstPosition[2] = POSITION_RIGHT;
            lstPosition[3] = POSITION_MIDDLE;
            lstPosition[4] = POSITION_POS_1;
            lstPosition[5] = POSITION_POS_2;
            lstPosition[6] = POSITION_POS_3;
			lstPosition[7] = POSITION_POS_4;
            lstPosition[8] = POSITION_TOP;
            lstPosition[9] = POSITION_FOOTER;
            lstPosition[10] = POSITION_BANNER;
            return lstPosition;
        }

        //This funtion get Languages
        public static void getLanguages (DropDownList drpLanguages, TCSEntities entities) {
            var lstLanguages = entities.jos_languages.Where(a => a.published == true).OrderByDescending(x => x.default_status).ToList();
            drpLanguages.DataValueField = "lang_id";
            drpLanguages.DataTextField = "title";
            drpLanguages.DataSource = lstLanguages;
            drpLanguages.DataBind();
        }

        //This funtion get Languages
        public static void getLanguagesByDefaultLangID (DropDownList drpLanguages, TCSEntities entities)
        {
            int intDefaultLangID = CommonFunctionsAndProcedures.getDefaultLanguageID(entities);
            var lstLanguages = entities.jos_languages.Where(a => a.published == true && a.lang_id == intDefaultLangID).ToList();
            drpLanguages.DataValueField = "lang_id";
            drpLanguages.DataTextField = "title";
            drpLanguages.DataSource = lstLanguages;
            drpLanguages.DataBind();
        }

        //This funtions get Categories
        public static void getCategories(DropDownList drpCategory, TCSEntities entities, short intLangID)
        {            
            //var lstCategory = entities.jos_categories.Where (x=>x.lang_id == intLangID && x.published == true).ToList();
            var lstCategory = entities.jos_categories.Where(x => x.published == true).ToList();
            drpCategory.DataValueField = "id";
            drpCategory.DataTextField = "title";
            drpCategory.DataSource = lstCategory;
            drpCategory.DataBind();

            //Add Blank item into the top of Dropdownlist
            drpCategory.Items.Insert(0, new ListItem(Commons.CommonFuncs.BLANK_ITEM_TITLE, Commons.CommonFuncs.BLANK_ITEM_VALUE));
            drpCategory.SelectedIndex = 0;
        }

        public static string convertContent(string strContent)
        {
            if (String.IsNullOrEmpty(strContent)) return "";
            string strContentTemp = strContent;

            if (!strContentTemp.Contains(NEW_IMAGE_PATH))
            {
                strContentTemp = strContent.Replace(OLD_IMAGE_PATH, NEW_IMAGE_PATH);
            }
            return strContentTemp;
        }

        
                


        
    }
}