<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ContentList_Copy_TUYEN.aspx.cs" Inherits="CPanel.Modules.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>

        <div class="page-header"><h1 class="panel-title">Content Management</h1></div>

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>                

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Category<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpCategory" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpCategory_DataBinding" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"></asp:DropDownList>
        </div>                
 
        <div class="page-header"><h1 class="panel-title">List of contents</h1></div>
        
        
        <div class="panel-body">
            <dx:ASPxGridView ID="grvContents" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvContents_DataBinding" 
                    Settings-ShowGroupPanel="true" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn FieldName="title" Settings-AutoFilterCondition="Contains" Caption="Title" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="javascript:viewContent('<%# Eval("id") %>')"><%# Eval("title") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                                                
                    <dx:GridViewDataColumn FieldName="introtext" Settings-AutoFilterCondition="Contains" Caption="Intro Content" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval(Server.HtmlDecode("introtext")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                    

                    <dx:GridViewDataTextColumn FieldName="position" Settings-AutoFilterCondition="Contains" Caption="Position" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                        
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewContent" runat="server" OnClick="btnViewContent_Click" CssClass="invisible_css" />
            <asp:TextBox ID="txtContentID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>        
    </div>
    <script>
        function viewContent(strContentID) {
            $("#<%=txtContentID.ClientID%>").val(strContentID);
            __doPostBack("<%= btnViewContent.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>
