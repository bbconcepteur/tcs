using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using System.Web.Services;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class QuanLyVaiTro : System.Web.UI.Page
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
           
            var lstVaitro = entities.jos_rights.ToList();
            grvLib.DataSource = lstVaitro;            
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objMenu = entities.jos_rights.Find(e.Keys[0]);
                if (objMenu != null)
                {
                    entities.jos_rights.Remove(objMenu);
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

        protected void grvLib_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)// cho nút submit của edit
        {
            try
            {
                var objVaitro = entities.jos_rights.Find(Convert.ToInt32(e.Keys[0]));
                
                if (objVaitro != null)
                {
                    objVaitro.name = Convert.ToString(e.NewValues["name"]);
                    objVaitro.description = Convert.ToString(e.NewValues["description"]);
                }
                
                //Validation and update into DB
                if (validation(objVaitro))
                {
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.
                    grvLib.Focus();
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

        /**
         * DESCRIPTION: This funtion check fomat before updating into DB
         * INPUTS: Menu is the object need updated into DB
         * OUTPUTS: TRUE if data is valid; FALSE if data is invalid
         * WRITTEN BY: TUYENDV
         **/
        protected bool validation(jos_rights objMenu)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objMenu.name))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_TEN_VAI_TRO"), Page);
                return false;
            }

            return true;
        }

        protected void grvLib_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try {
                jos_rights objVaitro = new jos_rights();

                objVaitro.name = Convert.ToString(e.NewValues["name"]);

                objVaitro.description = Convert.ToString(e.NewValues["description"]);

                if (validation(objVaitro))
                {
                    entities.jos_rights.Add(objVaitro);
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.
                    grvLib.Focus();
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
                jos_menu objMenu = new jos_menu();

                if (entities.jos_menu.Count() > 0) 
                    objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
                else objMenu.id = 1;

                if (!String.IsNullOrEmpty(txtTieude.Text))
                {
                    objMenu.name = txtTieude.Text;
                    string str = drpMenus.SelectedValue;
                    if (!drpMenus.SelectedValue.Equals(Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")))
                        objMenu.parent = Convert.ToInt32(drpMenus.SelectedValue);
                }
                else
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);
                    return;
                }
                entities.jos_menu.Add(objMenu);
                entities.SaveChanges();

                //Save System Log
                Commons.CommonFunctionsAndProcedures.saveSystemLog (String.Format(Commons.TitleConst.getTitleConst("LOG_MENU_CREATE"), objMenu.name));

                redirectURL();
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }

    }
}