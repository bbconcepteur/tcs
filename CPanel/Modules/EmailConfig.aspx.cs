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

    public partial class EmailConfig : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities ();
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                //Get User's info
                var objSearchUser = entities.jos_users.Where(x => Commons.ConstValues.CONFIG_EMAIL.Equals(x.usertype)).FirstOrDefault();

                if (objSearchUser != null)
                {                       
                    getOutputs(objSearchUser);                                         
                }

                
            }
        }
        private void setInputs(jos_users objUserLogin)
        {   
            objUserLogin.name = txtFullName.Text;
            objUserLogin.email = txtEmail.Text;
            if (!String.IsNullOrEmpty(txtPassword_1.Text))
            {
                objUserLogin.password = txtPassword_1.Text;
            }                        
        }

        protected void grvRights_DataBinding(object sender, EventArgs e)
        {

        }

        private void getOutputs(jos_users objUserLogin)
        {            
            txtFullName.Text = objUserLogin.name;
            txtEmail.Text = objUserLogin.email;
            txtPassword_1.Text = objUserLogin.password;
            txtPassword_2.Text = objUserLogin.password;                       
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                jos_users objSearchUser = entities.jos_users.Where(x => Commons.ConstValues.CONFIG_EMAIL.Equals(x.usertype)).FirstOrDefault();
                bool blPassword = true;
                if (objSearchUser != null)
                {
                    //Check Password
                    //if (String.IsNullOrEmpty(txtPassword_1.Text) || String.IsNullOrEmpty(txtPassword_2.Text))
                    //{
                    //    Commons.ValidationFuncs.errorMessage_TimeDelay("You must type the password", Page);
                    //    return;
                    //}

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
                    Commons.ValidationFuncs.errorMessage_TimeDelay("Update successfully", Page); 
                    //backURL();
                }                
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
        
   }
}