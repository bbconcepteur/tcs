using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using System.Web.Services;

using Telerik.Web.UI;
using System.Configuration;
using HCSV.Models;

namespace CPanel.Modules
{
    public partial class QuyenTruyCapMenus : System.Web.UI.Page
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

            //get Menu Type (ADMINISTRATOR_MENU)


            Commons.CommonFunctionsAndProcedures.DrawTelericTreeView_Menus(radTreeView_Menus, intVaiTroID, 0, null, ConfigurationManager.AppSettings["MENU_ADMIN"], entities);                
        }
        protected void btnTempToFocus_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "selectNode('', '" + Request.QueryString["ElementID"] + "', '" + Request.QueryString["NextElementID"] + "');", true);
        }

        protected void drpVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshMenuTree();
        }

        protected void btnSave_Click (object sender, EventArgs e)
        {
            int intVaiTroID = Convert.ToInt32(drpVaiTro.SelectedValue);

            //Delete
            var lstVaiTroMenu = entities.jos_menu_rights.Where(x => x.id_right == intVaiTroID).ToList();
            if (lstVaiTroMenu != null && lstVaiTroMenu.Count > 0)
            {
                foreach (jos_menu_rights itemVaiTroMenu in lstVaiTroMenu)
                {
                    entities.jos_menu_rights.Remove(itemVaiTroMenu);
                    entities.SaveChanges();
                }
            }

            foreach (RadTreeNode item in radTreeView_Menus.CheckedNodes)
            {
                //Insert into DB
                jos_menu_rights objVaiTroMenu = new jos_menu_rights();
                if (entities.jos_menu_rights.Count() > 0) objVaiTroMenu.id = entities.jos_menu_rights.Max(x => x.id) + 1;
                else objVaiTroMenu.id = 1;
                objVaiTroMenu.id_right = intVaiTroID;
                objVaiTroMenu.id_menu = Convert.ToInt32(item.Value);
                entities.jos_menu_rights.Add(objVaiTroMenu);
                entities.SaveChanges();
            }

            //Display Message Box
            Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_UPDATE_SUCCESFULLY"), Page);           
        }
        
    }
}