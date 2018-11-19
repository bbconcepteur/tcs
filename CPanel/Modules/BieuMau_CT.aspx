<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BieuMau_CT.aspx.cs" Inherits="QLNS.Modules.QuanTriHeThong.BieuMau_CT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1">
        <div class="title_css"><%=QLNS.Commons.TitleConst.getTitleConst("BIEU_MAU_CHI_TIET") %></div>
        
        <div class="control_css">            
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-default" OnClick="btnCancel_Click"></asp:Button>
            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-success" OnClick="btnExport_Click"></asp:Button>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click">                
            </asp:Button>            
        </div>
            
        <div class="col-xs-6 col-space-right-css">
            <label class="control-label"><%=QLNS.Commons.TitleConst.getTitleConst("BIEU_MAU_CODE") %></label>
            <asp:TextBox ID="txtMaBieuMau" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
        </div>

        <div class="col-xs-6 col-space-right-css">
            <label class="control-label"><%=QLNS.Commons.TitleConst.getTitleConst("MO_TA") %></label>
            <asp:TextBox ID="txtMoTa" CssClass="form-control element_tab_css" runat="server"></asp:TextBox>
        </div>
        <div class="col-xs-6 col-space-right-css">
            &nbsp;
        </div>

        <div class="col-xs-6 col-space-right-css">
            <label class="control-label"><%=QLNS.Commons.TitleConst.getTitleConst("LOAI_QD_DS") %></label>
            <asp:DropDownList ID="drpLoaiQD" CssClass="form-control element_tab_css" runat="server"></asp:DropDownList>            
        </div>

        <div class="col-xs-6 col-space-right-css">
            <label class="control-label"><%=QLNS.Commons.TitleConst.getTitleConst("NOI_DUNG") %></label>
            <dx:ASPxHtmlEditor ID="edtNoiDung" runat="server" ClientInstanceName="HtmlEditor">                
            </dx:ASPxHtmlEditor>
        </div>

        <asp:TextBox ID="txtBieuMauID" runat="server" CssClass="invisible_css"></asp:TextBox>
    </div> 
    
    <!--Set button title-->
    <script>
        $("#<%=btnCancel.ClientID%>").val('<%=QLNS.Commons.TitleConst.getTitleConst("BUTTON_CANCEL")%>');
        $("#<%=btnSave.ClientID%>").val('<%=QLNS.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');
        $("#<%=btnExport.ClientID%>").val('<%=QLNS.Commons.TitleConst.getTitleConst("BUTTON_EXPORT")%>');
    </script>    

</asp:Content>
