function blurAnchors(){
  if(document.getElementsByTagName){
    var a = document.getElementsByTagName("a");
    for(var i = 0; i < a.length; i++){
      a[i].onfocus = function(){this.blur()};
    }
  }
}
window.onload = blurAnchors;

function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
function jumptoattention(newURL,newName,newFeatures) {
	var remote = open(newURL,newName,newFeatures);
}


function nospam(user,domain) {
	locationstring = "mailto:" + user + "@" + domain;
	window.location = locationstring;
}

// Copyright (c) 1996-1997 Athenia Associates.
// http://www.webreference.com/js/
// License is granted if and only if this entire
// copyright notice is included. By Tomer Shiran.


function fixDate (date) {
    var base = new Date(0);
    var skew = base.getTime();
    if (skew > 0)
        date.setTime(date.getTime() - skew);
}

function GetElementsWithClassName(elementName,className) {
	var allElements = document.getElementsByTagName(elementName);
	var elemColl = new Array();
	for (i = 0; i< allElements.length; i++) {
		if (allElements[i].className == className) {
			elemColl[elemColl.length] = allElements[i];
		}
	}
	return elemColl;
}