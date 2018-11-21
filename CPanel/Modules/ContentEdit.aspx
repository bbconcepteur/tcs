<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContentEdit.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.ContentEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Plugins/ckeditor/ckeditor.js"></script>
    <script src="../Plugins/ckfinder/ckfinder.js"></script>
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
        <strong>Selected File URL</strong><br/>
	<input id="xFilePath" name="FilePath" type="text" size="60" />
	<input type="button" value="Browse Server" onclick="BrowseServer( 'xFilePath' );" />
        <div class="bg_100pecents_css">
            <div class="col-md-6 editor_css">
                <div class="bg_100pecents_css">
                    <label class="control-label line_lb_css">Representative Images</label>
                </div>
                <div class="bg_100pecents_css">
                    <textarea name="edtRepresentativeImage" id="edtRepresentativeImage" rows="10" cols="80" runat="server">
                    
                    </textarea>
                </div>
            </div>

            <div class="col-md-6 editor_css">
                <div class="bg_100pecents_css">
                    <label class="control-label line_lb_css">Intro Content</label>
                </div>
                <div class="bg_100pecents_css">
                   <textarea name="edtIntroContent" id="edtIntroContent" rows="10" cols="80" runat="server">
                    
                   </textarea>
                </div>
                
                
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
        CKEDITOR.replace('<%= edtRepresentativeImage.ClientID %>');
        CKEDITOR.replace('<%= edtIntroContent.ClientID %>');
        CKEDITOR.replace('<%= edtFullContent.ClientID %>');

        function BrowseServer(inputId) {
            CKFinder.BasePath = '/Content/';
            CKFinder.SelectFunction = SetFileField;
            CKFinder.SelectFunctionData = inputId;
            CKFinder.popup();
        }

        function SetFileField(fileUrl, data) {
            document.getElementById(data["selectFunctionData"]).value = fileUrl;
        }
    </script>
</asp:Content>

