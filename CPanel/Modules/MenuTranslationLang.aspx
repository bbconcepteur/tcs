<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="MenuTranslationLang.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.MenuTranslationLang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>
        <div class="page-header">
            <h1 class="panel-title">Category Management</h1>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Language for Translation <span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpLanguages2" CssClass="form-control" OnSelectedIndexChanged="drpLanguages2_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="drpLanguages" AutoPostBack="true" runat="server" CssClass="invisible_css"></asp:DropDownList>

            </div>

        </div>

        <div class="page-header">
            <h1 class="panel-title"></h1>
        </div>


        <div class="col-md-6">
            
            <div class="bg_100pecents_css">
                <label class="control-label">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
        </div>
        <%--Tạo ngôn ngữ bên cạnh --%>
        <div class="col-md-6">
            
            <div class="bg_100pecents_css">
                <label class="control-label">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>

        </div>

        <asp:TextBox ID="txtMenuID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtTranslation" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtMenuID2" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>

    <script>
        /**Remove all of special characters*/
        /*$('#< %=btnSave.ClientID%>').click(function () {
            __doPostBack("< %= btnSave.UniqueID %>", "OnClick"); 
        });*/

        /*$('.remove_special_characters').focusout(function () {
            var str = $(this).val();
            //var strTemp = str.replace(/[^a-zA-Z 0-9]+/g, '');
            var strTemp = "";
            if (str != strTemp) {
                //havingError("Invalid Category (The Category has some special characters)");
                $(this).val(strTemp);
            }
        });*/
    </script>
</asp:Content>

