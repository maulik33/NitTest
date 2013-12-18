/***************************************************************
  Global Variables
***************************************************************/
var i = 1;
var endvalue = 2;
var selectedMenuItem;

/***************************************************************
  Global Functions
***************************************************************/
function handleWindowOpen(windowHeight, windowWidth, windowName, windowUri){
    var centerWidth = (window.screen.width - windowWidth) / 2;
    var centerHeight = (window.screen.height - windowHeight) / 2;
    var win = window.open(windowUri, windowName, 'status=1,resizable=yes,scrollbars=yes,width=' + windowWidth + ',height=' + windowHeight + ',left=' + centerWidth + ',top=' + centerHeight);
}
function init(sectionname) {
    document.getElementById(sectionname).style.display = 'none';
}
function hideElement(element){
  document.getElementById(element).style.display = 'none';
}

function toggle(sectionname){
    if(document.getElementById(sectionname).style.display=='none'){
        document.getElementById(sectionname).style.display = '';
    } else {
        document.getElementById(sectionname).style.display = 'none';
    }
}

function hideall(){
    for (i = 1; i <= endvalue; i++){
        init('expend'+i);
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

function keepMeAlive(imgName) {
    myImg = document.getElementById(imgName);

    if (myImg) {
        myImg.src = myImg.src.replace(/\?*?.*$/, '?' + Math.random());
    }
}

function change(id, newClass){
    id.className=newClass;
    id.style.cursor='hand'
}

function go(url){
    window.location=url;
}

function menuClick(element){
    if(selectedMenuItem){
        var priorImagePath = $(selectedMenuItem).attr('src');
        var newImagePath = priorImagePath.substring(0, priorImagePath.length - 9) + '.jpg';
        $(selectedMenuItem).attr('src', newImagePath);
    } else {
        var homeImage = $('#tabHomeImage').attr('src');
        var newHomeImage = homeImage.substring(0, homeImage.length - 9) + '.jpg';
        $('#tabHomeImage').attr('src', newHomeImage);
    }
    
    var currentImage = $(element).attr('src');
    var updateImage = currentImage.substring(0, currentImage.length - 4) + '_over.jpg';
    $(element).attr('src', updateImage);
    
    selectedMenuItem = element;
}

/***************************************************************
  Flash Loader
***************************************************************/
/**
 * FlashObject v1.2.3: Flash detection and embed - http://blog.deconcept.com/flashobject/
 *
 * FlashObject is (c) 2005 Geoff Stearns and is released under the MIT License:
 * http://www.opensource.org/licenses/mit-license.php
 *
 * com.deconcept.FlashObject.prototype.write Modified:	2006 01 03
 * 	-	Workaround for PC IE Memory Leak using document.write()	(PS)
 */
if(typeof com == "undefined") var com = new Object();
if(typeof com.deconcept == "undefined") com.deconcept = new Object();
if(typeof com.deconcept.util == "undefined") com.deconcept.util = new Object();
if(typeof com.deconcept.FlashObjectUtil == "undefined") com.deconcept.FlashObjectUtil = new Object();
com.deconcept.FlashObject = function(swf, id, w, h, ver, c, useExpressInstall, quality, redirectUrl, detectKey){
   this.DETECT_KEY = detectKey ? detectKey : 'detectflash';
   this.skipDetect = com.deconcept.util.getRequestParameter(this.DETECT_KEY);
   this.params = new Object();
   this.variables = new Object();
   this.attributes = new Array();

   if(swf) this.setAttribute('swf', swf);
   if(id) this.setAttribute('id', id);
   if(w) this.setAttribute('width', w);
   if(h) this.setAttribute('height', h);
   if(ver) this.setAttribute('version', new com.deconcept.PlayerVersion(ver.toString().split(".")));
   if(c) this.addParam('bgcolor', c);
   var q = quality ? quality : 'high';
   this.addParam('quality', q);
   this.setAttribute('redirectUrl', '');
   if(redirectUrl) this.setAttribute('redirectUrl', redirectUrl);
   if(useExpressInstall) {
   // check to see if we need to do an express install
   var expressInstallReqVer = new com.deconcept.PlayerVersion([6,0,65]);
   var installedVer = com.deconcept.FlashObjectUtil.getPlayerVersion();
      if (installedVer.versionIsValid(expressInstallReqVer) && !installedVer.versionIsValid(this.getAttribute('version'))) {
         this.setAttribute('doExpressInstall', true);
      }
   } else {
      this.setAttribute('doExpressInstall', false);
   }
}
com.deconcept.FlashObject.prototype.setAttribute = function(name, value){
	this.attributes[name] = value;
}
com.deconcept.FlashObject.prototype.getAttribute = function(name){
	return this.attributes[name];
}
com.deconcept.FlashObject.prototype.getAttributes = function(){
	return this.attributes;
}
com.deconcept.FlashObject.prototype.addParam = function(name, value){
	this.params[name] = value;
}
com.deconcept.FlashObject.prototype.getParams = function(){
	return this.params;
}
com.deconcept.FlashObject.prototype.getParam = function(name){
	return this.params[name];
}
com.deconcept.FlashObject.prototype.addVariable = function(name, value){
	this.variables[name] = value;
}
com.deconcept.FlashObject.prototype.getVariable = function(name){
	return this.variables[name];
}
com.deconcept.FlashObject.prototype.getVariables = function(){
	return this.variables;
}
com.deconcept.FlashObject.prototype.getParamTags = function(){
   var paramTags = ""; var key; var params = this.getParams();
   for(key in params) {
        paramTags += '<param name="' + key + '" value="' + params[key] + '" />';
    }
   return paramTags;
}
com.deconcept.FlashObject.prototype.getVariablePairs = function(){
	var variablePairs = new Array();
	var key;
	var variables = this.getVariables();
	for(key in variables){
		variablePairs.push(key +"="+ variables[key]);
	}
	return variablePairs;
}
com.deconcept.FlashObject.prototype.getHTML = function() {
    var flashHTML = "";
    if (navigator.plugins && navigator.mimeTypes && navigator.mimeTypes.length) { // netscape plugin architecture
        if (this.getAttribute("doExpressInstall")) { this.addVariable("MMplayerType", "PlugIn"); }
        flashHTML += '<embed type="application/x-shockwave-flash" src="'+ this.getAttribute('swf') +'" width="'+ this.getAttribute('width') +'" height="'+ this.getAttribute('height') +'" id="'+ this.getAttribute('id') + '" name="'+ this.getAttribute('id') +'"';
		var params = this.getParams();
        for(var key in params){ flashHTML += ' '+ key +'="'+ params[key] +'"'; }
		pairs = this.getVariablePairs().join("&");
        if (pairs.length > 0){ flashHTML += ' flashvars="'+ pairs +'"'; }
        flashHTML += '></embed>';
    } else { // PC IE
        if (this.getAttribute("doExpressInstall")) { this.addVariable("MMplayerType", "ActiveX"); }
        flashHTML += '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="'+ this.getAttribute('width') +'" height="'+ this.getAttribute('height') +'" id="'+ this.getAttribute('id') +'">';
        flashHTML += '<param name="movie" value="' + this.getAttribute('swf') + '" />';
		var tags = this.getParamTags();
        if(tags.length > 0){ flashHTML += tags; }
		var pairs = this.getVariablePairs().join("&");
        if(pairs.length > 0){ flashHTML += '<param name="flashvars" value="'+ pairs +'" />'; }
        flashHTML += '</object>';
    }
    return flashHTML;
}
com.deconcept.FlashObject.prototype.write = function(){
	if(this.skipDetect || this.getAttribute('doExpressInstall') || com.deconcept.FlashObjectUtil.getPlayerVersion().versionIsValid(this.getAttribute('version'))){
		if (this.getAttribute('doExpressInstall')) {
		   this.addVariable("MMredirectURL", escape(window.location));
		   document.title = document.title.slice(0, 47) + " - Flash Player Installation";
		   this.addVariable("MMdoctitle", document.title);
		}
		document.write(this.getHTML());
	}else{
		if(this.getAttribute('redirectUrl') != "") {
			document.location.replace(this.getAttribute('redirectUrl'));
		}
	}
}
/* ---- detection functions ---- */
com.deconcept.FlashObjectUtil.getPlayerVersion = function(){
   var PlayerVersion = new com.deconcept.PlayerVersion(0,0,0);
	if(navigator.plugins && navigator.mimeTypes.length){
		var x = navigator.plugins["Shockwave Flash"];
		if(x && x.description) {
			PlayerVersion = new com.deconcept.PlayerVersion(x.description.replace(/([a-z]|[A-Z]|\s)+/, "").replace(/(\s+r|\s+b[0-9]+)/, ".").split("."));
		}
	}else if (window.ActiveXObject){
	   try {
   	   var axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
   		PlayerVersion = new com.deconcept.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(","));
	   } catch (e) {}
	}
	return PlayerVersion;
}
com.deconcept.PlayerVersion = function(arrVersion){
	this.major = parseInt(arrVersion[0]) || 0;
	this.minor = parseInt(arrVersion[1]) || 0;
	this.rev = parseInt(arrVersion[2]) || 0;
}
com.deconcept.PlayerVersion.prototype.versionIsValid = function(fv){
	if(this.major < fv.major) return false;
	if(this.major > fv.major) return true;
	if(this.minor < fv.minor) return false;
	if(this.minor > fv.minor) return true;
	if(this.rev < fv.rev) return false;
	return true;
}
/* ---- get value of query string param ---- */
com.deconcept.util.getRequestParameter = function(param){
	var q = document.location.search || document.location.href.hash;
	if(q){
		var startIndex = q.indexOf(param +"=");
		var endIndex = (q.indexOf("&", startIndex) > -1) ? q.indexOf("&", startIndex) : q.length;
		if (q.length > 1 && startIndex > -1) {
			return q.substring(q.indexOf("=", startIndex)+1, endIndex);
		}
	}
	return "";
}

/* add Array.push if needed (ie5) */
if (Array.prototype.push == null) { Array.prototype.push = function(item) { this[this.length] = item; return this.length; }}

/* add some aliases for ease of use / backwards compatibility */
var getQueryParamValue = com.deconcept.util.getRequestParameter;
var FlashObject = com.deconcept.FlashObject;
