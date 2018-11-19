
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ContactEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strContactID = null;
                
                //get Default Language
                Commons.CommonFuncs.getLanguagesByDefaultLangID(drpLanguages, entities);

                //get Contact ID from session for finding Contact
                SessionForFindingContact objSessionForFindingContact = (SessionForFindingContact)Session[Commons.ConstValues.SESSION_CONTACT];

                if (objSessionForFindingContact != null)
                {
                    strContactID = objSessionForFindingContact.ID_CONTACT;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingContact.ID_LANGUAGE;
                }

                int intLangID =Convert.ToInt16(drpLanguages.SelectedValue);
                Commons.CommonFunctionsAndProcedures.drawTreeInDropDownList(0, null, drpIDParent, null, Commons.TitleConst.getTitleConst("TABLE_CONTACT"), intLangID, entities);

                //get Contact
                if (!String.IsNullOrEmpty(strContactID)) //Edit Contact
                {
                    int intContactID = Convert.ToInt32(strContactID);
                    jos_contact objContact = entities.jos_contact.Where(x => x.id == intContactID).FirstOrDefault();
                    if (objContact != null)
                    {
                        txtContactID.Text = objContact.id.ToString();
                        //set value for atribute
                        drpIDParent.SelectedValue = objContact.id_parent.ToString();
                        txtName.Text = objContact.name;
                        txtExt_Tel.Text = objContact.ext_tel;
                        txtEmail.Text = objContact.email;
                        txtOrder.Text = objContact.order.ToString();
                        txtDepManager.Text = objContact.department_manager;
                        txtPhone.Text = objContact.phone;
                        txtHotline.Text = objContact.hotline;
                        txtTitleOfManager.Text = objContact.title_of_manager;
                        //set begin value for Language
                        drpLanguages.SelectedValue = objContact.lang_id.ToString();
                    }
                }
                else //Create new Contact
                {
                    drpLanguages.SelectedValue = objSessionForFindingContact.ID_LANGUAGE;
                }

            }

        }

        protected bool validate()
        {

            if (String.IsNullOrEmpty(txtName.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtExt_Tel.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Tel", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Email", Page);
                return false;
            }
             if (String.IsNullOrEmpty(txtDepManager.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter DepartmentManager", Page);
                return false;
            } 
            if (String.IsNullOrEmpty(txtPhone.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Phone", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtHotline.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Hotline", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtTitleOfManager.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter TitleOfManager", Page);
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

            jos_contact objContact;
            //in case of create new item
            if (String.IsNullOrEmpty(txtContactID.Text))
            {
                var lstID = entities.jos_contact.Max(m => m.id);

                objContact = new jos_contact();
                objContact.id = lstID + 1;
                objContact.name = txtName.Text;
                objContact.published = true;
                objContact.created = DateTime.Now;
                objContact.lang_id = Convert.ToInt16(drpLanguages.SelectedValue);
                entities.jos_contact.Add(objContact);
            }
            else//in case of update info
            {
                int intContactID = Convert.ToInt32(txtContactID.Text);
                objContact = entities.jos_contact.Where(x => x.id == intContactID).FirstOrDefault();
            }
            objContact.name = txtName.Text;
            objContact.modified = DateTime.Now;
            objContact.ext_tel = txtExt_Tel.Text;
            objContact.email = txtEmail.Text;
            objContact.order = Convert.ToInt16(txtOrder.Text);
            objContact.department_manager = txtDepManager.Text;
            objContact.phone = txtPhone.Text;
            objContact.hotline = txtHotline.Text;
            objContact.title_of_manager = txtTitleOfManager.Text;
            objContact.published = true;
            objContact.lang_id = Convert.ToInt16(drpLanguages.SelectedValue.ToString());
            objContact.id_parent = Convert.ToInt16(drpIDParent.SelectedValue.ToString());

            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingContact();

            //redirect URL
            Response.Redirect(ConstURL.URL_CONTACT_VIEW);
        }

        //set session for finding content
        protected void setSessionForFindingContact()
        {
            SessionForFindingContact objSessionForFindingContact = new SessionForFindingContact();
            objSessionForFindingContact.ID_CONTACT = "";
            objSessionForFindingContact.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CONTACT] = objSessionForFindingContact;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            setSessionForFindingContact();
            Response.Redirect(ConstURL.URL_CONTACT_VIEW);
        }

        protected void drpLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get drpIDParent
            int intLangID = Convert.ToInt16(drpLanguages.SelectedValue);
            Commons.CommonFunctionsAndProcedures.drawTreeInDropDownList(0, null, drpIDParent, null, Commons.TitleConst.getTitleConst("TABLE_CONTACT"), intLangID, entities);
        }
    }
}