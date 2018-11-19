using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class PartnersLinkList : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);
            }
            searchPartners();
        }
        protected void getUsers()
        {


        }

        protected void grvPartners_DataBinding(object sender, EventArgs e)
        {
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchPartners();
        }

        public void searchPartners()
        {
            //get info from session for finding Partner
            SessionForFindingPartner objSessionForFindingPartner = (SessionForFindingPartner)Session[Commons.ConstValues.SESSION_PARTNER];
            if (objSessionForFindingPartner != null)
            {
                drpLanguages.SelectedValue = objSessionForFindingPartner.ID_LANGUAGE;
            }

            //Search Partner

            int intLanguageID = Convert.ToInt32(drpLanguages.SelectedValue);

            var lstPartners = entities.jos_links.Where(x => ((x.lang_id == intLanguageID) && (x.published == true))).ToList();
            grvPartners.DataSource = lstPartners;
            grvPartners.DataBind();

            //reset is null for session
            Session[Commons.ConstValues.SESSION_PARTNER] = null;
        }

        protected void drpCategory_DataBinding(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var lstPartners = grvPartners.GetSelectedFieldValues(grvPartners.KeyFieldName);
            foreach (long i in lstPartners)
            {
                jos_links objPartner = entities.jos_links.Where(x => x.id == i).FirstOrDefault();
                objPartner.published = false;
                entities.SaveChanges();
            }
            Response.Redirect(Commons.ConstURL.URL_PARTNER_LIST);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding Partner
            setSessionForFindingPartner();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_PARTNER_EDIT);
        }

        protected void drpLanguage_DataBinding(object sender, EventArgs e)
        {

        }

        protected void drpLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            searchPartners();
        }

        protected void btnViewPartner_Click(object sender, EventArgs e)
        {
            //set session for finding Partner
            setSessionForFindingPartner();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_PARTNER_EDIT);
        }

        //set session for finding Partner
        protected void setSessionForFindingPartner()
        {

            SessionForFindingPartner objSessionForFindingPartner = new SessionForFindingPartner();
            objSessionForFindingPartner.ID_PARTNER = txtPartnerID.Text;
            objSessionForFindingPartner.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_PARTNER] = objSessionForFindingPartner;
        }


        protected void btnViewTranslation_Click(object sender, EventArgs e)
        {

            //set session for finding Partner
            setSessionForFindingPartner();

            //redirect
            Response.Redirect(Commons.ConstURL.URL_PARTNER_TRANSLATION);

        }


    }
}