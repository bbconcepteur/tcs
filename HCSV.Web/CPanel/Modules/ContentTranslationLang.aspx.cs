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
    public partial class ContentTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strContentID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                // get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);

                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);


                //get Content ID from session for finding content
                SessionForFindingContent objSessionForFindingContent = (SessionForFindingContent)Session[Commons.ConstValues.SESSION_CONTENT];

                if (objSessionForFindingContent != null)
                {
                    strContentID = objSessionForFindingContent.ID_CONTENT;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingContent.ID_LANGUAGE;
                }

                //get Content for origin language
                if (!String.IsNullOrEmpty(strContentID)) //Edit Content
                {
                    int intContentID = Convert.ToInt32(strContentID);
                    jos_content objContent = entities.jos_content.Where(x => x.id == intContentID).FirstOrDefault();
                    if (objContent != null)
                    {
                        edtIntroContent.Html = objContent.introtext;
                        edtFullContent.Html = objContent.fulltext;
                        txtTitle.Text = objContent.title;                                                
                        //set begin value for Language
                        drpLanguages.SelectedValue = objContent.lang_id.ToString();
                        //set begin value for Content ID Textbox
                        txtContentID.Text = objContent.id.ToString();
                        //set begin value for Representative Image

                        edtRepresentativeImage.Html = objContent.images;
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    //jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intContentID && o.language_id == intLangTranslationID).FirstOrDefault();
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intContentID 
                                                                                                && o.reference_id != intContentID
                                                                                                && (Commons.ConstTable.TBL_JOS_CONTENT.Equals(o.reference_table))).FirstOrDefault();



                    if (objTranslation != null)
                    {
                        txtTranslation.Text = objTranslation.id.ToString();
                        int refContentID = objTranslation.reference_id;
                        jos_content objContent2 = entities.jos_content.Where(x => x.id == refContentID).FirstOrDefault();

                        // lay content cua translation language (reference_ID)
                        if (objContent2 != null)
                        {
                            edtIntroContent2.Html = objContent2.introtext;
                            edtFullContent2.Html = objContent2.fulltext;
                            txtTitle2.Text = objContent2.title;
                            
                            //set begin value for Language
                            drpLanguages2.SelectedValue = objContent2.lang_id.ToString();
                            //set begin value for Content ID Textbox
                            txtContentID2.Text = objContent2.id.ToString();
                            //set begin value for Representative Image
                            edtRepresentativeImage2.Html = objContent2.images;
                        }
                        else
                        {
                            txtContentID2.Text = "";
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

            jos_content objContent2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtContentID2.Text))
            {
                objContent2 = new jos_content();
                objContent2.created = DateTime.Now;
                //objContent2.created_by = (int) CheckUserInfo.GetUserId();
                objContent2.modified = null;
                objContent2.checked_out_time = null;
                objContent2.publish_down = null;
                objContent2.publish_up = null;                        
                entities.jos_content.Add(objContent2);
            }
            else
            {
                int intContentID2 = Convert.ToInt32(txtContentID2.Text);
                objContent2 = entities.jos_content.Where(x => x.id == intContentID2).FirstOrDefault();
            }
            objContent2.modified = DateTime.Now;
            // objContent.modified_by = (int)CheckUserInfo.GetUserId();
            objContent2.title = txtTitle2.Text;
            objContent2.introtext = edtIntroContent2.Html;
            objContent2.fulltext = edtFullContent2.Html;
            objContent2.images = edtRepresentativeImage2.Html;            
            objContent2.lang_id = Convert.ToInt32(drpLanguages2.SelectedValue);            
                        
            entities.SaveChanges();
            string refId = objContent2.id.ToString();
            // add item for Translation

            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtContentID.Text);
                objTranslation.reference_id = Convert.ToInt32(refId);
                objTranslation.reference_table = Commons.ConstTable.TBL_JOS_CONTENT;
                entities.jos_language_translation.Add(objTranslation);                                
                entities.SaveChanges();
            }
           
            
            
            //set session for finding content
            setSessionForFindingContent();

            //redirect URL
            Response.Redirect("/Modules/ContentList");
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
            if (String.IsNullOrEmpty(txtTitle2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title for", Page);
                return false;
            }

            if (String.IsNullOrEmpty(edtRepresentativeImage2.Html) && String.IsNullOrEmpty(edtIntroContent2.Html) && String.IsNullOrEmpty(edtFullContent2.Html))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Content", Page);
                return false;
            }
            
            return true;
        }



        //set session for finding content
        protected void setSessionForFindingContent()
        {
            SessionForFindingContent objSessionForFindingContent = new SessionForFindingContent();
            objSessionForFindingContent.ID_CONTENT = "";
            //objSessionForFindingContent.ID_CATEGORY = drpCategories.SelectedValue;
            objSessionForFindingContent.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CONTENT] = objSessionForFindingContent;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/ContentList");
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}