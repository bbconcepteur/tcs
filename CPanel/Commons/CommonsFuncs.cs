
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Security.Cryptography;

namespace CPanel.Commons
{
    public static class ValidationFuncs
    {
        public static void errorMessage_TimeDelay(string strErrorMessage, System.Web.UI.Page page)
        {
            System.Object ob = new System.Object();
            page.ClientScript.RegisterStartupScript(ob.GetType(), "MyKey", "havingError('"+strErrorMessage+"');", true);
        }

        public static void displayMessage_UpdateSuccessfully(string strErrorMessage, System.Web.UI.Page page)
        {
            System.Object ob = new System.Object();
            page.ClientScript.RegisterStartupScript(ob.GetType(), "MyKey", "UpdateSuccessfully('" + strErrorMessage + "');", true);
        }
    }

    
    public class Formats
    {
        public static string GetMD5(string input)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(input);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
            }

            return str_md5.Trim();
        }

        /**
         * Currency for Vietnam Dong 
         * (Convert from Float to Vietnam Dong Format (1,234,567.00 ==> 1 234 567,00))
         */
        public static string Currency_VND(decimal flMoney)
        {
            if (flMoney > 0) {
                return String.Format("{0:0,00}", flMoney).Replace(',', ' ').Replace('.', ',');
            }
            else
            {
                return "";
            }
             
        }

        /**
         * Currency for Vietnam Dong 
         * (Convert Value (get from web Form) to Vietnam Dong Format (1 234 567,00 ==> 1234567.00))
         */
        public static decimal Currency_VND_GetValueFromFORM (string strMoney)
        {
            if (!String.IsNullOrEmpty(strMoney))
            {
                return Convert.ToDecimal(strMoney.Replace(" ", "").Replace(',', '.'));
            }
            else
            {
                return 0;
            }

        }

    }
    
}