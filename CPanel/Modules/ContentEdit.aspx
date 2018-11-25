<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContentEdit.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.ContentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Plugins/ckeditor/ckeditor.js"></script>
    <script src="../Plugins/CKFinderScripts/ckfinder.js"></script>
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
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
                <label class="control-label">Choose Category<span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpCategories" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Position</label>
                <asp:DropDownList ID="drpPosition" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label">Order</label>
                <asp:TextBox ID="txtOrder" CssClass="number form-control" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-6">
                <label class="control-label">Special Content Type</label>
                <asp:CheckBox runat="server" ID="cbSpecialContentType"></asp:CheckBox>
            </div>
        </div>
        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Representative Images</label>
                <asp:TextBox runat="server" ID="edtRepresentativeImage" CssClass="form-control"></asp:TextBox>

            </div>
            <div class="col-md-2">
                <button id="ckfinder-popup-1" type="button" class="control-label line_lb_css btn btn-warning">Chọn ảnh</button></div>
        </div>
        <div class="bg_100pecents_css">
            <div class="bg_100pecents_css">
                <label class="control-label line_lb_css">Intro Content</label>
            </div>
            <div class="bg_100pecents_css">
                <textarea name="edtIntroContent" id="edtIntroContent" rows="10" cols="80" runat="server">
                    
                   </textarea>
            </div>

        </div>

        <div class="bg_100pecents_css bg_css_1">
            <label class="control-label line_lb_css" for="edtFullContent">Full Content</label>
        </div>
        <div class="bg_100pecents_css bg_css_1">
            <div>
                <textarea name="edtFullContent" id="edtFullContent" rows="10" cols="80" runat="server">
                    
                </textarea>
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

        function ShowFileInfo(fileUrl, data) {
            alert('The selected file URL is "' + fileUrl + '"');

            var formatDate = function (date) {
                return date.substr(0, 4) + "-" + date.substr(4, 2) + "-" + date.substr(6, 2) + " " + date.substr(8, 2) + ":" + date.substr(10, 2);
            }

            alert('The selected file URL is: "' + data['fileUrl'] + '"');
            alert('The size of selected file is: "' + data['fileSize'] + 'KB"');
            alert('The selected file was last modifed on: "' + formatDate(data['fileDate']) + '"');
            alert('The data passed to the function is: "' + data['selectFunctionData'] + '"');
        }
        var xxxx = null;
        function selectFileWithCKFinder(elementId) {

            CKFinder.popup({
                chooseFiles: true,
                width: 800,
                height: 600,

                onInit: function (finder) {
                    finder.on('files:choose', function (evt) {
                        evt.data.files.forEach(function (file) {
                            // Send command to the server.
                            finder.request('command:send', {
                                name: 'ImageInfo',
                                folder: file.get('folder'),
                                params: { fileName: file.get('name') }
                            }).done(function (response) {
                                // Process server response.
                                if (response.error) {
                                    // Some error handling.
                                    return;
                                }

                                // Log image data:
                                console.log('-------------------');
                                console.log('Name:', file.get('name'));
                                console.log('URL:', file.getUrl());
                                console.log('Dimensions:', response.width + 'x' + response.height);
                                console.log('Size:', response.size + 'B');
                            });
                        });
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

