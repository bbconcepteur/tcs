using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration; 

namespace CPanel.Commons
{
    public class ReadFiles
    {
        /**
         * @Copyright: By TUYENDV
         * This Function to upload File Excel and Read any Sheet, return Result is DataTable
         * fileUpload: to get file from Client to save into Server
         * isHDR {=Yes/No}: To whether read Header of table in Sheet
         * intSheet: number of Sheet (Begin from 0) 
         * Server: ~HttpServerUtility Page
         **/
        public static DataTable ReadExcelFile (FileUpload fileUpload, string isHDR, int intSheet, HttpServerUtility Server)
        {
            if (fileUpload.HasFile)
            {
                string FileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileUpload.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);

                fileUpload.SaveAs(FilePath);

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                //Bind Data to GridView
                return dt;

            }
            else
            {
                return null;
            }

        }
    }
}