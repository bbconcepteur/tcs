using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using QLNS.Commons;
using System.Web.Services;
using CPanel;

using CPanel.Commons;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class FunctionList : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //load grid view
                grvLib.DataBind();
            }

        }
        protected void getListOfFuns(int intCapFun, string strIDFun, List<jos_functions> lstResultOfFuns)
        {
            int intIDFun = 0; bool blNumber = false;
            if (!String.IsNullOrEmpty(strIDFun))
            {
                intIDFun = Convert.ToInt32(strIDFun);
                blNumber = true;
            }
           
            var objFuns = entities.jos_functions.Where(x => ((blNumber && x.id_parent == intIDFun)|| (blNumber == false && x.id_parent == 0))).ToList();
            if (objFuns.Count()>0)
            {
                btnCreate.Enabled = false;
            }
            else if (objFuns.Count() == 0)
            {
                btnCreate.Enabled = true;
            }

            if (objFuns != null)
            {
                
                foreach (var item in objFuns)
                {
                    string strLine = "";
                    strLine = strLine.PadLeft(intCapFun * 6, (char)Commons.TitleConst.getTitleConst("TITLE_ICON").ElementAt(0));
                    jos_functions obj = new jos_functions();
                    obj.id = item.id;
                    obj.id_parent = item.id_parent;
                    obj.ordering = item.ordering;
                    obj.link = item.link;
                    obj.published = item.published;
                    obj.access = item.access;
                    //obj.name=

                    if (intCapFun == 0) //Begining Level
                    {
                        obj.name = strLine + item.name;
                    }
                    else obj.name = strLine + item.name;

                    lstResultOfFuns.Add(obj);

                    getListOfFuns(intCapFun + 1, item.id.ToString(), lstResultOfFuns);
                }
            }
           
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //set session for finding category
            setSessionForFindingFunction();
            //redirect URL
            Response.Redirect(Commons.ConstURL.URL_FUNCTION_EDIT);
        }

        protected void grvLib_DataBinding(object sender, EventArgs e)
        {
            SessionForFindingFunction objSessionForFindingFun = (SessionForFindingFunction)Session["ss_Function"];
            List<jos_functions> lstFuns = new List<jos_functions>();

            getListOfFuns(0, null, lstFuns);

            grvLib.DataSource = lstFuns;

            //reset is null for session
            Session["ss_Function"] = null;
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objFun = entities.jos_functions.Find(e.Keys[0]);
                if (objFun != null)
                {
                    entities.jos_functions.Remove(objFun);
                    entities.SaveChanges();
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
                var objFun = entities.jos_functions.Find(Convert.ToInt32(e.Keys[0]));
                if (objFun != null)
                {
                    objFun.name = Convert.ToString(e.NewValues["name"]);
                    objFun.link = Convert.ToString(e.NewValues["link"]);
                }
                //Validation and update into DB
                if (validation(objFun))
                {
                    entities.SaveChanges();
                    grvLib.CancelEdit(); //This line closes the line Editor.
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
        protected bool validation(jos_functions objFun)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objFun.name))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay("Bạn chưa nhập Tên function", Page);
                return false;
            }

            return true;
        }

        protected void grvLib_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                jos_functions objFun = new jos_functions();

                if (entities.jos_functions.Count() > 0) objFun.id = entities.jos_functions.Max(x => x.id) + 1;
                else objFun.id = 1;

                if (!String.IsNullOrEmpty(Convert.ToString(e.NewValues["name"])))
                {
                    objFun.name = Convert.ToString(e.NewValues["name"]);
                    objFun.link = Convert.ToString(e.NewValues["link"]);
                }
                else
                {
                    Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("ERROR_TITLE_THIEU"), Page);
                    return;
                }
                entities.jos_functions.Add(objFun);
                entities.SaveChanges();

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
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_FUNCTION"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                jos_menu objMenu = new jos_menu();

                if (entities.jos_menu.Count() > 0) objMenu.id = entities.jos_menu.Max(x => x.id) + 1;
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
                Commons.CommonFunctionsAndProcedures.saveSystemLog(String.Format(Commons.TitleConst.getTitleConst("LOG_MENU_CREATE"), objMenu.name));

                redirectURL();
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            setSessionForFindingFunction();
            Response.Redirect(CPanel.Commons.TitleConst.getTitleConst("URL_ADD_FUNCTION"));
        }
        //set session for finding category
        protected void setSessionForFindingFunction()
        {
            SessionForFindingFunction objSessionForFindingFun = new SessionForFindingFunction();
            objSessionForFindingFun.ID_FUNCTION = txtFunID.Text;
            Session["ss_Function"] = objSessionForFindingFun;
        }

    }
}