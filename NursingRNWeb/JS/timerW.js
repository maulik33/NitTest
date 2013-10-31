/*
All functions written by Justin J. Eady - justinje@shaw.ca and are property of ACT 360 Media Ltd.
*/


//START: Timer Functions for Writing
var temp;
var timerID;
var timerRunning = false;
var today = new Date();
var enday = new Date();
var secPerDay = 0;
var minPerDay = 0;
var hourPerDay = 0;
var secsLeft = 0;
var secsRound = 0;
var secsRemain = 0;
var minLeft = 0;
var minRound = 0;
var minRemain = 0;
var timeRemain = 0;
var decremain = 0;
var decround = 0;

var timer_val;
var isTimer = false;
var pause;

var isWriting;

function stopTimerW (){
	if(timerRunning)
		clearTimeout(timerID);
		timerRunning = false;
}

function startTimerW (seconds, setWrite) {

	if(seconds == 0) return false;

	if(setWrite) isWriting = true;
	
	timer_val = parseInt(seconds);
	isTimer = true;

	pause = false;
	now = new Date();
	stopTimerW();
	showCountdownW();
}

function showCountdownW () {
	if(!pause){
		today = new Date();
		enday = new Date(now);
		var amount = timer_val;
		enday.setTime(enday.getTime() + amount*1000)
		enday.setYear(enday.getFullYear());

		secsPerDay = 1000 ;
		minPerDay = 60 * 1000 ;
		hoursPerDay = 60 * 60 * 1000;
		PerDay = 24 * 60 * 60 * 1000;

		/*Seconds*/
		secsLeft = (enday.getTime() - today.getTime()) / minPerDay;
		decremain = (enday.getTime() - today.getTime()) / secsPerDay;
		decround = Math.round(decremain);
		decremain = decround + " seconds";
		secsRound = Math.round(secsLeft);
		secsRemain = secsLeft - secsRound;
		secsRemain = (secsRemain < 0) ? secsRemain = 60 - ((secsRound - secsLeft) * 60) : secsRemain = (secsLeft - secsRound) * 60;
		secsRemain = Math.round(secsRemain);

		/*Minutes*/
		minLeft = ((enday.getTime() - today.getTime()) / hoursPerDay);
		minRound = Math.round(minLeft);
		minRemain = minLeft - minRound;
		minRemain = (minRemain < 0) ? minRemain = 60 - ((minRound - minLeft)  * 60) : minRemain = ((minLeft - minRound) * 60);
		minRemain = Math.round(minRemain - 0.495);

		/*Hours*/
		hoursLeft = ((enday.getTime() - today.getTime()) / PerDay);
		hoursRound = Math.round(hoursLeft);
		hoursRemain = hoursLeft - hoursRound;
		hoursRemain = (hoursRemain < 0) ? hoursRemain = 24 - ((hoursRound - hoursLeft)  * 24) : hoursRemain = ((hoursLeft - hoursRound) * 24);
		hoursRemain = Math.round(hoursRemain - 0.5);

		/*Days*/
		daysLeft = ((enday.getTime() - today.getTime()) / PerDay);
		daysLeft = (daysLeft - 0.5);
		daysRound = Math.round(daysLeft);
		daysRemain = daysRound;

		/*Adjust minutes and seconds if sec=60*/
		if (secsRemain == 60)
		{
			secsRemain = 0
			if (minRemain * 60 > timer_val)
			{
				minRemain = minRemain - 1;
			}  
			else if (minRemain * 60 < timer_val)
			{
				minRemain = minRemain + 1;
			}
		}

		/*Time*/
		timeRemain = ((hoursRemain< 10) ? "0" : "") + hoursRemain + ((minRemain < 10) ? ":0" : ":") + minRemain + ((secsRemain< 10) ? ":0" : ":") + secsRemain;

		if (document.all) {
			if(document.all('timer')) document.all('timer').innerHTML = timeRemain;
		}
		else if (document.getElementById) {
			if(document.getElementById('timer')) document.getElementById('timer').innerHTML = timeRemain;
		}

		document.myForm.remaining.value = (hoursRemain*60*60) + (minRemain*60) + secsRemain;
	}
	timerID = setTimeout("showCountdownW()",1000);
	timerRunning = true;

	if (daysRemain < 0) {
		clearTimeout(timerID);
		timerRunning = false;
		if (document.all) {
			if(document.all('timer')) document.all('timer').innerHTML = "00:00:00";
		}
		else if (document.getElementById) {
			if(document.getElementById('timer')) document.getElementById('timer').innerHTML = "00:00:00";
		}
		
		//check to see if it is the writing
		if(isWriting)
		{ 
			playLecture();
		}
		else
		{
			alert("The time allowed for this section has expired. After clicking OK, you will proceed to the next section.");
//			document.forms[0].action='change_section.asp?expired=yes';
//			document.forms[0].submit();
			document.myForm.answer.value=document.typeArea.typeHolder.value;goNextW();
			//alert("Time has expired, however, you may finish the test. It is important for you to work on your timing so that you can answer approximately one question per minute.");
		}
		
	}
}

function pauseTimerW(){

	if(isTimer){
		if(pause){
			var curr  = new Date();
			var offset = temp.getTime() - curr.getTime();
			var nowTime = now.getTime();

			now.setTime(nowTime - offset);
			pause = false;
		} 
		else {
			temp = new Date();
			pause = true;
		}
	}
}
//END: Timer Functions for Writing