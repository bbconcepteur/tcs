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
    public partial class MenuType : System.Web.UI.Page
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
            var lstMenuType = entities.jos_menu_types.ToList();
            grvLib.DataSource = lstMenuType;
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objMenuType = entities.jos_menu_types.Find(e.Keys[0]);
                if (objMenuType != null)
                {
                    entities.jos_menu_types.Remove(objMenuType);
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
                var objMenuType = entities.jos_menu_types.Find(Convert.ToInt32(e.Keys[0]));


                if (objMenuType != null)
                {
                    objMenuType.menutype = Convert.ToString(e.NewValues["menutype"]);
                    objMenuType.title = Convert.ToString(e.NewValues["title"]);
                    objMenuType.description = Convert.ToString(e.NewValues["description"]);
                }

                //Validation and update into DB
                if (validation(objMenuType))
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
                jos_menu_types objMenuType = new jos_menu_types();

                if (entities.jos_menu_types.Count() > 0) objMenuType.id = entities.jos_menu_types.Max(x => x.id) + 1;
                else objMenuType.id = 1;

                objMenuType.menutype = Convert.ToString(e.NewValues["menutype"]);
                objMenuType.title = Convert.ToString(e.NewValues["title"]);
                objMenuType.description = Convert.ToString(e.NewValues["description"]);

                if (validation(objMenuType))
                {
                    entities.jos_menu_types.Add(objMenuType);
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

        protected bool validation(jos_menu_types objMenuType)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objMenuType.title))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_TEN_VAI_TRO"), Page);
                return false;
            }

            return true;
        }
    }
}