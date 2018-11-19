using DevExpress.Web.ASPxGridView;
using CPanel.Commons;
//using QLNS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class ContentSelection_Popup : System.Web.UI.Page    
    {
        public string ACCOUNT_STATUS_PENDING = "0";
        public string ACCOUNT_STATUS_APPROVED = "1";
        public string ACCOUNT_STAUTS_REJECTED = "-1";

        public static string lbNotice;
        private TCSEntities entities = new TCSEntities ();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                CommonFunctionsAndProcedures.getSections(drpSection, entities);
                CommonFunctionsAndProcedures.getCategories(drpCategory, drpSection.SelectedValue, entities);
                
                grvUsers.DataBind();
            }
        }

        
        protected void grvUsers_DataBinding(object sender, EventArgs e)
        {
            bool blSearchAll = true;
            int intCatID = CommonFuncs.NUMBER_INVALID_INTEGER;
            int intDefaultLangID = Commons.CommonFunctionsAndProcedures.getDefaultLanguageID(entities);

            if (!Commons.CommonFuncs.BLANK_ITEM_VALUE.Equals(drpCategory.SelectedValue))
            {
                blSearchAll = false;
                intCatID = Convert.ToInt32 (drpCategory.SelectedValue);
            }
            var lstContents = entities.jos_content.Where(x => (blSearchAll ? true : x.catid == intCatID) 
                                                        && (x.state == 1)
                                                        && (x.lang_id == intDefaultLangID)).ToList();
            grvUsers.DataSource = lstContents;
        }

        protected void drpUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshPage();
        }

        public void refreshPage()
        {        
            grvUsers.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var lstUserId = grvUsers.GetSelectedFieldValues(grvUsers.KeyFieldName);
            foreach (long i in lstUserId)
            {
                var objContent = entities.jos_content.Where (x=>x.id == i).FirstOrDefault();

                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "selectNode('"+i+"', '"+objContent.title+"');", true);
                //Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "alert('FFF');selectNodeTuyen();", true);
                
                break;
            }            
        }

        protected void drpSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonFunctionsAndProcedures.getCategories(drpCategory, drpSection.SelectedValue, entities);
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvUsers.DataBind();
        }
    }
}