(function($) { 
	$('document').ready(function() {
		//$("ul.tabs").tabs("div.panes > div"); // setup ul.tabs to work as tabs for each div directly under div.panes
		//LANGUAGE
		ss.i18n.init();
		$('#'+ss.i18n.getLocale()+"_flag").hide();	
		// BANNER SLIDE SHOW
		$('.neoslideshow img:gt(0)').hide();
	    setInterval(function(){
	      	$('.neoslideshow :first-child').fadeOut()
			.next('img').fadeIn()
			.end().appendTo('.neoslideshow');},
	      	8000);
	    //$('.scrollable').jScrollPane({scrollbarWidth:8});
	    
		 // NAVIGATOR
		$('ul.sf-menu').superfish({
			delay:       100,                            // one second delay on mouseout
			animationOpen: {opacity:'.95',height: 'toggle'},			// fade-in and slide-down animation
			animationClose: {opacity:'hide'},			// fade-in and slide-down animation
			speed:       'slow',                        // faster animation speed
			autoArrows:  false,                         // disable generation of arrow mark-up
			dropShadows: false                          // disable drop shadows
		});
		/*
		$("#accordion").accordion({
			autoHeight: false,
			navigation: true,
			active: -1
		});
		*/
		// handle footer
		current_footer = '$data.Link';
		$('.footer ul li a[href='+current_footer+']').parent().attr({'class':'active'});
		// sepical for forums 
		if(current_footer.search('forums')>=0 || current_footer.search('ForumMemberProfile')>=0){
			$('#footer_forums_link').parent().attr({'class':'active'});
		}
		
		//SEARCH FORM
		$(".btn").click(function(){
			value=$(this).attr('value');
			form_id = $(this).attr('form');
			$('#'+form_id+'_submit').click();
			return false;
		});
		//alert('conco');
		if(!($.browser.msie && $.browser.version < 7)) {
			//alert('ie7');
			$("#agent_form").validate({
				//errorClass: "required",
				errorContainer: "#agent_errorBox",
				errorLabelContainer: "#agent_errorBox ul",
				wrapper: "li",
				focusCleanup: false,
				messages: {user:ss.i18n._t('VALIDATOR.Username_required','Tên đăng nhập là bắt buộc'), passwd: ss.i18n._t('VALIDATOR.Password_required','Mật khẩu là bắt buộc')},
				rules: {user:{required: true}, passwd:{required: true}}
			});
			$("#customer_form").validate({
				//errorClass: "required",
				errorContainer: "#customer_errorBox",
				errorLabelContainer: "#customer_errorBox ul",
				wrapper: "li",
				focusCleanup: false,
				messages: {awbFirst: {
										required: ss.i18n._t('VALIDATOR.AwbFirst_required','Số không vận đơn đầu là bắt buộc'),
										minlength: ss.i18n._t('VALIDATOR.AwbFirst_minlength','Số không vận đơn tối thiểu là 3 ký tự')
									 }
						   ,awbLast: {
							    required: ss.i18n._t('VALIDATOR.AwbLast_required','Số không vận đơn sau là bắt buộc'),
								minlength: ss.i18n._t('VALIDATOR.AwbLast_minlength','Số không vận đơn sau thiểu là 8 ký tự')
							}},
				rules: {awbFirst:{required: true ,number:true ,minlength:3}, awbLast:{required: true ,number:true ,minlength:8}}
			});
			$("#airline_form").validate({
				//errorClass: "required",
				errorContainer: "#airline_errorBox",
				errorLabelContainer: "#airline_errorBox ul",
				wrapper: "li",
				focusCleanup: false,
				messages: {user:ss.i18n._t('VALIDATOR.Username_required','Tên đăng nhập là bắt buộc'), passwd: ss.i18n._t('VALIDATOR.Password_required','Mật khẩu là bắt buộc')},
				rules: {user:{required: true}, passwd:{required: true}}
			});
		}
		$("#awbFirst").keyup(function(){
			l = $(this).val().length;
			if(l >= 3){
				$('#awbLast').focus();
			}
		});
		if($.browser.msie || $.browser.safari){
			$('form').keypress(function(event){
				if (event.keyCode == '13') {
					$(this).find('.btn').click();
				}
			});
		}
		// Trangdt add
		//$("#MemberLoginForm_LoginForm_Remember").attr({class:left});
		// trangdt end
	});
})(jQuery);