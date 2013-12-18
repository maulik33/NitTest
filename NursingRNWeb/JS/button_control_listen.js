/*
All functions written by Justin J. Eady - justinje@shaw.ca and are property of ACT 360 Media Ltd.


function go(){
	var isAns = checkAnswer();
	
	if(isAns){
		top.storage.user[top.storage.currQuestion] = isAns;
	}

	top.storage.currQuestion ++;
	if(top.storage.currQuestion < top.storage.right.length){
		location.href = "q" + top.storage.currQuestion + ".html";
	}
	else {
		location.href = "review.html";
	}
}

function goBack(){
	top.storage.currQuestion --;
	location.href = "q" + top.storage.currQuestion + ".html";
}

function checkAnswer(){
	var isAns = false;
	var isArray = document.questions.answer[0];

	if(isArray){
		//if isArray is true there are radio buttons so run this code
		for(var i=0;i<document.questions.answer.length;i++){
			if(document.questions.answer[i].checked){
				var isAns = document.questions.answer[i].value;
				break;
			}
		}
	} else {
		//if isArray is false there are NO radio buttons so run this code
		if(document.questions.answer.value != ""){
			var isAns = document.questions.answer.value;
		}
	}
	
	return isAns;

}
*/

function delay(secs){
	setTimeout("setConfirm();",secs*1000);
	}

function setNext(){
	//var isAns = checkAnswer();
	
	var isAns = (document.myForm.answer.value == "") ? false : true;

	if(isAns){
		//turn off confirm button
		setConfirm("off");
//		document.NextAction.value = "1"
		//document.getElementById('next').innerHTML = '<a href="javascript:document.myForm.submit();"><img src="/kaptoefl/images/next.gif" class="button" alt="" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" /></a>';
		document.getElementById('next').innerHTML = '<a href="javascript:submitNext();"><img src="/kaptoefl/images/confirm.gif" class="button" alt="" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" /></a>';
	}
	else {
		//Open layer
		displayIt("noAns","block");
	}
}


function submitNext(){
	var isAns = (document.myForm.answer.value == "") ? false : true;
	
	if(isAns){
		document.myForm.submit();
	}
	else {
		//Open layer
		displayIt("noAns","block");
	}
}

function setConfirm(state){

	if(state == "off"){
		var bString = '<img src="/kaptoefl/images/next_off.gif" class="button" alt="" width="75" height="25" />'
	} else {
		var bString = '<a href="javascript:setNext();"><img src="/kaptoefl/images/next.gif" class="button" alt="" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" /></a>';
	}

	if(document.all){
		document.all('confirm').innerHTML = bString;
	}
	else if (document.getElementById){
		document.getElementById('confirm').innerHTML = bString;
	}

	//setHelp();
}

function suspend(){
	var agree = true
	agree=confirm('Are you sure you want to suspend the test?');
	if (agree){
		document.myForm.action='../student_home.asp?SuspendAction=1'
		document.myForm.submit();
	}
}

/*

function setNextReading(form){

	if(form.nextPassage.value == ""){

		form.submit();

	}else if(form.currPassage.value != form.nextPassage.value){

		//Open last question in a series alert layer
		if(document.all){
			document.all('readingAlert').style.visibility = 'visible'; 
		}
		else if (document.getElementById){
			document.getElementById('readingAlert').style.visibility = 'visible';
		}

	} else {

		form.submit();

	}

}

function setHelp(){

	var bString = '<input type="button" class="button" style="width: 80px; height: 18px" value="Help" onClick=help("visible");>'
	
	if(document.all){
		document.all('b_help').innerHTML = bString;
	}
	else if (document.getElementById){
		document.getElementById('b_help').innerHTML = bString;
	}
}

function clearAlert(reading){
	var l = (reading) ? "readingAlert" : "nextAlert";
	if(document.all){
		document.all(l).style.visibility = 'hidden'; //.innerHTML = '&nbsp;';
	}
	else if (document.getElementById){
		document.getElementById(l).style.visibility = 'hidden'; //.innerHTML = '&nbsp;';
	}
}

function suspend(form){
	form.suspendAction.value = 1;
	form.submit();
}

function previous(form){
	form.previousAction.value = 1;
	form.submit();
}


function toeicNextAlert(){

	var holdingImage = document.images["holdspace"];
	var canvasLeft = holdingImage.offsetLeft;
	var canvasWidth = holdingImage.width;

		if(document.all){
			document.all('nextAlert').style.width = canvasWidth;
			document.all('nextAlert').style.left = canvasLeft;
		}
		else if (document.getElementById){
			document.getElementById('nextAlert').style.width = canvasWidth;
			document.getElementById('nextAlert').style.left = canvasLeft;
		}
}

function toeicHelp(){

	var holdingImage = document.images["holdspace"];
	var canvasLeft = holdingImage.offsetLeft;
	var canvasWidth = holdingImage.width;

		if(document.all){
			document.all('help').style.width = canvasWidth;
			document.all('i_content').style.width = canvasWidth - 4;
			document.all('help').style.left = canvasLeft;
		}
		else if (document.getElementById){
			document.getElementById('help').style.width = canvasWidth;
			document.getElementById('help').style.width = canvasWidth - 4;
			document.getElementById('help').style.left = canvasLeft;
		}
}

function setLayer(id,left){

	var holdingImage = document.images["layerspace"];
	var canvasTop = holdingImage.offsetTop;
	var canvasLeft = holdingImage.offsetLeft;

	//alert("canvasTop: " + canvasTop + "\ncanvasLeft: " + canvasLeft);

	var isMac_ie = false;


		
	if(navigator.appVersion.indexOf('Mac') != -1 && navigator.appName == 'Microsoft Internet Explorer'){
		var isMac_ie = true;
	}

	if(!isMac_ie){

		if(document.all){
			document.all(id).style.top = canvasTop;
			if(left)document.all(id).style.left = canvasLeft + 3;
		}
		else if (document.getElementById){
			document.getElementById(id).style.top = canvasTop;
			if(left)document.getElementById(id).style.left = canvasLeft + 3;
		}
	}
}
*/