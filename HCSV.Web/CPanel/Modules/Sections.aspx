<%@ Page Title="Khai báo giới tính" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Sections.aspx.cs" Inherits="CPanel.Modules.Sections" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
    
        <div class="page-header"><h1 class="panel-title">Section Management</h1></div>            
    
        <dx:ASPxGridView ID="grvLib" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvLib_DataBinding"
            OnRowDeleting="grvLib_RowDeleting" OnRowUpdating="grvLib_RowUpdating" AutoGenerateColumns="False"
            EnableRowsCache="False" OnRowInserting="grvLib_RowInserting" EnableCallBacks="false" SettingsEditing-Mode="Inline" SettingsPager-Mode="ShowAllRecords">
            
            <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'id');
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


                <dx:GridViewDataTextColumn FieldName="title" Caption="SECTION_TITLE" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                                            

                <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="PUBLISHED" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                    <DataItemTemplate>                                                
                        <%# ((bool)Eval("published") ? "<div class='active_icon_css'></div>" : "")%>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>

                <%-- <dx:GridViewDataDateColumn FieldName="NGAY_TAO" Caption="NGAY_TAO" Settings-AllowSort="False"  CellStyle-CssClass="center_css" HeaderStyle-HorizontalAlign="Center" VisibleIndex="6">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" DisplayFormatInEditMode="true"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>                --%>
                <dx:GridViewDataTextColumn FieldName="ordering" Caption="ORDERING" Settings-AllowSort="False" CellStyle-CssClass="center_css"  HeaderStyle-HorizontalAlign="Center" VisibleIndex="8" />
            </Columns>
        </dx:ASPxGridView>        
    </div>
</asp:Content>
