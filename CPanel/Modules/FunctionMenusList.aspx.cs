using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class FunctionMenusList : System.Web.UI.Page    
    {        
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //Check Rule: not SUPPER_ADMIN
                if (!Commons.CheckUserInfo.checkSupperAdmin())
                {
                    btnCreate.Visible = false;
                }                
            }
            searchCategories();
        }

        protected void getUsers() {
            
            
        }

        protected void grvContents_DataBinding(object sender, EventArgs e)
        {            
        }

        public void searchCategories ()
        {
            //get info from session for finding Menu
            SessionForFindingContent objSessionForFindingContent = (SessionForFindingContent)Session[Commons.ConstValues.SESSION_CONTENT];
            if (objSessionForFindingContent != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingContent.ID_LANGUAGE;
            }
   
            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);
            
            var lstCategories = entities.jos_categories.Where(x => (x.lang_id == intLanguageID) && (x.published == true)).OrderBy(y=>y.ordering).ToList();
            grvCategories.DataSource = lstCategories;
            grvCategories.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_CONTENT] = null;
        }

       
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstCategories = grvCategories.GetSelectedFieldValues(grvCategories.KeyFieldName);
            
            foreach (int i in lstCategories)
            {                
                jos_categories objCategories = entities.jos_categories.Where(x => x.id == i).FirstOrDefault();
                objCategories.published = false;
                entities.SaveChanges();
            }
            Response.Redirect("/Modules/FunctionMenusList");
        }        

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/FunctionMenusEdit");
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchCategories();

        }

        protected void grvCategories_DataBinding(object sender, EventArgs e)
        {

        }

        protected void setSessionForFindingMenus()
        {

            SessionForFindingContent objSessionForFindingContent = new SessionForFindingContent();
            objSessionForFindingContent.ID_LANGUAGE = drpLanguages.SelectedValue;
            objSessionForFindingContent.ID_MENU = txtMenuID.Text;
            Session[Commons.ConstValues.SESSION_CONTENT] = objSessionForFindingContent;
        }

        protected void btnViewMenus_Click(object sender, EventArgs e)
        {
            //set session for finding content
            setSessionForFindingMenus();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_MENU_EDIT);
        }

        
    }
}