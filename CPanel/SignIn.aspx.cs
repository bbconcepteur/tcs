using System;
using System.Configuration;
using System.Linq;
using CPanel.Commons;
using System.Security.Cryptography;
using HCSV.Models;

namespace CPanel
{
    public partial class SignIn : System.Web.UI.Page
    {
        private TCSEntities entities = new TCSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strAction = Request.QueryString["action"];
                if ((strAction != null) && (strAction.Equals("LOG_OUT")))
                {
                    Session.Clear();
                    Response.Redirect("/SignIn.aspx");
                }

                //txtUsername.Text = "tuyendv.aits";
                //txtPassword.Text = "123445";
                txtUsername.Focus();

                //if (chkDoanhNghiep.Checked == false) cboDoanhNghiep.Enabled = false;
                //else cboDoanhNghiep.Enabled = true;
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                //kiem tra xem co phai truy cap tu dong khong - check  
                if (Captcha.Code != Request.Params["tbCode"])
                {
                    string alert = "Mã Captcha chưa đúng, yêu cầu nhập lại!";
                    string alert2 = "Captcha code wrong, please try again!";
                    lbNotice.Text = string.Format(@"<div class='alert alert-danger alert-dismissible fade in' role='alert'>
                                                         <button type='button' class='close' data-dismiss='alert'>
                                                              <span aria-hidden='true'>&times;</span>
                                                              <span class='sr-only'>Close</span>
                                                         </button>
                                                         <strong>{0}</strong>
                                                         </br>
                                                         <strong class='english'>{1}</strong>
                                                       </div>", alert, alert2);
                    return;
                }



                lbNotice.Text = String.Empty;
                jos_users obj = null;
                //if (ConfigurationManager.AppSettings["LDAPConnect"].ToUpper() == "TRUE")
                {
                    //string username = txtUsername.Text + ConfigurationManager.AppSettings["LDAPDomain"];

                    //if (UserValidate(username, txtPassword.Text))
                    {
                        //int count = entities.USERLOGINs.ToList().Count;
                        string strPassword = Formats.GetMD5(txtPassword.Text);
                        var lstUsers = entities.jos_users.Where(x => x.username.Contains(txtUsername.Text) && x.password.Equals(strPassword)).ToList();

                        if (lstUsers.Count > 1)
                        {
                            /*if (chkDoanhNghiep.Checked == true)
                            {
                                int idDoanhNghiep = Convert.ToInt32(cboDoanhNghiep.SelectedValue);
                                obj = lstUsers.FirstOrDefault(x => x.IDDOANH_NGHIEP == idDoanhNghiep);
                            }
                            else
                            {
                                lbNotice.Text = "Chưa chọn doanh nghiệp!";
                                return;
                            }*/
                        }
                        else
                        {
                            obj = lstUsers.FirstOrDefault();
                        }
                        if (obj != null)
                        {
                            if (obj.block == 1)
                            {
                                lbNotice.Text = "Tài khoản bị khóa!";
                                return;
                            }
                            Session["UserId"] = obj.id;
                            if (!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
                            {
                                Response.Redirect(Request.QueryString["returnUrl"].ToString());
                            }
                            else Response.Redirect("/Modules/ContentList.aspx");
                        }
                        else
                        {
                            lbNotice.Text = "Tài khoản không có quyền truy cập!";
                            return;
                        }
                    }
                    /*else
                    {
                        lbNotice.Text = "Sai tên đăng nhập hoặc mật khẩu!";
                        return;
                    }*/
                }
            }
            catch (Exception ex)
            {

                lbNotice.Text = "Lỗi: " + Formats.GetMD5(txtPassword.Text) + ex.Message + ex.TargetSite + ex.StackTrace;
            }
        }

        protected Boolean UserValidate(string username, string password)
        {
            Boolean b = false;

            return b;
        }

        //protected void chkDoanhNghiep_CheckedChanged(object sender, EventArgs e)
        //{
        //    string username = txtUsername.Text + ConfigurationManager.AppSettings["LDAPDomain"];
        //    if (chkDoanhNghiep.Checked == true)
        //    {
        //        var lstDoanhNghiep = (from dn in entities.DOANH_NGHIEP
        //                              join u in entities.USERLOGINs on dn.ID equals u.IDDOANH_NGHIEP
        //                              where u.EMAIL == username
        //                              select dn).ToList();
        //        if (lstDoanhNghiep.Count > 0)
        //        {
        //            cboDoanhNghiep.DataSource = lstDoanhNghiep;
        //            cboDoanhNghiep.DataTextField = "NAME";
        //            cboDoanhNghiep.DataValueField = "ID";
        //            cboDoanhNghiep.DataBind();
        //            cboDoanhNghiep.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        cboDoanhNghiep.Items.Clear();
        //        cboDoanhNghiep.Enabled = false;
        //    }
        //}
    }
}