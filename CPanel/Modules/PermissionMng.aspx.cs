using DevExpress.Web.ASPxGridView;
using NH_Web.Commons;
using NH_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NH_Web.Modules.Admin
{
    public partial class PermissionMng : System.Web.UI.Page
    {
        public static string lbNotice;
        private Entities entities = new Entities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvFunctions.DataBind();
                grvGroups.DataBind();
                lstPermissions.DataBind();
            }
        }

        protected void grvFunctions_DataBinding(object sender, EventArgs e)
        {
            var lstFuntions = entities.FUNCTIONs.Select(x => new FUNCTIONViewModel()
            {
                ID = x.ID,
                CODE = x.CODE,
                DESCRIPT = x.DESCRIPT,
                LASTMODIFY = x.LASTMODIFY,
                MODIFYBY_MOD = entities.USERLOGINs.FirstOrDefault(a => a.ID == x.MODIFYBY).FULLNAME
            }).ToList();
            grvFunctions.DataSource = lstFuntions;
        }

        protected void grvFunctions_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                int id = (int)(e.Keys[0]);
                var obj = entities.FUNCTIONs.Find(id);
                obj.CODE = Convert.ToString(e.NewValues["CODE"]);
                obj.DESCRIPT = Convert.ToString(e.NewValues["DESCRIPT"]);
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
        }

        protected void grvFunctions_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                FUNCTION obj = new FUNCTION();
                if (entities.FUNCTIONs.Count() > 0) obj.ID = entities.FUNCTIONs.Max(x => x.ID) + 1;
                else obj.ID = 1;
                if (e.NewValues["CODE"] != null) obj.CODE = Convert.ToString(e.NewValues["CODE"]);
                else
                {
                    lbNotice = "Yêu cầu nhập mã chức năng";
                    return;
                }
                obj.DESCRIPT = Convert.ToString(e.NewValues["DESCRIPT"]);
                obj.CREATED = obj.LASTMODIFY = DateTime.Now;
                obj.CREATEDBY = obj.MODIFYBY = CheckUserInfo.GetUserId();
                entities.FUNCTIONs.Add(obj);
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

        protected void grvFunctions_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                int id = (int)(e.Keys[0]);
                var obj = entities.FUNCTIONs.Find(id);
                entities.FUNCTIONs.Remove(obj);
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

        protected void grvFunctions_FocusedRowChanged(object sender, EventArgs e)
        {
            grvGroups.Selection.UnselectAll();
            int FuntionId = (int)grvFunctions.GetRowValues(grvFunctions.FocusedRowIndex, grvFunctions.KeyFieldName);
            List<int?> objs = entities.GROUPS_PERMISSION.Where(x => x.IDFUNCTION == FuntionId).Select(x => x.IDGROUP).Distinct().ToList();
            foreach (var i in objs)
            {
                grvGroups.Selection.SetSelectionByKey(i, true);
            }
        }

        protected void grvGroups_DataBinding(object sender, EventArgs e)
        {
            grvGroups.DataSource = entities.GROUPS.ToList();
        }

        protected void grvGroups_FocusedRowChanged(object sender, EventArgs e)
        {

            int FuntionId = (int)grvFunctions.GetRowValues(grvFunctions.FocusedRowIndex, grvFunctions.KeyFieldName);
            int GroupId = (int)grvGroups.GetRowValues(grvGroups.FocusedRowIndex, grvGroups.KeyFieldName);
            var obj = entities.GROUPS_PERMISSION.FirstOrDefault(x => x.IDFUNCTION == FuntionId && x.IDGROUP == GroupId);
            if (obj != null && obj.IDPERMISSION != null) lstPermissions.Items.FindByValue(obj.IDPERMISSION.ToString()).Selected = true;
            //else lstPermissions.Items.FindByText("Read").Selected = true; // mặc định là quyền Read
        }

        protected void lstPermissions_DataBinding(object sender, EventArgs e)
        {
            lstPermissions.DataSource = entities.PERMISSIONs.ToList();
            lstPermissions.ValueField = "ID";
            lstPermissions.TextField = "DESCRIPT";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int FuntionId = (int)grvFunctions.GetRowValues(grvFunctions.FocusedRowIndex, grvFunctions.KeyFieldName);
            var lstGroupId = grvGroups.GetSelectedFieldValues(grvGroups.KeyFieldName);

            // Xóa group khỏi funtion nếu không chọn
            var orgGroupId = entities.GROUPS_PERMISSION.Where(x => x.IDFUNCTION == FuntionId).Select(x => x.IDGROUP).ToList();
            foreach (int g in orgGroupId)
            {
                if (!lstGroupId.Contains(g))
                {
                    GROUPS_PERMISSION removeObj = entities.GROUPS_PERMISSION.FirstOrDefault(x => x.IDFUNCTION == FuntionId && x.IDGROUP == g);
                    entities.GROUPS_PERMISSION.Remove(removeObj);
                    entities.SaveChanges();
                }
            }
            // Thêm group vào function, nếu chọn group nhưng chưa có trong function
            foreach (int i in lstGroupId)
            {
                bool b = entities.GROUPS_PERMISSION.Where(x => x.IDFUNCTION == FuntionId && x.IDGROUP == i).Any();
                if (!b) // nếu function chưa có group này
                {
                    GROUPS_PERMISSION obj = new GROUPS_PERMISSION();
                    if (entities.GROUPS_PERMISSION.Count() > 0) obj.ID = entities.GROUPS_PERMISSION.Max(x => x.ID) + 1;
                    else obj.ID = 1;
                    obj.IDFUNCTION = FuntionId;
                    obj.IDGROUP = i;
                    obj.IDPERMISSION = 1; // Mặc định là quyền Read
                    obj.CREATED = obj.LASTMODIFY = DateTime.Now;
                    obj.CREATEDBY = obj.MODIFYBY = CheckUserInfo.GetUserId();
                    entities.GROUPS_PERMISSION.Add(obj);
                    entities.SaveChanges();
                }
            }

            /* update permission cho group */
            int focusGroupId = (int)grvGroups.GetRowValues(grvGroups.FocusedRowIndex, grvGroups.KeyFieldName);
            var permissionObj = entities.GROUPS_PERMISSION.FirstOrDefault(x => x.IDFUNCTION == FuntionId && x.IDGROUP == focusGroupId);
            if (grvGroups.Selection.IsRowSelected(grvGroups.FocusedRowIndex))
            {
                if (permissionObj != null)
                {
                    permissionObj.IDPERMISSION = Convert.ToInt32(lstPermissions.SelectedItem.Value);
                    permissionObj.LASTMODIFY = DateTime.Now;
                    permissionObj.MODIFYBY = CheckUserInfo.GetUserId();
                    entities.SaveChanges();
                }
                else
                {
                    GROUPS_PERMISSION newPer = new GROUPS_PERMISSION();
                    if (entities.GROUPS_PERMISSION.Count() > 0) newPer.ID = entities.GROUPS_PERMISSION.Max(x => x.ID) + 1;
                    else newPer.ID = 1;
                    newPer.IDFUNCTION = FuntionId;
                    newPer.IDGROUP = focusGroupId;
                    newPer.IDPERMISSION = Convert.ToInt32(lstPermissions.SelectedItem.Value);
                    newPer.CREATED = newPer.LASTMODIFY = DateTime.Now;
                    newPer.CREATEDBY = newPer.MODIFYBY = CheckUserInfo.GetUserId();
                    entities.GROUPS_PERMISSION.Add(newPer);
                    entities.SaveChanges();
                }
            }                     
        }

        protected void grvFunctions_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
        {
            if (e.FieldName == "LASTMODIFY" && e.Value != null)
                e.Value = DateTime.Parse(e.Value.ToString());
        }

        protected void grvFunctions_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        } 
    }
}