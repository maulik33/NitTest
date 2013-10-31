/*
All functions written by Justin J. Eady - justinje@shaw.ca and are property of ACT 360 Media Ltd.
*/

var a_check = new Array();

function checkAns(elem,form){

alert("does this work");

	var currState = (elem.src == "/kaptoefl/images/box_unchecked.gif") ? false : true;
	
	if(currState){
		if(a_check.length < 2){
			a_check.push(true);
			elem.src = "/kaptoefl/images/box_checked.gif"
		} else {
			elem.src = "/kaptoefl/images/box_unchecked.gif"
		}
	} else {
		a_check.pop();
	}
	//makeAns(elem,form);
}

function makeAns(elem,form){
	var aString = "";
	for(i=0;i<form.ANS.length;i++){
		if(form.ANS[i].checked){
			aString += form.ANS[i].value;
		}
	}
	form.answer.value = aString;
}


function setAns(aString){
	clearAlert();
	document.questions.answer.value = aString;
}
	