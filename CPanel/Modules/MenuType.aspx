<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MenuType.aspx.cs" Inherits="CPanel.Modules.MenuType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
        
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_TYPE_LIST") %></h1></div>
    
        <dx:ASPxGridView ID="grvLib" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvLib_DataBinding"
            OnRowDeleting="grvLib_RowDeleting" OnRowUpdating="grvLib_RowUpdating" AutoGenerateColumns="False"
            EnableRowsCache="False" OnRowInserting="grvLib_RowInserting" EnableCallBacks="false" SettingsEditing-Mode="Inline" SettingsPager-Mode="ShowAllRecords">
            <ClientSideEvents RowClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" />
            <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'TEN_GIOI_TINH');
                                                                 }" />            
            
            
            <Columns>
                <dx:GridViewCommandColumn CellStyle-CssClass="bt_action__css" ButtonType="Image" VisibleIndex="20" Caption="ACTION" HeaderStyle-HorizontalAlign="Center">
                    <NewButton Visible="true" Image-SpriteProperties-CssClass="bt_add_css"  Text="Thêm"></NewButton>
                    <EditButton Visible="true" Image-SpriteProperties-CssClass="bt_edit_css" Text="Sửa">                        
                    </EditButton>
                    <UpdateButton Visible="false" Image-SpriteProperties-CssClass="bt_update_css" Text="Update">                        
                    </UpdateButton>
                    <CancelButton Visible="false" Image-SpriteProperties-CssClass="bt_cancel_css" Text="Cancel"></CancelButton>
                    <DeleteButton Visible="false" Text="Xóa"></DeleteButton>                    
                    <CustomButtons>                                                
                        <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_delete_css" Text="Xóa" ID="btnDelete">
                        </dx:GridViewCommandColumnCustomButton>                                                
                    </CustomButtons>
                    
                    <ClearFilterButton Visible="True"></ClearFilterButton>
                </dx:GridViewCommandColumn>


                <dx:GridViewDataTextColumn FieldName="menutype" Caption="CODE" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                            
                <dx:GridViewDataTextColumn FieldName="title" Caption="TITLE" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" />
                <dx:GridViewDataTextColumn FieldName="description" Caption="DESCRIPTION" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="8" />
            </Columns>
        </dx:ASPxGridView>        
    </div>
</asp:Content>
