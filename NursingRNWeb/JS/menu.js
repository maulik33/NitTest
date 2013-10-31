//ADJUST VALUES TO REFLECT NUMBER OF NAV ITEMS IN EACH MENU
var menu1_navItems = 8;
var menu2_navItems = 4;
var menu3_navItems = 5;
var menu4_navItems = 8;
var menu5_navItems = 2;


//START - PROTOTYPE FUNCTIONS
function floatY(speed,endy) {
	
	if(document.all){
		this.yFinal = this.obj.style.pixelTop;
		this.yPos = this.obj.style.pixelTop;
	} else if(document.getElementById){
		this.yFinal =  parseInt(this.obj.style.top);
		this.yPos = parseInt(this.obj.style.top);
	}

	this.speed = "."+speed;
	this.endy = endy;
	this.dist_y = Math.round(endy - this.yPos);
	this.vel_y = this.dist_y*this.speed;
	this.yFinal += this.vel_y;

	if(document.all) this.obj.style.pixelTop = this.yFinal;
	else if(document.getElementById) this.obj.style.top = this.yFinal + 'px';

}

function posX(endx) {
	if(document.all) this.obj.style.pixelLeft = endx;
	else if(document.getElementById) this.obj.style.left = endx + 'px';
}

function posY(endy) {
	if(document.all) this.obj.style.pixelTop = endy;
	else if(document.getElementById) this.obj.style.top = endy + 'px';
}

function vis(v) {
	this.obj.style.visibility = v;
}

function myObject(value) {
    if(document.all) this.obj = document.all(value);
	else if(document.getElementById) this.obj = document.getElementById(value);

}

myObject.prototype.floatY = floatY;
myObject.prototype.posX = posX;
myObject.prototype.posY = posY;
myObject.prototype.vis = vis;

//END - PROTOTYPE FUNCTIONS

//START - TOEIC FUNCTIONS
var flashObj;

function makeObj(){
	flashObj = new myObject('flash');
}

function setXYflash(x,y,file,w,h,c){
	flashObj.posX(x);
	flashObj.posY(y);
	flashObj.vis('visible');

	if(document.all){
		document.all('flash').innerHTML = buildFlash(file,w,h,c);
		}
	else if (document.getElementById){
		document.getElementById('flash').innerHTML = buildFlash(file,w,h,c);
	}

}

function buildFlash(file,w,h,c){

	var f = '<object ' +
		'classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" ' +
		'width="' + w + '" ' +
		'height="' + h + '" ' +
		'codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0"> ' +
		'<param name="movie" value="' + file + '"> ' +
		'<param name="play" value="true"> ' +
		'<param name="loop" value="false"> ' +
		'<param name="quality" value="high"> ' +
		'<param name="menu" value="false"> ' +
		'<param name="bgcolor" value="#' + c + '"> ' +
		'<embed ' +
		'	src="' + file + '" ' +
		'	width="' + w + '" ' +
		'	height="' + h + '" ' +
		'	bgcolor="#' + c + '" ' +
		'	play="true" ' +
		'	loop="false" ' +
		'	quality="high" ' +
		'	menu="false" ' +
		'	type="application/x-shockwave-flash" ' +
		'	pluginspage="http://www.macromedia.com/go/flashplayer/"> ' +
		'</embed> ' +
		'</object>';

	return f;
}

var titles = ['some current work','eacanada.com','intrawestecard.com','picklelogic.com','mooyouwinbc.com','karacters.com','drinkmilk.ca','shaftebury bocce'];
var quotes = ['Here is a sample of some of the cooler projects constructed over the past several months. Last year I was working over at Blastradius doing some Interface Development, but with a new year comes new clients, partners, and some really cool projects.','This was a fast paced site for EA Canada. Focused on exposing life @ EA. With a clean stylized design we built a showcase to highlight careers @ EA, the spectacular city of Vancouver, and the hip video gaming industry.','Blastradius brought me on board this summer to complete the work on this e-card. With real time compiled audio this flash e-card uses some pretty cool technology as well as delivering its users a unique experiencing. Come on send one to a friend ;-)','For this anti-smoking campaign we used a clean stylized Flash site to showcase the logic of smoking, or more precisely the logic of sticking pickles up your nose. I don\'t know about pickles in your nose, but the site sure turned out well!','Mooyouwin is back! This was a really fun project I did for TribalDDB last year. A Flash Music Mixer that enables user to save their mixers and enter a contest to win great prizes. A new year means a new contest so with a few updates... start mixing again. Still one of my favorites!','I did this one about 6 months ago. TribalDDB approached me to code out this site. Working closely with one of their designers we came up with this clean compact piece that expresses the amazing product design that karacters design group produces.','This site was conceived from a TV advertising campaign. The idea was to carry the concept of the TV spots through to the web. What we came up with was a innovative way to incorporate flash, php, and a cool plugin to produce a 3D scene. Look those guys have no bodies!','Who could forget Shaftebury bocce. This was one of my favorite projects. Not only did it involve beer and bocce, two things I love, but also some really cool action scripting. Who would of thought collision detection was so fun ;-) Be sure to give it a toss!'];

function setText(i){
	var title = true;
	var t = ['showTitle','showQuote'];
	var tagIn = ['<h1>','<p>'];
	var tagOut = ['</h1>','</p>'];
	for(var k=0;k<t.length;k++){
		var sObj = (document.all) ? document.all[t[k]] : document.getElementById(t[k]);
		var arrayObj = (title) ?  titles[i] : quotes[i];
		title = false;
		sObj.innerHTML = new String(tagIn[k] + arrayObj + tagOut[k]);
	}
}

var reset = false;

function restore(id){
	if(id){
		setText(id);
		clearTimeout(reset);
	} else {
		reset = setTimeout('setText(0)',1000);
	}
}
	
//END- TOEIC FUNCTIONS

var isEcom;
var top_1;
var top_2;
var top_3;
var top_4;
var top_5;
var bottom;

var currOpen = false;
var once = false;

function routine(id){

	if(currOpen && (currOpen != id)){
		document['m'+ currOpen].src = "/kaptoefl/images/m" + currOpen + ".gif";
		if(currOpen == 1) runOne(top_1,true);
		if(currOpen == 2) runTwo(top_2,true);
		if(currOpen == 3) runThree(top_3,true);
		if(currOpen == 4) runFour(top_4,true);
		if(currOpen == 5) runFive(top_5,true);
	}

	if(id){
		document['m'+ id].src = "/kaptoefl/images/m" + id + "_over.gif";
		if(id != currOpen){
			if(id == 1) runOne(bottom,true);
			if(id == 2) runTwo(bottom,true);
			if(id == 3) runThree(bottom,true);
			if(id == 4) runFour(bottom,true);
			if(id == 5) runFive(bottom,true);
		}

		if(!once){
			//TURN VISIBILITY OF LAYERS TO VISIBLE
			nav1.vis('visible');
			nav2.vis('visible');
			nav3.vis('visible');
			nav4.vis('visible');
			nav5.vis('visible');
			//ONLY NEED TO RUN THIS CODE ONCE 
			once = true;
		}
	}

	currOpen = id;
}


function makeObjects(){

	nav1 = new myObject('menu1');
	nav2 = new myObject('menu2');
	nav3 = new myObject('menu3');
	nav4 = new myObject('menu4');
	nav5 = new myObject('menu5');

	nav1.posX(getLeft(1));
	nav2.posX(getLeft(2));
	nav3.posX(getLeft(3));
	nav4.posX(getLeft(4));
	nav5.posX(getLeft(5));

	if(isEcom == "yes"){
		bottom = 169;
	} else{
		bottom = 248;
	}

	top_1 = bottom - parseInt(menu1_navItems * 20);
	top_2 = bottom - parseInt(menu2_navItems * 20);
	top_3 = bottom - parseInt(menu3_navItems * 20);
	top_4 = bottom - parseInt(menu4_navItems * 20);
	top_5 = bottom - parseInt(menu5_navItems * 20);

	nav1.posY(top_1);
	nav2.posY(top_2);
	nav3.posY(top_3);
	nav4.posY(top_4);
	nav5.posY(top_5);

}

var float1 = false;
var float2 = false;
var float3 = false;
var float4 = false;
var float5 = false;

function runOne(yPos,clear){
	if(clear) clearTimeout(float1);
	nav1.floatY(3,yPos);
	float1 = setTimeout('runOne(' + yPos +')',100);
}

function runTwo(yPos,clear){
	if(clear) clearTimeout(float2);
	nav2.floatY(3,yPos);
	float2 = setTimeout('runTwo(' + yPos +')',100);
}

function runThree(yPos,clear){
	if(clear) clearTimeout(float3);
	nav3.floatY(3,yPos);
	float3 = setTimeout('runThree(' + yPos +')',100);
}

function runFour(yPos,clear){
	if(clear) clearTimeout(float4);
	nav4.floatY(3,yPos);
	float4 = setTimeout('runFour(' + yPos +')',100);
}

function runFive(yPos,clear){
	if(clear) clearTimeout(float5);
	nav5.floatY(3,yPos);
	float5 = setTimeout('runFive(' + yPos +')',100);
}

function getLeft(id){
	if(document.images['m' + id]){
		var holdingImage = document.images['m' + id];
		return holdingImage.offsetLeft;
	}
}

function getTop(id){
	if(document.images['m' + id]){
		var holdingImage = document.images['m' + id];
		return holdingImage.offsetTop;
	}
}

function roll(id){

   var objSrc = id.src;
   var ext = (objSrc.indexOf(".jpg") != -1) ? ".jpg" : ".gif";
   var idLength = objSrc.length;

   if(objSrc.indexOf("_over") != -1){
      var path = objSrc.substr(0, idLength - 9);
      id.src = path + ext;
   } else {
      var path = objSrc.substr(0, idLength - 4);
      id.src = path + "_over" + ext;
   }

}

function rollGif(id,over){
	if(over) document[id].src = "/kaptoefl/images/" + id + "_over.gif";
	else document[id].src = "/kaptoefl/images/" + id + ".gif";
}

function bGeneric(id,over,img){
	if(over) id.src = "/kaptoefl/images/" + img + "_over.gif";
	else id.src = "/kaptoefl/images/" + img + ".gif";
}


function formAction(a,form){
	if(a == "submit"){
		form.submit();
		return true;
	} else {
		form.name.value = "";
		form.email_from.value = "";
		form.subject.value = "";
		form.message.value = "";
		return false;
	}
}