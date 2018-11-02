(function($) { 
$('document').ready(function(){
	//alert(ss.i18n.getLocale());
	//ss.i18n.init();
	$("#accordion").accordion({
		autoHeight: false,
		navigation: true,
		collapsible: true,
		active: 10
	});
	/*
	$("#accordion div").hide();
	$("#accordion").tabs("#accordion div", {tabs: 'h3', effect: 'slide', initialIndex:-1});
	*/
	$('area').click(function(){
		step = $(this).attr('alt');
		$(step).click();
		return false;
	})
	$("a[rel^='pPhoto']").prettyPhoto({
		allow_resize: false,
		social_tools:''
	});
	
});
})(jQuery);