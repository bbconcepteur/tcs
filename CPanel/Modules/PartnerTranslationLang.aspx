<%@ Page Title="Partner Translation" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PartnerTranslationLang.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.PartnersLinkTranslationLang" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>
        <div class="page-header">
            <h1 class="panel-title">Partner Management</h1>
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
                <label class="control-label line_lb_css">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Representative Images</label>
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

                <label class="control-label line_lb_css">Representative<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtRepresentative"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Link<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtLink"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Address<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtAddress"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Phone<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtPhone"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Fax<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtFax"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Description</label>
                <dx:aspxhtmleditor id="edtDescription" runat="server">
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

            </div>
            
            <%--Tạo ngôn ngữ bên cạnh --%>

             <div class="col-md-6">
                <label class="control-label line_lb_css">Name<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtName2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Representative Images</label>
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

                <label class="control-label line_lb_css">Representative<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtRepresentative2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Link<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtLink2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Address<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtAddress2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Phone<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtPhone2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Fax<span class="mandatory__css">(*)</span></label>
                <asp:TextBox ID="txtFax2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Order</label>
                <asp:TextBox ID="txtOrder2"  CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>

                <label class="control-label line_lb_css">Description</label>
                <dx:aspxhtmleditor id="edtDescription2" runat="server">
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

            </div>

        </div>

        <asp:TextBox ID="txtPartnerID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtTranslation" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        <asp:TextBox ID="txtPartnerID2" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
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

