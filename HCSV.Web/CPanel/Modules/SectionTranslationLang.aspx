<%@ Page Title="Section Translate" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SectionTranslationLang.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.SectionTranslationLang" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>
        <div class="page-header">
            <h1 class="panel-title">Section Management</h1>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-md-6">
                <label class="control-label line_lb_css">Choose Language<span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpLanguages2" CssClass="form-control" OnSelectedIndexChanged="drpLanguages2_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:DropDownList ID="drpLanguages"  AutoPostBack="true" runat="server" CssClass="invisible_css"></asp:DropDownList>
                
                 </div>

        </div>

        <div class="page-header">
            <h1 class="panel-title"></h1>
        </div>


        <div class="bg_100pecents_css" >
            <div class="col-md-6">
                <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitle"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>


                <label class="control-label line_lb_css">Name</label>
                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>

                

                <%--<label class="control-label line_lb_css">Representative Images</label>
                <dx:aspxhtmleditor id="edtRepresentativeImage" runat="server" clientinstancename="HtmlEditor">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>

                <label class="control-label line_lb_css">Intro Content</label>
                <dx:aspxhtmleditor id="edtIntroContent" runat="server">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif, .svg" MaxFileSize="500000">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>

                <label class="control-label line_lb_css">Full Content</label>
                <dx:aspxhtmleditor id="edtFullContent" runat="server">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif" MaxFileSize="500000">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>--%>

            </div>
            
            <%--Tạo ngôn ngữ bên cạnh --%>

             <div class="col-md-6">
                <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtTitle2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                
                <label class="control-label line_lb_css">Name</label>
                <asp:TextBox ID="txtName2" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <%--<label class="control-label line_lb_css">Representative Images</label>
                 <dx:aspxhtmleditor id="edtRepresentativeImage2" runat="server" clientinstancename="HtmlEditor">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>

                <label class="control-label line_lb_css">Intro Content</label>
                <dx:aspxhtmleditor id="edtIntroContent2" runat="server">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif" MaxFileSize="500000">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>

                <label class="control-label line_lb_css">Full Content</label>
                <dx:aspxhtmleditor id="edtFullContent2" runat="server">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif" MaxFileSize="500000">
                        </ValidationSettings>
                    </SettingsImageUpload>
                    <SettingsImageSelector enabled="True">
                        <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                
                        <uploadsettings enabled="True" nulltext="Select files...">
                        </uploadsettings>
                    </SettingsImageSelector>
                </dx:aspxhtmleditor>

            </div>--%>

        </div>

        <asp:TextBox ID="txtSectionID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtTranslation" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtSectionID2" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
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
    </script>
</asp:Content>

