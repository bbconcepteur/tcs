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
    public partial class FunctionMenusEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Check Rule: not SUPPER_ADMIN
                if (!Commons.CheckUserInfo.checkSupperAdmin())
                {
                    txtName.ReadOnly = true;
                    drpLanguages.Enabled=false;
                }                

                //get Content ID from session for finding content
                SessionForFindingContent objSessionForFindingContent = (SessionForFindingContent)Session[Commons.ConstValues.SESSION_CONTENT];

                //get Position
                string strFuntionMenuID = "";

                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                if (objSessionForFindingContent != null)
                {
                    strFuntionMenuID = objSessionForFindingContent.ID_MENU;
                }
                

                //get Content
                if (!String.IsNullOrEmpty(strFuntionMenuID))
                {
                    int intFuntionMenuID = Convert.ToInt32(strFuntionMenuID);
                    jos_categories objCategory = entities.jos_categories.Where(x => x.id == intFuntionMenuID).FirstOrDefault();
                    if (objCategory != null)
                    {
                        txtTitle.Text = objCategory.title;
                        txtLinkURL.Text = objCategory.description;
                        txtName.Text = objCategory.name;
                        txtOrder.Text = objCategory.ordering != null ? objCategory.ordering.ToString() : "";
                    }

                    //set begin value for Language
                    drpLanguages.SelectedValue = objCategory.lang_id.ToString();
                    //set begin value for Content ID Textbox
                    txtFunctionMenuID.Text = objCategory.id.ToString();                    
                }                

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            jos_categories objCategory;
            //in case of create new item
            if (String.IsNullOrEmpty(txtFunctionMenuID.Text))
            {
                objCategory = new jos_categories();                
                entities.jos_categories.Add(objCategory);
            }
            else//in case of update info
            {
                int intFunctionMenuID = Convert.ToInt32 (txtFunctionMenuID.Text);
                objCategory = entities.jos_categories.Where(x => x.id == intFunctionMenuID).FirstOrDefault();
                
            }
            objCategory.title = txtTitle.Text;
            objCategory.name = txtName.Text;
            
            objCategory.count = 0;
            objCategory.ordering = String.IsNullOrEmpty(txtOrder.Text) ? 1 : Convert.ToInt32(txtOrder.Text);
            objCategory.checked_out = 0;
            objCategory.checked_out_time = DateTime.Now;

            objCategory.lang_id = Convert.ToInt16 (drpLanguages.SelectedValue);            
            objCategory.published = true;//1 ~ public            
            entities.SaveChanges();

            //Update Description (Link URL)
            objCategory.description = "/" + txtName.Text + "/Index?catID=" + objCategory.id;
            entities.SaveChanges();

            Response.Redirect("/Modules/FunctionMenusList");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/FunctionMenusList");
        }
    }
}