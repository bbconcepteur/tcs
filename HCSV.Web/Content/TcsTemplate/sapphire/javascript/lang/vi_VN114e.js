if(typeof(ss) == 'undefined' || typeof(ss.i18n) == 'undefined') {
	if(typeof(console) != 'undefined') console.error('Class ss.i18n not defined');
} else {
	ss.i18n.addDictionary('vi_VN', {
		'VALIDATOR.FIELDREQUIRED': 'Vui lòng điền "%s", trường này là bắt buộc.',
		'HASMANYFILEFIELD.UPLOADING': 'Đang tải lên... %s',
		'TABLEFIELD.DELETECONFIRMMESSAGE': 'Bạn có chắc muốn xóa dữ liệu này?',
		'LOADING': 'loading...',
		'UNIQUEFIELD.SUGGESTED': "Đồi giá trị sang '%s' : %s",
		'UNIQUEFIELD.ENTERNEWVALUE': 'Bạn cần nhập giá trị mời cho trường này',
		'UNIQUEFIELD.CANNOTLEAVEEMPTY': 'Trường này không thể để trống',
		'RESTRICTEDTEXTFIELD.CHARCANTBEUSED': "Ký tự '%s' không được dùng trong trường này",
		'UPDATEURL.CONFIRM': 'Would you like me to change the URL to:\n\n%s/\n\nClick Ok to change the URL, click Cancel to leave it as:\n\n%s',
		'FILEIFRAMEFIELD.DELETEFILE': 'Xóa file',
		'FILEIFRAMEFIELD.UNATTACHFILE': 'Dừng đính kèm file',
		'FILEIFRAMEFIELD.DELETEIMAGE': 'Xóa hình',
		'FILEIFRAMEFIELD.CONFIRMDELETE': 'Are you sure you want to delete this file?'
	});
}
