<%@ Page Title="Quản lý phần" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SectionList.aspx.cs" Inherits="CPanel.Modules.SectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>  

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>   

        <div class="page-header"><h1 class="panel-title">List of sections</h1></div>
        
        
        <div class="panel-body">
            <dx:ASPxGridView ID="grvSections" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvSections_DataBinding" 
                    Settings-ShowGroupPanel="true" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn FieldName="title" Caption="Title" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="javascript:viewSection('<%# Eval("id") %>')"><%# Eval("title") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                    

                    <dx:GridViewDataColumn FieldName="name" Settings-AutoFilterCondition="Contains" Caption="Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval(Server.HtmlDecode("name")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                                                         
                    
                    <dx:GridViewDataColumn FieldName="translation" Settings-AutoFilterCondition="Contains" Caption="Translation Language" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
                        <DataItemTemplate>
                             <a href="javascript:viewTranslation('<%# Eval("id") %>')">Translation</a>
                                                                         
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn> 
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewSection" runat="server" OnClick="btnViewSection_Click" CssClass="invisible_css" />
            <asp:Button ID="btnViewTranslation" runat="server"  OnClick="btnViewTranslation_Click" CssClass="invisible_css"/>
            <asp:TextBox ID="txtSectionID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>        
    </div>
    <script>
        function viewSection(strSectionID) {
            $("#<%=txtSectionID.ClientID%>").val(strSectionID);
            __doPostBack("<%= btnViewSection.UniqueID %>", "OnClick");
        }
        function viewTranslation(strSectionID) {
            $("#<%=txtSectionID.ClientID%>").val(strSectionID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>

