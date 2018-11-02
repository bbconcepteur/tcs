using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCSV.Core
{
    public class UrlHelper
    {
        public static string GenerateSlug(object title, object id)
        {
            string phrase = string.Format("{0}-{1}", title, id);

            string str = ConvertToUnsign(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
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

        public static string Action(string actionName, string controllerName, object title, object id)
        {
            return string.Format("/{0}/{1}/{2}", controllerName, actionName, GenerateSlug(title, id));
        }
    }
}
