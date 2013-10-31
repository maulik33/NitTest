/*
All functions written by Justin J. Eady - justinje@shaw.ca and are property of ACT 360 Media Ltd.
*/


function hide(){

	setQues();

	if (document.all){
		document.all['toefl_a'].style.visibility = 'hidden';
	}
	else if (document.getElementById){
		document.getElementById('toefl_a').innerHTML = "&nbsp;";
		document.getElementById('toefl_a').style.visibility = 'hidden';
	}
	setConfirm();
	pauseTimer();
}

function setQues(){
	if(!isDrag){
		if (document.all){
			document.all.ques.innerHTML = qArray[0];
			document.all.a0.innerHTML = qArray[1];
			document.all.a1.innerHTML = qArray[2];
			document.all.a2.innerHTML = qArray[3];
			document.all.a3.innerHTML = qArray[4];
		} else {
			document.getElementById('ques').innerHTML = qArray[0];
			document.getElementById('a0').innerHTML = qArray[1];
			document.getElementById('a1').innerHTML = qArray[2];
			document.getElementById('a2').innerHTML = qArray[3];
			document.getElementById('a3').innerHTML = qArray[4];
		}
	}
}