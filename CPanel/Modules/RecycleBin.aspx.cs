using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class RecycleBin : System.Web.UI.Page    
    {        
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                getTypeOfContent(drpTypeOfContent);
                
            }
            searchContents();
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
            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);
            if (drpTypeOfContent.SelectedValue.Equals (Commons.CommonFuncs.RECYCLE_BIN_CONTENT_TITLE))
            {
                div_category.Visible = false;
                div_content.Visible = true;

                var lstContents = entities.jos_content.Where(x => (x.lang_id == intLanguageID) && (x.state != 1)).ToList();
                grvContents.DataSource = lstContents;
                grvContents.DataBind();
            }
            else if (drpTypeOfContent.SelectedValue.Equals(Commons.CommonFuncs.RECYCLE_BIN_CATEGORY_TITLE))
            {
                div_category.Visible = true;
                div_content.Visible = false;

                var lstCategories = entities.jos_categories.Where(x => (x.lang_id == intLanguageID) && (x.published == false)).ToList();
                grvCategories.DataSource = lstCategories;
                grvCategories.DataBind();
            }

            
        }

     

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchContents();
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            if (drpTypeOfContent.SelectedValue.Equals(Commons.CommonFuncs.RECYCLE_BIN_CONTENT_TITLE))
            {
                var lstContents = grvContents.GetSelectedFieldValues(grvContents.KeyFieldName);
                foreach (long i in lstContents)
                {
                    jos_content objContent = entities.jos_content.Where(x => x.id == i).FirstOrDefault();
                    objContent.state = 1;
                    entities.SaveChanges();
                }
            }
            else if (drpTypeOfContent.SelectedValue.Equals(Commons.CommonFuncs.RECYCLE_BIN_CATEGORY_TITLE))
            {
                var lstCategories = grvCategories.GetSelectedFieldValues(grvCategories.KeyFieldName);
                foreach (int i in lstCategories)
                {
                    jos_categories objCategory = entities.jos_categories.Where(x => x.id == i).FirstOrDefault();
                    objCategory.published = true;
                    entities.SaveChanges();
                }
            }
            
            Response.Redirect("/Modules/RecycleBin");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (drpTypeOfContent.SelectedValue.Equals(Commons.CommonFuncs.RECYCLE_BIN_CONTENT_TITLE))
            {
                var lstContents = grvContents.GetSelectedFieldValues(grvContents.KeyFieldName);
                foreach (long i in lstContents)
                {
                    jos_content objContent = entities.jos_content.Where(x => x.id == i).FirstOrDefault();
                    entities.jos_content.Remove(objContent);
                    entities.SaveChanges();
                }
            }
            else if (drpTypeOfContent.SelectedValue.Equals(Commons.CommonFuncs.RECYCLE_BIN_CATEGORY_TITLE))
            {
                var lstCategories = grvCategories.GetSelectedFieldValues(grvCategories.KeyFieldName);
                foreach (int i in lstCategories)
                {
                    jos_categories objCategory = entities.jos_categories.Where(x => x.id == i).FirstOrDefault();
                    entities.jos_categories.Remove(objCategory);
                    entities.SaveChanges();
                }
            }
            
            Response.Redirect("/Modules/RecycleBin");
        }

        public void getTypeOfContent (DropDownList drpTypeOfContent)
        {
            drpTypeOfContent.Items.Add(new ListItem(Commons.CommonFuncs.RECYCLE_BIN_CONTENT_TITLE, Commons.CommonFuncs.RECYCLE_BIN_CONTENT_TITLE));
            drpTypeOfContent.Items.Add(new ListItem(Commons.CommonFuncs.RECYCLE_BIN_CATEGORY_TITLE, Commons.CommonFuncs.RECYCLE_BIN_CATEGORY_TITLE));            
        }

        protected void drpTypeOfContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchContents();
        }

        protected void grvCategories_DataBinding(object sender, EventArgs e)
        {

        }
        
    }
}