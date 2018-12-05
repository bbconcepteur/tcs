using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class PartnerLinkEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPartnerID = null;

                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Partner ID from session for finding Partner
                SessionForFindingPartner objSessionForFindingPartner = (SessionForFindingPartner)Session[Commons.ConstValues.SESSION_PARTNER];

                if (objSessionForFindingPartner != null)
                {
                    strPartnerID = objSessionForFindingPartner.ID_PARTNER;

                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingPartner.ID_LANGUAGE;
                }

                //get Partner
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

                        txtOrder.Text = objPartner.order!= null ? objPartner.order.ToString() : "";


                        
                        //set begin value for Language
                        drpLanguages.SelectedValue = objPartner.lang_id.ToString();
                        //set begin value for Partner ID Textbox
                        txtPartnerID.Text = objPartner.id.ToString();
                        //set begin value for Representative Image
                        edtRepresentativeImage.Html =  CommonFuncs.convertContent(objPartner.image);
                    }
                }
                else //Create new Partner
                {
                    drpLanguages.SelectedValue = objSessionForFindingPartner.ID_LANGUAGE;
                }

            }

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
            if (String.IsNullOrEmpty(txtName.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtLink.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Link", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtAddress.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Address", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtPhone.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Phone Number", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtFax.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Fax Number", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtRepresentative.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Representative", Page);
                return false;
            }

            int order;
            if (!int.TryParse(txtOrder.Text, out order))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Number on Order", Page);
                return false;
            }

            if (String.IsNullOrEmpty(edtRepresentativeImage.Html))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Representative Image", Page);
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

            jos_links objPartner;
            //in case of create new item
            if (String.IsNullOrEmpty(txtPartnerID.Text))
            {
                objPartner = new jos_links();
                objPartner.name = null;
                objPartner.link = null;
                objPartner.address =null;
                objPartner.phone = null;
                objPartner.fax = null;
                objPartner.representative = null;
                objPartner.created = DateTime.Now;
                objPartner.modified = null;
                entities.jos_links.Add(objPartner);
            }
            else//in case of update info
            {
                int intPartnerID = Convert.ToInt32(txtPartnerID.Text);
                objPartner = entities.jos_links.Where(x => x.id == intPartnerID).FirstOrDefault();

            }

            objPartner.modified = DateTime.Now;
            objPartner.description = edtDescription.Html;
            objPartner.image = edtRepresentativeImage.Html;
            objPartner.name = txtName.Text;
            objPartner.link = txtLink.Text;
            objPartner.address = txtAddress.Text;
            objPartner.phone = txtPhone.Text;
            objPartner.fax = txtFax.Text;
            objPartner.lang_id = Convert.ToInt32(drpLanguages.SelectedValue);
            objPartner.representative = txtRepresentative.Text;

            if (!String.IsNullOrEmpty(txtOrder.Text))
                objPartner.order = Convert.ToInt32(txtOrder.Text);
            else
                objPartner.order = 1;
            objPartner.published = true;//1 ~ public


            entities.SaveChanges();

            //set session for finding Partner
            setSessionForFindingPartner();

            //redirect URL
            Response.Redirect("/Modules/PartnersLinkList");
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
            Response.Redirect("/Modules/PartnersLinkList");
        }

        protected void drpLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}