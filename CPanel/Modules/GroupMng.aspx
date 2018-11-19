<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="GroupMng.aspx.cs" Inherits="CPanel.Modules.GroupMng" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-default" OnClick="btnDelete_Click" Text="Delete"></asp:Button>
            <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" CssClass="btn btn-warning" Text="Create"></asp:Button>            
        </div><br /><br />

        <div class="page-header"><h1 class="panel-title">Users Management</h1></div>        
        
        <br />    
        <div class="bg_100pecents_css errorMessage_css_1">
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
        </div>
        <div class="panel-body">
            <dx:ASPxGridView ID="grvUsers" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvUsers_DataBinding" Settings-ShowFilterRow="false"
                    Settings-ShowFilterRowMenu="true" Settings-ShowGroupPanel="false" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn Caption="Full name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="/Modules/UserDetails.aspx?userID=<%# Eval("id") %>"><%# Eval("name") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                        
                    <dx:GridViewDataTextColumn FieldName="email" Caption="Email" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="username" Caption="User name" HeaderStyle-HorizontalAlign="Center" VisibleIndex="3" />                    
                        
                    <dx:GridViewDataColumn Caption="Status" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="4">
                        <DataItemTemplate>                                                
                            <%#(Eval("block").ToString().Equals("0")) ? "Active":Eval("block").ToString().Equals("-1")?"REJECTED":"Inactive" %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                        
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                </SettingsPager>
            </dx:ASPxGridView>
        </div>

        <!--BEGIN: POPUP Xem so lieu cac tai khoan ke toan -->       
        <cc1:ModalPopupExtender ID="mp1" BehaviorID="popUpBehaviorID_Menu" RepositionMode="RepositionOnWindowResize" runat="server" PopupControlID="ID_Panel" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="ID_Panel" runat="server" CssClass="modalPopup modalPopupMenu_css" align="center" style = "display:none">
            <asp:Button ID="btnClose" CssClass="popUpClose_css" runat="server" Text="Close" />               

        
            <label class="control-label">fff</label>
            <asp:DropDownList ID="drpMenus" AutoPostBack="false" CssClass="form-control element_tab_css" runat ="server"></asp:DropDownList>

            <label class="control-label">fff</label>
            <asp:TextBox ID="txtTieude" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
        
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn-danger" Text="Submit"  />
        
            <asp:Button ID="btnMenuEdit" runat="server" CssClass="invisible_css" OnClick="btnMenuEdit_Click" Text="Edit Menu" />
        </asp:Panel>
        <!--END: POPUP Xem so lieu cac tai khoan ke toan -->
        <script>
            function viewPopup() {
                
                //$('#imagepreview').attr('src', $('#imageresource').attr('src'));
                $("#<%=ID_Panel.ClientID%>").show();
                $("#bd").css("background", "black");
                $(".modalPopup").addClass("modal_css");
                //$find('popUpBehaviorID_Menu').show();
                
            }

            function editPopup(intItemID) {                
                __doPostBack("<%= btnMenuEdit.UniqueID %>", "OnClick");
            }
        </script>
    </div>
        
</asp:Content>
