<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="QuanLyVaiTro.aspx.cs" Inherits="CPanel.Modules.QuanLyVaiTro" %>

<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">        

        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("RULE_MANAGEMENT") %></h1></div>
        
        <dx:ASPxGridView ID="grvLib" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvLib_DataBinding" on
                OnRowDeleting="grvLib_RowDeleting" OnRowUpdating="grvLib_RowUpdating" AutoGenerateColumns="False"
                EnableRowsCache="False" OnRowInserting="grvLib_RowInserting" EnableCallBacks="false" SettingsEditing-Mode="Inline" SettingsPager-Mode="ShowAllRecords">
                
                <ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" />  
                <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'name');}" /> 

                <Columns>
                    <dx:GridViewCommandColumn CellStyle-CssClass="bt_action__css" ButtonType="Image" VisibleIndex="6" Caption="ACTION" HeaderStyle-HorizontalAlign="Center">
                        
                        <EditButton Visible="true" Image-SpriteProperties-CssClass="bt_edit_css" Text="Sửa">                        
                        </EditButton>
                        <NewButton Visible="true" Image-SpriteProperties-CssClass="bt_add_css"  Text="Thêm">
                        </NewButton>

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
                    <dx:GridViewDataTextColumn FieldName="name" Caption="RULE_NAME" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="description" Caption="DESCRIPTION" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" />                            
                </Columns>
            </dx:ASPxGridView>        
    
        <!-- ModalPopupExtender --> 
    
        <!--BEGIN: POPUP Xem so lieu cac tai khoan ke toan -->       
        <cc1:ModalPopupExtender ID="mp1" BehaviorID="popUpBehaviorID_Menu" RepositionMode="RepositionOnWindowResize" runat="server" PopupControlID="ID_Panel" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="ID_Panel" runat="server" CssClass="modalPopup modalPopupMenu_css" align="center" style = "display:none">
            <asp:Button ID="btnClose" CssClass="popUpClose_css" runat="server" Text="Close" />               

            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_CHA") %></label>
            <asp:DropDownList ID="drpMenus" AutoPostBack="false" CssClass="form-control element_tab_css" runat ="server"></asp:DropDownList>

            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("TIEU_DE") %></label>
            <asp:TextBox ID="txtTieude" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
        
        </asp:Panel>
        <!--END: POPUP Xem so lieu cac tai khoan ke toan -->   
    </div>

</asp:Content>
