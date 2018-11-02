(function ($) {
    $('document').ready(function () {
        if ($.browser.msie && $.browser.version < 7) {
            var filter_tabs = $("#search_accordion div.pane");
            $.each($("#search_accordion div.pane"), function () {

                var pane = $(this);
                pane.prev('h2').click(function () {
                    $('#search_accordion h2').removeClass('current');
                    $(this).addClass('current');
                    $("#search_accordion div.pane").hide();
                    pane.show();
                    //tab_order[] = $(this);
                });
            });
            //$(filter_tabs[selected_tab]).click();
            $(filter_tabs[selected_tab]).prev('h2').click();
        } else {
            $("#search_accordion").tabs("#search_accordion div.pane", { tabs: 'h2', effect: 'slide', initialIndex: selected_tab });
        }
    });
})(jQuery);