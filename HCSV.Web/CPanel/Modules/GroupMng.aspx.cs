using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class GroupMng : System.Web.UI.Page    
    {        
        public bool blViewPopup = false;
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {   
                grvUsers.DataBind();
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            // call your download function
            if (blViewPopup)
                Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "viewPopup();", true);
        }

        protected void getUsers() {            
            
        }
        
        protected void grvUsers_DataBinding(object sender, EventArgs e)
        {
            string strAdminUserName = Commons.ConstValues.SUPPER_ADMIN.ToUpper();
            var lstUser = entities.jos_users.Where (x=> !Commons.ConstValues.CONFIG_EMAIL.Equals(x.usertype) && !strAdminUserName.Equals(x.username.ToUpper())).ToList();
            grvUsers.DataSource = lstUser;
        }

        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            blViewPopup = true;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/UserDetails");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstUsers = grvUsers.GetSelectedFieldValues(grvUsers.KeyFieldName);
            int intCurrentUserID = Convert.ToInt32(Session["UserId"]);
            foreach (int i in lstUsers)
            {

                if (intCurrentUserID == i)
                {
                    lbMessage.Text = "You can not delete your account";
                    return;
                }

                jos_users objUser = entities.jos_users.Where(x => x.id == i).FirstOrDefault();
                entities.jos_users.Remove (objUser);
                entities.SaveChanges();
            }
            Response.Redirect("/Modules/GroupMng");
        }

        
    }
}