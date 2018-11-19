using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class LanguageList : System.Web.UI.Page
    {

        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            searchLanguages();
        }

        protected void getUsers()
        {


        }

        protected void grvLanguages_DataBinding(object sender, EventArgs e)
        {

        }


        public void searchLanguages()
        {
            //get info from session for finding category
            SessionForFindingLanguage objSessionForFindingLanguage = (SessionForFindingLanguage)Session[Commons.ConstValues.SESSION_LANGUAGE];
            if (objSessionForFindingLanguage != null)
            {

            }

            //Search category
            //int intCatID;

            var lstLanguages = entities.jos_languages.Where(x => (x.published == true)).ToList();

            grvLanguages.DataSource = lstLanguages;
            grvLanguages.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_CATEGORY] = null;
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstLanguage = grvLanguages.GetSelectedFieldValues(grvLanguages.KeyFieldName);
            foreach (int i in lstLanguage)
            {
                jos_languages objlanguage = entities.jos_languages.Where(x => x.lang_id == i).FirstOrDefault();
                objlanguage.published = false;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_LANGUAGE_LIST);
        }
        protected void btnViewLanguage_Click(object sender, EventArgs e)
        {
            //set session for finding Language
            setSessionForFindingLanguage();
            //redirect
            Response.Redirect(Commons.ConstURL.URL_LANGUAGE_EDIT);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding Language
            setSessionForFindingLanguage();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_LANGUAGE_EDIT);
        }

        //set session for finding Language
        protected void setSessionForFindingLanguage()
        {

            SessionForFindingLanguage objSessionForFindingLanguage = new SessionForFindingLanguage();
            objSessionForFindingLanguage.ID_LANGUAGE = txtLanguageID.Text;
            Session[Commons.ConstValues.SESSION_LANGUAGE] = objSessionForFindingLanguage;
        }


        protected void drpSections_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchLanguages();
        }


    }
}