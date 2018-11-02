(function($) { 
$('document').ready(function(){
	//alert(ss.i18n.getLocale());
	//ss.i18n.init();
	
	$("#service_accordion").accordion({
		autoHeight: false,
		navigation: true,
		collapsible: true,
		active: 100
	});
	/*
	$("#service_accordion").tabs("#service_accordion div.pane", {tabs: 'h3', effect: 'slide', initialIndex:0});
	*/
	
	
});
})(jQuery);