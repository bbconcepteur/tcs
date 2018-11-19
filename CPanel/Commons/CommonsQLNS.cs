using System;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Linq;
using log4net;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;

using DevExpress.Web.ASPxGridView;
using Telerik.Web.UI;
using CPanel.ViewModels;
using HCSV.Models;

namespace CPanel.Commons
{
    //public class Session
    //{
    //    public static string MENU_CHUC_NANG_ID = "MENU_CHUC_NANG_ID";        
    //}

    /**
     * @Description: This class declares Value Const
     * @Written by: TuyenDV
     **/
    public class ValueConst
    {
        public static string BLANK_ITEM = "BLANK_ITEM";
    }

    /**
     * @Description: This class declares variables used for DateTime type
     * @Written by: TuyenDV
     **/
    public class DateTimeType
    {
        public static string DATE_FORMAT_DD_MM_YYYY = "dd-MM-yyyy";
        public static string DATE_FORMAT_YYYYMMDD = "yyyyMMdd";
        public static string DATE_FORMAT_MM_DD_YYYY = "MM/dd/yyyy";
        public static string DATE_FORMAT_DD_MM_YYYY_HH_MM_SS = "dd-MM-yyyy HH:mm:ss";
    }

    public class PositionClass {
        public static string getPositionByCategory(string strCategoryName, string strPositionName_Origin, DropDownList drpCategory, TCSEntities entities)
        {
            string strPositionName = ""; 
            switch (strCategoryName)
            {
                case "Home":
                    if (!String.IsNullOrEmpty(strPositionName_Origin) && (strPositionName_Origin.Equals(CommonFuncs.POSITION_BANNER)))
                    {
                        strPositionName = CommonFuncs.POSITION_BANNER; 
                    }
                    break;

                case "OurTeams":
                    strPositionName = CommonFuncs.POSITION_BANNER;
                    break;
                case "About":
                    strPositionName = CommonFuncs.POSITION_BANNER + " " + CommonFuncs.POSITION_RIGHT;
                    break;
                case "Services":
                    strPositionName = CommonFuncs.POSITION_BANNER;
                    break;
                case "CaseStudy":                    
                    strPositionName = "";
                    break;
                case "News":
                    strPositionName = "";
                    break;
                case "Clients":
                    strPositionName = "";
                    break;
                case "Support":
                    strPositionName = "";
                    break;               
                default:
                    break;
            }
            return strPositionName;
        }
    }
    
    /**
     * @Description: This class to get Title Const from XML file     
     * @Written by: TuyenDV
     **/
    public class TitleConst
    {
        /**
         * @Description: This function to get Title Const from XML file
         * @Parameters: name of tag
         * @Return: the content of XML tag
         * @Written by: TuyenDV
         **/
        public static string getTitleConst (string strItem)
        {
            XmlDocument doc = new XmlDocument();

            var XMLLoadfullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            Path.Combine("App_Data", "TitleConst.xml"));

            doc.Load(XMLLoadfullPath);

            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName(strItem);

            string result = "";
            for (int i = 0; i < elemList.Count; i++)
            {
                result = elemList[i].InnerXml;
            }

            return String.IsNullOrEmpty(result) ? strItem : result;
        }

        public static void setTitleConst_ASPxGridView(ASPxGridView gridView)
        {
            //Set Captions for GridView
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                gridView.Columns[i].Caption = Commons.TitleConst.getTitleConst(gridView.Columns[i].Caption);
            }
        }

        
    }




    public class TreeObject
    {
        public decimal ID { get; set; }
        public string TIEU_DE { get; set; }
    }

    /**
     * @Description: This class's used for Log File
     * @Written by: TuyenDV
     **/
    public class LoggerUtil
    {
        private ILog aLog = null;

        public LoggerUtil(string pClassName)
        {
            log4net.Config.XmlConfigurator.Configure();
            aLog = LogManager.GetLogger(pClassName);

        }

        public void Debug(object pMessage)
        {
            aLog.Debug(pMessage);
        }

        public void Debug(object pMessage, Exception pException)
        {
            aLog.Debug(pMessage, pException);
        }

        public void Error(object pMessage)
        {
            aLog.Error(pMessage);
        }

        public void Error(object pMessage, Exception pException)
        {
            aLog.Error(pMessage, pException);
        }

        public void Close(string pClassName)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(pClassName);
            log4net.Repository.Hierarchy.Logger l = (log4net.Repository.Hierarchy.Logger)log.Logger;
            l.CloseNestedAppenders();

        }
    }

    /**
     * @Description: This class to draw menus
     * @Written by: TuyenDV
     **/
    public class SystemMenus {

        TCSEntities entities = new TCSEntities();

        public void getAdministratorMenus(Label lbMenus)
        {
            //get User ID
            int intUserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());

            //get "VAI TRO"            
            List<int> lstRights = entities.jos_rights_users.Where(x => x.id_user == intUserId).Select(y => (int)y.id_right).ToList();
            if (lstRights != null && lstRights.Count > 0)
            {

                lbMenus.Text = @"
                    <div class='container'>
                        <nav class='navbar navbar-default'>
                        <div class='container-fluid'>
                          <div class='navbar-header'>
                            <button type='button' class='navbar-toggle collapsed' data-toggle='collapse' data-target='#navbar' aria-expanded='false' aria-controls='navbar'>
                              <span class='sr-only'>Toggle navigation</span>
                              <span class='icon-bar'></span>
                              <span class='icon-bar'></span>
                              <span class='icon-bar'></span>            </button>
          	                  <a class='navbar-brand' href='#'>CPanel</a>          
		                  </div>
                          <div id='navbar' class='navbar-collapse collapse'>
                              <ul class='nav navbar-nav'>
                            ";
                //get Menu Type ID
                string strMenuType = ConfigurationManager.AppSettings["MENU_ADMIN"];
                var objMenuType = entities.jos_menu_types.Where(x => strMenuType.Equals(x.menutype)).FirstOrDefault();
                if (objMenuType != null)
                {
                    //draw Administrator Menus
                    drawAdministratorMenus(null, 0, (int)objMenuType.id, lbMenus, lstRights);
                }


                lbMenus.Text = lbMenus.Text + @"
                                    </div><!--/.nav-collapse -->
                                </div><!--/.container-fluid -->
                                </nav>   
                            </div>
                        ";
            }
        } 



        /**
         * @Description: This function to draw menus
         * @Parameters: strIDMenu - ID of root menu; intCapMenu - Level of root menu
         * @Return: void
         * @Written by: TuyenDV
         **/
        public void drawAdministratorMenus(string strIDMenu, int intCapMenu, int intMenuTypeID, Label lbMenus, List<int> lstRights)
        {            
            int intIDMenu = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }


            var objMenus = entities.jos_menu.Where(x => ((blNumber && x.parent == intIDMenu) || (blNumber == false && x.parent == 0)) && (x.id_menutype == intMenuTypeID)).ToList();

            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {
                    var objRight = entities.jos_menu_rights.Where(x => x.id_menu == item.id && lstRights.Contains(x.id_right)).FirstOrDefault();
                    if (objRight != null) 
                    {
                        if (entities.jos_menu.Where(x => x.parent == item.id).Count() > 0) //Have child menus
                        {
                            if (intCapMenu == 0) //Level 1
                            {
                                lbMenus.Text = lbMenus.Text + @"
                                                <li class='dropdown'>
                                                    <a href='#' class='dropdown-toggle' data-toggle='dropdown' 
                                                        role='button' aria-haspopup='true' aria-expanded='false'>" + item.name + @"<span class='caret'></span></a>
                                                        <ul class='dropdown-menu'>
                                                ";

                                drawAdministratorMenus(item.id.ToString(), intCapMenu + 1, intMenuTypeID, lbMenus, lstRights);
                                lbMenus.Text = lbMenus.Text + @"</ul></li>";                            
                            }
                            else if (intCapMenu == 1) //Level 2
                            {   
                                lbMenus.Text = lbMenus.Text + @"
                                                <li class='dropdown-submenu'>
                                                    <a  tabindex='-1' href='"+item.link+"'>" + item.name + @"</a>
                                                        <ul class='dropdown-menu child-dropdown-menu'>
                                                ";
                                drawAdministratorMenus(item.id.ToString(), intCapMenu + 1, intMenuTypeID, lbMenus, lstRights);
                                lbMenus.Text = lbMenus.Text + @"</ul></li>";
                            }
                            else if (intCapMenu == 2) //Level 3
                            {
                                lbMenus.Text = lbMenus.Text + "<li><a href='" + item.link + "'>" + item.name + "</a></li>";
                            }
                        
                        }
                        else //Have no child menus
                        {
                            lbMenus.Text = lbMenus.Text + "<li><a href='" + item.link + "'>" + item.name + "</a></li>";
                        }
                    }
                }
            }

        }
    }

    

    /**
     * @Description: This class declare all functions and procedures shared     
     * @Written by: TuyenDV
     **/   
    
    public class CommonFunctionsAndProcedures
    {
        //This function's created by VINHDT
        public static void DrawTelericTreeView_Funs(RadTreeView radTreeView, int intVaiTroID, int intParentNodeID, RadTreeNode node, TCSEntities entities)
        {
            bool blCheck = false;
            bool blZero = false;
            if (intParentNodeID == 0) blZero = true;
            var objMenus = entities.jos_functions.Where(x => (blZero ? x.id_parent == 0 : x.id_parent == intParentNodeID)).ToList();

            if ((objMenus != null) && (objMenus.Count > 0))
            {
                foreach (var item in objMenus)
                {
                    var objVaiTroFunction = entities.jos__function_rights.Where(x => x.id_right == intVaiTroID && x.id_function == item.id).ToList();
                    if (objVaiTroFunction != null && objVaiTroFunction.Count > 0)
                        blCheck = true;
                    else blCheck = false;

                    string strMoTa = item.name;//item.TIEU_DE
                    if (node == null)
                    {
                        RadTreeNode nodeCap1 = new RadTreeNode(strMoTa);
                        nodeCap1.Value = item.id.ToString();
                        DrawTelericTreeView_Funs(radTreeView, (int)intVaiTroID, (int)item.id, nodeCap1, entities);
                        nodeCap1.Expanded = true; //Expand all
                        nodeCap1.Checked = blCheck;
                        radTreeView.Nodes.Add(nodeCap1);
                    }
                    else
                    {
                        RadTreeNode child = new RadTreeNode(strMoTa);
                        child.Value = item.id.ToString();
                        child.Expanded = true; //Expand all
                        child.Checked = blCheck;
                        node.Nodes.Add(child);
                        DrawTelericTreeView_Funs(radTreeView, (int)intVaiTroID, (int)item.id, child, entities);

                    }

                }

            }
        }



        /**
        * Telerik:RadtreeView: variable to create TreeView (get DB from tbl SO_TAI_KHOAN)
        * intCap: Begin from 1
        * intParentNodeID: Begin from 0
        * node: Begin is null
        **/
        public static void DrawTelericTreeView_Menus(RadTreeView radTreeView, int intVaiTroID, int intParentNodeID, RadTreeNode node, string strMenuType, TCSEntities entities)
        {
            bool blCheck = false;
            bool blZero = false;
            int intDefaultLangID = getDefaultLanguageID(entities);
            if (intParentNodeID == 0) blZero = true;
            //var objMenus = entities.jos_menu.Where(x => (blZero ? x.parent == 0 : x.parent == intParentNodeID) && (x.id_menutype == intMenuType)).ToList();
            var objMenus = entities.jos_menu.Join(entities.jos_menu_types, M => (int)M.id_menutype, T => T.id, (M, T) => new { M, T })
                .Where(x => (x.M.lang_id == intDefaultLangID) && (x.M.published)
                    && (blZero ? x.M.parent == 0 : x.M.parent == intParentNodeID) 
                    && (strMenuType.Equals(x.T.menutype))).Select(z => z.M).OrderBy(y => y.ordering).ToList();


            if ((objMenus != null) && (objMenus.Count > 0))
            {
                foreach (var item in objMenus)
                {
                    var objVaiTroMenu = entities.jos_menu_rights.Where(x => x.id_right == intVaiTroID && x.id_menu == item.id).ToList();
                    if (objVaiTroMenu != null && objVaiTroMenu.Count > 0)
                        blCheck = true;
                    else blCheck = false;

                    string strMoTa = item.alias;//item.TIEU_DE
                    if (node == null)
                    {
                        RadTreeNode nodeCap1 = new RadTreeNode(strMoTa);
                        nodeCap1.Value = item.id.ToString();
                        DrawTelericTreeView_Menus(radTreeView, (int)intVaiTroID, (int)item.id, nodeCap1, strMenuType, entities);
                        nodeCap1.Expanded = true; //Expand all
                        nodeCap1.Checked = blCheck;
                        radTreeView.Nodes.Add(nodeCap1);
                    }
                    else
                    {
                        RadTreeNode child = new RadTreeNode(strMoTa);
                        child.Value = item.id.ToString();
                        child.Expanded = true; //Expand all
                        child.Checked = blCheck;
                        node.Nodes.Add(child);
                        DrawTelericTreeView_Menus(radTreeView, (int)intVaiTroID, (int)item.id, child, strMenuType, entities);
                    }

                }

            }
        }

        /**
         * Draw Tree for DropDownlist
         */
        public static void drawTreeInDropDownList(int intCapMenu, string strIDMenu, DropDownList drpDownList1, ASPxComboBox aspComboBox, string strTableName, int  intDefaultLangID, TCSEntities entities)
        {
            int intIDMenu = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }
            else //Begin --> reset DropDownlist
            {
                if (drpDownList1 != null)
                {
                    drpDownList1.Items.Clear();
                    ListItem objListItem = new ListItem();
                    objListItem.Value = Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE");
                    objListItem.Text = Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE");
                    drpDownList1.Items.Add(objListItem);
                }
                else if (aspComboBox != null)
                {
                    aspComboBox.Items.Clear();
                    aspComboBox.Items.Add(Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE"), Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE"));
                }

            }


            List<TreeObject> lstTreeObjects = new List<TreeObject>();
            if (Commons.TitleConst.getTitleConst("TABLE_QT_MENUS").Equals(strTableName))
            {
                var lstObjects = entities.jos_menu.Where(x => (x.lang_id == intDefaultLangID) && ((blNumber && x.parent == intIDMenu) || (blNumber == false && x.parent == 0))).ToList();

                if ((lstObjects != null) && (lstObjects.Count > 0))
                {
                    foreach (var item in lstObjects)
                    {
                        TreeObject objTreeObject = new TreeObject();
                        objTreeObject.ID = item.id;
                        objTreeObject.TIEU_DE = item.name;
                        lstTreeObjects.Add(objTreeObject);
                    }
                }

            }

            else if (Commons.TitleConst.getTitleConst("TABLE_CONTACT").Equals(strTableName))
            {
                var lstObjects = entities.jos_contact.Where(x => (blNumber && x.id_parent == intIDMenu && x.lang_id == intDefaultLangID) || (blNumber == false && x.id_parent == 0 && x.lang_id == intDefaultLangID)).ToList();

                if ((lstObjects != null) && (lstObjects.Count > 0))
                {
                    foreach (var item in lstObjects)
                    {
                        TreeObject objTreeObject = new TreeObject();
                        objTreeObject.ID = item.id;
                        objTreeObject.TIEU_DE = item.name;
                        lstTreeObjects.Add(objTreeObject);
                    }
                }
            }

            //else if (Commons.TitleConst.getTitleConst("TABLE_NS_DON_VI").Equals(strTableName))
            //{
            //    var lstObjects = entities.NS_DON_VI.Where(x => (blNumber && x.PARENT_ID == intIDMenu) || (blNumber == false && x.PARENT_ID == null)).ToList();

            //    if ((lstObjects != null) && (lstObjects.Count > 0))
            //    {
            //        foreach (var item in lstObjects)
            //        {
            //            TreeObject objTreeObject = new TreeObject();
            //            objTreeObject.ID = item.ID;
            //            objTreeObject.TIEU_DE = item.TEN_DON_VI;
            //            lstTreeObjects.Add(objTreeObject);
            //        }
            //    }
            //}

            if ((lstTreeObjects != null) && (lstTreeObjects.Count > 0))
            {
                foreach (var item in lstTreeObjects)
                {

                    string strLine = "";
                    strLine = strLine.PadLeft(intCapMenu * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    if (drpDownList1 != null)
                    {
                        ListItem objListItem = new ListItem();
                        objListItem.Value = item.ID.ToString();

                        objListItem.Text = strLine + item.TIEU_DE;

                        if (intCapMenu == 0) //Begining Level
                        {
                            objListItem.Attributes.Add("style", "font-weight: bold");
                        }
                        drpDownList1.Items.Add(objListItem);
                    }
                    else if (aspComboBox != null)
                    {
                        aspComboBox.Items.Add(strLine + item.TIEU_DE, item.ID.ToString());
                    }


                    drawTreeInDropDownList(intCapMenu + 1, item.ID.ToString(), drpDownList1, aspComboBox, strTableName, intDefaultLangID, entities);
                }
            }
        }

        public static int getDefaultLanguageID(TCSEntities entities)
        {
            var objDefaultLang = entities.jos_languages.Where(x => x.default_status == 1).FirstOrDefault();
            return (int) objDefaultLang.lang_id;
        }
        /**
         * @Description: This function to store information of system log (by file and by DB)
         * @Parameters: strMoTa - Description of log message
         * @Return: void
         * @Written by: TuyenDV
         **/
        public static void saveSystemLog(string strMoTa)
        {
            TCSEntities entities = new TCSEntities();
            //Log DB
           
            /*
            log_he_thong objLogHT = new log_he_thong();
            if (entities.log_he_thong.Count() > 0) objLogHT.ID = entities.log_he_thong.Max(x => x.ID) + 1;
            else objLogHT.ID = 1;

            //get Raw URL
            string strURL = HttpContext.Current.Request.RawUrl;
            
            //get user name
            int intUserID = (int)CheckUserInfo.GetUserId();
            userlogin objUser = entities.userlogins.Find(intUserID);


            objLogHT.MO_TA = strMoTa;
            objLogHT.URL = strURL;
            objLogHT.ID_NGUOI_TAO = intUserID;
            objLogHT.NGAY_TAO = DateTime.Now;
            
            
            entities.log_he_thong.Add(objLogHT);
            entities.SaveChanges();

            //log file
            LoggerUtil objLogFile = new LoggerUtil(Commons.TitleConst.getTitleConst("LOG_FILE_NAME"));                        
            objLogFile.Debug (String.Format(Commons.TitleConst.getTitleConst("LOG_CONTENT_FORMAT_FILE"), 
                                            DateTime.Now.ToString(Commons.DateTimeType.DATE_FORMAT_DD_MM_YYYY_HH_MM_SS),
                                            objUser.USERNAME, strMoTa, objLogHT.URL));
        */
        }

        //get list of Menus
        public static void getMenuType(DropDownList drpMenuType, TCSEntities entities)
        {
            drpMenuType.DataSource = entities.jos_menu_types.ToList();
            drpMenuType.DataValueField = "id";
            drpMenuType.DataTextField = "title";
            drpMenuType.DataBind();

            drpMenuType.Items.Insert(0, new ListItem(Commons.CommonFuncs.BLANK_ITEM_TITLE, Commons.CommonFuncs.BLANK_ITEM_VALUE));
            drpMenuType.SelectedIndex = 0;
        }

        //get list of Sections
        public static void getSections(DropDownList drpSection, TCSEntities entities)
        {
            drpSection.DataSource = entities.jos_sections.Where(x=>x.published == true).ToList();
            drpSection.DataValueField = "id";
            drpSection.DataTextField = "title";
            drpSection.DataBind();

            drpSection.Items.Insert(0, new ListItem(Commons.CommonFuncs.BLANK_ITEM_TITLE, Commons.CommonFuncs.BLANK_ITEM_VALUE));
            drpSection.SelectedIndex = 0;
        }

        //get list of Categories
        public static void getCategories(DropDownList drpCategory, string strSectionID, TCSEntities entities)
        {
            bool blAllSection = true; int intSectionID = Commons.CommonFuncs.NUMBER_INVALID_INTEGER;
            if (!Commons.CommonFuncs.BLANK_ITEM_VALUE.Equals(strSectionID)) {
                blAllSection = false;
                intSectionID = Convert.ToInt32 (strSectionID);
            }

            drpCategory.DataSource = entities.jos_categories.Where(x=>(x.published==true) && (blAllSection?true:x.section == intSectionID)).ToList();
            drpCategory.DataValueField = "id";
            drpCategory.DataTextField = "title";
            drpCategory.DataBind();

            drpCategory.Items.Insert(0, new ListItem(Commons.CommonFuncs.BLANK_ITEM_TITLE, Commons.CommonFuncs.BLANK_ITEM_VALUE));
            drpCategory.SelectedIndex = 0;           
            
        }

        //get list of Categories by SectionID
        public static List<jos_categories_ViewModel> getCategoriesBySectionID(string strSectionID, TCSEntities entities)
        {
            bool blAllSection = true; int intSectionID = Commons.CommonFuncs.NUMBER_INVALID_INTEGER;
            if (!Commons.CommonFuncs.BLANK_ITEM_VALUE.Equals(strSectionID))
            {
                blAllSection = false;
                intSectionID = Convert.ToInt32(strSectionID);
            }

            var lstResult = entities.jos_categories.Where(x => (x.published == true) && (blAllSection ? true : x.section == intSectionID))
                                    .Select(y => new jos_categories_ViewModel() {name = y.name, title = y.title, section = y.section, id=y.id})
                                    .ToList();

            return lstResult;
        }

        //get Section by SectionID
        public static string getSectionNameBySectionID(int intSectionID, TCSEntities entities)
        {            
            jos_sections objSection = entities.jos_sections.Where(x => (x.published == true) && (x.id == intSectionID)).FirstOrDefault();
            return (objSection == null ? "" : objSection.name);
        }

        //get list of Types of Page
        public static void getTypeOfPage(DropDownList drpTypeOfPage, TCSEntities entities)
        {
            drpTypeOfPage.DataSource = entities.jos_type_of_page.ToList();
            drpTypeOfPage.DataValueField = "name";
            drpTypeOfPage.DataTextField = "description";
            drpTypeOfPage.DataBind();
        }
    }
}