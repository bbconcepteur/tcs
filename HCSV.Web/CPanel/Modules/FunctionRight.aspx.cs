using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using System.Web.Services;
using HCSV.Models;
//
using Telerik.Web.UI;


namespace CPanel.Modules
{
    public partial class FunctionRight : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Load 'Vai tro'
                var lstVaitro = entities.jos_rights.ToList();
                drpVaiTro.DataSource = lstVaitro;
                drpVaiTro.DataValueField = "id";
                drpVaiTro.DataTextField = "name";
                drpVaiTro.DataBind();

                //load Menu tree
                refreshMenuTree();
            }

        }

        public void refreshMenuTree()
        {
            //load Menu tree
            radTreeView_Menus.Nodes.Clear();
            radTreeView_Menus.Visible = true;
            int intVaiTroID = Convert.ToInt32(drpVaiTro.SelectedValue);
            Commons.CommonFunctionsAndProcedures.DrawTelericTreeView_Funs(radTreeView_Menus, intVaiTroID, 0, null, entities);
        }
        protected void btnTempToFocus_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "selectNode('', '" + Request.QueryString["ElementID"] + "', '" + Request.QueryString["NextElementID"] + "');", true);
        }

        protected void drpVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshMenuTree();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intVaiTroID = Convert.ToInt32(drpVaiTro.SelectedValue);

            //Delete
            var lstVaiTroMenu = entities.jos__function_rights.Where(x => x.id_right == intVaiTroID).ToList();
            if (lstVaiTroMenu != null && lstVaiTroMenu.Count > 0)
            {
                foreach (jos__function_rights itemVaiTroMenu in lstVaiTroMenu)
                {
                    entities.jos__function_rights.Remove(itemVaiTroMenu);
                    entities.SaveChanges();
                }
            }

            foreach (RadTreeNode item in radTreeView_Menus.CheckedNodes)
            {
                //Insert into DB
                jos__function_rights objVaiTroMenu = new jos__function_rights();
                if (entities.jos__function_rights.Count() > 0) objVaiTroMenu.id = entities.jos__function_rights.Max(x => x.id) + 1;
                else objVaiTroMenu.id = 1;
                objVaiTroMenu.id_right = intVaiTroID;
                objVaiTroMenu.id_function = Convert.ToInt32(item.Value);
                entities.jos__function_rights.Add(objVaiTroMenu);
                entities.SaveChanges();
            }

            //Display Message Box
            Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_UPDATE_THANH_CONG"), Page);
        }

    }
}