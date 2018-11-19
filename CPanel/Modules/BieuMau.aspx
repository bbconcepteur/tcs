<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BieuMau.aspx.cs" Inherits="QLNS.Modules.QuanLyCongViec.BieuMau" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="main_contain_css main_contain_css_1">


        <div class="col-xs-10">
            <label class="control-label title_css"><%=QLNS.Commons.TitleConst.getTitleConst("BIEU_MAU_DS") %></label>
        </div>

        <div class="control_css">                        
            <asp:Button ID="btnCreate" runat="server" CssClass="btn btn-warning" OnClick="btnCreate_Click">                
            </asp:Button>            
        </div>

    
    

        <dx:ASPxGridView ID="grvLib" Width="100%" runat="server" KeyFieldName="ID" OnDataBinding="grvLib_DataBinding" OnRowDeleting="grvLib_RowDeleting"  AutoGenerateColumns="False"
            EnableRowsCache="False" EnableCallBacks="false" SettingsEditing-Mode="Inline" SettingsPager-Mode="ShowAllRecords">
            
            <ClientSideEvents CustomButtonClick="function(s, e) {if (e.buttonID == 'btnDelete')	deleteConfirm_ASPxGridView_ByAnyFields(s, e, 'MA_BIEU_MAU');
                                                                    else if (e.buttonID == 'btnEdit1')	funEdit(s,e, 'ID');
                                                                    else if (e.buttonID == 'btnAdd') funAdd();}" />            
            
            
            <Columns>
                <dx:GridViewCommandColumn CellStyle-CssClass="bt_action__css" ButtonType="Image" VisibleIndex="6" Caption="THAO_TAC" HeaderStyle-HorizontalAlign="Center">
                    <NewButton Visible="false"></NewButton>
                    <EditButton Visible="false" Text="Sửa"></EditButton>
                    <DeleteButton Visible="false" Text="Xóa"></DeleteButton>                    

                    <UpdateButton Visible="false" Image-SpriteProperties-CssClass="bt_update_css" Text="Update">                        
                    </UpdateButton>
                    <CancelButton Visible="false" Image-SpriteProperties-CssClass="bt_cancel_css" Text="Cancel"></CancelButton>
                        
                        
                    <CustomButtons>                                                
                        <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_add_css" ID="btnAdd">
                        </dx:GridViewCommandColumnCustomButton>                                                
                    </CustomButtons>

                    <CustomButtons>                                                
                        <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_edit_css" ID="btnEdit1">
                        </dx:GridViewCommandColumnCustomButton>                                                
                    </CustomButtons>

                    <CustomButtons>                                                
                        <dx:GridViewCommandColumnCustomButton Image-SpriteProperties-CssClass="bt_delete_css" Text="Xóa" ID="btnDelete">
                        </dx:GridViewCommandColumnCustomButton>                                                
                    </CustomButtons>
                    
                    <ClearFilterButton Visible="True"></ClearFilterButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="MA_BIEU_MAU" Caption="BIEU_MAU_CODE" Settings-AllowSort="False" SortOrder="Ascending" CellStyle-CssClass="center_css" HeaderStyle-HorizontalAlign="Center" VisibleIndex="1" />
                <dx:GridViewDataTextColumn FieldName="MO_TA" Caption="MO_TA" Settings-AllowSort="False" HeaderStyle-HorizontalAlign="Center" VisibleIndex="2" />                    
            </Columns>
        </dx:ASPxGridView>
        
       
        <asp:TextBox ID="txtMenuID" runat="server" CssClass="invisible_css"></asp:TextBox>
        <asp:Button ID="btnEdit" runat="server" CssClass="invisible_css" OnClick="btnEdit_Click" />

    </div>

    <script>
        function funAdd () {
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");                        
        }

        function funEdit (s, e, strField) {
            s.GetRowValues(e.visibleIndex, strField, OnGetRow_Action_URL);
        }

        function OnGetRow_Action_URL(intID) {            
            $("#<%=txtMenuID.ClientID%>").val(intID);
            __doPostBack("<%= btnEdit.UniqueID %>", "OnClick");                        
        }

        //Set button title    
        $("#<%=btnCreate.ClientID%>").val('<%=QLNS.Commons.TitleConst.getTitleConst("BUTTON_CREATE")%>');        
    
    </script>
</asp:Content>
