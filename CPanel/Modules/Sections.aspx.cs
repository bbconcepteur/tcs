using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using System.Web.Services;
using System.Globalization;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class Sections : System.Web.UI.Page
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set Captions for GridView
            Commons.TitleConst.setTitleConst_ASPxGridView(grvLib);

            if (!IsPostBack)
            {

                grvLib.DataBind();



            }

        }
        protected void grvLib_DataBinding(object sender, EventArgs e)
        {
            var lstSections = entities.jos_sections.ToList();
            grvLib.DataSource = lstSections;
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objMenu = entities.jos_sections.Find(e.Keys[0]);
                if (objMenu != null)
                {
                    entities.jos_sections.Remove(objMenu);
                    entities.SaveChanges();

                    //Display Message Box
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_XOA_THANH_CONG"), Page);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // ghi log
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void grvLib_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                var objSection = entities.jos_sections.Find(Convert.ToInt32(e.Keys[0]));


                if (objSection != null)
                {

                    objSection.title = Convert.ToString(e.NewValues["title"]);
                    objSection.published = (Convert.ToInt32(e.NewValues["published"]) == 0 ? false : true);
                    objSection.ordering = Convert.ToInt32(e.NewValues["ordering"]);
                    
                }

                //Validation and update into DB
                if (validation(objSection))
                {
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.

                    //Display Message Box
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_UPDATE_THANH_CONG"), Page);
                }

            }
            catch (Exception ex)
            {
                throw ex;
                // ghi log
            }
            finally
            {
                e.Cancel = true;

            }
        }

        protected void grvLib_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                jos_sections objSection = new jos_sections();

                if (entities.jos_sections.Count() > 0) objSection.id = entities.jos_sections.Max(x => x.id) + 1;
                else objSection.id = 1;

                objSection.title = Convert.ToString(e.NewValues["title"]);
                objSection.published = true;
                objSection.ordering = 1;

                if (validation(objSection))
                {
                    entities.jos_sections.Add(objSection);
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.

                    //Display Message Box
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_THEM_MOI_THANH_CONG"), Page);
                }


            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
            finally
            {
                e.Cancel = true;

            }
        }

        protected void redirectURL()
        {
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_MENUS"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                jos_sections objSection = new jos_sections();

                if (entities.jos_sections.Count() > 0) objSection.id = entities.jos_sections.Max(x => x.id) + 1;
                else objSection.id = 1;

                //if (!String.IsNullOrEmpty(txtTieude.Text))
                //{
                //    objMenu.TIEU_DE = txtTieude.Text;
                //    string str = drpMenus.SelectedValue;
                //    if (!drpMenus.SelectedValue.Equals(Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")))
                //        objMenu.ID_PARENT = Convert.ToInt32(drpMenus.SelectedValue);
                //}
                //else
                //{
                //    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);
                //    return;
                //}
                entities.jos_sections.Add(objSection);
                entities.SaveChanges();

                //Save System Log
                Commons.CommonFunctionsAndProcedures.saveSystemLog(String.Format(Commons.TitleConst.getTitleConst("LOG_MENU_CREATE"), objSection.title));

                redirectURL();
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }

        protected bool validation(jos_sections objSection)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objSection.title))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_TEN_VAI_TRO"), Page);
                return false;
            }

            return true;
        }
    }
}