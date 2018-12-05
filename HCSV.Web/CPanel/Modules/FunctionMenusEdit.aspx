<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FunctionMenusEdit.aspx.cs" Inherits="CPanel.Modules.FunctionMenusEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">
        
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-warning" Text="Save" runat="server" />
        </div>

        <div class="page-header"><h1 class="panel-title">Content Management</h1></div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>            
                <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>    
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Link Name<span class="mandatory__css">(*)</span></label>            
                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">URL<span class="mandatory__css">(*)</span></label>            
                <asp:TextBox ID="txtLinkURL" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>            
                <asp:DropDownList ID="drpLanguages" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Order<span class="mandatory__css"></span></label>            
                <asp:TextBox ID="txtOrder" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-6">
                &nbsp;
            </div>
        </div>
        
        
        
        <asp:TextBox ID="txtFunctionMenuID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>
</asp:Content>
