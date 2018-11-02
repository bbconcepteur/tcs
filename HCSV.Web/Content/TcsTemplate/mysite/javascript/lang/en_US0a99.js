if(typeof(ss) == 'undefined' || typeof(ss.i18n) == 'undefined') {
	if(typeof(console) != 'undefined') console.error('Class ss.i18n not defined');
} else {
	ss.i18n.addDictionary('en_US', {
		'VALIDATOR.Username_required': 'Username is required.',
		'VALIDATOR.Password_required': 'Password is required.',
		'VALIDATOR.AwbFirst_required': 'AwbFirst is required',
		'VALIDATOR.AwbFirst_minlength': 'Please enter at least {0} characters',
		'VALIDATOR.AwbLast_required': 'AwbLast is required',
		'VALIDATOR.AwbLast_minlength': 'Please enter at least {0} characters'
	});
}
