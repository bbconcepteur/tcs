using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCSV.Core
{
    public class UrlExtentions
    {
        public static string GenerateSlug(object title, object id)
        {
            if (title == null || string.IsNullOrEmpty(title.ToString())) title = "";
            string str = ConvertToUnsign(title.ToString()).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return string.Format("{0}-{1}", str, id);
        }

        static Regex _convertToUnsignRg = null;
        public static string ConvertToUnsign(string strInput)
        {
            if (ReferenceEquals(_convertToUnsignRg, null))
            {
                _convertToUnsignRg = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            }
            var temp = strInput.Normalize(NormalizationForm.FormD);
            return _convertToUnsignRg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }

        public static string Action(string actionName, object menu, object title, object id)
        {
            if (menu != null && !string.IsNullOrEmpty(menu.ToString()))
                return string.Format("/{0}/{1}/{2}", actionName, menu, GenerateSlug(title, id));
            return string.Format("/{0}/{1}", actionName, GenerateSlug(title, id));
        }
    }
}
