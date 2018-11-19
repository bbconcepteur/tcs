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
    public partial class PartnersLinkTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strPartnerID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                // get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);

                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);


                //get Partner ID from session for finding Partner
                SessionForFindingPartner objSessionForFindingPartner = (SessionForFindingPartner)Session[Commons.ConstValues.SESSION_PARTNER];

                if (objSessionForFindingPartner != null)
                {
                    strPartnerID = objSessionForFindingPartner.ID_PARTNER;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingPartner.ID_LANGUAGE;
                }

                //get Categories for origin language
                //Commons.CommonFuncs.getCategories(drpCategories, entities, Convert.ToInt16(drpLanguages.SelectedValue));


                //get Partner for origin language
                if (!String.IsNullOrEmpty(strPartnerID)) //Edit Partner
                {
                    int intPartnerID = Convert.ToInt32(strPartnerID);
                    jos_links objPartner = entities.jos_links.Where(x => x.id == intPartnerID).FirstOrDefault();
                    if (objPartner != null)
                    {
                        edtDescription.Html = CommonFuncs.convertContent(objPartner.description);
                        txtName.Text = objPartner.name;
                        txtLink.Text = objPartner.link;
                        txtAddress.Text = objPartner.address;
                        txtPhone.Text = objPartner.phone;
                        txtFax.Text = objPartner.fax;
                        txtRepresentative.Text = objPartner.representative;

                        txtOrder.Text = objPartner.order != null ? objPartner.order.ToString() : "";



                        //set begin value for Language
                        drpLanguages.SelectedValue = objPartner.lang_id.ToString();
                        //set begin value for Partner ID Textbox
                        txtPartnerID.Text = objPartner.id.ToString();
                        //set begin value for Representative Image
                        edtRepresentativeImage.Html = CommonFuncs.convertContent(objPartner.image);
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intPartnerID
                                                                    && (o.language_id == intLangTranslationID) && (Commons.ConstTable.TBL_JOS_LINKS.Equals(o.reference_table))).FirstOrDefault();



                    if (objTranslation != null)
                    {
                        int refPartnerID = objTranslation.reference_id;
                        
                        jos_links objPartner2 = entities.jos_links.Where(x => x.id == refPartnerID).FirstOrDefault();

                        // lay Partner cua translation language (reference_ID)
                        if (objPartner2 != null)
                        {
                            edtDescription2.Html = CommonFuncs.convertContent(objPartner2.description);
                            txtName2.Text = objPartner2.name;
                            txtLink2.Text = objPartner2.link;
                            txtAddress2.Text = objPartner2.address;
                            txtPhone2.Text = objPartner2.phone;
                            txtFax2.Text = objPartner2.fax;
                            txtRepresentative2.Text = objPartner2.representative;
                            txtOrder2.Text = objPartner2.order != null ? objPartner2.order.ToString() : "";
                            //set begin value for Language
                            drpLanguages2.SelectedValue = objPartner2.lang_id.ToString();
                            //set begin value for Partner ID Textbox
                            txtPartnerID2.Text = objPartner2.id.ToString();
                            //set begin value for Representative Image
                            edtRepresentativeImage2.Html = CommonFuncs.convertContent(objPartner2.image);

                            txtTranslation.Text = intPartnerID.ToString();
                        }
                        else
                        {
                            txtTranslation.Text = "";
                        }
                    }
                    else
                    {
                        edtDescription2.Html = CommonFuncs.convertContent(objPartner.description);
                        txtName2.Text = objPartner.name;
                        txtLink2.Text = objPartner.link;
                        txtAddress2.Text = objPartner.address;
                        txtPhone2.Text = objPartner.phone;
                        txtFax2.Text = objPartner.fax;
                        txtRepresentative2.Text = objPartner.representative;
                        txtOrder2.Text = objPartner.order != null ? objPartner.order.ToString() : "";
                        //set begin value for Language
                        drpLanguages2.SelectedValue = objPartner.lang_id.ToString();
                        //set begin value for Partner ID Textbox
                        txtPartnerID2.Text = objPartner.id.ToString();
                        //set begin value for Representative Image
                        edtRepresentativeImage2.Html = CommonFuncs.convertContent(objPartner.image);
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

            jos_links objPartner2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtPartnerID2.Text))
            {
                objPartner2 = new jos_links();
                objPartner2.name = null;
                objPartner2.link = null;
                objPartner2.address = null;
                objPartner2.phone = null;
                objPartner2.fax = null;
                objPartner2.representative = null;
                objPartner2.created = DateTime.Now;
                objPartner2.modified = null;
                entities.jos_links.Add(objPartner2);
            }
            else  //in case of update info
            {
                int intPartnerID2 = Convert.ToInt32(txtPartnerID2.Text);
                objPartner2 = entities.jos_links.Where(x => x.id == intPartnerID2).FirstOrDefault();

            }

            objPartner2.modified = DateTime.Now;
            objPartner2.image = edtRepresentativeImage.Html;
            objPartner2.name = txtName2.Text;
            objPartner2.link = txtLink2.Text;
            objPartner2.address = txtAddress2.Text;
            objPartner2.phone = txtPhone2.Text;
            objPartner2.fax = txtFax2.Text;
            objPartner2.lang_id = Convert.ToInt32(drpLanguages2.SelectedValue);
            objPartner2.representative = txtRepresentative2.Text;

            if (!String.IsNullOrEmpty(txtOrder.Text))
                objPartner2.order = Convert.ToInt32(txtOrder2.Text);
            else
                objPartner2.order = 1;
            objPartner2.published = true;//true ~ public


            entities.SaveChanges();
            string refId = objPartner2.id.ToString();
            // add item for Translation

            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtPartnerID.Text); 
                objTranslation.reference_id = Convert.ToInt32(refId);
                entities.jos_language_translation.Add(objTranslation);
                entities.SaveChanges();
            }


            //set session for finding Partner
            setSessionForFindingPartner();

            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_PARTNER_LIST);
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

        public static void getPosition(DropDownList drpPosition)
        {
            drpPosition.Items.Clear();

            string[] lstPosition = Commons.CommonFuncs.getListOfPosition();
            for (int i = 0; i < lstPosition.Length; i++)
            {
                ListItem lstItem = new ListItem();
                lstItem.Text = lstPosition[i];
                lstItem.Value = lstPosition[i];
                drpPosition.Items.Add(lstItem);
            }
        }

        protected bool validate()
        {
            if (String.IsNullOrEmpty(txtName2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtLink2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Link", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtAddress2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Address", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtPhone2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Phone Number", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtFax2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Fax Number", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtRepresentative2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Representative", Page);
                return false;
            }
            int order;
            if (!int.TryParse(txtOrder.Text,out order))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Number", Page);
                return false;
            }

            if (String.IsNullOrEmpty(edtRepresentativeImage2.Html))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Representative Image", Page);
                return false;
            }
            return true;
        }



        //set session for finding Partner
        protected void setSessionForFindingPartner()
        {
            SessionForFindingPartner objSessionForFindingPartner = new SessionForFindingPartner();
            objSessionForFindingPartner.ID_PARTNER = "";
            objSessionForFindingPartner.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_PARTNER] = objSessionForFindingPartner;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Commons.ConstURL.URL_PARTNER_LIST);
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get Categories
            //Commons.CommonFuncs.getCategories(drpCategories, entities, Convert.ToInt16(drpLanguages.SelectedValue));
        }
    }
}