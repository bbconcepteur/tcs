<%@ Page Title="Edit Contact" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContactEdit.aspx.cs" Inherits="CPanel.Modules.ContactEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>

        <div class="page-header">
            <h1 class="panel-title">Contact Management</h1>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Language<span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpLanguages" CssClass="form-control" OnSelectedIndexChanged="drpLanguages_SelectedIndexChanged" Enabled="false" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Parent Item</label>
                <asp:DropDownList ID="drpIDParent" AutoPostBack="false" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Department's Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Tel / Ext<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtExt_Tel" CssClass="form-control" runat="server" ></asp:TextBox>
                
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Email<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtEmail" CssClass=" email form-control email_css_100 " runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder" CssClass="form-control number" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Manager's Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtDepManager" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Phone<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Hot Line<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtHotline" CssClass="form-control number element_tab_css" runat="server" ></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Title of Manager<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitleOfManager" CssClass="form-control " runat="server"></asp:TextBox>
            </div>
        </div>

       

        <asp:TextBox ID="txtContactID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>

    <script>
       
        /**Remove all of special characters*/
        $('.remove_special_characters').focusout(function () {
            var str = $(this).val();
            var strTemp = str.replace(/[^a-zA-Z 0-9]+/g, '');
            if (str != strTemp) {
                //havingError("Invalid content (The content has some special characters)");
                $(this).val(strTemp);
            }
        });

        $('.email_css_100').focusout(function () {
            var str = $(this).val();
            var strTemp = str.replace(/[< / \\ - >]+/g, '');
            if (str != strTemp) {
                //havingError("Invalid content (The content has some special characters)");
                $(this).val(strTemp);
            }
        });

        $(".element_tab_css").keypress(function (e) {
            var key = e.which || e.keyCode;
            if (key === 13) {
                var ele = document.forms[0].elements;

                //next focus
                for (var i = 0; i < ele.length; i++) {
                    //var q = (i == ele.length - 1) ? 0 : i + 1;// if last element : if any other 
                    if (this.tabIndex + 1 == ele[i].tabIndex) {
                        ele[i].focus(); break
                    }
                }
            }
        });
       
    </script>
</asp:Content>


