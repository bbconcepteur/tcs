
using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class CategoryList : System.Web.UI.Page

    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Sections
                Commons.CommonFunctionsAndProcedures.getSections(drpSections, entities);
            }
            searchCategories();
        }

       

        protected void getUsers()
        {


        }

        protected void grvCategories_DataBinding(object sender, EventArgs e)
        {
        }


        public void searchCategories()
        {
            //get info from session for finding category
            SessionForFindingCategory objSessionForFindingCategory = (SessionForFindingCategory)Session[Commons.ConstValues.SESSION_CATEGORY];
            if (objSessionForFindingCategory != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingCategory.ID_LANGUAGE;
            }

            //Search category
            //int intCatID;

            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);

            grvCategories.DataSource = CommonFunctionsAndProcedures.getCategoriesBySectionID (drpSections.SelectedValue, entities);
            grvCategories.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_CATEGORY] = null;
        }

        protected void drpCategory_DataBinding(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstCategory = grvCategories.GetSelectedFieldValues(grvCategories.KeyFieldName);
            foreach (int i in lstCategory)
            {
                jos_categories objcategory = entities.jos_categories.Where(x => x.id == i).FirstOrDefault();
                objcategory.published = false;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_CATEGORY_VIEW);
        }
        protected void btnViewCategory_Click(object sender, EventArgs e)
        {
            //set session for finding category
            setSessionForFindingCategory();
            //redirect
            Response.Redirect(Commons.ConstURL.URL_CATEGORY_EDIT);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding category
            setSessionForFindingCategory();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_CATEGORY_EDIT);
        }

        //set session for finding category
        protected void setSessionForFindingCategory()
        {

            SessionForFindingCategory objSessionForFindingCategory = new SessionForFindingCategory();
            objSessionForFindingCategory.ID_CATEGORY = txtCategoryID.Text;
            objSessionForFindingCategory.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CATEGORY] = objSessionForFindingCategory;
        }


        protected void btnViewTranslation_Click(object sender, EventArgs e)
        {

            //set session for finding category
            setSessionForFindingCategory();

            //redirect
            Response.Redirect("/Modules/CategoryTranslationLang");

        }

        protected void drpSections_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchCategories();
        }


    }
}