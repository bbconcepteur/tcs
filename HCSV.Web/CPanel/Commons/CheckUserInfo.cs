using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using HCSV.Models;

namespace CPanel.Commons
{    
    public class CheckUserInfo
    {
        private static TCSEntities entities = new TCSEntities();
                
        
        /// <summary>
        /// Kiểm tra người dùng đã đăng nhập vào hệ thống hay chưa. Nếu chưa đăng nhập thì chuyển về trang SignIn
        /// </summary>
        public static void CheckLogin()
        {   
            if(HttpContext.Current.Session["UserId"] != null)
            {                
                
                
                int UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                var obj = entities.jos_users.Where(x => x.id == UserId).FirstOrDefault();
                if (obj == null || obj.block == 1) HttpContext.Current.Response.Redirect(String.Format(ConstURL.URL_SIGN_IN, HttpContext.Current.Request.RawUrl));
                
                //Check Permission
                if (!checkPermission(UserId))
                {
                    //HttpContext.Current.Response.Redirect(ConstURL.URL_CHECK_PERMISSION);
                }
            }
            else HttpContext.Current.Response.Redirect(String.Format(ConstURL.URL_SIGN_IN, HttpContext.Current.Request.RawUrl));
        }

        public static bool checkSupperAdmin()
        {
            if (HttpContext.Current.Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
                var obj = entities.jos_users.Where(x => x.id == UserId).FirstOrDefault();
                if (Commons.ConstValues.SUPPER_ADMIN.ToUpper().Equals(obj.username.ToUpper()))
                {
                    return true;

                }

            }
            return false;
        }

        //public static int? GetCompanyID()
        //{
        //    int? usrId = CheckUserInfo.GetUserId();
        //    return (int) entities.userlogins.Where(x => x.ID == usrId && x.IS_ACTIVE == 1).FirstOrDefault().IDDOANH_NGHIEP;
        //}

        /// <summary>
        /// Lấy UserId của người dùng hiện tại
        /// </summary>
        /// <returns></returns>
        public static int? GetUserId()
        {
            if (HttpContext.Current.Session["UserId"] != null) return Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            return null;
        }
        
        /// <summary>
        /// Kiểm tra quyền sử dụng chức năng của người dùng        
        public static bool checkPermission(int intUserID)
        {
            //Check Permission
            string strURL = HttpContext.Current.Request.RawUrl;
            var objMenu = entities.jos_menu.Where(x => (strURL.IndexOf(x.link) >= 0) && (x.link != null && x.link.Trim().Length > 0)).FirstOrDefault();
            if (objMenu != null)
            {
                //get List of Right
                List<int> lstRights = entities.jos_rights_users.Where(x => x.id_user == intUserID).Select(y => (int)y.id_right).ToList();
                //Check Permission
                if (lstRights != null && lstRights.Count > 0)
                {
                    var objPermission = entities.jos_menu_rights.Where(x => x.id_menu == objMenu.id && lstRights.Contains(x.id_right)).FirstOrDefault();
                    if (objPermission == null)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check permission
        /// </summary>
        /// <param name="IDBM"></param>
        /// <param name="functionCode"></param>
        /// <returns>0: deny, 1: read, 2: edit, 3: full(inc. delete)</returns>
        public static int CheckPermissionV2(int IDBM, String functionCode)
        {
            int res = 0;            
            return res;
        }
        
    }
}