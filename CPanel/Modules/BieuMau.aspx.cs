using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLNS.Commons;
using System.Web.Services;
using QLNS.Models;

namespace QLNS.Modules.QuanLyCongViec
{
    public partial class BieuMau : System.Web.UI.Page
    {
        Entities entities = new Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Set Captions for GridView
                Commons.TitleConst.setTitleConst_ASPxGridView(grvLib);

                //load grid view
                grvLib.DataBind();

            }

        }
        

        protected void grvLib_DataBinding(object sender, EventArgs e)
        {
            var lstBieuMau = entities.CV_BIEU_MAU.ToList();
            
            //Hidden create button if having any BM items
            if ((lstBieuMau == null) || ((lstBieuMau != null) && (lstBieuMau.Count == 0)))
                btnCreate.Visible = true;
            else btnCreate.Visible = false;

            grvLib.DataSource = lstBieuMau;            
        }

        protected void grvLib_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                var objBieuMau = entities.CV_BIEU_MAU.Find(e.Keys[0]);
                if (objBieuMau != null)
                {
                    entities.CV_BIEU_MAU.Remove(objBieuMau);
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

        
        protected void redirectURL()
        {
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_BIEU_MAU_CT"));
        }
        
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Session[Commons.Session.BIEU_MAU_ID] = String.IsNullOrEmpty(txtMenuID.Text) ? null : txtMenuID.Text;
            redirectURL();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            redirectURL();
        }
        
    }
}