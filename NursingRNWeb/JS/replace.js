var item;
var currType;

var colorOn = "#FFFFCC";
var colorOff = "#FFFFFF";

function setAnswer(obj){

	document['a'].src = "/kaptoefl/images/radio_up.gif";
	document['b'].src = "/kaptoefl/images/radio_up.gif";
	document['c'].src = "/kaptoefl/images/radio_up.gif";
	document['d'].src = "/kaptoefl/images/radio_up.gif";
			
	obj.src = "/kaptoefl/images/radio_down.gif";
			
	document.myForm.answer.value = obj.id;
			
}

function buildTable(s){

	var t =	'<span class="content">' + s + '</span>';
	
	return t;
}

function replace(string,text,tag,newString) {

	if(newString)item ++;
		
    var strLength = string.length, txtLength = text.length;
    if ((strLength == 0) || (txtLength == 0)) return string;

    var i = string.indexOf(text);
    if ((!i) && (text != string.substring(0,txtLength))) return string;
    if (i == -1) return string;


	if(quesAction == "answer"){
		//if the user is answers the questions run this code
		var whatTag = (tag == "</a>") ? "</a>" : '<a href="javascript:setLink(' + item + ')" onFocus="if(this.blur)this.blur()" class="main" id="link' + item + '" >';
	} else {
		//if the user is review the questions run this code
		if(userAns == item){
			var whatTag = (tag == "</span>") ? "</span>" : '<span class="yourAnswer" id="link' + item + '">';
		} else {
			var whatTag = (tag == "</span>") ? "</span>" : '<span style="text-decoration: underline;"  id="link' + item + '">'
		}
	}

    var newstr = string.substring(0,i) + whatTag;

    if (i+txtLength < strLength)
		var isOpenTag= (tag == "</a>") ? false : true;
        newstr += replace(string.substring(i+txtLength,strLength),text,tag,isOpenTag);

    return newstr;
}

function setQuestion(layer,type,string){
	
	item = 0;
	currType = type
	
	var tempStr = new String(string);
	
	if(tempStr.indexOf("</I>") == -1) {
		var italic = tempStr.indexOf("</i>") + 4;
	}
	else {
		var italic = tempStr.indexOf("</I>") + 4;
		}
	
	document.getElementById('title').innerHTML = tempStr.substr(0,italic);
	
	//alert(tempStr.substr(italic,tempStr.length));
		
	var temp = tempStr.substr(italic,tempStr.length)//new String(string);

	temp = replace(temp,'<s>',null, true);

	if(quesAction == "answer"){
		temp = replace(temp,'</s>','</a>');
	} else {
		temp = replace(temp,'</s>','</span>');
	}

	if(document.all)
		document.all(layer).innerHTML = temp;
	else if (document.getElementById)
		document.getElementById(layer).innerHTML = temp;

	if(quesAction == "review" && type == 2){
		setTimeout("placeSentence()",500);

	} else {
	
		setTimeout("setAnchor()",1000);
		
	}
}

function setAnchor(){
	window.location.hash = "pos";
	}

function placeSentence(reading){
	if(document.all){
		document.all('link' + correctAns).innerHTML = getSentence(currSentence);
		if(reading)document.all('link' + correctAns).style.background = colorOn;
	}
	else if (document.getElementById){
		document.getElementById('link' + correctAns).innerHTML = getSentence(currSentence);
		if(reading)document.getElementById('link' + correctAns).style.background = colorOn;
	}
}

function setLink(id){

	if(currType == 2)addItem(id);

	for(var i=1; i<item;i++){
		if(document.all){
			document.all('link' + i).style.background = colorOff;
		}
		else if (document.getElementById){
			document.getElementById('link' + i).style.background = colorOff;
		}
	}
	if(document.all){
		document.all('link' + id).style.background = colorOn;
	}
	else if (document.getElementById){
		document.getElementById('link' + id).style.background = colorOn;
	}

	document.myForm.answer.value = id;
}

function addItem(id){

	var let = ["A","B","C","D","E","F","G","H","I","J","K","L",];

	for(var i=1; i<item;i++){
		if(document.all){
			document.all('link' + i).innerHTML = "<span class='plus'>+</span>";
		}
		else if (document.getElementById){
			document.getElementById('link' + i).innerHTML = "<span class='plus'>+</span>"; 
		}
	}
	if(document.all){
		document.all('link' + id).innerHTML = getSentence(currSentence);
	}
	else if (document.getElementById){
		document.getElementById('link' + id).innerHTML = getSentence(currSentence);
	}
}

function getSentence(string){

	var bStart = string.indexOf("<b>");
	var bEnd = string.indexOf("</b>");

	var tempString = string.substring( bStart + 3, bEnd)
	//alert(string + "\n" + tempString);

	return tempString;
}