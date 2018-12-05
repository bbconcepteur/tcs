<%@ Page Title="Quản lý nhóm" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="GanVaiTro.aspx.cs" Inherits="CPanel.Modules.GanVaiTro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click"></asp:Button>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click"></asp:Button>
        </div>

        <div class="page-header">
            <h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("PAGE_RULE_ASSIGNMENT") %></h1>
        </div>
        <div class="col-md-12">
            <div class="col-md-6">
                <label class="control-label line_lb_css"><%=CPanel.Commons.TitleConst.getTitleConst("USER_STATUS") %></label>
                <asp:DropDownList ID="drpAccountStatus" AutoPostBack="true" OnSelectedIndexChanged="drpAccountStatus_SelectedIndexChanged" CssClass="form-control element_tab_css" TabIndex="2" runat="server">
                </asp:DropDownList>
            </div>
        </div>

        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("RULE_NAME") %></h1></div>
           
        <div class="panel-body">
            <dx:aspxgridview id="grvVaiTro" width="100%" runat="server" keyfieldname="id" ondatabinding="grvVaiTro_DataBinding" settings-showfilterrow="false"
                autogeneratecolumns="False" settings-showfilterrowmenu="true" OnFocusedRowChanged="grvVaiTro_FocusedRowChanged"
                enablerowscache="False" settingsediting-mode="Inline"
                enablecallbacks="false">
                <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                <SettingsText CommandNew="Add" CommandEdit="Edit" CommandDelete="Delete" CommandUpdate="Save" CommandCancel="Cancel" ConfirmDelete="Bạn chắc chứ?"/>  
                <Columns>
                    <dx:GridViewCommandColumn CellStyle-CssClass="invisible_css" HeaderStyle-CssClass="invisible_css" NewButton-Visible="true" DeleteButton-Visible="true" EditButton-Visible="true" Caption="Action" HeaderStyle-HorizontalAlign="Center" Width="120px" VisibleIndex="0" />
                    <dx:GridViewDataTextColumn FieldName="name" width="200px" Caption="RULE_NAME" HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" />
                    <dx:GridViewDataTextColumn FieldName="description" Caption="DESCRIPTION" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" ReadOnly="true" />                        
                </Columns>
                <SettingsPager PageSize="20">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
            </dx:aspxgridview>
        </div>    

        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("USER_LIST") %></h1></div>

        
        <div class="panel-body">
            <dx:aspxgridview id="grvUsers" width="100%" runat="server" keyfieldname="id" ondatabinding="grvUsers_DataBinding" settings-showfilterrow="false"
                settings-showfilterrowmenu="true" settings-showgrouppanel="false" autogeneratecolumns="False" clientinstancename="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn Caption="Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <%# Eval("name") %>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                        
                    <dx:GridViewDataTextColumn FieldName="email" Caption="Email" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                
                    <dx:GridViewDataColumn Settings-AutoFilterCondition="Contains" Caption="STATUS" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                        <DataItemTemplate>                                                
                            <%# ((short)Eval("block") == 0 ? "<div class='active_icon_css'></div>" : "")%>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    
                    <dx:GridViewDataTextColumn FieldName="usertype" Caption="LUONG_DON_VI" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                                          
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
            </dx:aspxgridview>
        </div>
        
    </div>

    <!--Set button title-->
    <script>
        $("#<%=btnCancel.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_CANCEL")%>');
        $("#<%=btnSave.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');
    </script>

</asp:Content>
