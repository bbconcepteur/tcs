<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MenuChucNang.aspx.cs" Inherits="CPanel.Modules.QuanTriHeThong.MenuChucNang" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="main_contain_css main_contain_css_1">        
        
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_MANAGEMENT") %></h1></div>

        <div class="col-xs-6 col-space-right-css">
            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_TYPE") %></label>
            <asp:DropDownList ID="drpMenuType" CssClass="form-control element_tab_css" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpMenuType_SelectedIndexChanged"></asp:DropDownList>
        </div>
    
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_LIST") %></h1></div>

        <dx:ASPxGridView ID="grvLib" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvLib_DataBinding"
                OnRowDeleting="grvLib_RowDeleting" OnRowUpdating="grvLib_RowUpdating" AutoGenerateColumns="False"
                EnableRowsCache="False" OnRowInserting="grvLib_RowInserting" EnableCallBacks="false" SettingsEditing-Mode="Inline" SettingsPager-Mode="ShowAllRecords">
            
                <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'TIEU_DE');
                                                                     else if (e.buttonID == 'btnEdit1')	funEdit(s,e, 'id');
                                                                     else if (e.buttonID == 'btnAdd') funAdd();}" />            
            
            
                <Columns>
                    <dx:GridViewCommandColumn CellStyle-CssClass="bt_action__css" Width="150px" ButtonType="Image" VisibleIndex="6" Caption="#" HeaderStyle-HorizontalAlign="Center">
                        <NewButton Visible="false"></NewButton>
                        <EditButton Visible="false" Text="Sửa"></EditButton>
                        <DeleteButton Visible="false" Text="Xóa"></DeleteButton>                    

                        <UpdateButton Visible="false" Image-SpriteProperties-CssClass="bt_update_css" Text="Update">                        
                        </UpdateButton>
                        <CancelButton Visible="false" Image-SpriteProperties-CssClass="bt_cancel_css" Text="Cancel"></CancelButton>
                        
                        
                        <CustomButtons>                                                
                            <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_add_css" ID="btnAdd">
                            </dx:GridViewCommandColumnCustomButton>                                                
                        </CustomButtons>

                        <CustomButtons>                                                
                            <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_edit_css" ID="btnEdit1">
                            </dx:GridViewCommandColumnCustomButton>                                                
                        </CustomButtons>

                        <CustomButtons>                                                
                            <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_delete_css" Text="Xóa" ID="btnDelete">
                            </dx:GridViewCommandColumnCustomButton>                                                
                        </CustomButtons>
                        <ClearFilterButton Visible="True"></ClearFilterButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ordering" Caption="STT" Settings-AllowSort="False" CellStyle-CssClass="center_css" HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn FieldName="name" Caption="Tên menu" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="link" Caption="URL" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" />                            

                    <dx:GridViewDataColumn FieldName="translation" Settings-AutoFilterCondition="Contains" Caption="Translation Language" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
                        <DataItemTemplate>
                                <a href="javascript:viewTranslation('<%# Eval("id") %>')">Translation</a>                                                                         
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn>
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
        
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn-danger" Text="Submit" OnClick="btnSubmit_Click" />
            
        </asp:Panel>
        <!--END: POPUP Xem so lieu cac tai khoan ke toan -->
        <asp:TextBox ID="txtMenuID" runat="server" CssClass="invisible_css"></asp:TextBox>
        <asp:Button ID="btnEdit" runat="server" CssClass="invisible_css" OnClick="btnEdit_Click" />
        <asp:Button ID="btnViewTranslation" runat="server"  OnClick="btnViewTranslation_Click" CssClass="invisible_css"/>
        <asp:TextBox ID="txtOriginID" runat="server" CssClass="invisible_css"></asp:TextBox>

    </div>

    <script>
        function funAdd () {
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");                        
        }

        function funEdit (s, e, strField) {
            s.GetRowValues(e.visibleIndex, strField, OnGetRow_Action_URL);
        }

        function OnGetRow_Action_URL(intID) {            
            $("#<%=txtMenuID.ClientID%>").val(intID);
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");                        
        }

        function viewTranslation(strCategoryID) {
            $("#<%=txtOriginID.ClientID%>").val(strCategoryID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>
