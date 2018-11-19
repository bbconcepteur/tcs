<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="RecycleBin.aspx.cs" Inherits="CPanel.Modules.RecycleBin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" OnClick="btnDelete_Click" Text="Delete" runat="server" />
            <asp:Button ID="btnRestore" CssClass="btn btn-warning" Text="Restore" OnClick="btnRestore_Click" runat="server" />            
        </div>

        <div class="page-header"><h1 class="panel-title">Content Management</h1></div>

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>                

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Type<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpTypeOfContent" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpTypeOfContent_SelectedIndexChanged"></asp:DropDownList>
        </div>                
         
        <div class="page-header"><h1 class="panel-title">List of contents</h1></div>
        
        
        <div id="div_content" runat="server" class="panel-body">
            <dx:ASPxGridView ID="grvContents" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvContents_DataBinding" Settings-ShowFilterRow="false"
                    Settings-ShowFilterRowMenu="true" Settings-ShowGroupPanel="true" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn Caption="Title" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0" Width="200px">
                        <DataItemTemplate>                                                
                            <%# Eval("title") %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                                                
                    <dx:GridViewDataColumn Caption="Intro Content" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval(Server.HtmlDecode("introtext")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="position" Caption="Position" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                        
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
            </dx:ASPxGridView>
        </div>    
        
        <div id="div_category" runat="server" class="panel-body">
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
                            <%# Eval("title") %>
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
        </div>        
    </div>
</asp:Content>
