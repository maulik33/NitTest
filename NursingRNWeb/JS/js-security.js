/***************************************************************
  Global Variables
***************************************************************/
var isnn;
var isie;
var message;

/***************************************************************
  Initialization
***************************************************************/
if(navigator.appName=='Microsoft Internet Explorer'){
	isie = true;
}

if(navigator.appName=='Netscape'){
    isnn = true;
}

if (document.layers){
	window.captureEvents(Event.KEYPRESS);
	window.captureEvents(Event.MOUSEDOWN);
	document.onmousedown = clickNS;
    
} else {
	//document.onmouseup = clickNS;
	document.oncontextmenu = clickIE;
}

document.onkeyup = key;
document.onkeypress = key;
document.oncontextmenu = new Function("return false")
document.onselectstart = new Function("return false")

if (window.sidebar){
	document.onmousedown=disableselect
	document.onclick=reEnable
}

window.onbeforeprint=_np1;
window.onafterprint=_np2;

/***************************************************************
  Global Functions
***************************************************************/
function clickIE(){
	if(document.all){
	    (message);
	    return false;
    }
}

function clickNS(e){
	if(document.layers||(document.getElementById&&!document.all)){
		if (e.which==1||e.which==2||e.which==3){
			(message);
			return false;
        }
    }
}

function key(e){
	if(isie){
        if(event.keyCode==17 || event.keyCode==18 || event.keyCode==93) {
			return false;
		}
	}
	if(isnn){
		if(e.keyCode==17 || e.keyCode==18 || e.keyCode==93) {
			return false;
		}
	}
}

function disableselect(e){
	return false
}

function reEnable(){
	return true
}

function lastChMatch(s)
{
	if(s.lastIndexOf("_jasp") != -1 )
		return true ;
	else
		return false ;
}

function rvtbk(s)
{
	if(s.lastIndexOf("_jasp") != -1 && (s.length - s.lastIndexOf("_jasp"))==5)
		s = s.substring(0, s.length -5 );
	return s ;
}

function _np1(){
	for(wi=0;wi<document.all.length;wi++){
		if(document.all[wi].style.visibility!="hidden"){
			document.all[wi].style.visibility="hidden";
			document.all[wi].id = document.all[wi].id+"_jasp" ;
		}
	}
};

function _np2(){
	for (wi=0;wi<document.all.length;wi++){
		if(lastChMatch(document.all[wi].id))
			document.all[wi].style.visibility=""
			document.all[wi].id = rvtbk(document.all[wi].id);
	}
};