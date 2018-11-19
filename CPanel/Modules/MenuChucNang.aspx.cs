using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using QLNS.Commons;
using System.Web.Services;
using CPanel;

using CPanel.Commons;
using HCSV.Models;

namespace CPanel.Modules.QuanTriHeThong
{
    public partial class MenuChucNang : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                CommonFunctionsAndProcedures.getMenuType(drpMenuType, entities);
                
                //load grid view
                grvLib.DataBind();
                
                
            }

        }

        protected void getListOfMenus(int intCapMenu, string strIDMenu, List<jos_menu> lstResultOfMenus, int intMenuTypeID, int intDefaultLangID)
        {
            int intIDMenu = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }
            else //Begin --> reset DropDownlist
            {
                lstResultOfMenus.Clear();   
            }

            //var objMenus = entities.jos_menu.Where(x => ((blNumber && x.parent == intIDMenu) || (blNumber == false && x.parent == 0))
            //                                        && (((intMenuTypeID != CommonFuncs.NUMBER_INVALID_INTEGER) && (x.id_menutype == intMenuTypeID)) || ((intMenuTypeID == CommonFuncs.NUMBER_INVALID_INTEGER) && true)) 
            //                                       ).OrderBy(y=>y.ordering).ToList();

            //var objMenus = entities.jos_menu.Join(entities.jos_language_translation, M => M.id, L => L.origin_id, (M, L) => new { M, L })
            //                                .Where(x => ((blNumber && x.M.parent == intIDMenu) || (blNumber == false && x.M.parent == 0)) 
            //                                       && (x.M.lang_id == intDefaultLangID) && (Commons.ConstTable.TBL_JOS_MENU.Equals(x.L.reference_table))  
            //                                       && (((intMenuTypeID != CommonFuncs.NUMBER_INVALID_INTEGER) && (x.M.id_menutype == intMenuTypeID)) || ((intMenuTypeID == CommonFuncs.NUMBER_INVALID_INTEGER) && true)) 
            //                                       ).Select(z=>z.M).OrderBy(y=>y.ordering).ToList();
            var objMenus = entities.jos_menu.Where(x => ((blNumber && x.parent == intIDMenu) || (blNumber == false && x.parent == 0))
                                                   && (x.lang_id == intDefaultLangID)
                                                   && (((intMenuTypeID != CommonFuncs.NUMBER_INVALID_INTEGER) && (x.id_menutype == intMenuTypeID)) || ((intMenuTypeID == CommonFuncs.NUMBER_INVALID_INTEGER) && true))
                                                   ).OrderBy(y => y.ordering).ToList();

            
            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intCapMenu * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    jos_menu objMenu = new jos_menu();
                    objMenu.id = item.id;
                    objMenu.parent = item.parent;
                    objMenu.ordering = item.ordering;
                    objMenu.link = item.link;
                    
                    
                    if (intCapMenu == 0) //Begining Level
                    {
                        objMenu.name = strLine + item.name; 
                    }
                    else objMenu.name = strLine + item.name;
                    lstResultOfMenus.Add(objMenu);
                    getListOfMenus(intCapMenu + 1, item.id.ToString(), lstResultOfMenus, intMenuTypeID, intDefaultLangID);
                }
            }
        }

        public void drawTreeInDropDownList_Menus(int intCapMenu, string strIDMenu, DropDownList drpDownList, int intMenuTypeID)
        {
            int intIDMenu = 0; bool blNumber_IDMenu = false;
            
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber_IDMenu = true;
            }
            else //Begin --> reset DropDownlist
            {
                drpDownList.Items.Clear();
                ListItem objListItem = new ListItem();
                objListItem.Value = Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE");
                objListItem.Text = Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE");
                drpDownList.Items.Add(objListItem);
            }

            var objMenus = entities.jos_menu.Where(x => ((blNumber_IDMenu && x.parent == intIDMenu) || (blNumber_IDMenu == false && x.parent == 0)) && ((intMenuTypeID != CommonFuncs.NUMBER_INVALID_INTEGER && (x.id_menutype == intMenuTypeID)) || (intMenuTypeID == CommonFuncs.NUMBER_INVALID_INTEGER && true))).ToList();

            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intCapMenu * 6, (char) Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    ListItem objListItem = new ListItem();
                    objListItem.Value = item.id.ToString();
                    objListItem.Text = strLine + item.name;                    
                    if (intCapMenu ==0) //Begining Level
                    {
                        objListItem.Attributes.Add("style", "font-weight: bold");
                    }                    
                    drpDownList.Items.Add(objListItem);
                    drawTreeInDropDownList_Menus(intCapMenu + 1, item.id.ToString(), drpDownList, intMenuTypeID);
                }
            }
        }

        //public void drawMenus (string strIDMenu, int intCapMenu, Label lbMenus)
        //{            
        //    int intIDMenu = 0; bool blNumber = false;
        //    if (!String.IsNullOrEmpty(strIDMenu))
        //    {
        //        intIDMenu = Convert.ToInt32(strIDMenu);
        //        blNumber = true;
        //    }

        //    var objMenus = entities.QT_MENUS.Where(x => (blNumber && x.ID_PARENT == intIDMenu) || (blNumber == false && x.ID_PARENT == null)).ToList();            
                          
        //    if (objMenus != null)
        //    {
        //        foreach (var item in objMenus)
        //        {
        //            string strLine = "";
        //            if (intCapMenu == 1) strLine = "--";
        //            else if (intCapMenu == 2) strLine = "----";
        //            lbMenus.Text = lbMenus.Text + "<li>" + strLine + "<a>" + item.TIEU_DE + "</a></li>";
        //            drawMenus(item.ID.ToString(), intCapMenu+1, lbMenus);                      
        //        }
        //    }
            
        //}

        protected void grvLib_DataBinding(object sender, EventArgs e)
        {
            //get info from session for finding Menus
            SessionForFindingMenu objSessionForFindingMenu = (SessionForFindingMenu)Session[Commons.ConstValues.SESSION_MENU];
            if (objSessionForFindingMenu != null)
            {
                drpMenuType.SelectedValue = objSessionForFindingMenu.ID_MENU_TYPE;
            }

            //get Data
            int intMenuTypeID = CommonFuncs.NUMBER_INVALID_INTEGER;
            if (!Commons.CommonFuncs.BLANK_ITEM_VALUE.Equals(drpMenuType.SelectedValue))
            {
                intMenuTypeID = Convert.ToInt32(drpMenuType.SelectedValue);
            }

            List<jos_menu> lstMenus = new List<jos_menu>();
            
            getListOfMenus(0, null, lstMenus, intMenuTypeID, Commons.CommonFunctionsAndProcedures.getDefaultLanguageID(entities));
            grvLib.DataSource = lstMenus;

            //reset is null for session
            Session[Commons.ConstValues.SESSION_MENU] = null;
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objMenu = entities.jos_menu.Find(e.Keys[0]);
                if (objMenu != null)
                {
                    entities.jos_menu.Remove(objMenu);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // ghi log
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void grvLib_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                var objMenu = entities.jos_menu.Find(Convert.ToInt32(e.Keys[0]));
                

                if (objMenu != null)
                {
                    objMenu.name = Convert.ToString(e.NewValues["TIEU_DE"]);
                }
                
                //Validation and update into DB
                if (validation(objMenu))
                {
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
                // ghi log
            }
            finally
            {
                e.Cancel = true;

            }
        }

        /**
         * DESCRIPTION: This funtion check fomat before updating into DB
         * INPUTS: Menu is the object need updated into DB
         * OUTPUTS: TRUE if data is valid; FALSE if data is invalid
         * WRITTEN BY: TUYENDV
         **/
        protected bool validation(jos_menu objMenu)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objMenu.name))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("Bạn chưa nhập Tên menu", Page);
                return false;
            }

            return true;
        }

        protected void grvLib_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try {
                jos_menu objMenu = new jos_menu();

                if (entities.jos_menu.Count() > 0) objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
                else objMenu.id = 1;

                if (!String.IsNullOrEmpty(Convert.ToString(e.NewValues["TIEU_DE"])))
                {
                    objMenu.name = Convert.ToString(e.NewValues["TIEU_DE"]);
                }
                else
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);                    
                    return;
                }
                entities.jos_menu.Add (objMenu);
                entities.SaveChanges();                

            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
            finally
            {
                e.Cancel = true;
                
            }
        }

        protected void redirectURL()
        {
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_MENUS"));
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                jos_menu objMenu = new jos_menu();

                if (entities.jos_menu.Count() > 0) objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
                else objMenu.id = 1;

                if (!String.IsNullOrEmpty(txtTieude.Text))
                {
                    objMenu.name = txtTieude.Text;
                    string str = drpMenus.SelectedValue;
                    if (!drpMenus.SelectedValue.Equals(Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")))
                        objMenu.parent = Convert.ToInt32(drpMenus.SelectedValue);
                }
                else
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);
                    return;
                }
                entities.jos_menu.Add(objMenu);
                entities.SaveChanges();

                //Save System Log
                Commons.CommonFunctionsAndProcedures.saveSystemLog (String.Format(Commons.TitleConst.getTitleConst("LOG_MENU_CREATE"), objMenu.name));

                redirectURL();
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }
                
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            setSessionForFindingMenu();
            Response.Redirect(CPanel.Commons.TitleConst.getTitleConst("URL_ADD_MENU"));
        }

        protected void drpMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvLib.DataBind();
        }

        protected void btnViewTranslation_Click(object sender, EventArgs e)
        {
            //set session for finding category
            setSessionForFindingMenu();

            //redirect
            Response.Redirect("/Modules/MenuTranslationLang");
        }

        //set session for finding category
        protected void setSessionForFindingMenu()
        {

            SessionForFindingMenu objSessionForFindingMenu = new SessionForFindingMenu();
            objSessionForFindingMenu.ID_MENU = txtOriginID.Text;
            objSessionForFindingMenu.ID_MENU_EDIT = txtMenuID.Text;
            objSessionForFindingMenu.ID_MENU_TYPE = drpMenuType.SelectedValue;
            //objSessionForFindingMenu.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_MENU] = objSessionForFindingMenu;
        }
        
    }
}