<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"  ValidateRequest="false" CodeBehind="ContactTranslationLang.aspx.cs" Inherits="CPanel.Modules.ContactTranslationLang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:content id="Content1" contentplaceholderid="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>
        <div class="page-header">
            <h1 class="panel-title">Contact Translation Management</h1>
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


        <div class="col-md-6" style="border-left-color:#6b6060">
             <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">ID_parent</label>
                <asp:DropDownList ID="drpIDParent" ReadOnly="true" AutoPostBack="false" CssClass="form-control" runat="server"></asp:DropDownList>
           </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName" ReadOnly="true" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Ext_Tel<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtExt_Tel" ReadOnly="true" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Email<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtEmail" ReadOnly="true" CssClass=" email form-control remove_special_characters email_css_100" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder" ReadOnly="true" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">DepartmentManager<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtDepManager"  ReadOnly="true"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Phone<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtPhone" ReadOnly="true"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">HotLine<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtHotline"  ReadOnly="true" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
           </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Title_Of_Manager<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitleOfManager"  ReadOnly="true" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
        </div>
        <%--Tạo ngôn ngữ bên cạnh --%>
        <div class="col-md-6">
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">ID_parent</label>
                <asp:DropDownList ID="drpIDParent2" AutoPostBack="false" CssClass="form-control" runat="server"></asp:DropDownList>
           </div>
            <div class="bg_100pecents_css">

                <label class="control-label line_lb_css">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName2" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Ext_Tel<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtExt_Tel2" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Email<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtEmail2" CssClass=" email form-control  email_css_100" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">DepartmentManager<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtDepManager2" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Phone<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtPhone2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">HotLine<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtHotline2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
           </div>
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Title_Of_Manager<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitleOfManager2" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
        </div>

        <asp:TextBox ID="txtContactID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtTranslation" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtContactID2" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>

    <script>
        /**Remove all of special characters*/
        /*$('#< %=btnSave.ClientID%>').click(function () {
            __doPostBack("< %= btnSave.UniqueID %>", "OnClick"); 
        });*/

        $('.remove_special_characters').focusout(function () {
            var str = $(this).val();
            var strTemp = str.replace(/[^a-zA-Z 0-9]+/g, '');
            if (str != strTemp) {
                //havingError("Invalid Category (The Category has some special characters)");
                $(this).val(strTemp);
            }
        });
    </script>
</asp:content>

