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
    public partial class SectionEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string strSectionID = null;

                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Section ID from session for finding section
                SessionForFindingSection objSessionForFindingSection = (SessionForFindingSection)Session[Commons.ConstValues.SESSION_SECTION];

                if (objSessionForFindingSection != null)
                {
                    strSectionID = objSessionForFindingSection.ID_SECTION;
                }

                //get Section
                if (!String.IsNullOrEmpty(strSectionID)) //Edit Section
                {
                    int intSectionID = Convert.ToInt32(strSectionID);
                    jos_sections objSection = entities.jos_sections.Where(x => x.id == intSectionID).FirstOrDefault();
                    if (objSection != null)
                    {
                        txtName.Text = objSection.name;
                        txtTitle.Text = objSection.title;




                        //set begin value for Section ID Textbox
                        txtSectionID.Text = objSection.id.ToString();
                    }
                }
                else //Create new section
                {

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
            if (String.IsNullOrEmpty(txtTitle.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Title", Page);
                return false;
            }
            if (String.IsNullOrEmpty(txtName.Text))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must enter Name", Page);
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

            jos_sections objSection;
            //in case of create new item
            if (String.IsNullOrEmpty(txtSectionID.Text))
            {
                objSection = new jos_sections();
                objSection.alias = null;
                objSection.image = null;
                objSection.scope = null;
                objSection.image_position = null;
                objSection.description = null;
                objSection.@params = null;
                entities.jos_sections.Add(objSection);

            }
            else//in case of update info
            {
                int intSectionID = Convert.ToInt32(txtSectionID.Text);
                objSection = entities.jos_sections.Where(x => x.id == intSectionID).FirstOrDefault();

            }         

            objSection.title = txtTitle.Text;
            objSection.name = txtName.Text;
            objSection.lang_id = Convert.ToInt32(drpLanguages.SelectedValue);

            objSection.published = true;//true ~ public


            entities.SaveChanges();

            //set session for finding section
            setSessionForFindingSection();

            //redirect URL
            Response.Redirect("/Modules/SectionList");
        }
        //set session for finding section
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

        protected void drpLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
}