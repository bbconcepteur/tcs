using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLNS.Commons;
using System.Web.Services;
using QLNS.Models;
using System.Text;

namespace QLNS.Modules.QuanTriHeThong
{
    public partial class BieuMau_CT : System.Web.UI.Page
    {
        Entities entities = new Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int intBieuMauID = 0;

                //Get LOAI_QUYET_DINH
                getLoaiQD();

                if (Session[Commons.Session.BIEU_MAU_ID] != null)
                {                    
                    intBieuMauID = Convert.ToInt32(Session[Commons.Session.BIEU_MAU_ID].ToString());
                    Session[Commons.Session.BIEU_MAU_ID] = null;

                    txtBieuMauID.Text = intBieuMauID.ToString();


                    CV_BIEU_MAU objBieuMau = entities.CV_BIEU_MAU.Find(intBieuMauID);                    
                    txtMoTa.Text = objBieuMau.MO_TA;
                    txtMaBieuMau.Text = objBieuMau.MA_BIEU_MAU;
                    edtNoiDung.Html = objBieuMau.NOI_DUNG;
                    drpLoaiQD.SelectedValue = objBieuMau.ID_LOAI_QUYET_DINH.ToString();
                }
                
            }

        }

        private void getLoaiQD ()
        {
            drpLoaiQD.Items.Clear();
            ListItem objListLoaiQD = new ListItem();
            objListLoaiQD.Value = Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE");
            objListLoaiQD.Text = Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE");
            drpLoaiQD.Items.Add(objListLoaiQD);

            var lstLoaiQD = entities.CV_LOAI_QUYET_DINH.ToList();
            if (lstLoaiQD != null && lstLoaiQD.Count > 0)
                foreach (var item in lstLoaiQD)
                {
                    ListItem objLoaiQDItem = new ListItem();
                    objLoaiQDItem.Value = item.ID.ToString();
                    objLoaiQDItem.Text = item.MO_TA;
                    drpLoaiQD.Items.Add(objLoaiQDItem);
                }
        }

        
        /**
         * DESCRIPTION: This funtion check fomat before updating into DB
         * INPUTS: BieMau is the object need updated into DB
         * OUTPUTS: TRUE if data is valid; FALSE if data is invalid
         * WRITTEN BY: TUYENDV
         **/
        protected bool validation(CV_BIEU_MAU objBieuMau)
        {
            //Check whether TITLE is empty
            if (String.IsNullOrEmpty(objBieuMau.MA_BIEU_MAU))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_MA_BM_RONG"), Page);
                return false;
            }

            //Check whether NOI_DUNG is empty
            if (String.IsNullOrEmpty(objBieuMau.NOI_DUNG))
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_NOI_DUNG_BM_RONG"), Page);
                return false;
            }

            //Check whether LOAI_QUYET_DINH is empty
            if (objBieuMau.ID_LOAI_QUYET_DINH == null)
            {
                Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_LOAI_QD_RONG"), Page);
                return false;
            }

            return true;
        }

        
        protected void redirectURL()
        {
            Response.Redirect(Commons.TitleConst.getTitleConst("URL_BIEU_MAU"));
        }

        protected void btnSave_Click (object sender, EventArgs e)
        {
            try
            {
                CV_BIEU_MAU objBieuMau = new CV_BIEU_MAU();
                if (String.IsNullOrEmpty(txtBieuMauID.Text)) //Create new item
                {
                    if (entities.CV_BIEU_MAU.Count() > 0) objBieuMau.ID = entities.CV_BIEU_MAU.Max(x => x.ID) + 1;
                    else objBieuMau.ID = 1;

                    entities.CV_BIEU_MAU.Add(objBieuMau);
                }
                else //Edit the item
                {
                    int intBieuMauID = Convert.ToInt32 (txtBieuMauID.Text);
                    objBieuMau = entities.CV_BIEU_MAU.Find(intBieuMauID);
                }

                if (!Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE").Equals(drpLoaiQD.SelectedValue))
                {
                    objBieuMau.ID_LOAI_QUYET_DINH = Convert.ToInt32(drpLoaiQD.SelectedValue);
                }
                else
                {
                    objBieuMau.ID_LOAI_QUYET_DINH = null;
                }

                objBieuMau.MA_BIEU_MAU = txtMaBieuMau.Text;
                objBieuMau.MO_TA = txtMoTa.Text;
                objBieuMau.NOI_DUNG = edtNoiDung.Html;
                
                if (validation(objBieuMau))
                {   
                    entities.SaveChanges();
                 
                    //Save System Log
                    Commons.CommonFunctionsAndProcedures.saveSystemLog(String.Format(Commons.TitleConst.getTitleConst("LOG_BIEU_MAU_CAP_NHAT"), objBieuMau.MA_BIEU_MAU));

                    redirectURL();
                }
            }
            catch (Exception ex)
            {
                //errorMsg.Text = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            redirectURL();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var strBody = new StringBuilder();

            strBody.Append("<html " +
             "xmlns:o='urn:schemas-microsoft-com:office:office' " +
             "xmlns:w='urn:schemas-microsoft-com:office:word'" +
              "xmlns='http://www.w3.org/TR/REC-html40'>" +
              "<head><title>Time</title>");

            //The setting specifies document's view after it is downloaded as Print
            //instead of the default Web Layout
            strBody.Append("<!--[if gte mso 9]>" +
             "<xml>" +
             "<w:WordDocument>" +
             "<w:View>Print</w:View>" +
             "<w:Zoom>90</w:Zoom>" +
             "<w:DoNotOptimizeForBrowser/>" +
             "</w:WordDocument>" +
             "</xml>" +
             "<![endif]-->");

            strBody.Append("<style>" +
             "<!-- /* Style Definitions */" +
             "@page Section1" +
             "   {size:8.5in 11.0in; " +
             "   margin:0.7in 0.8in 0.7in 1.3in ; " +
             "   mso-header-margin:.5in; " +
             "   mso-footer-margin:.5in; mso-paper-source:0;}" +
             " div.Section1" +
             "   {page:Section1;}" +
             "-->" +
             "</style></head>");

            strBody.Append("<body lang=EN-US style='tab-interval:.5in'>" +
             "<div class=Section1>");
            
            strBody.Append(edtNoiDung.Html);
            strBody.Append("</div></body></html>");

            //Force this content to be downloaded 
            //as a Word document with the name of your choice
            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=myword.doc");

            Response.Write(strBody.ToString());
        }
    }
}