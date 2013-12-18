var benn = "toff";
var ny="toff"

function click(e) {
if (document.all) {
if (event.button==2||event.button==3) {
oncontextmenu='return false';
setInterval("window.status='your message here'",10);
}
}
if (document.layers) {
if (e.which == 3) {
oncontextmenu='return false';
setInterval("window.status='your message here'",10);
}
}
}
if (document.layers) {
document.captureEvents(Event.MOUSEDOWN);
}
document.onmousedown=click;
// --> 

//disable function code


keys = new Array();
keys["f112"] = 'f1';
keys["f113"] = 'f2';
keys["f114"] = 'f3';
keys["f115"] = 'f4';
keys["f116"] = 'f5';
keys["f117"] = 'f6';
keys["f118"] = 'f7';
keys["f119"] = 'f8';
keys["f120"] = 'f9';
keys["f121"] = 'f10';
keys["f122"] = 'f11';
keys["f123"] = 'f12';

saveCode=""
function myFunc(code) {
  //alert('testing '+code)
}

document.onkeydown = function(){
 // Capture and remap F-key
  if(window.event && keys["f"+window.event.keyCode])  {
    saveCode=window.event.keyCode;
    window.event.keyCode = 505;
  }
  if(window.event && window.event.keyCode == 505) { 
    // New action for keycode
    myFunc(saveCode)
    return false; // Must return false or the browser will execute old code
     }
}
 


function copy() {

if (benn == "ton") {
  ny="ton" 
  document.execCommand('copy','',null);
    }
benn = "toff"
}
function cut() {
if (benn == "ton") {
   	document.execCommand('cut','',null);
    }
benn = "toff"
wordcount(document.typeArea.typeHolder.value);
}



function paste() {
	if (ny=="ton")
	{
		document.typeArea.typeHolder.focus();
		document.execCommand('paste','',null);
		wordcount(document.typeArea.typeHolder.value);
	}
  ny = "toff"
}




function copy1(){ 
	var val = document.getElementById('typeHolder').value; 

	if (IE4) text = document.selection.createRange().text; 
	else text = document.getSelection().createRange().text; 

	clipboardData.setData('TEXT' , text); 
} 

function paste1(dest) { 
	if (clipboardData.getData('TEXT')==null)
	{
		clipboardData.setData('TEXT' , "");
	}

	insKarrot(dest, clipboardData.getData('TEXT'));
	wordcount(document.typeArea.typeHolder.value);
} 

function cut1(){ 
	copy(); 
	var target = document.getElementById('typeHolder'); 
	target.value = target.value.replace(clipboardData.getData('TEXT'), ''); 
	
	wordcount(document.typeArea.typeHolder.value);
} 

function storeCaret (obj) { 
	if (obj.createTextRange) obj.caretPos = document.selection.createRange().duplicate(); 
} 


function setmeoff () { 
	benn = "toff";
	//alert(benn);
} 

function setmeon () { 
	benn = "ton";
	//alert(benn);
} 

function insKarrot (destObj, txt) { 
	if (destObj.createTextRange && destObj.caretPos) { 
		var caretPos = destObj.caretPos; 
		caretPos.text = caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? txt + ' ' : txt; 
	} 
	else { 
		destObj.value = txt; 
	} 
}

