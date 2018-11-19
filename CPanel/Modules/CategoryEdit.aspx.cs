using System;
using System.Linq;
using CPanel.Commons;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class CategoryEdit : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strCategoryID = null;
                //get Languages
                Commons.CommonFuncs.getLanguages(drpLanguages, entities);

                //get Sections
                Commons.CommonFunctionsAndProcedures.getSections(drpSections, entities);

                //get Category ID from session for finding Category
                SessionForFindingCategory objSessionForFindingCategory = (SessionForFindingCategory)Session[Commons.ConstValues.SESSION_CATEGORY];
                
                if (objSessionForFindingCategory != null)
                {
                    strCategoryID = objSessionForFindingCategory.ID_CATEGORY;
                    //set default value Language
                    drpLanguages.SelectedValue = objSessionForFindingCategory.ID_LANGUAGE;
                }


                //get Category
                if (!String.IsNullOrEmpty(strCategoryID)) //Edit Category
                {                    
                    int intCategoryID = Convert.ToInt32(strCategoryID);
                    jos_categories objCategory = entities.jos_categories.Where(x => x.id == intCategoryID).FirstOrDefault();
                    if (objCategory != null)
                    {
                        txtCategoryID.Text = objCategory.id.ToString();
                      //set begin value Title
                        txtTitle.Text = objCategory.title;
                        //set name
                        txtName.Text = objCategory.name;
                        
                        //set value for section
                        drpSections.SelectedValue = objCategory.section.ToString();

                        //set begin value for Language
                        drpLanguages.SelectedValue = objCategory.lang_id.ToString();
                    }
                }
                else //Create new Category
                {
                    drpLanguages.SelectedValue = objSessionForFindingCategory.ID_LANGUAGE;
                }

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

            if (Commons.CommonFuncs.BLANK_ITEM_VALUE.Equals(drpSections.SelectedValue))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("You must choose section", Page);
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

            jos_categories objCategory;
            //in case of create new item
            if (String.IsNullOrEmpty(txtCategoryID.Text))
            {
                objCategory = new jos_categories();                
                entities.jos_categories.Add(objCategory);
            }
            else//in case of update info
            {
                int intCategoryID = Convert.ToInt32 (txtCategoryID.Text);
                objCategory = entities.jos_categories.Where(x=>x.id == intCategoryID).FirstOrDefault();
            }

            objCategory.title = txtTitle.Text;
            objCategory.section = Convert.ToInt32(drpSections.SelectedValue);
            objCategory.name = txtName.Text;
            objCategory.published = true;
            objCategory.lang_id = Convert.ToInt16(drpLanguages.SelectedValue);
            
            entities.SaveChanges();

            //set session for finding content
            setSessionForFindingCategory();

            //redirect URL
            Response.Redirect("/Modules/CategoryList");            
        }

        //set session for finding content
        protected void setSessionForFindingCategory()
        {
            SessionForFindingCategory objSessionForFindingCategory = new SessionForFindingCategory();
            objSessionForFindingCategory.ID_CATEGORY = "";
            objSessionForFindingCategory.ID_LANGUAGE = drpLanguages.SelectedValue;
            Session[Commons.ConstValues.SESSION_CATEGORY] = objSessionForFindingCategory;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Modules/CategoryList", false);
        }

        protected void drpSections_DataBinding(object sender, System.EventArgs e)
        {

        }
    }
}