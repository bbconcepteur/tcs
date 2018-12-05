<%@ Page Title="ChangePassword" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="CPanel.Modules.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main_contain_css main_contain_css_1 main_2">
        
        <div class="bg_100pecents_css bg_button_css">
            
        </div>

        <div class="page-header"><h1 class="panel-title">Change Password</h1></div>

        <div class="bg_100pecents_css" style="width:50%; margin-left:25%">
            
                <label class="control-label">Old Password</label>            
                <asp:TextBox ID="txtOldPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>    

        </div>  
        <div class="bg_100pecents_css" style="width:50%; margin-left:25%">
   
                <label class="control-label">New Password</label>            
                <asp:TextBox ID="txtNewPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>    
                  
        </div> 
        <div class="bg_100pecents_css" style="width:50%; margin-left:25%">
                <label class="control-label">Retype New Password</label>            
                <asp:TextBox ID="txtRetypeNewPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>    
        </div>    
        <div class="bg_100pecents_css" style="width:50%; margin-left:25%">
            <asp:Button ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" Text="Save" runat="server" />          
 
        </div>
        <asp:TextBox ID="txtContentID" CssClass="invisible_css" runat="server" Text=""></asp:TextBox>
        
    </div>
    
</asp:Content>
