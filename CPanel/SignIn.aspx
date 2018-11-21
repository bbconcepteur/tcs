<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="CPanel.SignIn" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Content Management System</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="/Templates/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Templates/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Templates/css/default.css" type="text/css" />
    <script src="/Templates/js/jquery-1.10.2.min.js"></script>
    <script src="/Templates/js/bootstrap.min.js"></script>
    <link href="/Templates/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        // <![CDATA[
        function GetRefreshButton() {
            return document.getElementById("refreshButton");
        }
        function OnCaptchaBeginCallback(s, e) {
            var refreshButton = GetRefreshButton();
            refreshButton.src = "Templates/images/refreshButton.gif";
        } function OnCaptchaEndCallback(s, e) {
            var refreshButton = GetRefreshButton();
            refreshButton.src = "Templates/images/refreshButton.gif";
            document.getElementById("tbCode").value = "";
            if (typeof (lblCorrectCodeMessage) != "undefined")
                lblCorrectCodeMessage.SetVisible(false);
            if (typeof (lblIncorrectCodeMessage) != "undefined")
                lblIncorrectCodeMessage.SetVisible(false);
        }
        // ]]> 
    </script>
    <style type="text/css">
        .mainTable {
            background: url(Templates/images/bg.jpg) no-repeat;
            width: 224px;
            margin-left: auto;
            margin-right: auto;
        }

        .refreshButton {
            margin-left: 2px;
            margin-top: 2px;
            margin-bottom: 15px;
            border-style: none;
            cursor: pointer;
            position: relative;
            z-index: 2;
        }


        .captchaDiv {
            margin-top: -17px;
            margin-left: 9px;
            position: relative;
            z-index: 1;
        }

        .headerB {
            padding-bottom: 2px;
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
            color: #27a3b0;
        }

        .labelCell {
            padding-top: 20px;
            padding-left: initial;
            font-family: Tahoma;
            font-size: 9pt;
            color: #3B84C3;
        }

        .textBoxCell {
            padding-left: 12px;
            /*padding-top: 6px;*/
            padding-bottom: 14px;
        }

            .textBoxCell input {
                border: 1px;
                width: 200px;
                padding-bottom: 3px;
            }
    </style>


</head>
<body class="wrapper-login">

    <form id="form1" runat="server">

        <div class="panel panel-primary text-center login-form">
            <div class="panel-heading">
                <h3 class="panel-title">Sign in to your account</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="alert-danger">
                        <asp:Literal ID="lbNotice" runat="server"></asp:Literal>
                    </div>
                    <br />
                    <div class="input-group col-md-10 col-md-offset-1">
                        <div class="input-group-addon" style="width: 150px">User name</div>
                        <asp:TextBox ID="txtUsername" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <br />
                    <div class="input-group col-md-10 col-md-offset-1" style="display: none">
                        <span class="input-group-addon" style="width: 150px"></span>
                        <asp:DropDownList ID="cboDoanhNghiep" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="input-group col-md-10 col-md-offset-1">
                        <span class="input-group-addon" style="width: 150px">Password</span>
                        <asp:TextBox ID="txtPassword" ClientIDMode="Static" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <%--capcha--%>
                    <div class="input-group col-md-10 col-md-offset-1">
                        <script type="text/javascript" src="//www.google.com/recaptcha/api.js" async></script>
                        <div class="g-recaptcha" data-sitekey="6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI" data-theme="light" data-type="image">
                        </div>
                        <span class="field-validation-valid" data-valmsg-for="ReCaptcha" data-valmsg-replace="true"></span>
                        <%--<label class="headerB"></label>
                        <table class="mainTable">
                            <tr>
                                <td style="font-size: 0;">
                                    <img id="refreshButton" src="Templates/images/refreshButton.gif" alt="Show another code" title=""
                                        class="refreshButton" onclick="captcha.Refresh();" />
                                    <div class="captchaDiv">
                                        <dx:aspxcaptcha id="Captcha" runat="server" clientinstancename="captcha" codelength="6">
                                                        <ChallengeImage BackgroundColor="Transparent" ForegroundColor="#676767" BorderWidth="0" Height="72" />
                                                        <ValidationSettings EnableValidation="False">
                                                        </ValidationSettings>
                                                        <TextBox Visible="False" />
                                                        <RefreshButton Visible="False">
                                                        </RefreshButton>
                                                        <LoadingPanel Enabled="False" />
                                                        <ClientSideEvents BeginCallback="OnCaptchaBeginCallback" EndCallback="OnCaptchaEndCallback" />
                                                    </dx:aspxcaptcha>

                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="labelCell">
                                    <label for="tbCode">Type the code for Captcha</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="textBoxCell">
                                    <input type="text" id="tbCode" name="tbCode" autocomplete="off" />
                                </td>
                            </tr>
                        </table>--%>
                    </div>

                    <%--endcapcha--%>

                    <div class="input-group col-md-10 col-md-offset-1">
                        <span style="width: 150px; float: left">&nbsp;</span>
                        <div style="float: left; padding-top: 20px">
                            <asp:Button ID="btnSignIn" ClientIDMode="Static" CssClass="btn btn-success" Text="Log on" OnClick="btnSignIn_Click" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        $(function () {
            $('#btnSignIn').click(function () {
                if ($('#txtUsername').val() == '') {
                    alert('Chưa có tên đăng nhập');
                    $("#txtUsername").focus();
                    return false;
                }
                if ($('#txtPassword').val() == '') {
                    alert('Chưa có mật khẩu');
                    $("#txtPassword").focus();
                    return false;
                }
            });
        });
    </script>

</body>
</html>
