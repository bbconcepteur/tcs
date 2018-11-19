using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxUploadControl;
using CPanel.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HCSV.Models;

namespace CPanel.Modules.Admin
{

    public partial class UserDetails : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities ();
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                //Get User's info
                string strUserID = Request.QueryString["userID"];
                if (!String.IsNullOrEmpty(strUserID))
                {
                    txtUserID.Text = strUserID;

                    int intUserID = Convert.ToInt32(strUserID);
                    var objSearchUser = entities.jos_users.Where(x => x.id == intUserID).FirstOrDefault();
                    if (objSearchUser != null)
                    {
                        getOutputs(objSearchUser);                        
                    }
                }

                
            }
        }
        private void setInputs(jos_users objUserLogin)
        {            
            objUserLogin.username = txtUsername.Text;            
            objUserLogin.name = txtFullName.Text;
            objUserLogin.email = txtEmail.Text;
            if (!String.IsNullOrEmpty(txtPassword_1.Text))
            {
                objUserLogin.password = txtPassword_1.Text;
            }            
            objUserLogin.block = (sbyte)(cbActiveSatus.Checked ? 0 : 1);
        }

        protected void grvRights_DataBinding(object sender, EventArgs e)
        {

        }

        private void getOutputs(jos_users objUserLogin)
        {
            txtUsername.Text = objUserLogin.username;
            txtFullName.Text = objUserLogin.name;
            txtEmail.Text = objUserLogin.email;
            
            if (objUserLogin.block == 0)
            {
                cbActiveSatus.Checked = true;
            }
            else
            {
                cbActiveSatus.Checked = false;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {   
                jos_users objSearchUser = new jos_users();
                bool blPassword = true;

                if (String.IsNullOrEmpty(txtUsername.Text))
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay("You must type User name", Page);
                    return;
                }

                if (String.IsNullOrEmpty(txtFullName.Text))
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay("You must type Full name", Page);
                    return;
                }

                if (String.IsNullOrEmpty(txtUserID.Text)) //Create New
                {
                    entities.jos_users.Add(objSearchUser);
                    //Check Password
                    if (String.IsNullOrEmpty(txtPassword_1.Text) || String.IsNullOrEmpty(txtPassword_2.Text))
                    {
                        Commons.ValidationFuncs.errorMessage_TimeDelay("You must type the password", Page);                        
                        return;
                    }

                }
                else //Update
                {
                    int intUserID = Convert.ToInt32(txtUserID.Text);
                    objSearchUser = entities.jos_users.Where(x => x.id == intUserID).FirstOrDefault();                    
                }

                //check Password                
                if (!String.IsNullOrEmpty(txtPassword_1.Text))
                {
                    if (!txtPassword_1.Text.Equals(txtPassword_2.Text))
                        blPassword = false;
                } 
                else if (!String.IsNullOrEmpty(txtPassword_2.Text))
                {
                    if (!txtPassword_2.Text.Equals(txtPassword_1.Text))
                        blPassword = false;
                }
                if (!blPassword)
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay("Password is incorrect", Page);                                            
                    return;
                }

                setInputs(objSearchUser);                        
                entities.SaveChanges();                    
                backURL();
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
                
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            backURL();
        }
        private void backURL()
        {
            Response.Redirect("/Modules/GroupMng.aspx");
        }

        //private void saveInToGroupUsers()
        //{
        //    int intUserID = -999;//Any Value
        //    var objSearchUser = entities.USERLOGINs.Where(x => x.USERNAME.IndexOf(txtUsername.Text) >= 0).FirstOrDefault();
        //    if (objSearchUser != null)
        //    {
        //        intUserID = objSearchUser.ID;
        //    }
        //    var lstRights = grvRights.GetSelectedFieldValues(grvRights.KeyFieldName);


        //    if (intUserID > 0)
        //    {
        //        // Xóa user khỏi tbl GROUP_USERs
        //        var lstGroupUser = entities.GROUPS_USER.Where(x => x.IDUSER == intUserID).ToList();
        //        if (lstGroupUser != null)
        //        {
        //            foreach (var item in lstGroupUser)
        //            {
        //                entities.GROUPS_USER.Remove(item);
        //                entities.SaveChanges();
        //            }
        //        }

        //        // Thêm user vào GROUP_USERS
        //        foreach (int i in lstRights)
        //        {
        //            GROUPS_USER obj = new GROUPS_USER();
        //            if (entities.GROUPS_USER.Count() > 0) obj.ID = entities.GROUPS_USER.Max(x => x.ID) + 1;
        //            else obj.ID = 1;
        //            obj.IDGROUPS = i;
        //            obj.IDUSER = intUserID;
        //            obj.CREATED = obj.LASTMODIFY = DateTime.Now;
        //            obj.CREATEDBY = obj.MODIFYBY = CheckUserInfo.GetUserId();
        //            entities.GROUPS_USER.Add(obj);
        //            entities.SaveChanges();
        //        }
        //    }


        //}

        protected void btnReject_Click(object sender, EventArgs e)
        {
            //try
            //{                               
            //    if (!String.IsNullOrEmpty(txtUsername.Text))
            //    {
            //        var objSearchUser = entities.USERLOGINs.Where(x => x.USERNAME.IndexOf(txtUsername.Text) >= 0).FirstOrDefault();
            //        if (objSearchUser != null)
            //        {
            //            setInputs(objSearchUser);
                        
            //            //Set status = "REJECTED" (=-1)
            //            objSearchUser.IS_ACTIVE = -1;

            //            entities.SaveChanges();
            //            lbMessage.Text = "Reject successfully";
            //        }
            //        backURL();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lbMessage.Text = ex.Message;
            //}
        }
   }
}