<%@ Page Title="Phân quyền" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PermissionMng.aspx.cs" Inherits="NH_Web.Modules.Admin.PermissionMng" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <span class="col-md-12 label label-danger"><%= lbNotice %></span>
    <div class="btn-group">
      <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Lưu"></asp:Button>
      <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Hủy"></asp:Button>
    </div><br /><br />
    <div class="col-md-12 panel panel-primary">
        <div class="panel-heading"><h3 class="panel-title">Quản lý chức năng</h3></div>
        <div class="panel-body">
            <dx:ASPxGridView ID="grvFunctions" Width="100%" runat="server" KeyFieldName="ID"  OnDataBinding="grvFunctions_DataBinding"
                OnRowDeleting="grvFunctions_RowDeleting" OnRowUpdating="grvFunctions_RowUpdating" AutoGenerateColumns="False"
                EnableRowsCache="False" SettingsEditing-Mode="Inline" OnRowInserting="grvFunctions_RowInserting" OnHeaderFilterFillItems="grvFunctions_HeaderFilterFillItems" Settings-ShowHeaderFilterButton="true"
                OnFocusedRowChanged="grvFunctions_FocusedRowChanged" EnableCallBacks="false" OnParseValue="grvFunctions_ParseValue">
                <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                <SettingsText CommandNew="Thêm" CommandEdit="Sửa" CommandDelete="Xóa" CommandUpdate="Lưu" CommandCancel="Hủy" ConfirmDelete="Bạn chắc chứ?"/>  
                <Columns>
                    <dx:GridViewCommandColumn NewButton-Visible="true" DeleteButton-Visible="true" EditButton-Visible="true" Caption="Thao tác" HeaderStyle-HorizontalAlign="Center" Width="120px" VisibleIndex="0" />
                    <dx:GridViewDataTextColumn FieldName="CODE" Caption="Mã chức năng" HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" SortOrder="Ascending" />
                    <dx:GridViewDataTextColumn FieldName="DESCRIPT" Caption="Mô tả" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="MODIFYBY_MOD" Caption="Sửa bởi" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" ReadOnly="true" />
                    <dx:GridViewDataTextColumn FieldName="LASTMODIFY" Caption="Sửa lúc" HeaderStyle-HorizontalAlign="Center" VisibleIndex="4" ReadOnly="true" />
                </Columns>
                <SettingsPager PageSize="20">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
            </dx:ASPxGridView>
        </div>
    </div>
    <div class="col-md-12 panel panel-primary">
        <div class="panel-heading"><h3 class="panel-title">Nhóm sử dụng</h3></div>
        <div class="panel-body">
            <dx:ASPxGridView ID="grvGroups" Width="100%" runat="server" KeyFieldName="ID" OnDataBinding="grvGroups_DataBinding" Settings-ShowFilterRow="true" 
                AutoGenerateColumns="False" ClientInstanceName="grvGroups" EnableCallBacks="false" OnFocusedRowChanged="grvGroups_FocusedRowChanged">
                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvGroups.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="GROUP_NAME" Caption="Tên nhóm" HeaderStyle-HorizontalAlign="Center" VisibleIndex="0" SortOrder="Ascending" />
                </Columns>
                <SettingsPager PageSize="20">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
            </dx:ASPxGridView>
        </div>
    </div>
    <div class="col-md-12s panel panel-primary">
        <div class="panel-heading"><h3 class="panel-title">Quyền</h3></div>
        <div class="panel-body">
            <dx:ASPxRadioButtonList ID="lstPermissions" OnDataBinding="lstPermissions_DataBinding" runat="server" Width="100%"></dx:ASPxRadioButtonList>
        </div>
    </div>
     <div class="btn-group">
      <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" Text="Lưu"></asp:Button>
      <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Hủy"></asp:Button>
    </div><br /><br />
</asp:Content>
