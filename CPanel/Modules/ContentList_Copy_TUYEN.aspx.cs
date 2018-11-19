using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CPanel.Models;
namespace CPanel.Modules
{
    public partial class ContentList : System.Web.UI.Page    
    {        
        private cpanelEntities entities = new cpanelEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Categories
                getCategories();                
            }
            searchContents();
        }

        public void getCategories()
        {
            //get Categories
            Commons.CommonFuncs.getCategories(drpCategory, entities, Convert.ToInt16(drpLanguages.SelectedValue));            
        }

        protected void getUsers() {
            
            
        }

        protected void grvContents_DataBinding(object sender, EventArgs e)
        {            
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchContents();
        }

        public void searchContents ()
        {
            //get info from session for finding content
            SessionForFindingContent objSessionForFindingContent = (SessionForFindingContent)Session[Commons.ConstValues.SESSION_CONTENT];
            if (objSessionForFindingContent != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingContent.ID_LANGUAGE;
                //get Category
                getCategories();  
                drpCategory.SelectedValue = objSessionForFindingContent.ID_CATEGORY;
                
            }

            //Search Content
            int intCatID;

            if (drpCategory.SelectedValue.Equals(Commons.CommonFuncs.BLANK_ITEM_VALUE)) intCatID = 0;
            else intCatID = Convert.ToInt32(drpCategory.SelectedValue);

            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);
            
            //var lstContents = entities.jos_content.Where(x => ((intCatID == 0) ? true : x.catid == intCatID) && (x.lang_id == intLanguageID) && (x.state == 1)).ToList();
            var lstContents = entities.jos_content.Where(x => ((intCatID == 0) ? true : x.catid == intCatID) && (x.state == 1)).ToList();
            grvContents.DataSource = lstContents;
            grvContents.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_CONTENT] = null;
        }

        protected void drpCategory_DataBinding(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstContents = grvContents.GetSelectedFieldValues(grvContents.KeyFieldName);
            foreach (long i in lstContents)
            {
                jos_content objContent = entities.jos_content.Where(x => x.id == i).FirstOrDefault();
                objContent.state = 0;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_CONTENT_VIEW);
        }        

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding content
            setSessionForFindingContent();
            
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_CONTENT_EDIT);
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {
            
        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get Categories
            getCategories();

            searchContents();
        }

        protected void btnViewContent_Click(object sender, EventArgs e)
        {
            //set session for finding content
            setSessionForFindingContent();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_CONTENT_EDIT);
        }

        //set session for finding content
        protected void setSessionForFindingContent()        {
            
            SessionForFindingContent objSessionForFindingContent = new SessionForFindingContent();
            objSessionForFindingContent.ID_CONTENT = txtContentID.Text;
            objSessionForFindingContent.ID_CATEGORY = drpCategory.SelectedValue;
            objSessionForFindingContent.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CONTENT] = objSessionForFindingContent;
        }

        
    }
}