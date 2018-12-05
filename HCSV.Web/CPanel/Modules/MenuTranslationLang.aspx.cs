using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class MenuTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strMenuID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);
                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);

                //get Category ID from session for finding Menu
                SessionForFindingMenu objSessionForFindingMenu = (SessionForFindingMenu)Session[Commons.ConstValues.SESSION_MENU];

                if (objSessionForFindingMenu != null)
                {
                    strMenuID = objSessionForFindingMenu.ID_MENU;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingMenu.ID_LANGUAGE;
                }

                //get Category for origin language
                if (!String.IsNullOrEmpty(strMenuID)) //Edit Category
                {
                    int intMenuID = Convert.ToInt32(strMenuID);
                    jos_menu objMenu = entities.jos_menu.Where(x => x.id == intMenuID).FirstOrDefault();
                    if (objMenu != null)
                    {                        
                        txtName.Text = objMenu.name;
                        //set begin value for Language
                        drpLanguages.SelectedValue = objMenu.lang_id.ToString();
                        //set begin value for Category ID Textbox
                        txtMenuID.Text = objMenu.id.ToString();
                        //set begin value for Representative Image
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intMenuID 
                                                                    && o.language_id == intLangTranslationID
                                                                    && (Commons.ConstTable.TBL_JOS_MENU.Equals(o.reference_table))).FirstOrDefault();

                    if (objTranslation != null)
                    {
                        txtTranslation.Text = objTranslation.id.ToString();
                        int refMenuID = objTranslation.reference_id;
                        jos_menu objMenu2 = entities.jos_menu.Where(x => x.id == refMenuID).FirstOrDefault();
                        // lay content cua translation language (reference_ID)
                        if (objMenu2 != null)
                        {                            
                            txtName2.Text = objMenu2.name; 
                            
                            //set begin value for Languages
                            drpLanguages2.SelectedValue = objMenu2.lang_id.ToString();
                            //set begin value for objCategory2 ID Textbox
                            txtMenuID2.Text = objMenu2.id.ToString();
                            
                        }
                        else
                        {
                            txtMenuID2.Text = "";
                        }
                    }
                    else
                    {
                        txtTranslation.Text = "";
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validate())//Validation before updating            
            {
                return;
            }

            jos_menu objMenu2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtMenuID2.Text))
            {
                objMenu2 = new jos_menu();
                if (entities.jos_menu.Count() > 0) objMenu2.id = entities.jos_menu.Max(x => x.id) + 1;
                else objMenu2.id = 1;

                entities.jos_menu.Add(objMenu2);
            }
            else
            {
                int intMenuID2 = Convert.ToInt32(txtMenuID2.Text);
                objMenu2 = entities.jos_menu.Where(x => x.id == intMenuID2).FirstOrDefault();
            }
            
            objMenu2.name = txtName2.Text;
            objMenu2.alias = txtName2.Text;
            
            objMenu2.lang_id = Convert.ToInt16(drpLanguages2.SelectedValue.ToString());
            objMenu2.published = true;                

            entities.SaveChanges();
            string refId = objMenu2.id.ToString();
            
            // add item for Translation
            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtMenuID.Text);
                objTranslation.reference_id = Convert.ToInt32(refId);
                objTranslation.modified = DateTime.Now;
                entities.jos_language_translation.Add(objTranslation);                
            }
            else
            {
                int intTranID = Convert.ToInt32(txtTranslation.Text);
                objTranslation = entities.jos_language_translation.Where(x => x.id == intTranID).FirstOrDefault();
            }
            objTranslation.reference_table = Commons.ConstTable.TBL_JOS_MENU;
            objTranslation.modified = DateTime.Now;
            
            entities.SaveChanges();
            setSessionForFindingMenu();

            //redirect URL
            Response.Redirect("/Modules/MenuChucNang");
        }


        public static void getLanguagesTranslation(DropDownList drpLanguages2, DropDownList drpLanguages, TCSEntities entities)
        {
            int intTranslationLang = Convert.ToInt16(drpLanguages.SelectedValue);
            var lstLanguages = entities.jos_languages.Where(a => a.published == true && a.lang_id != intTranslationLang).OrderByDescending(x => x.default_status).ToList();
            drpLanguages2.DataValueField = "lang_id";
            drpLanguages2.DataTextField = "title";
            drpLanguages2.DataSource = lstLanguages;
            drpLanguages2.DataBind();
        }

        public static void getPosition(DropDownList drpPosition)
        {
            drpPosition.Items.Clear();

            string[] lstPosition = Commons.CommonFuncs.getListOfPosition();
            for (int i = 0; i < lstPosition.Length; i++)
            {
                ListItem lstItem = new ListItem();
                lstItem.Text = lstPosition[i];
                lstItem.Value = lstPosition[i];
                drpPosition.Items.Add(lstItem);
            }
        }

        protected bool validate()
        {
            if (String.IsNullOrEmpty(txtName2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Content", Page);
                return false;
            }

            return true;
        }



        //set session for finding content
       protected void setSessionForFindingMenu()
        {
            SessionForFindingMenu objSessionForFindingMenu = new SessionForFindingMenu();
            objSessionForFindingMenu.ID_MENU = "";
            objSessionForFindingMenu.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CATEGORY] = objSessionForFindingMenu;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/MenuChucNang");
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}