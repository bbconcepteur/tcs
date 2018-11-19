<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentSelection_Popup.aspx.cs" Inherits="CPanel.Modules.ContentSelection_Popup" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>    
    <link href="/Templates/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Templates/css/default.css" type="text/css"/>
    <link rel="stylesheet" href="/Templates/css/template.css" type="text/css"/>
    <link rel="stylesheet" href="/Templates/css/typo.css" type="text/css"/>
    <link rel="stylesheet" href="/Templates/css/blue.css" type="text/css"/>

    
    <script type="text/javascript" src="/Templates/js/jquery.min.js"></script>    

    <script>
        function selectNode(strContentID, strContentTitle) {            
            window.parent.selectParentNode(strContentID, strContentTitle);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server"> 

        <div class="main_contain_css main_contain_css_1">
            <div class="bg_100pecents_css text-right">
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning" OnClick="btnSave_Click" ></asp:Button>            
            </div>

            <div class="page-header">
                <h1 class="panel-title">
                    <%=CPanel.Commons.TitleConst.getTitleConst("PAGE_SELECT_CONTENT") %>
                </h1>
            </div>
            
        
            

            <div class="bg_100pecents_css css_under_css">
                <div class="col-xs-6">
                    <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("SECTIONS") %></label>
                    <asp:DropDownList ID="drpSection" CssClass="form-control element_tab_css" OnSelectedIndexChanged="drpSection_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>

                <div class="col-xs-6">
                    <label class="control-label"><%=CPanel.Commons.TitleConst.getTitleConst("CATEGORIES") %></label>
                    <asp:DropDownList ID="drpCategory" CssClass="form-control element_tab_css" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </div>
            
            
            <div class="panel-body">
                <dx:ASPxGridView ID="grvUsers" Width="100%" runat="server" KeyFieldName="id" OnDataBinding="grvUsers_DataBinding" Settings-ShowFilterRow="false"
                        Settings-ShowFilterRowMenu="true" Settings-ShowGroupPanel="true" AutoGenerateColumns="False" ClientInstanceName="grvUsers">
                    <SettingsBehavior AllowSelectByRowClick="true" AllowFocusedRow="true" />                
                    <Columns>                    
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" 
                                    ClientSideEvents-CheckedChanged="function(s, e) { grvUsers.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>

                        
                        <dx:GridViewDataTextColumn FieldName="title" Caption="title" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                        
                                          
                    </Columns>
                    <SettingsPager PageSize="50">
                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                    </SettingsPager>
                </dx:ASPxGridView>
            </div>
            
    </form>
    
    
    <!--Set button title-->    
    <script>
        $("#<%=btnSave.ClientID%>").val('<%=CPanel.Commons.TitleConst.getTitleConst("BUTTON_SUBMIT")%>');        
    </script>      

</body>


</html>

