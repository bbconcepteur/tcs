using CPanel.Commons;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class ContentEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getPosition(drpPosition);
                string strContentID = null;
                
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);
                
                //get Content ID from session for finding content
                SessionForFindingContent objSessionForFindingContent = (SessionForFindingContent)Session[Commons.ConstValues.SESSION_CONTENT];
                
                if (objSessionForFindingContent != null)
                {
                    strContentID = objSessionForFindingContent.ID_CONTENT;
                    
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingContent.ID_LANGUAGE;
                }

                //get Categories
                Commons.CommonFuncs.getCategories(drpCategories, entities, Convert.ToInt16(drpLanguages.SelectedValue));

                //get Content
                if (!String.IsNullOrEmpty(strContentID)) //Edit Content
                {                    
                    int intContentID = Convert.ToInt32(strContentID);
                    jos_content objContent = entities.jos_content.Where(x => x.id == intContentID).FirstOrDefault();
                    if (objContent != null)
                    {
                        edtIntroContent.Value = CommonFuncs.convertContent(objContent.introtext);
                        edtFullContent.Value = CommonFuncs.convertContent(objContent.fulltext);
                        txtTitle.Text = objContent.title;
                        txtOrder.Text = objContent.ordering != null ? objContent.ordering.ToString() : "";
                        cbSpecialContentType.Checked = (objContent.mask == 1 ? true : false);
                    

                        //set begin value for Position
                        if (!String.IsNullOrEmpty(objContent.position))
                            drpPosition.SelectedValue = objContent.position;
                        //set begin value for Categories
                        drpCategories.SelectedValue = objContent.catid.ToString();
                        //set begin value for Language
                        drpLanguages.SelectedValue = objContent.lang_id.ToString();
                        //set begin value for Content ID Textbox
                        txtContentID.Text = objContent.id.ToString();
                        //set begin value for Representative Image
                        edtRepresentativeImage.Value = CommonFuncs.convertContent(objContent.images);
                    }
                }
                else //Create new content
                {
                    drpLanguages.SelectedValue = objSessionForFindingContent.ID_LANGUAGE;
                    drpCategories.SelectedValue = objSessionForFindingContent.ID_CATEGORY;
                }

            }

        }

        public static void getPosition(DropDownList drpPosition)
        {
            drpPosition.Items.Clear();

            string [] lstPosition = Commons.CommonFuncs.getListOfPosition();
            for (int i=0; i<lstPosition.Length; i++) {
                ListItem lstItem = new ListItem();
                lstItem.Text = lstPosition[i];
                lstItem.Value = lstPosition[i];
                drpPosition.Items.Add(lstItem);                
            }            
        }

        protected bool validate()
        {
            if (String.IsNullOrEmpty(txtTitle.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title", Page);
                return false;
            }

            if (String.IsNullOrEmpty(edtRepresentativeImage.Value) && String.IsNullOrEmpty(edtIntroContent.Value) && String.IsNullOrEmpty(edtFullContent.Value))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Content", Page);
                return false;
            }

            if (drpCategories.SelectedValue.Equals(Commons.CommonFuncs.BLANK_ITEM_VALUE))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must choose Category", Page);
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

            jos_content objContent;
            //in case of create new item
            if (String.IsNullOrEmpty(txtContentID.Text))
            {
                objContent = new jos_content();
                objContent.created = null;
                objContent.modified = null;
                objContent.checked_out_time = null;
                objContent.publish_down = null;
                objContent.publish_up = null;                        
                entities.jos_content.Add(objContent);
            }
            else//in case of update info
            {
                int intContentID = Convert.ToInt32 (txtContentID.Text);
                objContent = entities.jos_content.Where(x=>x.id == intContentID).FirstOrDefault();
                
            }
            objContent.mask = (cbSpecialContentType.Checked ? 1 : 0);
            objContent.title = txtTitle.Text;
            objContent.introtext = edtIntroContent.Value;
            objContent.fulltext = edtFullContent.Value;
            objContent.images = edtRepresentativeImage.Value;
            objContent.catid = Convert.ToInt32(drpCategories.SelectedValue);
            objContent.lang_id = Convert.ToInt32(drpLanguages.SelectedValue);
            objContent.position = drpPosition.SelectedValue;
            
            if (!String.IsNullOrEmpty(txtOrder.Text))
                objContent.ordering = Convert.ToInt32(txtOrder.Text);
            else
                objContent.ordering = 1;
            objContent.state = 1;//1 ~ public
            
            
            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingContent();

            //redirect URL
            Response.Redirect("/Modules/ContentList");            
        }

        //set session for finding content
        protected void setSessionForFindingContent()
        {
            SessionForFindingContent objSessionForFindingContent = new SessionForFindingContent();
            objSessionForFindingContent.ID_CONTENT = "";
            objSessionForFindingContent.ID_CATEGORY = drpCategories.SelectedValue;
            objSessionForFindingContent.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CONTENT] = objSessionForFindingContent;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/ContentList");
        }

        protected void drpLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get Categories
            Commons.CommonFuncs.getCategories(drpCategories, entities, Convert.ToInt16(drpLanguages.SelectedValue));
        }
    }
}