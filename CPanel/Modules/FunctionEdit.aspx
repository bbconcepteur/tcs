<%@ Page Title="Edit Function" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FunctionEdit.aspx.cs" Inherits="CPanel.Modules.FunctionEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="main_contain_css main_contain_css_1">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" OnClick="btnSubmit_Click" Text="Save" />
        </div>

        <div class="page-header">
            <h1 class="panel-title">Function information</h1>
        </div>

        <div style="padding-top: 20px;">
            <font style="color: red">
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
        </font>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-6">
                <label class="control-label">Parent</label>
                <asp:DropDownList ID="drpFunType" CssClass="form-control element_tab_css" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFunType_SelectedIndexChanged"></asp:DropDownList>
            </div>

        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Link<span class="mandatory__css">(*)</span></label>
                <asp:TextBox  ID="txtLink" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Published</label><br />
                <asp:CheckBox ID="ckPublish" BorderStyle="None" runat="server" />
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Ordering</label>
                <asp:TextBox ID="txtOrdering" CssClass=" form-control number" runat="server"></asp:TextBox>
            </div>
            
            <div class="col-md-6">
                <label class="control-label line_lb_css">Access</label><br />
                <asp:CheckBox ID="ckAccess" BorderStyle="None" runat="server" />
            </div>

        </div>
        <asp:TextBox ID="txtFunctionID" runat="server" CssClass="invisible_css"></asp:TextBox>

    </div>
</asp:Content>


