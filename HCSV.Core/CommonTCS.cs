using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSV.Core
{
    public class CommonTCS
    {
        public class Commons
        {
            public static string DATE_FORMAT_DD_MM_YYYY = "dd.MM.yyyy";
            public static string DATE_FORMAT_DD_MM_YYYY_2 = "dd/MM/yyyy";
            public static string DATE_FORMAT_DD_MM_YY = "dd/MM/yy";
            public static string DATE_FORMAT_DD_MMM_YYYY = "dd-MMM-yyyy";
            public static string DATE_FORMAT_DD_MM_YYYY_3_1 = "MM/dd/yy HH:mm:ss";
            public static string DATE_FORMAT_DD_MM_YYYY_3_2 = "MM/dd/yyyy HH:mm";
            public static string DATE_FORMAT_DDMMMYYYY = "ddMMMyyyy";
            public static string DATE_FORMAT_DD_MM_YYYY_4 = "dd/MM/yyyy HH:mm";
            public static string DATE_FORMAT_DD_MM_YYYY_4_1 = "dd/MM/yyyy HH:mm tt";
            public static string DATE_FORMAT_DD_MMM_YYYY_4 = "dd-MMM-yyyy HH:mm";

            public static string DATE_FORMAT_DD_MM_YYYY_5 = "MM/dd/yyyy HH:mm";
            public static string DATE_FORMAT_MM_dd_YYYY = "MM/dd/yyyy";
            public static string DATE_FORMAT_MM_dd_YY = "MM/dd/yy";

            public static string DATE_FORMAT_HOUR = "HH";
            public static string DATE_FORMAT_MINUTE = "mm";
            public static string DATE_FORMAT_YYYYMMDD = "yyyyMMdd";


            //for web service
            public static string user_code = "user_code";
            public static string lg_codesgshdgsksasa = "lg_codesgshdgsksasa";
            public static string locale = "locale";
            public static string awbFirst = "awbFirst";
            public static string awbLast = "awbLast";
            public static string awbFst = "awbFst";
            public static string awbLst = "awbLst";
            public static string serie = "serie";


            public static string[] strSplit(string strValue, string strMask)
            {
                string strTemp;
                if (!String.IsNullOrEmpty(strValue))
                {
                    int intLengMask = strMask.Length;
                    int intLengValue = strValue.Length;
                    strTemp = strValue.Replace(strMask, "");
                    intLengValue = intLengValue - strTemp.Length;

                    int intTotalElement = (intLengValue / intLengMask) + 1;
                    string[] strResult = new string[intTotalElement];

                    int intFirst = 0, intLast = 0;
                    int i = 0;
                    strTemp = "";
                    while (i < intTotalElement)
                    {
                        intLast = strValue.IndexOf(strMask);
                        if (intFirst == intLast)
                        {
                            strResult[i] = "";
                        }
                        else if (intLast == -1)
                        { //don't find
                            strResult[i] = strValue;
                            break;
                        }
                        else
                        {
                            strResult[i] = strValue.Substring(intFirst, intLast - intFirst);
                        }

                        strTemp = strValue.Substring(intFirst, intLast - intFirst + intLengMask);
                        strValue = strValue.Substring(intLast - intFirst + intLengMask, strValue.Length - strTemp.Length);

                        //intFirst = intLast + intLengMask;
                        i++;
                    }
                    return strResult;
                }
                return null;
            }

            public static bool checkDate(string strMM, string strDD, string strYYYY)
            {
                string strDate = String.Format("{0}/{1}/{2}", strDD, strMM, strYYYY);

                strDate = getCorrectDateFormat(strDate);

                try
                {
                    DateTime dt = DateTime.ParseExact(strDate, Commons.DATE_FORMAT_DD_MM_YYYY_2, System.Globalization.CultureInfo.InvariantCulture);
                    return true;
                }
                catch
                {
                    return false;
                }

            }

            public static DateTime mktime(string strMM, string strDD, string strYYYY)
            {

                string strDate = String.Format("{0}/{1}/{2}", strDD, strMM, strYYYY);

                strDate = getCorrectDateFormat(strDate);

                try
                {//dd/MM/yyyy
                    return DateTime.ParseExact(strDate, Commons.DATE_FORMAT_DD_MM_YYYY_2, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch//dd/MM/yy
                {
                    return DateTime.ParseExact(strDate, Commons.DATE_FORMAT_DD_MM_YY, System.Globalization.CultureInfo.InvariantCulture);
                }


            }

            public static string getCorrectDateFormat(string strDate)
            {
                string[] temp = strDate.Split('/');
                for (int i = 0; i < temp.Count(); i++)
                {
                    temp[i] = temp[i].Length == 1 ? "0" + temp[i] : temp[i];
                }
                strDate = temp.Count() > 1 ? String.Format("{0}/{1}/{2}", temp[0], temp[1], temp[2]) : strDate;

                return strDate;
            }

            public static DateTime strToTime(string strDate) //FORMAT: MM/dd/yy HH:mm:ss
            {
                strDate = getCorrectDateFormat(strDate);
                //MM/dd/yy HH:mm:ss; MM/dd/yyyy HH:mm; MM/dd/yy;MM/dd/yyyy; ddMMMyyyy
                string[] dateFormat = { DATE_FORMAT_DD_MM_YYYY_3_1, DATE_FORMAT_DD_MM_YYYY_3_2, DATE_FORMAT_MM_dd_YY, DATE_FORMAT_MM_dd_YYYY, DATE_FORMAT_DDMMMYYYY };
                for (int i = 0; i < dateFormat.Count(); i++)
                {
                    try
                    {
                        return DateTime.ParseExact(strDate, dateFormat[i], System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {

                    }
                }

                throw (new Exception("DINH DANG FORMAT NGAY SAI"));
            }

            public static string date_m_d_Y_H_i(DateTime dtDate)
            {
                return dtDate.ToString(Commons.DATE_FORMAT_DD_MM_YYYY_5);
            }

            public static string date_m_d_Y(DateTime dtDate)
            {
                return dtDate.ToString(Commons.DATE_FORMAT_MM_dd_YYYY);
            }

            public static string date_d_M_Y_H_i(DateTime dtDate) //d-M-Y H:i ex: 03-Mar-2017 10:18
            {
                return dtDate.ToString(Commons.DATE_FORMAT_DD_MMM_YYYY_4);
            }


            public static string date_d_M_Y_H_i_A(DateTime dtDate) //dd/MM/yyyy H:i AM or PM
            {
                return dtDate.ToString(Commons.DATE_FORMAT_DD_MM_YYYY_4_1);
            }

            public static string date_d_m_Y(DateTime dtDate) //d/m/Y
            {
                return dtDate.ToString(Commons.DATE_FORMAT_DD_MM_YYYY_2);
            }

            public static string date_d_M_Y(DateTime dtDate) //12/Nov/2018
            {
                return dtDate.ToString(Commons.DATE_FORMAT_DD_MMM_YYYY);
            }

            public static string date_H(DateTime dtDate) //H: Hour
            {
                return dtDate.ToString(Commons.DATE_FORMAT_HOUR);
            }

            public static string date_i(DateTime dtDate) //i: Minutes
            {
                return dtDate.ToString(Commons.DATE_FORMAT_MINUTE);
            }

            public static string date_Ymd(DateTime dtDate) //yyyyMMdd
            {
                return dtDate.ToString(Commons.DATE_FORMAT_YYYYMMDD);
            }

        }
    }
}
