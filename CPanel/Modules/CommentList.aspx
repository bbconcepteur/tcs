<%@ Page Title="Thông tin liên hệ" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CommentList.aspx.cs" Inherits="CPanel.Modules.CommentList" %>

<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="main_contain_css main_contain_css_1">
       

        <div class="page-header"><h1 class="panel-title">Comment Management</h1></div>

        
        <div class="col-md-6 invisible_css">
            <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpLanguages" CssClass="form-control" AutoPostBack="true" runat="server" OnDataBinding="drpLanguage_DataBinding" OnSelectedIndexChanged="drpLanguage_SelectedIndexChanged"></asp:DropDownList>
        </div>                
                 
         <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnDelete" CssClass="btn btn-default" Text="Delete" OnClick="btnDelete_Click" runat="server" />
            <asp:Button ID="btnCreate" CssClass="btn btn-warning" OnClick="btnCreate_Click" Text="Create" runat="server" />
        </div>    
        <div class="page-header"><h1 class="panel-title">List of Comments</h1></div>
        
        

        <div class="panel-body">
            <dx:ASPxGridView ID="grvComments" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvComments_DataBinding" 
                    Settings-ShowGroupPanel="false" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />  

                <Columns>                    
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server"
                                ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                   
                    <dx:GridViewDataColumn FieldName="name" Settings-AutoFilterCondition="Contains" Caption="Department's Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <a href="javascript:viewComment('<%# Eval("id") %>')"><%# Eval("name") %></a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ext_tel" Settings-AutoFilterCondition="Contains" Caption="Tel" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("ext_tel")%></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                    
                    <dx:GridViewDataColumn FieldName="email" Settings-AutoFilterCondition="Contains" Caption="Email" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("email")%></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                    
                    
                     
                    <dx:GridViewDataColumn FieldName="department_manager" Settings-AutoFilterCondition="Contains" Caption="Manager's Name" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("department_manager") %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>   
                         
                     <dx:GridViewDataColumn FieldName="title_of_manager" Settings-AutoFilterCondition="Contains" Caption="Title of Manager" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("title_of_manager") %></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn> 
                    
                    <dx:GridViewDataColumn Width="35px" FieldName="order" Settings-AllowAutoFilter="False" Caption="Order" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="0">
                        <DataItemTemplate>                                                
                            <div class="intro_css"><%# Eval("order")%></div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>                     
                    
                    <dx:GridViewDataColumn FieldName="translation"  Settings-AllowAutoFilter="False" Caption="Translation" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                        <DataItemTemplate>
                             <a href="javascript:viewTranslation('<%# Eval("id") %>')"><div class="flag_css"></div></a>
                        </DataItemTemplate> 
                    </dx:GridViewDataColumn> 
                </Columns>
                <SettingsPager PageSize="20">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <Settings ShowFilterRow="True"/>                
            </dx:ASPxGridView>
            <asp:Button ID="btnViewComment" runat="server" OnClick="btnViewComment_Click" CssClass="invisible_css" />
           <%-- <asp:Button ID="btnViewTranslation" runat="server"  OnClick="btnViewTranslation_Click" CssClass="invisible_css"/>--%>
            <asp:TextBox ID="txtCommentID" runat="server" CssClass="invisible_css"></asp:TextBox>
        </div>        
    </div>
    <script>
        function viewComment(strCommentID) {
            $("#<%=txtCommentID.ClientID%>").val(strCommentID);
            __doPostBack("<%= btnViewComment.UniqueID %>", "OnClick");
        }
      <%--  function viewTranslation(strCommentID) {
            $("#<%=txtCommentID.ClientID%>").val(strCommentID);
            __doPostBack("<%= btnViewTranslation.UniqueID %>", "OnClick");
        }--%>
        
       
    </script>
</asp:Content>

