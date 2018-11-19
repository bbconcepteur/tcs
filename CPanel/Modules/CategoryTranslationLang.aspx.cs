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
    public partial class CategoryTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCategoryID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);
             // get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);
                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);

                //get Category ID from session for finding Category
                SessionForFindingCategory objSessionForFindingCategory = (SessionForFindingCategory)Session[Commons.ConstValues.SESSION_CATEGORY];

                if (objSessionForFindingCategory != null)
                {
                    strCategoryID = objSessionForFindingCategory.ID_CATEGORY;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingCategory.ID_LANGUAGE;
                }

                //get Category for origin language
                if (!String.IsNullOrEmpty(strCategoryID)) //Edit Category
                {
                    int intCategoryID = Convert.ToInt32(strCategoryID);
                    jos_categories objCategory = entities.jos_categories.Where(x => x.id == intCategoryID).FirstOrDefault();
                    if (objCategory != null)
                    {
                        txtTitle.Text = objCategory.title;
                        txtName.Text = objCategory.name;
                        //set begin value for Language
                        drpLanguages.SelectedValue = objCategory.lang_id.ToString();
                        //set begin value for Category ID Textbox
                        txtCategoryID.Text = objCategory.id.ToString();
                        //set begin value for Representative Image
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intCategoryID && o.language_id == intLangTranslationID).FirstOrDefault();

                    if (objTranslation != null)
                    {
                        txtTranslation.Text = objTranslation.id.ToString();
                        int refCategoryID = objTranslation.reference_id;
                        jos_categories objCategory2 = entities.jos_categories.Where(x => x.id == refCategoryID).FirstOrDefault();
                        // lay content cua translation language (reference_ID)
                        if (objCategory2 != null)
                        {
                            txtTitle2.Text = objCategory2.title;
                            txtName2.Text = objCategory2.name; 
                            
                            //set begin value for Languages
                            drpLanguages2.SelectedValue = objCategory2.lang_id.ToString();
                            //set begin value for objCategory2 ID Textbox
                            txtCategoryID2.Text = objCategory2.id.ToString();
                            
                        }
                        else
                        {
                            txtCategoryID2.Text = "";
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

            jos_categories objCategory2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtCategoryID2.Text))
            {
                objCategory2 = new jos_categories();
                objCategory2.title = txtTitle2.Text;
                objCategory2.name=txtName2.Text;
                objCategory2.lang_id = Convert.ToInt16(drpLanguages2.SelectedValue.ToString());
                objCategory2.published=true;
                entities.jos_categories.Add(objCategory2);
            }
            else
            {
                int intCategoryID2 = Convert.ToInt32(txtCategoryID2.Text);
                objCategory2 = entities.jos_categories.Where(x => x.id == intCategoryID2).FirstOrDefault();
            }
           
            objCategory2.title = txtTitle2.Text;
            objCategory2.name = txtName2.Text;
            objCategory2.lang_id = Convert.ToInt16(drpLanguages2.SelectedValue.ToString());
            entities.SaveChanges();
            string refId = objCategory2.id.ToString();
            // add item for Translation

            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtCategoryID.Text);
                objTranslation.reference_id = Convert.ToInt32(refId);
                objTranslation.modified = DateTime.Now;
                entities.jos_language_translation.Add(objTranslation);
                entities.SaveChanges();
            }
            else
            {
                int intTranID = Convert.ToInt32(txtTranslation.Text);
                objTranslation = entities.jos_language_translation.Where(x => x.id == intTranID).FirstOrDefault();
            }
            objTranslation.modified = DateTime.Now;
            entities.SaveChanges();
            setSessionForFindingCategory();

            //redirect URL
            Response.Redirect("/Modules/CategoryList");
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
            if (String.IsNullOrEmpty(txtTitle2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title for", Page);
                return false;
            }

            if (String.IsNullOrEmpty(txtName2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Content", Page);
                return false;
            }

            return true;
        }



        //set session for finding content
       protected void setSessionForFindingCategory()
        {
            SessionForFindingCategory objSessionForFindingCategory = new SessionForFindingCategory();
            objSessionForFindingCategory.ID_CATEGORY = "";
            objSessionForFindingCategory.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CATEGORY] = objSessionForFindingCategory;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/CategoryList");
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}