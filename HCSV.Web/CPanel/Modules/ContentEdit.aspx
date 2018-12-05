<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContentEdit.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.ContentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="./../Plugins/ckeditor/ckeditor.js"></script>
    <script src="./../Plugins/CKFinderScripts/ckfinder.js"></script>
    <script src="./../Plugins/datepicker/datepicker.min.js"></script>
    <link rel="stylesheet" type="text/css" href="./../Plugins/datepicker/datepicker.min.css" />
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnPreview" CssClass="btn btn-info" OnClick="btnPreview_Click" Text="Preview" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>

        <div class="page-header">
            <h1 class="panel-title">Content Management</h1>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpLanguages" CssClass="form-control" OnSelectedIndexChanged="drpLanguages_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitle" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Category<span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpCategories" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Position</label>
                <asp:DropDownList ID="drpPosition" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder" CssClass="number form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Special Content Type</label>
                <asp:CheckBox runat="server" ID="cbSpecialContentType"></asp:CheckBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-2">
                <label class="control-label line_lb_css">Publish Time</label>
                <%--   <input type="text" id="PublishDateTime" name="PublishDateTime" placeholder="" class="form-control input-sm datepicker" autocomplete="off" />--%>
                <asp:TextBox runat="server" ID="datePublishDateTime" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                &nbsp;
            </div>
            <div class="col-md-3">
                <label class="control-label line_lb_css">Status</label>
                <asp:DropDownList ID="drpStatus" CssClass="form-control element_tab_css" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                &nbsp;
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Representative Images</label>
                <asp:TextBox runat="server" ID="edtRepresentativeImage" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label class="control-label line_lb_css">Chọn ảnh</label>
                <button id="ckfinder-popup-1" type="button" class="control-label line_lb_css btn btn-warning">Browse</button>
            </div>
            <div class="col-md-4">
                &nbsp;
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Intro Content</label>
            </div>
            <div class="bg_100pecents_css">
                <textarea name="edtIntroContent" id="edtIntroContent" rows="10" cols="80" runat="server">  </textarea>
            </div>
        </div>
        <div class="bg_100pecents_css bg_css_1">
            <label class="control-label line_lb_css" for="edtFullContent">Full Content</label>
        </div>
        <div class="bg_100pecents_css bg_css_1">
            <div>
                <textarea name="edtFullContent" id="edtFullContent" rows="10" cols="80" runat="server">  </textarea>
            </div>
        </div>
        <asp:TextBox ID="txtContentID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
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
                //havingError("Invalid content (The content has some special characters)");
                $(this).val(strTemp);
            }
        });

        CKFinder.setupCKEditor(CKEDITOR.replace('<%= edtIntroContent.ClientID %>'));
        CKFinder.setupCKEditor(CKEDITOR.replace('<%= edtFullContent.ClientID %>'));

        var button1 = document.getElementById('ckfinder-popup-1');
        button1.onclick = function () {
            selectFileWithCKFinder('<%= edtRepresentativeImage.ClientID %>');


        };
        function selectFileWithCKFinder(elementId) {

            CKFinder.popup({
                chooseFiles: true,
                width: 800,
                height: 600,

                onInit: function (finder) {
                    finder.on('files:choose', function (evt) {
                        var file = evt.data.files.first();
                        var output = document.getElementById(elementId);
                        output.value = file.getUrl();
                    });

                    finder.on('file:choose:resizedImage', function (evt) {
                        var output = document.getElementById(elementId);
                        output.value = evt.data.resizedUrl;
                    });

                }
            });
        }
    </script>
</asp:Content>

