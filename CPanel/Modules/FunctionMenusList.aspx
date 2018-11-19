<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FunctionMenusList.aspx.cs" Inherits="CPanel.Modules.FunctionMenusList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>

        <div class="page-header"><h1 class="panel-title">Function Menus</h1></div>

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>                
         
        <div class="page-header"><h1 class="panel-title">List of Funtion Menus</h1></div>
        
        
        <div class="panel-body">
            <dx:ASPxGridView ID="grvCategories" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvCategories_DataBinding" Settings-ShowFilterRow="false"
                    Settings-ShowFilterRowMenu="true" Settings-ShowGroupPanel="true" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn Caption="Menu title" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                                            
                            <a href="javascript:viewMenus('<%# Eval("id") %>')"><%# Eval("title") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                                                
                    <dx:GridViewDataColumn Caption="URL" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval(Server.HtmlDecode("description")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewMenus" runat="server" OnClick="btnViewMenus_Click" CssClass="invisible_css" />
            <asp:TextBox ID="txtMenuID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>        
    </div>
    <script>
        function viewMenus(strMenuID) {
            $("#<%=txtMenuID.ClientID%>").val(strMenuID);
            __doPostBack("<%= btnViewMenus.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>
