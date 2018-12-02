<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ContentList.aspx.cs" Inherits="CPanel.Modules.ContentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>

        <div class="page-header">
            <h1 class="panel-title">Content Management</h1>
        </div>

        <div class="col-md-6 ">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Section</label>
            <asp:DropDownList ID="drpSection" CssClass="form-control" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
        </div>

        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Category</label>
            <asp:DropDownList ID="drpCategory" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpCategory_DataBinding" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-6">
            <label class="control-label line_lb_css">Tiêu Chí</label>
            <asp:DropDownList ID="drpTieuChi" CssClass="form-control" runat="server" OnDataBinding="drpTieuChi_DataBinding"></asp:DropDownList>
        </div>
        <div class="page-header">
            <h1 class="panel-title">List of contents</h1>
        </div>


        <div class="panel-body">
            <dx:aspxgridview id="grvContents" width="100%" runat="server" keyfieldname="id" ondatabinding="grvContents_DataBinding"
                settings-showgrouppanel="false" autogeneratecolumns="False" clientinstancename="grvUsers">
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
                     <dx:GridViewDataTextColumn FieldName="hits" Settings-AutoFilterCondition="Contains" Caption="Hits" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                        <dx:GridViewDataTextColumn FieldName="comment_count" Settings-AutoFilterCondition="Contains" Caption="Commnets" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                   <dx:GridViewDataColumn FieldName="state" Settings-AutoFilterCondition="Contains" Caption="Status" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                           <%# Eval("state").ToString().Contains("1")?"Publish" :"Pendding" %> 
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    
                     <dx:GridViewDataTextColumn FieldName="created_by" Settings-AutoFilterCondition="Contains" Caption="Create by" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                     <dx:GridViewDataTextColumn FieldName="approved_by" Settings-AutoFilterCondition="Contains" Caption="Approved by" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    <dx:GridViewDataTextColumn FieldName="publish_by" Settings-AutoFilterCondition="Contains" Caption="Published by" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                   
                     <dx:GridViewDataTextColumn FieldName="position" Visible="false" Settings-AutoFilterCondition="Contains" Caption="Position" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />

                    <dx:GridViewDataColumn Caption="Created Date" CellStyle-CssClass="center_css" Settings-AllowSort="False" CellStyle-Wrap="False" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Center" Width="100px" VisibleIndex="3">
                        <DataItemTemplate> 
                            <%# Eval("created") == null ? "" : ((DateTime)Eval("created")).ToString(CPanel.Commons.DateTimeType.DATE_FORMAT_DD_MM_YYYY) %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn> 

                    <dx:GridViewDataColumn FieldName="translation" Settings-AllowAutoFilter="False" Caption="Translation" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"  Width="100px" VisibleIndex="4">
                        <DataItemTemplate>
                             <a href="javascript:viewTranslation('<%# Eval("id") %>')"><div class="flag_css"></div></a>                                                                         
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn> 
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:aspxgridview>
            <asp:Button ID="btnViewContent" runat="server" OnClick="btnViewContent_Click" CssClass="invisible_css" />
            <asp:Button ID="btnViewTranslation" runat="server" OnClick="btnViewTranslation_Click" CssClass="invisible_css" />
            <asp:TextBox ID="txtContentID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>
    </div>
    <script>
        function viewContent(strContentID) {
            $("#<%=txtContentID.ClientID%>").val(strContentID);
            __doPostBack("<%= btnViewContent.UniqueID %>", "OnClick");
        }
        function viewTranslation(strContentID) {
            $("#<%=txtContentID.ClientID%>").val(strContentID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>
