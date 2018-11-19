<%@ Page Title="Functions Manager" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FunctionList.aspx.cs" Inherits="CPanel.Modules.FunctionList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="main_contain_css main_contain_css_1">

        <div class="page-header">
            <h1 class="panel-title">Functions manager</h1>
        </div>
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnCreate" CssClass="btn btn-info" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>
        <%--<div id="NutAdd" class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>--%>

        <div class="page-header">
            <h1 class="panel-title">List Of Functions</h1>
        </div>

        <dx:aspxgridview id="grvLib" width="100%" runat="server" keyfieldname="id" ondatabinding="grvLib_DataBinding"
            onrowdeleting="grvLib_RowDeleting" onrowupdating="grvLib_RowUpdating" autogeneratecolumns="False"
            enablerowscache="False" onrowinserting="grvLib_RowInserting" enablecallbacks="false" settingsediting-mode="Inline" settingspager-mode="ShowAllRecords">
            
                <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'name');
                                                                     else if (e.buttonID == 'btnEdit1')	funEdit(s,e, 'id');
                                                                     else if (e.buttonID == 'btnAdd') funAdd();}" />            
            
                <Columns>
                    <dx:GridViewCommandColumn CellStyle-CssClass="bt_action__css" Width="150px" ButtonType="Image" VisibleIndex="6" Caption="#" HeaderStyle-HorizontalAlign="Center">
                        <NewButton Visible="false" Text="Thêm"></NewButton>
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
                    <dx:GridViewDataTextColumn FieldName="name" Caption="Tên Function" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="link" Caption="Link" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" />
                   
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Status" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="4">
                        <DataItemTemplate>                                                
                            <%# ((bool)Eval("published") ? "<div class='active_icon_css'></div>" : "")%>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Access" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="5">
                        <DataItemTemplate>                                                
                            <%# (Eval("access").ToString()=="1"? "Yes" : "No")%>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                   
                    
                </Columns>
            </dx:aspxgridview>

        <!-- ModalPopupExtender -->


        <!--BEGIN: POPUP Xem so lieu cac tai khoan ke toan -->
        <cc1:ModalPopupExtender ID="mp1" BehaviorID="popUpBehaviorID_Menu" RepositionMode="RepositionOnWindowResize" runat="server" PopupControlID="ID_Panel" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="ID_Panel" runat="server" CssClass="modalPopup modalPopupMenu_css" align="center" Style="display: none">
            <asp:Button ID="btnClose" CssClass="popUpClose_css" runat="server" Text="Close" />


            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_CHA") %></label>
            <asp:DropDownList ID="drpMenus" AutoPostBack="false" CssClass="form-control element_tab_css" runat="server"></asp:DropDownList>

            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("TIEU_DE") %></label>
            <asp:TextBox ID="txtTieude" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>

            <asp:Button ID="btnSubmit" runat="server" CssClass="btn-danger" Text="Submit" OnClick="btnSubmit_Click" />

        </asp:Panel>
        <!--END: POPUP Xem so lieu cac tai khoan ke toan -->
        <asp:TextBox ID="txtFunID" runat="server" CssClass="invisible_css"></asp:TextBox>
        <asp:Button ID="btnEdit" runat="server" CssClass="invisible_css" OnClick="btnEdit_Click" />
        <asp:TextBox ID="txtOriginID" runat="server" CssClass="invisible_css"></asp:TextBox>

    </div>

    <script>
        function funAdd() {
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");
        }

        function funEdit(s, e, strField) {
            s.GetRowValues(e.visibleIndex, strField, OnGetRow_Action_URL);
        }

        function OnGetRow_Action_URL(intID) {
            $("#<%=txtFunID.ClientID%>").val(intID);
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");
        }

    </script>
</asp:Content>
