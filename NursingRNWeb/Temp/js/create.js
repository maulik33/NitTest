var l_e_total = 100;
var l_m_total = 100;
var l_d_total = 100;
var l_a_total = 100;

var l_e_unused = 10;
var l_m_unused = 21;
var l_d_unused = 31;
var l_a_unused = 41;


var l_total = parseInt(l_e_total + l_m_total + l_d_total + l_a_total);
var l_total_unsed = l_avaliable - parseInt(l_e_unused + l_m_unused + l_d_unused + l_a_unused);

var qTypes = new Array();

/*
qReuse values:
1 = Unused only
2 = Unused and incorrect
3 = All correct used
*/

var qReuse = 3;

function addQtype(isChecked,which){

	if(isChecked){
		if(which == "listening") qTypes[0] = true;
		if(which == "structure") qTypes[1] = true;
		if(which == "reading") qTypes[2] = true;
	} else{
		if(which == "listening") qTypes[0] = false;
		if(which == "structure") qTypes[1] = false;
		if(which == "reading") qTypes[2] = false;
	}

}


function set_qReuse(k){
	qReuse = k;
}



function setRadio(){

	var r = '<input type="radio" name="range" value="">';


}


function sendEmail(file,winName){

	var posX = -100;
	var posY = -100;

	var win = window.open(file,winName,"screenX="+posX+",screenY="+posY+",top="+posY+",left="+posX+",height=10,width=10,fullscreen=0,location=0,menubar=0,resizable=0,scrollbars=0,status=0,toolbar=0");
		
	if (window.opener == null) window.opener = self;

	win.opener.focus();

}


