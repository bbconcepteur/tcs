<%@ Page Title="Edit Comment" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CommentEdit.aspx.cs" Inherits="CPanel.Modules.CommentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>

        <div class="page-header">
            <h1 class="panel-title">Comment Management</h1>
        </div>

        
        <div class="bg_100pecents_css">
            <div class="col-md-12">
                <label class="control-label line_lb_css">News's Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtNewsName" CssClass="form-control" runat="server"></asp:TextBox>
            </div> 
        </div>
          <div class="bg_100pecents_css">
            <div class="col-md-12">
                <label class="control-label line_lb_css">Comment's Content </label>
                <asp:TextBox ID="txtCommentContent" CssClass="form-control" runat="server"></asp:TextBox>
            </div> 
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Sender Name </label>
                <asp:TextBox ID="txtSenderName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Sender Email</label>
                <asp:TextBox ID="txtSenderEmail" CssClass="email form-control email_css_100 " runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Sent Date </label>
                <asp:TextBox ID="txtSentDate" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <label class="control-label line_lb_css">Last Edited </label>
                <asp:TextBox ID="txtLastEdited" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
        </div> 
         <div class="bg_100pecents_css">
            <div class="col-md-12">
                <label class="control-label line_lb_css">Status</label>
                <asp:TextBox ID="txtStatus" CssClass="form-control" runat="server"></asp:TextBox>
            </div> 
        </div> 
        <asp:TextBox ID="txtCommentID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
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


