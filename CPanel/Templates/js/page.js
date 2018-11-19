function displayForCreateButtonFun() {
	document.getElementById('createButtonID').style.display="none";
	document.getElementById('saveButtonID').style.display="-webkit-inline-box";				
	document.getElementById('cancelButtonID').style.display="-webkit-inline-box";
	document.getElementById('deleteButtonID').style.display="none";	
}

function create() {
	document.getElementById('newRowID').style.display="table-row";
	displayForCreateButtonFun();						
}
function save() {				
	document.getElementById('newRowID').style.display="none";
	document.getElementById('createButtonID').style.display="-webkit-inline-box";
	document.getElementById('saveButtonID').style.display="none";				
	document.getElementById('cancelButtonID').style.display="none";
	document.getElementById('deleteButtonID').style.display="-webkit-inline-box";	
}			

function cancel() {
	document.getElementById('newRowID').style.display="none";
	document.getElementById('createButtonID').style.display="-webkit-inline-box";
	document.getElementById('saveButtonID').style.display="none";				
	document.getElementById('cancelButtonID').style.display="none";
	document.getElementById('deleteButtonID').style.display="-webkit-inline-box";	
}

//BEGIN: Action for Success Save
document.querySelector('#saveButtonID').onclick = function(){
	save();
	swal("Cập nhật thành công!", "Dữ liệu đã được lưu vào hệ thống.", "success");
	//refreshMainContent();
};
//END: Action for Success Save


//BEGIN: Action for Delete button
document.querySelector('#deleteButtonID').onclick = function(){
	swal({
		title: "Bạn có chắc chắn xóa không?",
		text: "Nếu xóa, bạn sẽ không thể khôi phục lại được",
		type: "warning",
		showCancelButton: true,
		confirmButtonColor: '#DD6B55',
		confirmButtonText: 'Đồng ý',
		closeOnConfirm: false
	},
	function(){
		swal("Đã xóa thành công!", "Thông tin đã được xóa khỏi hệ thống!", "success");
	});
};
//END: Action for Delete button


//BEGIN: Edit cell on the table by Jquery
$("#tableID tr").click ( function(){
	if ( !$(this).hasClass('clicked') )
	{
		$(this).children('td').each ( function() {
			var cellValue = $(this).text().trim();
			if (cellValue != "") {
				$(this).text('').append ( "<input type='text' class='text_css' value='" + cellValue + "' />" );
			}
			displayForCreateButtonFun();
		});

		$(this).addClass('clicked');
	}
});

$(".viewMessageButtonCSS").mouseout ( function(){
	$("#tableID tr").removeClass('clicked');			  
});

$(".viewMessageButtonCSS").click ( function(){
	$("#tableID tr").addClass('clicked');			  
});

$(".allowConfirmatioButtonCSS").mouseout ( function(){
	$("#tableID tr").removeClass('clicked');			  
});

$(".allowConfirmatioButtonCSS").click ( function(){
	$("#tableID tr").addClass('clicked');			  
});

//END: Edit cell on the table by Jquery

function refreshMainContent () {
	//parent.document.getElementById('main_content').contentWindow.location.reload(true);	
	window.document.location.reload(true);	
}
