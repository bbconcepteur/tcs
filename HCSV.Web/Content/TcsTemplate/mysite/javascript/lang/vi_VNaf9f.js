if(typeof(ss) == 'undefined' || typeof(ss.i18n) == 'undefined') {
	if(typeof(console) != 'undefined') console.error('Class ss.i18n not defined');
} else {
	ss.i18n.addDictionary('vi_VN', {
		'VALIDATOR.Username_required': 'Tên đăng nhập là bắt buộc.',
		'VALIDATOR.Password_required': 'Mật khẩu là bắt buộc.',
		'VALIDATOR.AwbFirst_required': 'Số không vận đơn đầu là bắt buộc',
		'VALIDATOR.AwbFirst_minlength': 'Số không vận đơn ít nhất là 3 ký tự',
		'VALIDATOR.AwbLast_required': 'Số không vận đơn sau là bắt buộc',
		'VALIDATOR.AwbLast_minlength': 'Sồ không vận đơn sau ít nhất là 11 ký tự'
	});
}
