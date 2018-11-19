<%@ Page Title="Quản lý danh mục các nhóm tin(Categories)"Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CategoryList.aspx.cs" Inherits="CPanel.Modules.CategoryList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
            <asp:DropDownList runat="server" ID="drpLanguages" CssClass="form-control" Visible="false" ></asp:DropDownList>
        </div>

        <div class="page-header"><h1 class="panel-title">Category Management</h1></div>

        
        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Sections<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpSections" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpSections_DataBinding" OnSelectedIndexChanged="drpSections_SelectedIndexChanged"></asp:DropDownList>
        </div>                

        <div class="page-header"><h1 class="panel-title">List of Categories</h1></div>
        <div class="panel-body">
            <dx:ASPxGridView ID="grvCategories" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvCategories_DataBinding" 
                    Settings-ShowGroupPanel="false" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
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
                            <a href="javascript:viewCategory('<%# Eval("id") %>')"> <%# Eval("title")%> </a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                   

                    <dx:GridViewDataColumn FieldName="name" Settings-AutoFilterCondition="Contains" Caption="Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("name") %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                    
                    
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="Section" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
                        <DataItemTemplate>
                             <%# Eval("section_name") %>
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="translation" Visible="false" Settings-AutoFilterCondition="Contains" Caption="Translation Language" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="3">
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
            <asp:Button ID="btnViewCategory" runat="server" OnClick="btnViewCategory_Click" CssClass="invisible_css" />
            <asp:Button ID="btnViewTranslation" runat="server"  OnClick="btnViewTranslation_Click" CssClass="invisible_css"/>
            <asp:TextBox ID="txtCategoryID" runat="server" CssClass="invisible_css"></asp:TextBox>
            
        </div>        
    </div>
    <script>
        function viewCategory(strCategoryID) {
            $("#<%=txtCategoryID.ClientID%>").val(strCategoryID);
            __doPostBack("<%= btnViewCategory.UniqueID %>", "OnClick");
        }
        function viewTranslation(strCategoryID) {
            $("#<%=txtCategoryID.ClientID%>").val(strCategoryID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>

