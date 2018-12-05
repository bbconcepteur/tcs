using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxClasses.Internal;
using CPanel.Commons;
using HCSV.Models;

namespace CPanel
{
    public partial class RootMaster : System.Web.UI.MasterPage 
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e) 
        {
            if(!IsPostBack)
            {
                CheckUserInfo.CheckLogin();
                int intUserID = (int) CheckUserInfo.GetUserId();
                var obj = entities.jos_users.Where(x => x.id == intUserID).FirstOrDefault();
                txtFullName.Text = "Chào mừng " + obj.name;
                //if (obj.IS_ADMIN == 1) lnkCP.Visible = true;

                //get Menus
                SystemMenus objSysMenus = new SystemMenus();
                objSysMenus.getAdministratorMenus(lbMenus);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/SignIn.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConstURL.URL_CHANGE_PASSWORD);
        }
    }
}