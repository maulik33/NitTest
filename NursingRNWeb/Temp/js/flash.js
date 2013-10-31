
function updateInteractionState(interactionStateXML)
{

	//var oFlashInteractionXML = document.getElementById("flashInteractionXML");
	//var oFlashInteractionXML.value = escape(interactionStateXML) ;
	var test =interactionStateXML;
	alert(test);

	//var oRequireResponse = document.getElementById("requireResponse");
	//var requireResponse = false ;
	//if( oRequireResponse != null && oRequireResponse.value == "true" )
		//requireResponse = true ;
	
	//if( requireResponse)
	//{
	//	showControl('nextPosition',true);
	//}
    document.getElementById("requireResponse").innerText=test;




}


