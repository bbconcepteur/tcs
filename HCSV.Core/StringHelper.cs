using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSV.Core
{
    public class StringHelper
    {
        public StringHelper() { }
        public static void RepalceData<T>(T item) where T : new()
        {
            // Just grabbing this to get hold of the type name:
            var type = item.GetType();

            // Get the PropertyInfo object:
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var oldValue = property.GetValue(item, null);
                if (oldValue != null && oldValue is string)
                {
                    if (!string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        property.SetValue(item, ReplaceImageString(oldValue.ToString()));
                    }
                }
                
            }

        }

        private static string ReplaceImageString(string source)
        {
            var srcUrl = ConfigurationManager.AppSettings["SRC_URL_REPLACE"];
            var desUrl = ConfigurationManager.AppSettings["DES_URL_REPLACE"];
            return source.Replace(srcUrl, desUrl);
        }
    }
}
