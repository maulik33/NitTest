/***************************************************
Globals
***************************************************/
var testParams;
var clientAccessToken;

/***************************************************
Main Test Engine Functions
***************************************************/

// get client access token for ajax requests
clientAccessToken = readCookie('KaplanNursingRn');


function loadTestRequest(){
    
}

function validateTestRequest(){
    // validate action and parse passed params
}

function timerControl(){

}

function handleClientError(){

}

/***************************************************
Test UI Functions
***************************************************/


/***************************************************
Navigation Functions
***************************************************/
function handleTestNext(){

}

function handleTestPrev(){

}

function handleTestEnd(){

}

function handleTestStart(){

}

function handleTestResume(){

}

/*****************************************************
General utility functions
*****************************************************/
function createCookie(name, value, days){
    if(days){
        var date = new Date();
        date.setTime(date.getTime() + (days*24*60*60*1000));
        var expires = date.toUTCString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name){
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i < ca.length; i++){
        var c = ca[i];
        while(c.charAt(0)==' ') c = c.substring(1, c.length);
        if(c.indexOf(nameEq) == 0) return c.substring(nameEq.length, c.length);
    }
    return null;
}

function clearCookie(name){
    createCookie(name, "", -1);
}