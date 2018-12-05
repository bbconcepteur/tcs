var s, e;//s, e is 2 variables of <dx:ASPxGridView>

/**
* Display Error Message in 4s
* Written by: TuyenDV
*/
function havingError(strMessage) {
    swal({ title: strMessage, text: "This window will close after 4 seconds.", timer: 4000, showConfirmButton: false });
}

//display Message (in Change Password)
function UpdateSuccessfully(strMessage) {
    swal({
        title: "Change Password",
        text: strMessage,
        type: "warning",
        showCancelButton: false,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'OK',
        closeOnConfirm: false
    },
    function () {
        document.location.href = "/SignIn";
    });

}

/**
* Confirm of Deleting
* strMessage: Display Message
* s: get from <ClientSideEvents CustomButtonClick="function(s, e) ...>
* e: get from <ClientSideEvents CustomButtonClick="function(s, e) ...>
* strFields: string of Fields' name
* Written by: TuyenDV
*/
function deleteConfirm_ASPxGridView_ByAnyFields(s1, e1, strFields) {
    // Query the server for the fields from the focused row 
    // The values will be returned to the OnGetRow_Action_ByAnyFields_TuyenDV() function     
    s = s1;
    e = e1;
    s.GetRowValues(e.visibleIndex, strFields, OnGetRow_Action_ByAnyFields_TuyenDV);    

}

//Value array contains "CAP_1;CAP_2;CAP_3" and "MO_TA" field values returned from the server 
function OnGetRow_Action_ByAnyFields_TuyenDV(values) {
    swal({
        title: values,
        text: "Nếu xóa, bạn sẽ không thể khôi phục lại được",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Đồng ý',
        closeOnConfirm: false
    },
    function () {
        s.DeleteRow(e.visibleIndex);
    });
}


/**
* Confirm of Deleting
* strMessage: Display Message
* s: get from <ClientSideEvents CustomButtonClick="function(s, e) ...>
* e: get from <ClientSideEvents CustomButtonClick="function(s, e) ...>
* Written by: TuyenDV
*/
function deleteConfirm_ASPxGridView(s1, e1) {
    // Query the server for the "CAP_1;CAP_2;CAP_3;" and "MO_TA" fields from the focused row 
    // The values will be returned to the OnGetRow_Action_TuyenDV() function     
    s = s1;
    e = e1;
    s.GetRowValues(e.visibleIndex, 'CAP_1;CAP_2;CAP_3;MO_TA', OnGetRow_Action_TuyenDV);
    
}

//Value array contains "CAP_1;CAP_2;CAP_3" and "MO_TA" field values returned from the server 
function OnGetRow_Action_TuyenDV(values) {    
    swal({
        title: values[0] + values[1] + values[2] + " - " + values[3],
        text: "Nếu xóa, bạn sẽ không thể khôi phục lại được",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Đồng ý',
        closeOnConfirm: false
    },
    function () {
        s.DeleteRow(e.visibleIndex);
    });
}


function deleteConfirm_AndCallAction (strMessage) {
    swal({
        title: "<%=txtTKSO.Text + " - " + txtMoTa.Text%> ?",
        text: "Nếu xóa, bạn sẽ không thể khôi phục lại được",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Đồng ý',
        closeOnConfirm: false
    },
    function () {
        //swal("Đã xóa thành công!", "Thông tin đã được xóa khỏi hệ thống!", "success");                
        //window.document.getElementById('< %=BtDelete.ClientID%>').onclick();                
        __doPostBack("<%= BtDelete.UniqueID %>", "OnClick");
        //swal({ title: "Auto close alert!", text: "I will close in 2 seconds.", timer: 2000, showConfirmButton: false });
    });

}


/**** BEGIN: Format Currency ****/
var $demoValue = $('.currency_format_css');

$demoValue.bind('keydown', function (e) {
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)
        || (e.keyCode == 13) /*Enter*/
        || (e.keyCode == 8) /*Backspace*/
        || (e.keyCode == 46) /*Forward Backspace, Delete*/
        || (e.keyCode >= 37 && e.keyCode <= 40)/*Arrow Left: 37, up: 38, right: 39, down: 40*/
        || (e.keyCode == 116) /*F5*/
        || (e.keyCode == 9) /*Tab*/
        || (e.keyCode == 17) /*Ctrl*/
        ) {
    }
    else {
        e.preventDefault();
    }
});

//$demoValue.add($demoSymbol).bind('keydown keyup keypress focus blur paste change', function() {
$demoValue.bind('change', function () {
    var strValue = $(this).val();
    strValue = strValue.replace(',', '.');
    var symbol = "" /*Symbol = $, VNĐ*/,
        result = accounting.formatMoney(
            strValue,
            symbol,
            2,
            " ",
            ","
            /*ex: 435,345,345.00*/
        );
    $(this).val(result);
});
/**** END: Format Currency ****/

/**** BEGIN: Format Currency 2 ****/
var $demoValue_Input = $('.currency_format_css input');

$demoValue_Input.bind('keydown', function (e) {
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)
        || (e.keyCode == 13) /*Enter*/
        || (e.keyCode == 8) /*Backspace*/
        || (e.keyCode == 46) /*Forward Backspace, Delete*/
        || (e.keyCode >= 37 && e.keyCode <= 40)/*Arrow Left: 37, up: 38, right: 39, down: 40*/
        || (e.keyCode == 116) /*F5*/
        || (e.keyCode == 9) /*Tab*/
        || (e.keyCode == 17) /*Ctrl*/
        ) {
    }
    else {
        e.preventDefault();
    }
});

$demoValue_Input.bind('change', function () {
    var strValue = $(this).val();
    strValue = strValue.replace(',', '.');
    var symbol = "" /*Symbol = $, VNĐ*/,
        result = accounting.formatMoney(
            strValue,
            symbol,
            2,
            " ",
            ","
            /*ex: 435,345,345.00*/
        );
    $(this).val(result);
});
/**** END: Format Currency 2****/