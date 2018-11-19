/********************************************************BEGIN: AJAX**************************************************/
function ajaxRes(url, divHTML){
	var ajaxRequest;
	try{
	  // Opera 8.0+, Firefox, Safari
	  ajaxRequest = new XMLHttpRequest();
	} catch (e){
	  // Internet Explorer Browsers
	  try{
		 ajaxRequest = new ActiveXObject("Msxml2.XMLHTTP");
	  } catch (e) {
		 try{
			ajaxRequest = new ActiveXObject("Microsoft.XMLHTTP");
		 } catch (e){
			// Something went wrong
			alert("Incompatible Browser");
			var ajaxDisplay = document.getElementById('ajaxDiv');
			statusDisplay.innerHTML = "Incompatible Browser";
			return false;
		 }
	  }
   }

   /// Create a function that will receive data sent from the server
   ajaxRequest.onreadystatechange = function(){
	   if(ajaxRequest.readyState == 4 && ajaxRequest.status==200){		   
			// For some reason, the returned text has a bunch of carrage returns which messes things up unless removed with trim.
			 var ajaxResponse = trim(ajaxRequest.responseText);
			 if(ajaxResponse=='') {
				if (typeof document.adminForm.onsubmit == "function") {
				   document.adminForm.onsubmit();
				}
				document.adminForm.submit();
			 } else {
				document.getElementById(divHTML).innerHTML = ajaxResponse;
			 }
		  }
   }
	//url = "index.php?option=com_quang&task=SearchUserType&userTypeName="+document.getElementById('userType').value;
	//alert (url);   
	ajaxRequest.open("GET", url, true);
	ajaxRequest.send(null);	
}

// **************************************************************
function trim(str) {
	str = str.replace(/^\s+/, '');
	for (var i = str.length - 1; i > 0; i--) {
	  if (/\S/.test(str.charAt(i))) {
		 str = str.substring(0, i + 1);
		 break;
	  }
	}
	return str;
}
/********************************************************END: AJAX**************************************************/
