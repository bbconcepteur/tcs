<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FunctionRight.aspx.cs" Inherits="CPanel.Modules.FunctionRight" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
        <div class="title_css">
            kiểm tra quyền truy cập các Module chức năng)
        </div>

        <div class="col-md-12">
            <div class="col-md-10">
            </div>

            <div class="col-md-2">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click"></asp:Button>
            </div>
        </div>

        <div class="col-md-12">

            <div class="col-md-5">
                <label class="control-label line_lb_css"><%=CPanel.Commons.TitleConst.getTitleConst("VAI_TRO") %><span class="mandatory__css">(*)</span></label>
                <asp:DropDownList ID="drpVaiTro" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpVaiTro_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="page-header">
            <h1 class="panel-title"></h1>
        </div>

        <div id="treeViewTKSO_DIV" runat="server">
            <h3 class="title_css">Danh sách functions</h3>
            <telerik:RadTreeView ID="radTreeView_Menus" Visible="false" AccessKey="M" CheckBoxes="True" Width="100%" runat="server"></telerik:RadTreeView>
        </div>
    </div>

    <!--Set button title-->
    <script>
        $("#<%=btnSave.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');
    </script>
</asp:Content>
