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
    public partial class SectionTranslationLang : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                string strSectionID = null;
                //get Languages for origin language
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                // get Languages for Translation languages
                getLanguagesTranslation(drpLanguages2, drpLanguages, entities);

                int intLangTranslationID = Convert.ToInt16(drpLanguages2.SelectedValue);


                //get Section ID from session for finding Section
                SessionForFindingSection objSessionForFindingSection = (SessionForFindingSection)Session[Commons.ConstValues.SESSION_SECTION];

                if (objSessionForFindingSection != null)
                {
                    strSectionID = objSessionForFindingSection.ID_SECTION;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingSection.ID_LANGUAGE;
                }

                //get Section for origin language
                if (!String.IsNullOrEmpty(strSectionID)) //Edit Section
                {
                    int intSectionID = Convert.ToInt32(strSectionID);
                    jos_sections objSection = entities.jos_sections.Where(x => x.id == intSectionID).FirstOrDefault();
                    if (objSection != null)
                    {
                        txtTitle.Text = objSection.title;
                        txtName.Text = objSection.name;
                        //set begin value for Language
                        drpLanguages.SelectedValue = objSection.lang_id.ToString();
                        //set begin value for Section ID Textbox
                        txtSectionID.Text = objSection.id.ToString();
                    }

                    //kiem tra lien ket jos_translation da ton tai hay chua
                    jos_language_translation objTranslation = entities.jos_language_translation.Where(o => o.origin_id == intSectionID && o.language_id == intLangTranslationID).FirstOrDefault();



                    if (objTranslation != null)
                    {
                        txtTranslation.Text = objTranslation.id.ToString();
                        int refSectionID = objTranslation.reference_id;
                        jos_sections objSection2 = entities.jos_sections.Where(x => x.id == refSectionID).FirstOrDefault();

                        // lay Section cua translation language (reference_ID)
                        if (objSection2 != null)
                        {
                            txtTitle2.Text = objSection2.title;
                            txtName2.Text = objSection2.name;
                            //set begin value for Language
                            drpLanguages2.SelectedValue = objSection2.lang_id.ToString();
                            //set begin value for Section ID Textbox
                            txtSectionID2.Text = objSection2.id.ToString();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        txtTitle2.Text = objSection.title;
                        txtName2.Text = objSection.name;
                        drpLanguages2.SelectedValue = objSection.lang_id.ToString();
                        //set begin value for Section ID Textbox
                        txtSectionID2.Text = objSection.id.ToString();
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

            jos_sections objSection2;
            //in case of create new item
            if (String.IsNullOrEmpty(txtSectionID2.Text))
            {
                objSection2 = new jos_sections();
                objSection2.alias = null;
                objSection2.image = null;
                objSection2.scope = null;
                objSection2.image_position = null;
                objSection2.description = null;
                objSection2.@params = null;
                entities.jos_sections.Add(objSection2);
            }
            else
            {
                int intSectionID2 = Convert.ToInt32(txtSectionID2.Text);
                objSection2 = entities.jos_sections.Where(x => x.id == intSectionID2).FirstOrDefault();
            }
            objSection2.title = txtTitle2.Text;
            objSection2.name = txtName2.Text;
            objSection2.lang_id = Convert.ToInt32(drpLanguages2.SelectedValue);
            objSection2.published = true;//1 ~ public

            entities.SaveChanges();
            string refId = objSection2.id.ToString();
            // add item for Translation

            jos_language_translation objTranslation;

            if (String.IsNullOrEmpty(txtTranslation.Text))
            {
                //in case of create new item for Translation Language
                //new item for jos_translation
                objTranslation = new jos_language_translation();
                objTranslation.language_id = Convert.ToInt32(drpLanguages2.SelectedValue);
                objTranslation.origin_id = Convert.ToInt32(txtSectionID.Text);         
                objTranslation.reference_id = Convert.ToInt32(refId);
                entities.jos_language_translation.Add(objTranslation);
                entities.SaveChanges();
            }



            //set session for finding section
            setSessionForFindingSection();

            //redirect URL
            Response.Redirect("/Modules/SectionList");
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
            if (String.IsNullOrEmpty(txtTitle2.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title for", Page);
                return false;
            }

            //if (String.IsNullOrEmpty(edtRepresentativeImage2.Html) && String.IsNullOrEmpty(edtIntroSection2.Html) && String.IsNullOrEmpty(edtFullSection2.Html))
            //{
            //    Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Section", Page);
            //    return false;
            //}

            //if (drpCategories2.SelectedValue.Equals(Commons.CommonFuncs.CATEGORY_BLANK_ITEM_VALUE))
            //{
            //    Commons.ValidationFuncs.errorMessage_TimeDelay("You must choose Category", Page);
            //    return false;
            //}
            return true;
        }



        //set session for finding Section
        protected void setSessionForFindingSection()
        {
            SessionForFindingSection objSessionForFindingSection = new SessionForFindingSection();
            objSessionForFindingSection.ID_SECTION = "";
            objSessionForFindingSection.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_SECTION] = objSessionForFindingSection;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/SectionList");
        }

        protected void drpLanguages2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}