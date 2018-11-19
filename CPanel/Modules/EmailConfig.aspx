<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EmailConfig.aspx.cs" Inherits="CPanel.Modules.Admin.EmailConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    

    <div class="main_contain_css main_contain_css_1">

        <div class="bg_100pecents_css bg_button_css">            
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" OnClick="btnSubmit_Click" Text="Submit"/>                         
        </div>

        <div class="page-header"><h1 class="panel-title">Account information</h1></div>


        
        <div style="padding-top: 20px;"><font style="color: red">
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
        </font></div>
              
        <div class="bg_100pecents_css">                        
            <div class="col-md-6">                    
                <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtFullName" CssClass="form-control remove_special_characters"  runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">                    
                <label class="control-label line_lb_css">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="email form-control email_css_100" runat="server"></asp:TextBox>
            </div>    
        </div>

        <div class="bg_100pecents_css">            
            <div class="col-md-6">
                <label class="control-label line_lb_css">Password<span class="mandatory__css">(*)</span></label>
                <asp:TextBox  TextMode="Password" ID="txtPassword_1" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
            </div>            
            <div class="col-md-6">                    
                <label class="control-label line_lb_css">Password (Retype)<span class="mandatory__css">(*)</span></label>
                <asp:TextBox  TextMode="Password" ID="txtPassword_2" CssClass="form-control remove_special_characters"  runat="server"></asp:TextBox>
            </div>
        </div>
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
    </script>
    
</asp:Content>

