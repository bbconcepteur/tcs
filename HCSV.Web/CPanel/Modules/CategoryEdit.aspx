﻿<%@ Page Title="Edit Category" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="CPanel.Modules.CategoryEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">

    <div class="bg_100pecents_css bg_button_css">
        <asp:Button ID="btnBack" CssClass="btn btn-default" OnClick="btnBack_Click" Text="Back" runat="server" />
        <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />
        <asp:DropDownList ID="drpLanguages" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
    </div>

    <div class="page-header">
        <h1 class="panel-title">Categories Management</h1>
    </div>

    <div class="bg_100pecents_css">
        <div class="col-md-6">
            <label class="control-label line_lb_css">Choose Sections<span class="mandatory__css">(*)</span></label>            
            <asp:DropDownList ID="drpSections" CssClass="form-control" runat="server" OnDataBinding="drpSections_DataBinding"></asp:DropDownList>
        </div>                        
        <div class="col-md-6">
            <label class="control-label line_lb_css">Title<span class="mandatory__css">(*)</span></label>
            <asp:TextBox ID="txtTitle" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div>
    </div>

    


    <div class="bg_100pecents_css">
        <div class="col-md-6">
            <label class="control-label">Name<span class="mandatory__css">(*)</span></label>
            <asp:TextBox ID="txtName" CssClass="form-control remove_special_characters" runat="server"></asp:TextBox>
        </div>    
    </div>



    <asp:TextBox ID="txtCategoryID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
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

