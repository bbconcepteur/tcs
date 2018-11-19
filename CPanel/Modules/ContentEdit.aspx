<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ContentEdit.aspx.cs" ValidateRequest="false" Inherits="CPanel.Modules.ContentEdit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">
        
        <div class="bg_100pecents_css bg_button_css">
            <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        </div>

        <div class="page-header"><h1 class="panel-title">Content Management</h1></div>

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
            <div class="col-md-6 editor_css">
                <label class="control-label line_lb_css">Representative Images</label>            
                <dx:ASPxHtmlEditor ID="edtRepresentativeImage" runat="server" ClientInstanceName="HtmlEditor">
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

            <div class="col-md-6 editor_css">
                <label class="control-label line_lb_css">Intro Content</label>    
                <dx:ASPxHtmlEditor ID="edtIntroContent" runat="server">
                    <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                        <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif" MaxFileSize="500000">
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

        <div class="bg_100pecents_css bg_css_1">
            <label class="control-label line_lb_css">Full Content</label>    
            <dx:ASPxHtmlEditor ID="edtFullContent" runat="server">
                <SettingsImageUpload UploadImageFolder="~\Upload\Images\">
                    <ValidationSettings AllowedFileExtensions=".jpg, .png, .gif" MaxFileSize="500000">
                    </ValidationSettings>
                </SettingsImageUpload>
                <SettingsImageSelector enabled="True">
                    <CommonSettings EnableMultiSelect="True" RootFolder="~/Upload/Images" ThumbnailFolder="~/Upload/Images_thumb/" />
                
                    <uploadsettings enabled="True" nulltext="Select files...">
                    </uploadsettings>
                </SettingsImageSelector>
            </dx:ASPxHtmlEditor>
        </div>
        
        <asp:TextBox ID="txtContentID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
    </div>
      
    <script>        
        /**Remove all of special characters*/
        /*$('#< %=btnSave.ClientID%>').click(function () {
            __doPostBack("< %= btnSave.UniqueID %>", "OnClick"); 
        });*/

        $('.remove_special_characters').focusout (function () {
            var str = $(this).val();
            var strTemp = str.replace(/[^a-zA-Z 0-9]+/g, '');
            if (str != strTemp) {
                //havingError("Invalid content (The content has some special characters)");
                $(this).val(strTemp);
            }            
        });
    </script>
</asp:Content>

