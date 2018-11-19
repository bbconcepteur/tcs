<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="QuyenTruyCapMenus.aspx.cs" Inherits="CPanel.Modules.QuyenTruyCapMenus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">        
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("PAGE_PERMISSION_MENU") %>   </h1></div>
        
            <div class="bg_100pecents_css bg_button_css">            
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click"></asp:Button>            
            </div>
       
    <div class="col-md-6">
        <div class="col-md-6">
            <label class="control-label line_lb_css"><%=CPanel.Commons.TitleConst.getTitleConst("RULE") %><span class="mandatory__css">(*)</span></label>
            <asp:DropDownList ID="drpVaiTro" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpVaiTro_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>            
        </div>        
     </div>
        
        <div class="page-header"><h1 class="panel-title"><%=CPanel.Commons.TitleConst.getTitleConst("LIST_OF_MENU") %></h1></div>

        <div id="treeViewTKSO_DIV" runat="server">            
            <telerik:RadTreeView ID="radTreeView_Menus" Visible="false" accesskey="M" checkboxes="True"  Width="100%" runat="server"></telerik:RadTreeView>
        </div>
    </div>
    
    <!--Set button title-->
    <script>        
        $("#<%=btnSave.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');
    </script>    
</asp:Content>
