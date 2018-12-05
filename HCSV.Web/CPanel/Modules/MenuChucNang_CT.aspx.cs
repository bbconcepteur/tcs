using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using System.Web.Services;
using HCSV.Models;

namespace CPanel.Modules.QuanTriHeThong
{
    public partial class MenuChucNang_CT : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int intMenuID = 0;
                
                //get Default Language ID                
                Commons.CommonFunctionsAndProcedures.drawTreeInDropDownList(0, null, drpMenus, null, Commons.TitleConst.getTitleConst("TABLE_QT_MENUS"),
                                                                            Commons.CommonFunctionsAndProcedures.getDefaultLanguageID(entities), entities);

                CommonFunctionsAndProcedures.getMenuType(drpMenuType, entities);
                CommonFunctionsAndProcedures.getSections(drpSection, entities);
                CommonFunctionsAndProcedures.getCategories(drpCategory, drpSection.SelectedValue, entities);
                CommonFunctionsAndProcedures.getTypeOfPage(drpTypeOfPage, entities);

                //get Content ID from session for finding content
                SessionForFindingMenu objSessionForFindingMenu = (SessionForFindingMenu)Session[Commons.ConstValues.SESSION_MENU];

                if ((objSessionForFindingMenu != null) && (!String.IsNullOrEmpty(objSessionForFindingMenu.ID_MENU_EDIT)))
                {
                    intMenuID = Convert.ToInt32(objSessionForFindingMenu.ID_MENU_EDIT);
                    Session[Commons.ConstValues.SESSION_MENU] = null;

                    txtMenuID.Text = intMenuID.ToString();
                    
                    
                    jos_menu objMenu = entities.jos_menu.Find(intMenuID);
                    txtTieude.Text = objMenu.name;
                    txtURL.Text = objMenu.link;                    
                    txtSTT.Text = (objMenu.ordering == null ? "" : objMenu.ordering.ToString());
                    edtImages.Html = objMenu.@params;

                    if (objMenu.published)
                    {
                        cbPublishedStatus.Checked = true;
                    }
                    else
                    {
                        cbPublishedStatus.Checked = false;
                    }

                    if (objMenu.parent != 0)
                        drpMenus.SelectedValue = objMenu.parent.ToString();
                    
                    //set Menu Type
                    drpMenuType.SelectedValue = objMenu.id_menutype.ToString();

                    //set Type of Page
                    var objTypeOfPage = entities.jos_menu_link_types.Join(entities.jos_type_of_page, ML => ML.id_type_of_page, P => P.id, (ML, P) => new { ML, P })
                                                   .Where (x=>x.ML.id_menu == objMenu.id).Select(y=>y.P).FirstOrDefault();

                    if (objTypeOfPage != null)
                    {
                        drpTypeOfPage.SelectedValue = objTypeOfPage.name;

                        if (CommonFuncs.TYPE_OF_PAGE_NEWS.Equals(drpTypeOfPage.SelectedValue))
                        {
                            setCategoryAndSection(objMenu);
                        }
                        else if (CommonFuncs.TYPE_OF_PAGE_CONTENT.Equals(drpTypeOfPage.SelectedValue))
                        {
                            setContent(objMenu);
                        }
                    }

                    //set controller
                    setController();
                    
                }
            }

        }

        public void setController()
        {
            if (CommonFuncs.TYPE_OF_PAGE_NEWS.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = true;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            else if (CommonFuncs.TYPE_OF_PAGE_CONTENT.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = true;
                txtURL.ReadOnly = true;
            }
            else if (CommonFuncs.TYPE_OF_PAGE_HOME.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            else if (CommonFuncs.TYPE_OF_PAGE_LINK.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            else if (CommonFuncs.TYPE_OF_PAGE_FAQ.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = true;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Content Service Page
            else if (CommonFuncs.TYPE_OF_PAGE_SERVICE_CONTENT.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = true;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Tab Service Page
            else if (CommonFuncs.TYPE_OF_PAGE_SERVICE_TAB.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = true;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Tab Guide Page
            else if (CommonFuncs.TYPE_OF_PAGE_GUIDES.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = true;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Feedback Page
            else if (CommonFuncs.TYPE_OF_PAGE_FEEDBACK.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Statistics Page
            else if (CommonFuncs.TYPE_OF_PAGE_STATISTICS.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Contact Page
            else if (CommonFuncs.TYPE_OF_PAGE_CONTACT.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = true;
                txtURL.ReadOnly = true;
            }
            //Sitemap Page
            else if (CommonFuncs.TYPE_OF_PAGE_SITEMAP.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = true;
            }
            //Others Page
            else if (CommonFuncs.TYPE_OF_PAGE_OTHERS.Equals(drpTypeOfPage.SelectedValue))
            {
                category_id_css.Visible = false;
                content_id_css.Visible = false;
                txtURL.ReadOnly = false;
            }
        }

        public void setContent (jos_menu objMenu)
        {
            var objMenuLinkType = entities.jos_menu_link_types.Where(x => x.id_menu == objMenu.id).FirstOrDefault();
            if (objMenuLinkType != null)
            {
                //set value for content
                var objContent = entities.jos_content.Where(x => x.id == objMenuLinkType.id_content).FirstOrDefault();
                if (objContent != null)
                {
                    txtContentID.Text = objContent.id.ToString();
                    txtContentTitle.Text = objContent.title;
                }
            }
        }

        public void setCategoryAndSection(jos_menu objMenu)
        {
            var objMenuLinkType = entities.jos_menu_link_types.Where(x => x.id_menu == objMenu.id).FirstOrDefault();
            if (objMenuLinkType != null)
            {
                //set value for category
                drpCategory.SelectedValue = objMenuLinkType.id_category.ToString();
                
                //set value for section
                var objSection = entities.jos_sections.Join(entities.jos_categories, S => S.id, C => C.section, (S, C) => new { S, C })
                                         .Where(x => x.C.id == objMenuLinkType.id_category).Select(y => y.S).FirstOrDefault();
                if (objSection != null)
                {
                    drpSection.SelectedValue = objSection.id.ToString();
                }
            }
        }

        /*public void drawTreeInDropDownList_Menus (int intCapMenu, string strIDMenu, DropDownList drpDownList)
        {
            int intIDMenu = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }
            else //Begin --> reset DropDownlist
            {
                drpDownList.Items.Clear();
                ListItem objListItem = new ListItem();
                objListItem.Value = Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE");
                objListItem.Text = Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE");
                drpDownList.Items.Add(objListItem);
            }

            var objMenus = entities.QT_MENUS.Where(x => (blNumber && x.ID_PARENT == intIDMenu) || (blNumber == false && x.ID_PARENT == null)).ToList();

            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intCapMenu * 6, (char) Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    ListItem objListItem = new ListItem();
                    objListItem.Value = item.ID.ToString();
                    
                    objListItem.Text = strLine + item.TIEU_DE;                                        

                    if (intCapMenu ==0) //Begining Level
                    {
                        objListItem.Attributes.Add("style", "font-weight: bold");
                    }                    
                    drpDownList.Items.Add(objListItem);
                    drawTreeInDropDownList_Menus(intCapMenu + 1, item.ID.ToString(), drpDownList);
                }
            }
        }*/

        /*public void drawMenus (string strIDMenu, int intCapMenu, Label lbMenus)
        {            
            int intIDMenu = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDMenu))
            {
                intIDMenu = Convert.ToInt32(strIDMenu);
                blNumber = true;
            }

            var objMenus = entities.QT_MENUS.Where(x => (blNumber && x.ID_PARENT == intIDMenu) || (blNumber == false && x.ID_PARENT == null)).ToList();            
                          
            if (objMenus != null)
            {
                foreach (var item in objMenus)
                {
                    string strLine = "";
                    if (intCapMenu == 1) strLine = "--";
                    else if (intCapMenu == 2) strLine = "----";
                    lbMenus.Text = lbMenus.Text + "<li>" + strLine + "<a>" + item.TIEU_DE + "</a></li>";
                    drawMenus(item.ID.ToString(), intCapMenu+1, lbMenus);                      
                }
            }
            
        }*/

        
        
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
                Commons.ValidationFuncs.errorMessage_TimeDelay("Bạn chưa nhập Tên Menu", Page);
                return false;
            }

            //Check whether URL is empty
            if (String.IsNullOrEmpty(objMenu.link))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("Bạn chưa nhập URL", Page);
                return false;
            }

            return true;
        }

        
        protected void redirectURL()
        {
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_MENUS"));
        }

        protected void btnSave_Click (object sender, EventArgs e)
        {
            try
            {
                jos_menu objMenu = new jos_menu();
                if (String.IsNullOrEmpty(txtMenuID.Text)) //Create new item
                {
                    if (entities.jos_menu.Count() > 0) objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
                    else objMenu.id = 1;

                    entities.jos_menu.Add(objMenu);
                }
                else //Edit the item
                {
                    int intMenuID = Convert.ToInt32 (txtMenuID.Text);
                    objMenu = entities.jos_menu.Find(intMenuID);
                }

                

                objMenu.name = txtTieude.Text;
                objMenu.alias = objMenu.name;
                objMenu.@params = edtImages.Html;
                objMenu.published = (cbPublishedStatus.Checked ? true : false);
                objMenu.lang_id = Commons.CommonFunctionsAndProcedures.getDefaultLanguageID(entities);

                if (CommonFuncs.TYPE_OF_PAGE_NEWS.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_NEWS.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null) {
                        objMenu.link = String.Format(objTypeOfPage.link, drpCategory.SelectedValue, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, Convert.ToInt32(drpCategory.SelectedValue), objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }                    
                    
                }
                else if (CommonFuncs.TYPE_OF_PAGE_HOME.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_HOME.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }                    
                    
                }
                else if (CommonFuncs.TYPE_OF_PAGE_CONTENT.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_CONTENT.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, txtContentID.Text, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, Convert.ToInt32(txtContentID.Text));
                    }

                }
                else if (CommonFuncs.TYPE_OF_PAGE_FAQ.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_FAQ.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, drpCategory.SelectedValue, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, Convert.ToInt32(drpCategory.SelectedValue), objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }
                else if (CommonFuncs.TYPE_OF_PAGE_LINK.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_LINK.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }

                //Type of Content Service Page
                else if (CommonFuncs.TYPE_OF_PAGE_SERVICE_CONTENT.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_SERVICE_CONTENT.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, drpCategory.SelectedValue, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, Convert.ToInt32(drpCategory.SelectedValue), objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }

                //Type of Tab Service Page
                else if (CommonFuncs.TYPE_OF_PAGE_SERVICE_TAB.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_SERVICE_TAB.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, drpCategory.SelectedValue, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, Convert.ToInt32(drpCategory.SelectedValue), objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }
                }

                //Type of Guides Page
                else if (CommonFuncs.TYPE_OF_PAGE_GUIDES.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_GUIDES.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, drpCategory.SelectedValue, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, Convert.ToInt32(drpCategory.SelectedValue), objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }
                }
                
                //Type of Feedback Page
                else if (CommonFuncs.TYPE_OF_PAGE_FEEDBACK.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_FEEDBACK.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }

                //Type of Statistics Page
                else if (CommonFuncs.TYPE_OF_PAGE_STATISTICS.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_STATISTICS.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }

                //Type of Contact Page
                else if (CommonFuncs.TYPE_OF_PAGE_CONTACT.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_CONTACT.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, txtContentID.Text, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, Convert.ToInt32(txtContentID.Text));
                    }
                }
                //Type of Sitemap Page
                else if (CommonFuncs.TYPE_OF_PAGE_SITEMAP.Equals(drpTypeOfPage.SelectedValue))
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_SITEMAP.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = String.Format(objTypeOfPage.link, objMenu.id);

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }

                }
                //Type of Others Page
                else if (CommonFuncs.TYPE_OF_PAGE_OTHERS.Equals(drpTypeOfPage.SelectedValue)) 
                {
                    var objTypeOfPage = entities.jos_type_of_page.Where(x => CommonFuncs.TYPE_OF_PAGE_OTHERS.Equals(x.name)).FirstOrDefault();
                    if (objTypeOfPage != null)
                    {
                        objMenu.link = txtURL.Text;

                        //Save into Menu Link Type
                        updateMenuLinkType(objMenu.id, CommonFuncs.NUMBER_INVALID_INTEGER, objTypeOfPage.id, CommonFuncs.NUMBER_INVALID_INTEGER);
                    }
                }
                


                if (!String.IsNullOrEmpty(txtSTT.Text))
                    objMenu.ordering = Convert.ToInt16(txtSTT.Text);
                string str = drpMenus.SelectedValue;
                if (!drpMenus.SelectedValue.Equals(Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")))
                {
                    objMenu.parent = Convert.ToInt32(drpMenus.SelectedValue);

                    if (objMenu.id == objMenu.parent) //Avoid assigning itself
                    {
                        Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_ERROR_KHONG_CHON_MENU_CHA"), Page);
                        return;
                    }
                    else
                    {
                        jos_menu objParentMenu = entities.jos_menu.Find(objMenu.parent);
                        if (objParentMenu != null)
                        {
                            objMenu.id_menutype = objParentMenu.id_menutype;
                        }
                    }
                }
                else
                {
                    objMenu.parent = 0;
                    
                    if (!String.IsNullOrEmpty(drpMenuType.SelectedValue))
                    {
                        objMenu.id_menutype = Convert.ToInt32(drpMenuType.SelectedValue);
                    }
                    else
                    {
                        Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_ERROR_SELECT_MENU_TYPE"), Page);
                        return;
                    }
                }



                if (validation(objMenu))
                {
                    //Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);
                    
                    entities.SaveChanges();
                    //Save System Log
                    Commons.CommonFunctionsAndProcedures.saveSystemLog(String.Format(Commons.TitleConst.getTitleConst("LOG_MENU_CREATE"), objMenu.name));

                    //set value for session
                    setSessionForFindingMenu();

                    redirectURL();
                }
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }

        protected void updateMenuLinkType(int intMenuID, int intCategoryID, int intTypeOfPageID, int intContentID)
        {
            jos_menu_link_types objMenuLinkType;
            objMenuLinkType = entities.jos_menu_link_types.Where(x => x.id_menu == intMenuID).FirstOrDefault();
            if (objMenuLinkType == null)
            {
                objMenuLinkType = new jos_menu_link_types();
                if (entities.jos_menu_link_types.Count() > 0) objMenuLinkType.id = entities.jos_menu_link_types.Max(x => x.id) + 1;
                else objMenuLinkType.id = 1;
                entities.jos_menu_link_types.Add(objMenuLinkType);
            }

            objMenuLinkType.id_menu = intMenuID;
            objMenuLinkType.id_category = intCategoryID;
            objMenuLinkType.id_type_of_page = intTypeOfPageID;
            objMenuLinkType.id_content = intContentID;            
        }

        //set session for finding Menus
        protected void setSessionForFindingMenu()
        {
            SessionForFindingMenu objSessionForFindingMenu = new SessionForFindingMenu();
            objSessionForFindingMenu.ID_MENU_TYPE = drpMenuType.SelectedValue;
            Session[Commons.ConstValues.SESSION_MENU] = objSessionForFindingMenu;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            redirectURL();
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFunctionsAndProcedures.getCategories(drpCategory, drpSection.SelectedValue, entities);
        }

        protected void drpTypeOfPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            setController();
        }
        
    }
}