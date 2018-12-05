using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CPanel.Commons;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;
using HCSV.Models;

namespace CPanel
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CheckUserInfo.CheckLogin();
                //LoadMenuNew();
                //cbNam.DataBind();
            }
        }

        //private void LoadMenu()
        //{
        //    int? UserId = CheckUserInfo.GetUserId();
        //    var lstFunc = (from g in entities.GROUPS_PERMISSION
        //                   join f in entities.FUNCTIONs on g.IDFUNCTION equals f.ID
        //                   join p in entities.GROUPS_USER on g.IDGROUP equals p.IDGROUPS
        //                   where p.IDUSER == UserId
        //                   select f.CODE).Distinct().ToList();
        //    var lstMenu = (from m in entities.MENUS
        //                   join f in lstFunc on m.FUNC_CODE equals f
        //                   select m).OrderBy(x=>x.ORDERNO).ToList();
        //    var rootMenus = lstMenu.Where(x => x.IDPARENT == 0 || x.IDPARENT == null);
        //    int i = 0;
        //    foreach(var rMenu in rootMenus)
        //    {
        //        navMenu.Groups.Add(rMenu.TITLE);
        //        var lstChildrenMenu = lstMenu.Where(x => x.IDPARENT == rMenu.ID).OrderByDescending(x => x.ORDERNO).ToList();
        //        foreach(var itmChild in lstChildrenMenu)
        //        {
        //            navMenu.Groups[i].Items.Add(itmChild.TITLE, itmChild.TITLE, "", itmChild.URL);
        //        }
        //        i++;
        //    }
        //}

        

        //private DevExpress.Web.ASPxMenu.MenuItem CreateMenuItem(menu obj)
        //{
        //    DevExpress.Web.ASPxMenu.MenuItem ret = new DevExpress.Web.ASPxMenu.MenuItem();
        //    ret.Text = obj.TITLE;
        //    ret.NavigateUrl = obj.URL;
        //    return ret;
        //}

        protected void cbNam_DataBinding(object sender, EventArgs e)
        {
            List<int> objs = new List<int>();
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 10; i++)
            {
                objs.Add(i);
            }
            cbNam.DataSource = objs;
            if (Session["year"] == null)
            {
                Session["year"] = DateTime.Now.Year;
            }
            cbNam.Text = Session["year"].ToString();

        }

        protected void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["year"] = cbNam.SelectedItem.Value;
            try
            {
                ASPxGridView gridview = (ASPxGridView)MainContent.FindControl("grid");
                gridview.DataBind();
            }
            catch (Exception)
            {

                //throw;
            }
        }
    }
}