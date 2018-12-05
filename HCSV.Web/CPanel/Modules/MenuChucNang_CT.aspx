<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MenuChucNang_CT.aspx.cs" Inherits="CPanel.Modules.QuanTriHeThong.MenuChucNang_CT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">        
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_DETAILS") %></h1></div>        

        <div class="control_css">            
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click"></asp:Button>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click">                
            </asp:Button>            
        </div>

        <div class="bg_100pecents_css">
            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("TITLE") %></label>
                <asp:TextBox ID="txtTieude" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
            </div>

            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_PARENT") %></label>
                <asp:DropDownList ID="drpMenus" AutoPostBack="false" CssClass="form-control element_tab_css" runat ="server"></asp:DropDownList>
            </div>
        </div>
        
        <div class="bg_100pecents_css">
            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_TYPE") %></label>
                <asp:DropDownList ID="drpMenuType" CssClass="form-control element_tab_css" runat="server"></asp:DropDownList>
            </div>

            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_STT") %></label>
                <asp:TextBox ID="txtSTT" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="bg_100pecents_css">
            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("TYPE_OF_PAGE") %></label>
                <asp:DropDownList ID="drpTypeOfPage" OnSelectedIndexChanged="drpTypeOfPage_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control element_tab_css" runat="server"></asp:DropDownList>
            </div>


            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("MENU_URL") %></label>
                <asp:TextBox ID="txtURL" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
            </div>
        </div>

        <div id="category_id_css" runat="server" class="bg_100pecents_css">
            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("SECTIONS") %></label>
                <asp:DropDownList ID="drpSection" CssClass="form-control element_tab_css" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>

            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("CATEGORIES") %></label>
                <asp:DropDownList ID="drpCategory" CssClass="form-control element_tab_css" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div id="content_id_css" runat="server" class="bg_100pecents_css">
            <asp:TextBox ID="txtContentID" CssClass="invisible_css" runat="server"></asp:TextBox>

            <div class="col-xs-6">
                <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("CONTENTS") %></label>
                <asp:TextBox ID="txtContentTitle" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
                <asp:Button ID="btnShow"  runat="server" Text="Show Modal Popup" CssClass="bt_search_icon_css" />        
            </div>            
        </div>

        <div class="col-xs-6">
            <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("PUBLISHED") %></label>
            <asp:CheckBox ID="cbPublishedStatus" runat="server" />
        </div>

        <div class="col-md-6 editor_css">
            <label class="control-label line_lb_css">Intro Image</label>    
            <dx:ASPxHtmlEditor ID="edtImages" runat="server">
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

        
        <asp:TextBox ID="txtMenuID" runat="server" CssClass="invisible_css"></asp:TextBox>        
    </div> 
    
    
    <!-- ModalPopupExtender -->
        <cc1:ModalPopupExtender ID="mp1" BehaviorID="popUpBehaviorID" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
            <asp:Button ID="btnClose" CssClass="popUpClose_css" runat="server" Text="Close" />
            <iframe style=" width: 550px; height: 400px;" src="/Modules/ContentSelection_Popup" runat="server"></iframe>            
        </asp:Panel>
    <!-- ModalPopupExtender -->
    
    <!--Set button title-->
    <script>
        $("#<%=btnCancel.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_CANCEL")%>');
        $("#<%=btnSave.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');

        function selectParentNode(strContentID, strContentTitle) {
            $find('popUpBehaviorID').hide();
            $("#<%=txtContentID.ClientID%>").val(strContentID);
            $("#<%=txtContentTitle.ClientID%>").val(strContentTitle);
        }
    </script>    

</asp:Content>
