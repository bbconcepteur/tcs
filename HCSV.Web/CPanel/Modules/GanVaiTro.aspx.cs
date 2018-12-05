using DevExpress.Web.ASPxGridView;
using System;
using CPanel.Commons;
//using QLNS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HCSV.Models;


namespace CPanel.Modules
{
    public partial class GanVaiTro : System.Web.UI.Page    
    {
        public string ACCOUNT_STATUS_PENDING = "0";
        public string ACCOUNT_STATUS_APPROVED = "1";
        public string ACCOUNT_STAUTS_REJECTED = "-1";

        public static string lbNotice;
        private TCSEntities entities = new TCSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                //Set Captions for GridView
                Commons.TitleConst.setTitleConst_ASPxGridView(grvVaiTro);
                Commons.TitleConst.setTitleConst_ASPxGridView(grvUsers);

                //get "ACCOUNT STAUTS"
                getAccountStatus();

                grvVaiTro.DataBind();
                grvUsers.DataBind();
            }
        }

        protected void getAccountStatus()
        {
            drpAccountStatus.Items.Add(new ListItem(Commons.TitleConst.getTitleConst("BLANK_ITEM_TITLE"), Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")));
            drpAccountStatus.Items.Add(new ListItem(Commons.TitleConst.getTitleConst("STATUS_ACTIVE_TITLE"), Commons.TitleConst.getTitleConst("STATUS_ACTIVE_VALUE")));
            drpAccountStatus.Items.Add(new ListItem(Commons.TitleConst.getTitleConst("STATUS_INACTIVE_TITLE"), Commons.TitleConst.getTitleConst("STATUS_INACTIVE_VALUE")));
        }

        protected void grvVaiTro_DataBinding(object sender, EventArgs e)
        {
            var lstGroup = entities.jos_rights.ToList();
            grvVaiTro.DataSource = lstGroup;
        }

        /*protected void grvVaiTro_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int id = (int)(e.Keys[0]);
                var obj = entities.QT_VAI_TRO.Find(id);
                obj.GROUP_NAME = Convert.ToString(e.NewValues["GROUP_NAME"]);
                obj.LASTMODIFY = DateTime.Now;
                obj.MODIFYBY = CheckUserInfo.GetUserId();
                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                lbNotice = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
            }
            finally
            {
                e.Cancel = true;
                (sender as ASPxGridView).CancelEdit();
            }
        }*/

        /*protected void grvVaiTro_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                GROUP obj = new GROUP();
                if (entities.GROUPS.Count() > 0) obj.ID = entities.GROUPS.Max(x => x.ID) + 1;
                else obj.ID = 1;
                if (e.NewValues["GROUP_NAME"]!=null) obj.GROUP_NAME = Convert.ToString(e.NewValues["GROUP_NAME"]);
                else
                {
                    lbNotice.Text = "Chưa nhập tên nhóm";
                    return;
                }
                obj.CREATED = obj.LASTMODIFY = DateTime.Now;
                obj.CREATEDBY = obj.MODIFYBY = CheckUserInfo.GetUserId();
                entities.GROUPS.Add(obj);
                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                lbNotice = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
            }
            finally
            {
                e.Cancel = true;
                (sender as ASPxGridView).CancelEdit();
            }
        }

        protected void grvVaiTro_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                int id = (int)(e.Keys[0]);
                var obj = entities.GROUPS.Find(id);
                entities.GROUPS.Remove(obj);
                entities.SaveChanges();
            }
            catch (Exception ex)
            {
                lbNotice = "Lỗi: " + ex.Message + ex.InnerException + ex.StackTrace;
                // ghi log
            }
            finally
            {
                e.Cancel = true;
            }
        }
        */
        protected void grvVaiTro_FocusedRowChanged(object sender, EventArgs e)
        {
            grvUsers.Selection.UnselectAll();
            int intVaiTroId = (int)grvVaiTro.GetRowValues(grvVaiTro.FocusedRowIndex, grvVaiTro.KeyFieldName);
            List<int> objs = entities.jos_rights_users.Where(x => x.id_right == intVaiTroId).Select(x => x.id_user).Distinct().ToList();
            foreach (var i in objs)
            {
                grvUsers.Selection.SetSelectionByKey(i, true);
            }
            refreshPage();
        }
        
        protected void grvUsers_DataBinding(object sender, EventArgs e)
        {
            bool blAccountStatusALL = false;
            int intAccountStatus = 0;
            if (drpAccountStatus.SelectedValue.Equals(Commons.TitleConst.getTitleConst("BLANK_ITEM_VALUE")))
            {
                blAccountStatusALL = true;
            }
            else
            {
                intAccountStatus = Convert.ToInt32(drpAccountStatus.SelectedValue);
            }

            //get list of user by VAI_TRO
            int intVaiTroId = (int)grvVaiTro.GetRowValues(grvVaiTro.FocusedRowIndex, grvVaiTro.KeyFieldName);
            List<int> lstVaiTroUsers = entities.jos_rights_users.Where(x => x.id_right == intVaiTroId).Select(x => x.id_user).Distinct().ToList();

            //set beginning value if result = NULL
            if (lstVaiTroUsers == null) 
            {
                lstVaiTroUsers.Add(CommonFuncs.NUMBER_INVALID_INTEGER);
            }

            int blStatus ;
            if (intAccountStatus == 1)
            {
                blStatus = 0;
            }
            else
            {
                blStatus = 1;
            }

            var lstNhanVien = entities.jos_users.Where(x => (blAccountStatusALL ? true : x.block == blStatus)).ToList();
             grvUsers.DataSource = lstNhanVien;

             //Unchecked & Checked
             grvUsers.Selection.UnselectAll();             
             foreach (var i in lstVaiTroUsers)
             {
                 grvUsers.Selection.SetSelectionByKey(i, true);
             }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intVaiTroID = Convert.ToInt16(grvVaiTro.GetRowValues(grvVaiTro.FocusedRowIndex, grvVaiTro.KeyFieldName));
            var lstUserId = grvUsers.GetSelectedFieldValues(grvUsers.KeyFieldName);

            // Xóa user khỏi group nếu không chọn
            var orgUserId = entities.jos_rights_users.Where(x => x.id_right == intVaiTroID).Select(x => x.id_user).ToList();
            foreach (int u in orgUserId)
            {
                if (!lstUserId.Contains(u))
                {
                    jos_rights_users removeObj = entities.jos_rights_users.FirstOrDefault(x => x.id_right == intVaiTroID && x.id_user == u);
                    entities.jos_rights_users.Remove(removeObj);
                    entities.SaveChanges();
                }
            }

            // Thêm user vào group nếu chọn nhưng chưa có trong group
            foreach (int i in lstUserId)
            {
                bool b = entities.jos_rights_users.Where(x => x.id_right == intVaiTroID && x.id_user == i).Any();
                if(!b)
                {
                    //Update status User
                    jos_users objNhanVien = entities.jos_users.Find(i);
                    objNhanVien.block = 0;                    

                    //Insert VAI_TRO cho USER
                    jos_rights_users obj = new jos_rights_users();
                    //if (entities.jos_rights_users.Count() > 0) obj.id = entities.jos_rights_users.Max(x => x.id) + 1;
                    //else obj.id=1;
                    obj.id_right = (int)intVaiTroID;
                    obj.id_user = (int)i;
                    //obj.CREATED = obj.LASTMODIFY = DateTime.Now;
                    //obj.CREATEDBY = obj.MODIFYBY = CheckUserInfo.GetUserId();
                    entities.jos_rights_users.Add(obj);
                    entities.SaveChanges();
                }
            }

            //Display Message Box
            Commons.ValidationFuncs.errorMessage_TimeDelay(Commons.TitleConst.getTitleConst("MSG_UPDATE_SUCCESFULLY"), Page);
        }

        protected void drpUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshPage();
        }

        protected void drpAccountStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshPage();
        }

        public void refreshPage()
        {
            grvVaiTro.DataBind();
            grvUsers.DataBind();
        }
    }
}