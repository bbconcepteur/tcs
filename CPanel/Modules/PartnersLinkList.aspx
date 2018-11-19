<%@ Page Title="Quản lý đối tác"Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PartnersLinkList.aspx.cs" Inherits="CPanel.Modules.PartnersLinkList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>

         <div class="page-header invisible_css"><h1 class="panel-title">Partner Management</h1></div>

        <div class="col-md-6 invisible_css">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="page-header"><h1 class="panel-title">List of Partners</h1></div>
        
        
        <div class="panel-body">
            
            <dx:ASPxGridView ID="grvPartners" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvPartners_DataBinding" 
                    Settings-ShowGroupPanel="false" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataColumn FieldName="image" Caption="Image" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# CPanel.Commons.CommonFuncs.convertContent((string)Eval("image")) %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>      

                      <dx:GridViewDataColumn FieldName="name" Settings-AutoFilterCondition="Contains" Caption="Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="javascript:viewPartner('<%# Eval("id") %>')"><%# Eval("name") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="representative" Settings-AutoFilterCondition="Contains" Caption="Representative" HeaderStyle-HorizontalAlign="Center" VisibleIndex="0" />
                                                
                    <dx:GridViewDataTextColumn FieldName="link" Visible="false" Settings-AutoFilterCondition="Contains" Caption="Link" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                       

                    <dx:GridViewDataTextColumn FieldName="address" Settings-AutoFilterCondition="Contains" Caption="Address" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                    <dx:GridViewDataTextColumn FieldName="phone" Settings-AutoFilterCondition="Contains" Caption="Phone" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" /> 
                    <dx:GridViewDataTextColumn FieldName="fax" Settings-AutoFilterCondition="Contains" Caption="Fax" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />
                    
                    <dx:GridViewDataColumn FieldName="translation" Settings-AutoFilterCondition="Contains" Caption="Translation" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                        <DataItemTemplate>
                             <a href="javascript:viewTranslation('<%# Eval("id") %>')"><div class="flag_css"></div></a>                                           
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn> 
                </Columns>
                <SettingsPager PageSize="50">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewPartner" runat="server" OnClick="btnViewPartner_Click" CssClass="invisible_css" />
            <asp:Button ID="btnViewTranslation" runat="server"  OnClick="btnViewTranslation_Click" CssClass="invisible_css"/>
            <asp:TextBox ID="txtPartnerID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>        
    </div>
    <script>
        function viewPartner(strPartnerID) {
            $("#<%=txtPartnerID.ClientID%>").val(strPartnerID);
            __doPostBack("<%= btnViewPartner.UniqueID %>", "OnClick");
        }
        function viewTranslation(strPartnerID) {
            $("#<%=txtPartnerID.ClientID%>").val(strPartnerID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }
    </script>
</asp:Content>
