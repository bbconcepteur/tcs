<%@ Page Title="Quản lý ngôn ngữ" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="LanguageList.aspx.cs" Inherits="CPanel.Modules.LanguageList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
            <asp:DropDownList runat="server" ID="drpLanguages" CssClass="form-control" Visible="false" ></asp:DropDownList>
        </div>

        <div class="page-header"><h1 class="panel-title">Language Management</h1></div>

   
        <div class="page-header"><h1 class="panel-title">List of Languages</h1></div>
        <div class="panel-body">
            <dx:ASPxGridView ID="grvLanguages" Width="100%" runat="server" KeyFieldName="lang_id" OnDataBinding="grvLanguages_DataBinding" 
                    Settings-ShowGroupPanel="false" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn FieldName="image" Settings-AutoFilterCondition="Contains" Caption="Image" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# CPanel.Commons.CommonFuncs.convertContent((string)Eval("image")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="title" Settings-AutoFilterCondition="Contains" Caption="Title" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="javascript:viewLanguage('<%# Eval("lang_id") %>')"> <%# Eval("title")%> </a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                           
                    <dx:GridViewDataColumn FieldName="sef" Settings-AutoFilterCondition="Contains" Caption="Sef" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
                        <DataItemTemplate>
                             <%# Eval("sef") %>
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn>

                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewLanguage" runat="server" OnClick="btnViewLanguage_Click" CssClass="invisible_css" />
            <asp:TextBox ID="txtLanguageID" runat="server" CssClass="invisible_css"></asp:TextBox>
            
        </div>        
    </div>
    <script>
        function viewLanguage(strLanguageID) {
            $("#<%=txtLanguageID.ClientID%>").val(strLanguageID);
            __doPostBack("<%= btnViewLanguage.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>

