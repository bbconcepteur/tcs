using System;
using System.Collections.Generic;
using System.Linq;
using CPanel.Commons;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class LanguageEdit : System.Web.UI.Page
    {

        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strLanguageID = null;

                //get Language ID from session for finding Language
                SessionForFindingLanguage objSessionForFindingLanguage = (SessionForFindingLanguage)Session[Commons.ConstValues.SESSION_LANGUAGE];

                if (objSessionForFindingLanguage != null)
                {
                    strLanguageID = objSessionForFindingLanguage.ID_LANGUAGE;
                }


                //get Language
                if (!String.IsNullOrEmpty(strLanguageID)) //Edit Language
                {
                    int intLanguageID = Convert.ToInt32(strLanguageID);
                    jos_languages objLanguage = entities.jos_languages.Where(x => x.lang_id == intLanguageID).FirstOrDefault();
                    if (objLanguage != null)
                    {
                        txtLanguageID.Text = objLanguage.lang_id.ToString();
                        //set begin value 
                        txtTitle.Text = objLanguage.title;
                        txtLang_code.Text = objLanguage.lang_code;
                        txtDescription.Text = objLanguage.description;
                        txtSef.Text = objLanguage.sef;
                        
                        cbPublished.Checked = objLanguage.published;

                        txtDefault_status.Text = objLanguage.default_status.ToString();
                        edtImages.Html = CommonFuncs.convertContent(objLanguage.image);

                    }
                }
                else //Create new Language
                {
                    
                }

            }

        }

        protected bool validate()
        {
            if (String.IsNullOrEmpty(txtTitle.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title", Page);
                return false;
            }

            if (String.IsNullOrEmpty(txtLang_code.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Lang_code", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtSef.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Sef", Page);
                return false;
            }
            
            if (String.IsNullOrEmpty(edtImages.Html))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Images", Page);
                return false;
            }
            if (txtDefault_status.Text != "0" && txtDefault_status.Text != "1")
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter 0 or 1 on Default_status", Page);
                return false;
            }


            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validate())//Validation before updating            
            {
                return;
            }

            jos_languages objLanguage;
            //in case of create new item
            if (String.IsNullOrEmpty(txtLanguageID.Text))
            {
                objLanguage = new jos_languages();
                entities.jos_languages.Add(objLanguage);
            }
            else//in case of update info
            {
                int intLanguageID = Convert.ToInt32(txtLanguageID.Text);
                objLanguage = entities.jos_languages.Where(x => x.lang_id == intLanguageID).FirstOrDefault();
            }

            objLanguage.title = txtTitle.Text;
            objLanguage.lang_code = txtLang_code.Text;
            objLanguage.description = txtDescription.Text;
            objLanguage.sef = txtSef.Text;
            objLanguage.published = cbPublished.Checked;
            objLanguage.default_status = Convert.ToByte(txtDefault_status.Text);
            objLanguage.image = edtImages.Html;

            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingLanguage();

            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_LANGUAGE_LIST);
        }

        //set session for finding language
        protected void setSessionForFindingLanguage()
        {
            SessionForFindingLanguage objSessionForFindingLanguage = new SessionForFindingLanguage();
            objSessionForFindingLanguage.ID_LANGUAGE = "";
            Session[Commons.ConstValues.SESSION_LANGUAGE] = objSessionForFindingLanguage;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Commons.ConstURL.URL_LANGUAGE_LIST);
        }

        protected void drpSections_DataBinding(object sender, System.EventArgs e)
        {

        }
    }
}