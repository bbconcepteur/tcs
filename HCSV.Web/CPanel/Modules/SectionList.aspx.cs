using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class SectionList : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);
            }
            searchSections();
        }

        protected void getUsers()
        {


        }

        protected void grvSections_DataBinding(object sender, EventArgs e)
        {
        }

        protected void drpSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchSections();
        }

        public void searchSections()
        {
            //get info from session for finding section
            SessionForFindingSection objSessionForFindingSection = (SessionForFindingSection)Session[Commons.ConstValues.SESSION_SECTION];

            //Search Section                
            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);

            var lstSections = entities.jos_sections.Where(x => (x.published == true) && (x.lang_id == intLanguageID)).ToList();
            grvSections.DataSource = lstSections;
            grvSections.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_SECTION] = null;
        }

        protected void drpCategory_DataBinding(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstSections = grvSections.GetSelectedFieldValues(grvSections.KeyFieldName);
            foreach (int i in lstSections)
            {
                jos_sections objSection = entities.jos_sections.Where(x => x.id == i).FirstOrDefault();
                objSection.published = false;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_SECTION_VIEW);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding Section
            setSessionForFindingSection();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_SECTION_EDIT);
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            searchSections();
        }

        protected void btnViewSection_Click(object sender, EventArgs e)
        {
            //set session for finding Section
            setSessionForFindingSection();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_SECTION_EDIT);
        }

        //set session for finding Section
        protected void setSessionForFindingSection()
        {

            SessionForFindingSection objSessionForFindingSection = new SessionForFindingSection();
            objSessionForFindingSection.ID_SECTION = txtSectionID.Text;
            Session[Commons.ConstValues.SESSION_SECTION] = objSessionForFindingSection;
        }


        protected void btnViewTranslation_Click(object sender, EventArgs e)
        {

            //set session for finding Section
            setSessionForFindingSection();

            //redirect
            Response.Redirect("/Modules/SectionTranslationLang");

        }

    }
}