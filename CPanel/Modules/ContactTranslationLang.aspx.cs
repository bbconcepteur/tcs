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
    public partial class ContactTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strContactID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Category ID from session for finding Category
                SessionForFindingContact objSessionForFindingContact = (SessionForFindingContact)Session[Commons.ConstValues.SESSION_CONTACT];
                if (objSessionForFindingContact != null)
                {
                    strContactID = objSessionForFindingContact.ID_CONTACT;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingContact.ID_LANGUAGE;
                }
                // get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);

                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);
                //get dropdown ID_Parent2
                int intLangID2 =Convert.ToInt16(drpLanguages2.SelectedValue);
                Commons.CommonFunctionsAndProcedures.drawTreeInDropDownList(0, null, drpIDParent2, null, Commons.TitleConst.getTitleConst("TABLE_CONTACT"), intLangID2, entities);


                //get Category for origin language
                if (!String.IsNullOrEmpty(strContactID)) //Edit Category
                {
                    int intContactID = Convert.ToInt32(strContactID);
                    jos_contact objContact = entities.jos_contact.Where(x => x.id == intContactID).FirstOrDefault();
                    if (objContact != null)
                    {
                        //set begin value for Language
                        drpLanguages.SelectedValue = objContact.lang_id.ToString();
                        //set begin value for Category ID Textbox
                        txtContactID.Text = objContact.id.ToString();
                        drpIDParent.SelectedValue = objContact.id_parent.ToString();
                        txtName.Text = objContact.name;
                        txtExt_Tel.Text = objContact.ext_tel;
                        txtEmail.Text = objContact.email;
                        txtOrder.Text = objContact.order.ToString();
                        txtDepManager.Text = objContact.department_manager;
                        txtPhone.Text = objContact.phone;
                        txtHotline.Text = objContact.hotline;
                        txtTitleOfManager.Text = objContact.title_of_manager;
                       
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => (o.origin_id == intContactID && o.language_id == intLangTranslationID) 
                                                                                && (o.reference_id != o.origin_id)
                                                                                && (Commons.ConstTable.TBL_JOS_CONTACT.Equals(o.reference_table))
                                                                            ).FirstOrDefault();

                    if (objTranslation != null)
                    {
                        txtTranslation.Text = objTranslation.id.ToString();
                        int refContactID = objTranslation.reference_id;
                        //int refContactID2=objTranslation.origin_id;
                        jos_contact objContact2 = entities.jos_contact.Where(x => x.id == refContactID).FirstOrDefault();
                        // lay content cua translation language (reference_ID)
                        if (objContact2 != null)
                        {

                            txtName2.Text = objContact2.name;
                            drpIDParent.SelectedValue = objContact2.id_parent.ToString();
                            txtExt_Tel2.Text = objContact2.ext_tel;
                            txtEmail2.Text = objContact2.email;
                            txtOrder2.Text = objContact2.order.ToString();
                            txtDepManager2.Text = objContact2.department_manager;
                            txtPhone2.Text = objContact2.phone;
                            txtHotline2.Text = objContact2.hotline;
                            txtTitleOfManager2.Text = objContact2.title_of_manager;
                            //set begin value for Languages
                            drpLanguages2.SelectedValue = objContact2.lang_id.ToString();
                            //set begin value for objContact2 ID Textbox
                            txtContactID2.Text = objContact2.id.ToString();
                            
                        }
                        else
                        {
                            txtContactID2.Text = "";
                        }
                    }
                    else
                    {
                        txtTranslation.Text = "";
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!validate())//Validation before updating            
            {
                return;
            }

            jos_contact objContact2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtContactID2.Text))
            {
                objContact2 = new jos_contact();
                objContact2.name = txtName.Text;
                objContact2.created=DateTime.Now;
                objContact2.published = true;
                entities.jos_contact.Add(objContact2);
            }
            else
            {
                int intContactID2 = Convert.ToInt32(txtContactID2.Text);
                objContact2 = entities.jos_contact.Where(x => x.id == intContactID2).FirstOrDefault();
            }
           
            //Edit
            objContact2.name = txtName2.Text;
            objContact2.modified = DateTime.Now;
            objContact2.ext_tel = txtExt_Tel2.Text;
            objContact2.email = txtEmail2.Text;
            objContact2.order = Convert.ToInt16(txtOrder2.Text);
            objContact2.department_manager = txtDepManager2.Text;
            objContact2.phone = txtPhone2.Text;
            objContact2.hotline = txtHotline2.Text;
            objContact2.title_of_manager = txtTitleOfManager2.Text;
            objContact2.published = true;
            objContact2.lang_id = Convert.ToInt16(drpLanguages2.SelectedValue);
            objContact2.id_parent = Convert.ToInt16(drpIDParent2.SelectedValue);

            entities.SaveChanges();
            string refId = objContact2.id.ToString();
            // add item for Translation

            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtContactID.Text);
                objTranslation.reference_id = Convert.ToInt32(refId);
                entities.jos_language_translation.Add(objTranslation);
                entities.SaveChanges();
            }
            else
            {
                int intTranID = Convert.ToInt32(txtTranslation.Text);
                objTranslation = entities.jos_language_translation.Where(x => x.id == intTranID).FirstOrDefault();
            }

            objTranslation.modified = DateTime.Now;
            entities.SaveChanges();
            setSessionForFindingContact();

            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_CONTACT_VIEW);
        }


        public static void getLanguagesTranslation(DropDownList drpLanguages2, DropDownList drpLanguages, TCSEntities entities)
        {
            int intTranslationLang = Convert.ToInt16(drpLanguages.SelectedValue);
            var lstLanguages = entities.jos_languages.Where(a => a.published == true && a.lang_id != intTranslationLang).OrderByDescending(x => x.default_status).ToList();
            drpLanguages2.DataValueField = "lang_id";
            drpLanguages2.DataTextField = "title";
            drpLanguages2.DataSource = lstLanguages;
            drpLanguages2.DataBind();
        }

       

        protected bool validate()
        {

            if (String.IsNullOrEmpty(txtName2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtExt_Tel2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Tel", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtEmail2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Email", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtDepManager2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter DepartmentManager", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtPhone2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Phone", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtHotline2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Hotline", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtTitleOfManager2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter TitleOfManager", Page);
                return false;
            }

            return true;
        }



        //set session for finding content
       protected void setSessionForFindingContact()
        {
            SessionForFindingContact objSessionForFindingContact = new SessionForFindingContact();
            objSessionForFindingContact.ID_CONTACT = "";
            objSessionForFindingContact.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CATEGORY] = objSessionForFindingContact;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Commons.ConstURL.URL_CONTACT_VIEW);
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intLangID2 = Convert.ToInt16(drpLanguages2.SelectedValue);
            Commons.CommonFunctionsAndProcedures.drawTreeInDropDownList(0, null, drpIDParent2, null, Commons.TitleConst.getTitleConst("TABLE_CONTACT"), intLangID2, entities);

        }
    }
}