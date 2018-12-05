 //alert (window.location.search.substr(1).split("&"));
var page = window.location.search.substr(1).split("&");
//alert (page);
if (page != "") {
	var intHeight = "480px"; //default value
	for ( var i = 0; i < page.length; i++) {
		var tmparr = page[i].split("=");
		if (i==0) //i=0 ~ page
		{
			var strPage = tmparr[1];
			if (strPage.substring(0,7) == "Message") {//page for messages (SCR, SAL, SHL, ...)				
				document.getElementById("main_content").src="./pages/Message.html?type="+strPage.substring(7,10);			
			} else {
				document.getElementById("main_content").src="./pages/"+strPage+".html";				
			}

		} else if (i==1) //i=1 ~ height
		{
			intHeight = tmparr[1];
		}
	}
	document.getElementById("main_content").height=intHeight;
} else {
	document.getElementById("main_content").height = "1px";
}
