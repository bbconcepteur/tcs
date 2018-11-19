<%@ Page Title="Edit Language" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="LanguageEdit.aspx.cs" Inherits="CPanel.Modules.LanguageEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

    <div class="bg_100pecents_css bg_button_css">
        <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
        <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
    </div>

    <div class="page-header">
        <h1 class="panel-title">Language Management</h1>
    </div>

    <div class="bg_100pecents_css">                       
        <div class="col-md-6">
            <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
            <asp:TextBox ID="txtTitle" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6">
            <label class="control-label line_lb_css">Lang_code<span class="mandatory__css">(*)</span></label>
            <asp:TextBox ID="txtLang_code" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="bg_100pecents_css">
        <div class="col-md-6">
            <label class="control-label">Description<span class="mandatory__css"></span></label>
            <asp:TextBox ID="txtDescription" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div> 
        <div class="col-md-6">
            <label class="control-label">Sef<span class="mandatory__css">(*)</span></label>
            <asp:TextBox ID="txtSef" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div> 
    </div>
    <div class="bg_100pecents_css">
        <div class="col-md-6">
            <label class="control-label">Published<span class="mandatory__css">(*)</span></label>            
            <asp:CheckBox ID="cbPublished" CssClass="form-control remove_special_characters" runat="server"></asp:CheckBox>
        </div> 
        <div class="col-md-6">
            <label class="control-label">Default_status<span class="mandatory__css"></span></label>
            <asp:TextBox ID="txtDefault_status" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div> 
    </div>
    <div class="bg_100pecents_css">
        <div class="col-md-6 editor_css">
                <label class="control-label line_lb_css">Images<span class="mandatory__css">(*)</span></label>            
                <dx:ASPxHtmlEditor ID="edtImages" runat="server" ClientInstanceName="HtmlEditor">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:ASPxHtmlEditor>
            </div>
    </div>



    <asp:TextBox ID="txtLanguageID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>
</asp:Content>

