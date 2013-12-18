<%@ Page Language="C#" AutoEventWireup="true" Inherits="STUDENT.Intro" ViewStateMode="Disabled" CodeBehind="Intro.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/STUDENT/ASCX/AlternateIntro.ascx" TagPrefix="ucALternateIntro"
    TagName="AlternateIntro" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.HtmlControls" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/confirmStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../CSS/jquery-ui.css" />
    <script type="text/javascript" src="../js/timer.js"></script>
    <script type="text/javascript" src="../js/main1.js"></script>
    <script type="text/javascript" src="../js/js-security.js"></script>
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script  type="text/javascript" src="../JS/jquery-1.9.1.min.js"></script>
    <script  type="text/javascript" src="../JS/jquery-ui.js"></script>
    <script type="text/javascript" src="../JS/jquery.ui.touch-punch.min.js"></script>
    <link href="../css/ui_002.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <asp:PlaceHolder runat="server">
    <% if (IsProctorTrackEnabled)
       { %>
        <script type="text/javascript" >
            $(document).ready(function () {
                
                var inFormOrLink;
                $('a').on('click', function () { inFormOrLink = true; });
                $('form').bind('submit', function () { inFormOrLink = true; });

        //Invoke Submit Proctor Track API On End Test
        $("input:image[name='ibIntro_S']").click(function() {
            console.log('Invoked End Test');
            submitVerificientTest();
             console.log('End Test Completed');
        });

        $("#btnHidden").click(function () {
            console.log('Invoked End Test in on blur dialog');
            submitVerificientTest();
            console.log('End Test Completed');
        });

        $(window).unload(function () {
            console.log('Invoked End Test in on window unload');
            if (!inFormOrLink) {
                console.log("user closing tab or window");
                submitVerificientTest();
                console.log('End Test Completed');
            }
        });
    });

    function submitVerificientTest() {
        console.log("in submitVerificientTest");
            $.ajax({
                type: "POST",
                url: "/Student/SubmitToProctorTrackOnEndTest.ashx",
                async: false,
                dataType: "json",
                success: function(data, textStatus, xhr) {
                    console.log("Inside success callback for Proctor Track Test submit call");
                    console.log("Status: " + textStatus + " with response data: " + data);
                },
                error: function(xhr, textStatus, errorThrown) {
                    console.log("Inside error callback for Proctor Track Test submit call");
                    console.log("Status: " + textStatus + " with response data: " + data + " and error: " + errorThrown);
                },
                complete: function(xhr, textStatus) {
                    console.log("Proctor Track Test submit call completed");
                    console.log("Request ReadyState and Status: " + xhr.readyState + " - " + xhr.status);
                }
            });
    }
        //     var verificientWindow;
       //     var verificientUrl;
        //    var userTestId;
         //   var testId;
         //   $(document).ready(function() {
                                //var chkPostBack = '<%= Page.IsPostBack ? "true" : "false" %>';
                                //if (chkPostBack == 'false') {
                                //    var url = "/student/verificientTestReview.html";
                                //    window.onblur = null;
                                //   userTestId = '<%= Student.UserTestId%>';
                                //   testId = '<%= Student.TestId%>';
                                //   var contentDiv = document.getElementById("content");
                                //   contentDiv.style.display = "none";
                                //to do:  pass in from web,config
                                //   verificientUrl = 'http://staging.verificient.com:8000/lti/launch/?using_lms=true';
                                //to do:  proper width and height
                                //  verificientWindow = window.open(url, 'verificientWindow', "location=0,status=0, toolbar=0, scrollbars=1, menubar=0, titlebar=0, resizable=0, width=1200, height=1000");
                                //  $(verificientWindow).load(function () {
                                //     if ((verificientWindow != null) && (verificientWindow.closed == false)) {
                                //         verificientWindow.launchVerificientIFrame(verificientUrl, userTestId, testId);
                                //     }
           //             }
             //       });
               // } else {
               //     window.onblur=HandleBlur;
               // }

               // $("input:image[name='ibIntro_S']").click(function() {
               //     console.log("in submit button handler");
               //     submitToVerificient();                    
               // });
                
               // $("input:image[name='btnQuit']").click(function() {
               //     console.log("in quit button handler");
               //     submitToVerificient();                    
               // });
                
                //to do:  event handler for window unload and submit on blur

        //    });
            
           // function submitToVerificient() {
           //     if (verificientWindow == null) {
           //         verificientWindow = window.open(null,'verificientWindow');
           //     }

            //    if ((verificientWindow != null) && (verificientWindow.closed == false)) {
            //        verificientWindow.ReceiveSubmitMessage();
             //   }

           // }
            //function setVerificientValues() {
             //   if ((verificientWindow != null) && (verificientWindow.closed == false)) {
             //       verificientWindow.launchVerificientIFrame(verificientUrl, userTestId, testId);
             //   }
           // }
        </script>
       <% }  %>
    </asp:PlaceHolder>
    <script type="text/javascript">
        $(document).ready(function() {
            $(function () {
                $("#sortable1, #sortable2").sortable({
                    connectWith: ".connectedSortable",
                    receive: Drop,
                    update: function (event, ui) { ReorderUnorderedList(); }
                }).disableSelection();

                function Drop(event, ui) {
                    $(this).find('li').removeClass('SelectedDragDropItem');
                    $(ui.item).addClass('SelectedDragDropItem');
                    CaptureInitialPosition();
                    SetButtonStatus();
                    ReorderUnorderedList();
                }
            });

            function ReorderUnorderedList() {
                var mylist = $('#sortable1');
                var listitems = mylist.children('li').get();
                listitems.sort(function (a, b) {
                    var compA = $(a).attr('initialPos').toUpperCase();
                    var compB = $(b).attr('initialPos').toUpperCase();
                    return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
                })
                $.each(listitems, function (idx, itm) { mylist.append(itm); });
            }

            $("#sortable2").on("mousedown", "li", function () {
                SetControlColor('#sortable2', this);
                SetButtonStatus();
            });


            $("#sortable1").on("mousedown", " li", function () {
                SetControlColor('#sortable1', this);
                SetButtonStatus();
            });

            $("#btnRight").click(function () {
                ChangeDroppedControlColor('#sortable2', '#sortable1');
                $("#sortable1 > .SelectedDragDropItem").appendTo("#sortable2");
                CaptureInitialPosition();
                SetButtonStatus();
            });

            $("#btnLeft").click(function () {
                ChangeDroppedControlColor('#sortable1', '#sortable2');
                $("#sortable2 > .SelectedDragDropItem").appendTo("#sortable1");
                ReorderUnorderedList();
                CaptureInitialPosition();
                SetButtonStatus();
            });

            $("#btnUp").click(function () {
                var current = $("#sortable2 > .SelectedDragDropItem");
                current.prev().before(current);
                CaptureInitialPosition();
                SetButtonStatus();
            });

            $("#btnDown").click(function () {
                var current = $("#sortable2 > .SelectedDragDropItem");
                current.next().after(current);
                CaptureInitialPosition();
                SetButtonStatus();
            });

            $("#btnLeft,#btnDown,#btnRight,#btnUp").mouseenter(function () {
                if (this.src.indexOf('inactive') <= 0) {
                    this.src = this.src.replace('active', 'mouseover')
                }
            });

            $("#btnLeft,#btnDown,#btnRight,#btnUp").mouseout(function () {
                if (this.src.indexOf('mouseover') > 0) {
                    this.src = this.src.replace('mouseover', 'active')
                }
            });

            function SetControlColor(controlId, thisli) {
                var id = controlId + ' li';
                $(id).removeClass('SelectedDragDropItem');
                $(thisli).addClass('SelectedDragDropItem');
            }

            function ChangeDroppedControlColor(destConotrol, sourceControl) {
                var sControl = sourceControl + ' > .SelectedDragDropItem';
                var dControllis = destConotrol + ' > li';
                if ($(sControl).length > 0) {
                    $(dControllis).removeClass('SelectedDragDropItem');
                }
            }

            function CaptureInitialPosition() {
                var initialPos = '';
                $('#sortable2').find('li').each(function () {
                    initialPos = initialPos + $(this).attr("initialPos") + ',';
                });

                objButton = document.getElementById('btnNext');
                if ($('#sortable1').find('li').length == 0) {
                    if (IsAudioPlayCompleted)
                        objButton.disabled = false;
                }
                else {
                    if (IsAudioPlayCompleted)
                        objButton.disabled = true;
                }

                document.getElementById("requireResponse").value = initialPos;
            }

            function SetButtonStatus() {
                $('#btnRight').attr('src', '../images/inactive-right.png');
                $('#btnLeft').attr('src', '../images/inactive-left.png');
                $('#btnUp').attr('src', '../images/inactive-up.png');
                $('#btnDown').attr('src', '../images/inactive-down.png');
                if ($("#sortable1 > .SelectedDragDropItem").length > 0) {
                    $('#btnRight').attr('src', '../images/active-right.png');
                }
                if ($("#sortable2 > .SelectedDragDropItem").length > 0) {
                    $('#btnLeft').attr('src', '../images/active-left.png');
                }
                if ($("#sortable2 > .SelectedDragDropItem").index() != 0 && $("#sortable2 > li").length > 0) {
                    $('#btnUp').attr('src', '../images/active-up.png');
                }
                if (($("#sortable2 > .SelectedDragDropItem").index() + 1) != $("#sortable2 > li").length && $("#sortable2 > li").length > 0) {
                    $('#btnDown').attr('src', '../images/active-down.png');
                }
            }
        });
    </script>
    <script type="text/javascript">

        var allowPrompt = true;
        window.onblur = HandleBlur;
        // Allow the user to be warned by default.
        window.onbeforeunload = WarnUser;
        var varQuitAlert = false;
        var blankField = false;
        var calFlag = false;
        var browserCheck = navigator.userAgent.toLowerCase();
        var chromeIssueFlag = false;

        function WarnUser() {
            allowPrompt = true;
        }

        function HandleBlur() {
            // safari-browser pop-up issue
            if (browserCheck.indexOf("safari") != -1) {
                var vbody = document.getElementById("body");
                vbody.addEventListener("blur", HandleBlur, true);
            }

            var vprdId = $('#hdProductId').val();
            var vaction = $('#hdAction').val();
            allowPrompt = false;
            if (!isblurred && vprdId == "1" && ((vaction == "NewTest") || (vaction == "Resume"))) {
                if (!varQuitAlert) {
                    // only Chrome & safari
                    if (browserCheck.indexOf("chrome") != -1 || browserCheck.indexOf("safari") != -1) {
                        if (!chromeIssueFlag) {
                            OpenJqueryPopup();
                        } else {
                            chromeIssueFlag = false;
                        }
                    } else {
                        OpenJqueryPopup();
                    }
                }
            }
            isblurred = false;

            if (blankField) {
                blankField = false;
                $('#tx').focus();
            }
        }

        function NoPrompt() {
            isblurred = true;
            allowPrompt = false;
        }

        function confirmCustom(message, callback) {
            $("#confirmCustom .message").text(message);
            $("#confirmCustom").dialog({
                resizable: false,
                modal: true,
                position: ['center', 'center'],
                width: 420,
                containerId: 'confirmModalContainer',
                title: 'Nursing Message Box',
                dialogClass: 'confirmCustom',
                create: function (event, ui) {
                    var widget = $(this).dialog("widget");
                    $('.ui-icon-myCloseButton').text('x');
                },
                open: function (event, ui)
                { $('.ui-dialog').attr("id", "confirmModalContainer"); },
                beforeClose: function (event, ui) { resetOverlays(this); },
                buttons: {
                    "Return to Test": function () {
                        resetOverlays(this);
                    },
                    "Submit Test": function () {
                        callback.apply();
                        resetOverlays(this);
                    }
                }
            });

            function resetOverlays(thisDiv) {
                $(thisDiv).dialog('destroy');
                var dialogs = $("div.ui-dialog");
                if (dialogs.length == 0) {
                    $(".ui-widget-overlay").remove();
                }
            }
        }

        function OpenJqueryPopup() {
            allowPrompt = false;
            confirmCustom("You are navigating away from your exam. If you choose to continue, your exam may be submitted immediately. In order to continue your exam, please click Return to Test.", function () {
                var btn = document.getElementById("btnHidden");
                btn.click();
            });
        }
        function LoadFocus() {
            var form = document.getElementById("myForm");
            if ("onfocusin" in form) {
                // Internet Explorer // the attachEvent method can also be used in IE9,// but we want to use the cross-browser addEventListener method if possible
                if (form.addEventListener) {    // IE from version 9
                    form.addEventListener("focusout", OnFocusOutForm, false);
                }
                else {
                    if (form.attachEvent) {     // IE before version 9
                        form.attachEvent("onfocusout", OnFocusOutForm);
                    }
                }
            }
            else {
                if (form.addEventListener) {
                    // Firefox, Opera, Google Chrome and Safari // since Firefox does not support the DOMFocusIn/Out events // and we do not want browser detection // the focus and blur events are used in all browsers excluding IE // capturing listeners, because focus and blur events do not bubble up
                    form.addEventListener("blur", OnFocusOtherBrowser, true);
                }
            }
        }
        function OnFocusOutForm(event) {
            isblurred = false;
            allowPrompt = true;
        }
        function OnFocusOtherBrowser(event) {
            isblurred = false;
            allowPrompt = true;
        }

        function NoPromptOnlyIE() {
            if (browserCheck.indexOf("firefox") != -1) {
                isblurred = false;
            }
            else if (browserCheck.indexOf("chrome") != -1 || browserCheck.indexOf("safari") != -1) {
                window.focus();
            }
            else {
                isblurred = true;
                allowPrompt = false;
            }
        }

        function NoPromptButton() {
            isblurred = true;
        }

        function CalCulatorPopup() {
            OpenCalPopup();
        }

        function OpenCalPopup() {
            document.getElementById('ModalPopupDiv').style.visibility = 'visible';
            if (browserCheck.indexOf("msie") != -1) {
                isblurred = true;
            }
            else {
                isblurred = false;
                window.focus();
            }
            document.getElementById('ModalPopupDiv').style.display = '';
            AnswerFocus();
        }

        function CloseCalPopup() {
            Memory = 0;
            Number1 = "";
            Number2 = "";
            NewNumber = "blank";
            opvalue = "";
            document.getElementById('answer').value = "";
            document.getElementById('mem').value = "";
            document.getElementById('ModalPopupDiv').style.display = 'none';
            isblurred = false;
        }

        var ie = document.all;
        var nn6 = document.getElementById && !document.all;

        var isdrag = false;
        var x, y;
        var dobj;

        function movemouse(e) {
            if (isdrag) {
                var vLeft = nn6 ? tx + e.clientX - x : tx + event.clientX - x;
                var vTop = nn6 ? ty + e.clientY - y : ty + event.clientY - y;
                if (vLeft >= 1135) { vLeft = 1135; } else if (vLeft < 0) { vLeft = 0; }
                if (vTop >= 440) { vTop = 440; } else if (vTop < 0) { vTop = 0; }
                dobj.style.left = vLeft + 'px';
                dobj.style.top = vTop + 'px';
                return false;
            }
        }

        function selectmouse(e) {
            var fobj = nn6 ? e.target : event.srcElement;
            var topelement = nn6 ? "HTML" : "BODY";
            while (fobj.tagName != topelement && fobj.className != "popup_Titlebar") {
                fobj = nn6 ? fobj.parentNode : fobj.parentElement;
            }
            if (fobj.className == "popup_Titlebar") {
                isdrag = true;
                dobj = document.getElementById("ModalPopupDiv");
                tx = parseInt(dobj.style.left + 0);
                ty = parseInt(dobj.style.top + 0);
                x = nn6 ? e.clientX : event.clientX;
                y = nn6 ? e.clientY : event.clientY;
                document.onmousemove = movemouse;
                window.focus();
                return false;
            }
        }

        document.onmousedown = selectmouse;
        document.onmouseup = new Function("isdrag=false");
    </script>
    <asp:PlaceHolder runat="server">
        <script type="text/javascript">
            <% if (IsProctorTrackEnabled)
           { %>
            function quitAlert() {
                var is_chrome = browserCheck.indexOf('chrome') > -1;
                if (!is_chrome) {
                    isblurred = true;
                }
                varQuitAlert = true;
                if (confirm('Are you sure that you want to quit the test?')) {
                    if (!is_chrome) {
                        isblurred = true;
                    }
                    varQuitAlert = false;
 
                    //Invoke Submit Proctor Track API On Quit Test
                    console.log('Invoked Quit Test');
                    submitVerificientTest();
                    console.log('End Quit Test Completed');
                    return true;
                }
                else {
                    if (!is_chrome) {
                        isblurred = true;
                    }
                    varQuitAlert = false;
                    return false;
                }
            }
            <% }
           else
           { %>
            function quitAlert() {
                var is_chrome = browserCheck.indexOf('chrome') > -1;
                if (!is_chrome) {
                    isblurred = true;
                }
                varQuitAlert = true;
                if (confirm('Are you sure that you want to quit the test?')) {
                    if (!is_chrome) {
                        isblurred = true;
                    }
                    varQuitAlert = false;
                    return true;
                }
                else {
                    if (!is_chrome) {
                        isblurred = true;
                    }
                    varQuitAlert = false;
                    return false;
                }
            }
            <% } %>
        
        </script>
        </asp:PlaceHolder>
    <script type="text/javascript">

        var IsAudioPlayCompleted = true;
        var isblurred = false;

        //For multiple-choice,multi-select
        function CheckForSelected(alternateText) {
            if (browserCheck.indexOf("safari") != -1) {
                isblurred = false;
            }

            var selected = new Array();
            if (alternateText == 'standard') {
                $("#TabQuestion_TabAlternate_ucAlternateIntro_D_AltAnswers input:checkbox").removeAttr("checked");
                selected.push($(this).attr('name'));
            }
            else if (alternateText == 'alternate') {
                $("#TabQuestion_TabStandard_D_Answers input:checkbox").removeAttr("checked");
                selected.push($(this).attr('name'));
            }
            else {
                $('#divD_Answers input:checked').each(function () {
                    selected.push($(this).attr('name'));
                });
            }
            var len = selected.length;
            if (len > 0) {
                EnableNextButtonexp();
            }
            else {
                DisableNextButtonexp();
            }
        }

        //For Fill-In
        function CheckTxt(alternateText) {
            if (alternateText == 'standard') {
                if ($('#TabQuestion_TabStandard_tx').val() != undefined && $('#TabQuestion_TabStandard_tx').val().length == 0) {
                    $('#TabQuestion_TabAlternate_ucAlternateIntro_Atx').val('');
                    DisableNextButtonexp();
                }
                else {
                    $('#TabQuestion_TabAlternate_ucAlternateIntro_Atx').val('');
                    EnableNextButtonexp();
                }
            }
            else if (alternateText == 'alternate') {
                if ($('#TabQuestion_TabAlternate_ucAlternateIntro_Atx').val() != undefined && $('#TabQuestion_TabAlternate_ucAlternateIntro_Atx').val().length == 0) {
                    $('#TabQuestion_TabStandard_tx').val('');
                    DisableNextButtonexp();
                }
                else {
                    $('#TabQuestion_TabStandard_tx').val('');
                    EnableNextButtonexp();
                }
            }
            else {
                if ($('#tx').val().length == 0) {
                    blankField = true;
                    DisableNextButtonexp();
                }
                else {
                    blankField = true;
                    EnableNextButtonexp();
                }
            }
        }

        function DisableNextButtonexp() {
            objButton = document.getElementById('btnNext');
            objButton.disabled = true;
            return;
        }

        function nextcontrol() {
            IsAudioPlayCompleted = true;
            EnableNextButton(-1, '');
        }

        function highlight(frm) {
            var inputs = frm.getElementsByTagName('INPUT');
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == 'radio') {
                    inputs[i].nextSibling.style.backgroundColor = (inputs[i].checked) ? 'ccffcc' : 'white';
                }
            }
        }

        //For Multi-select,single-choice
        function EnableNextButton(item, alternateText) {
            if (browserCheck.indexOf("safari") != -1) {
                isblurred = false;
            }
            objButton = document.getElementById('btnNext');
            obj = document.getElementById('txtA');
            if (obj != null) {
                if (item == 1) { obj.value = obj.value + "/A"; }
                if (item == 2) { obj.value = obj.value + "/B"; }
                if (item == 3) { obj.value = obj.value + "/C"; }
                if (item == 4) { obj.value = obj.value + "/D"; }
                if (item == 5) { obj.value = obj.value + "/E"; }
                if (item == 6) { obj.value = obj.value + "/F"; }
            }
            if (alternateText != 'standard') {
                $("#TabQuestion_TabStandard_D_Answers input:radio").removeAttr("checked");
            }
            else if (alternateText != 'alternate') {
                $("#TabQuestion_TabAlternate_ucAlternateIntro_D_AltAnswers input:radio").removeAttr("checked");
            }

            if (IsAudioPlayCompleted && obj.value != '') objButton.disabled = false;
            return;
        }

        function EnableNextButtonexp() {
            objButton = document.getElementById('btnNext');
            if (IsAudioPlayCompleted) objButton.disabled = false;
            return;
        }

        function updateInteractionState(interactionStateXML) {
            allowPrompt = false;
            var test = interactionStateXML;
            document.getElementById("requireResponse").value = test;
            var orderLength = $(test).attr("orderLength");
            //If orderlength is not undefined than it is drag and drop type question else it is "hot spot" type question else

            if (browserCheck.indexOf("msie") != -1) { } else { window.focus(); }

            if (orderLength != undefined) {
                var orderindexes = $(test).attr("orderedIndexes");
                var indexLength = orderindexes.split(',').length;
                if (indexLength == orderLength) {
                    if (IsAudioPlayCompleted) document.getElementById("btnNext").disabled = false;
                } else {
                    if (IsAudioPlayCompleted) document.getElementById("btnNext").disabled = true;
                }
            }
            else {
                if (IsAudioPlayCompleted) document.getElementById("btnNext").disabled = false;
            }
        }

        var up, down;
        var min1, sec1;
        var cmin1, csec1, cmin2, csec2;

        function Minutes(data) {
            for (var i = 0; i < data.length; i++) if (data.substring(i, i + 1) == ":") break;
            return (data.substring(0, i));
        }

        function Seconds(data) {
            for (var i = 0; i < data.length; i++) if (data.substring(i, i + 1) == ":") break;
            return (data.substring(i + 1, data.length));
        }

        function Display(min, sec) {
            var disp;
            if (min <= 9) disp = " 0";
            else disp = " ";
            disp += min + ":";
            if (sec <= 9) disp += "0" + sec;
            else disp += sec;
            return (disp);
        }

        function Up(sec_v) {
            LoadFocus();
            cmin1 = Minutes(sec_v);
            csec1 = Seconds(sec_v);
            UpRepeat();
        }

        function UpRepeat() {
            csec1++;
            if (csec1 == 60) { csec1 = 0; cmin1++; }
            document.getElementById('timer').innerHTML = Display(cmin1, csec1);
            document.getElementById('mytimer').value = Display(cmin1, csec1);
            document.getElementById('timer_up').value = Display(cmin1, csec1);
            up = setTimeout("UpRepeat()", 1000);
        }

        function Down() {
            cmin2 = 1 * Minutes(document.getElementById('timer_up').value);
            csec2 = 0 + Seconds(document.getElementById('timer_up').value);
            DownRepeat();
        }

        function DownRepeat() {
            csec2--;
            if (csec2 == -1) { csec2 = 59; cmin2--; }
            document.getElementById('timer').innerHTML = Display(cmin2, csec2);
            document.getElementById('mytimer').value = Display(cmin2, csec2);
            document.getElementById('timer_up').value = Display(cmin2, csec2);
            down = setTimeout("DownRepeat()", 1000);
        }

        function saveRemediationTime() {
            var vAction = document.getElementById('txtAction').value;
            var vTimer = document.getElementById('mytimer').value;
            var QID = document.getElementById('txtQID').value;
            var UserTestID = document.getElementById('txtUserTestID').value;

            PageMethods.saveRemediationTime(vAction, vTimer, QID, UserTestID);
        }
    </script>
    <script type="text/javascript">
        history.forward();
        function Tab_SelectionChanged(sender, e) {
            var msg = "";
            var tnumber = document.getElementById("tabNumber").value;
            msg += sender.get_activeTabIndex() + 1 + " of " + tnumber;
        }

        function getElementPos(elementId) {
            var ua = navigator.userAgent.toLowerCase();
            var isOpera = (ua.indexOf('opera') != -1);
            var isIE = (ua.indexOf('msie') != -1 && !isOpera); // not opera spoof
            var el = document.getElementById(elementId);
            if (el.parentNode === null || el.style.display == 'none') {
                return false;
            }
            var parent = null;
            var pos = [];
            var box;
            if (el.getBoundingClientRect)	//IE
            {
                box = el.getBoundingClientRect();
                var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop);
                var scrollLeft = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft);

                return { x: box.left + scrollLeft, y: box.top + scrollTop };
            }
            else if (document.getBoxObjectFor)	// gecko
            {
                box = document.getBoxObjectFor(el);

                var borderLeft = (el.style.borderLeftWidth) ? parseInt(el.style.borderLeftWidth) : 0;
                var borderTop = (el.style.borderTopWidth) ? parseInt(el.style.borderTopWidth) : 0;

                pos = [box.x - borderLeft, box.y - borderTop];
            }
            else	// safari & opera
            {
                pos = [el.offsetLeft, el.offsetTop];
                parent = el.offsetParent;
                if (parent != el) {
                    while (parent) {
                        pos[0] += parent.offsetLeft;
                        pos[1] += parent.offsetTop;
                        parent = parent.offsetParent;
                    }
                }
                if (ua.indexOf('opera') != -1
                    || (ua.indexOf('safari') != -1 && el.style.position == 'absolute')) {
                    pos[0] -= document.body.offsetLeft;
                    pos[1] -= document.body.offsetTop;
                }
            }
            if (el.parentNode) { parent = el.parentNode; }
            else { parent = null; }

            while (parent && parent.tagName != 'BODY' && parent.tagName != 'HTML') { // account for any scrolled ancestors
                pos[0] -= parent.scrollLeft;
                pos[1] -= parent.scrollTop;

                if (parent.parentNode) { parent = parent.parentNode; }
                else { parent = null; }
            }
            return { x: pos[0], y: pos[1] };
        }

        function UntileClick() {
            var p = getElementPos('imgExhibit');
            document.getElementById('PanelExhibit').style.width = "400px";
            document.getElementById('PanelExhibit').style.top = p.y;
            document.getElementById('PanelExhibit').style.left = p.x + 55;
            document.getElementById('PanelExhibit').style.height = "300px";
            document.getElementById('TabContainer1').style.height = "350px";
            document.getElementById('TabContainer1_body').style.height = "310px";
            document.getElementById('question_main').style.height = "400px";
            document.getElementById('divExhibit').style.height = "0px";
            if (document.getElementById('btnTile') != null) {
                document.getElementById('btnTile').style.visibility = "visible";
            }
            if (document.getElementById('btnUntile') != null) {
                document.getElementById('btnUntile').style.visibility = "hidden";
            }
            return false;

        }
        function tileClick() {
            var browserName = navigator.appName;
            if (browserName == "Microsoft Internet Explorer") {
                document.getElementById('PanelExhibit').style.top = "355px";
                document.getElementById('PanelExhibit').style.left = (document.body.clientWidth / 2 - 435) + "px";
                document.getElementById('PanelExhibit').style.width = "870px";
                document.getElementById('question_main').style.height = "250px";
                document.getElementById('divExhibit').style.height = "150px";
                document.getElementById('PanelExhibit').style.height = "150px";
                document.getElementById('TabContainer1').style.height = "150px";
            }
            else {
                document.getElementById('PanelExhibit').style.top = "395px";
                document.getElementById('PanelExhibit').style.left = (document.body.clientWidth / 2 - 440) + "px";
                document.getElementById('PanelExhibit').style.width = "868px";
                document.getElementById('PanelExhibit').style.height = "145px";
                document.getElementById('divExhibit').style.height = "145px";
                document.getElementById('TabContainer1').style.height = "145px";
                document.getElementById('question_main').style.height = "255px";
            }
            document.getElementById('TabContainer1_body').style.height = "114px";
            if (document.getElementById('btnTile') != null) {
                document.getElementById('btnTile').style.visibility = "hidden";
            }
            if (document.getElementById('btnUntile') != null) {
                document.getElementById('btnUntile').style.visibility = "visible";
            }

        }
        function CloseClick() {
            allowPrompt = true;
            isblurred = false;
            document.getElementById('question_main').style.height = "400px";
            if (document.getElementById('btnTile') != null) {
                document.getElementById('btnTile').style.visibility = "hidden";
            }
            if (document.getElementById('btnUntile') != null) {
                document.getElementById('btnUntile').style.visibility = "hidden";
            }

            if (document.getElementById('TabContainer1') != undefined) {
                document.getElementById('divExhibit').style.visibility = "hidden";
                document.getElementById('PanelExhibit').style.visibility = "hidden";
                document.getElementById('TabContainer1').style.display = "none";
                document.getElementById('TabContainer1_header').style.visibility = "hidden";
                document.getElementById('TabContainer1_body').style.visibility = "hidden";
                document.getElementById('TabContainer1').style.visibility = "hidden";
                document.getElementById('TabContainer1_TabPanel1_Exhibit1').style.visibility = "hidden";
                document.getElementById('TabContainer1').style.height = "0px";
                document.getElementById('divExhibit').style.height = "0px";
                document.getElementById('PanelExhibit').style.height = "0px";
            }
            if (document.getElementById('TabContainer1_TabPanel3_Exhibit2') != null) {
                document.getElementById('TabContainer1_TabPanel2_Exhibit2').style.visibility = "hidden";
            }
            if (document.getElementById('TabContainer1_TabPanel3_Exhibit3') != null) {
                document.getElementById('TabContainer1_TabPanel3_Exhibit3').style.visibility = "hidden";
            }

            return false;
        }

        function imgExhibitClick() {
            var browserName = navigator.appName;

            if (browserName == "Microsoft Internet Explorer") {
                document.getElementById('PanelExhibit').style.top = "355px";
                document.getElementById('PanelExhibit').style.left = (document.body.clientWidth / 2 - 435) + "px";
                document.getElementById('PanelExhibit').style.width = "870px";
                document.getElementById('question_main').style.height = "250px";
                document.getElementById('divExhibit').style.height = "150px";
                document.getElementById('PanelExhibit').style.height = "150px";
                document.getElementById('TabContainer1').style.height = "150px";
            }
            else {
                document.getElementById('PanelExhibit').style.top = "395px";
                document.getElementById('PanelExhibit').style.left = (document.body.clientWidth / 2 - 440) + "px";
                document.getElementById('PanelExhibit').style.width = "868px";
                document.getElementById('PanelExhibit').style.height = "156px";
                document.getElementById('divExhibit').style.height = "145px";
                document.getElementById('TabContainer1').style.height = "145px";
                document.getElementById('question_main').style.height = "255px";
            }

            document.getElementById('TabContainer1').style.display = "block";
            document.getElementById('TabContainer1_body').style.height = "114px";
            if (document.getElementById('btnTile') != null) {
                document.getElementById('btnTile').style.visibility = "hidden";
            }
            if (document.getElementById('btnUntile') != null) {
                document.getElementById('btnUntile').style.visibility = "visible";
            }
            document.getElementById('question_main').style.display = "block";
            document.getElementById('question_main').style.overflow = "auto";
            document.getElementById('PanelExhibit').style.visibility = "visible";
            document.getElementById('TabContainer1_header').style.visibility = "visible";
            document.getElementById('TabContainer1_body').style.visibility = "visible";
            document.getElementById('TabContainer1').style.visibility = "visible";
            document.getElementById('TabContainer1_TabPanel1_Exhibit1').style.visibility = "visible";
            if (document.getElementById('TabContainer1_TabPanel3_Exhibit2') != null) {
                document.getElementById('TabContainer1_TabPanel2_Exhibit2').style.visibility = "visible";
            }
            if (document.getElementById('TabContainer1_TabPanel3_Exhibit3') != null) {
                document.getElementById('TabContainer1_TabPanel3_Exhibit3').style.visibility = "visible";
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#answer").focusin(function () {
                calFlag = true;
            });
            $("#answer").focusout(function () {
                calFlag = false;
                chromeIssueFlag = true;
            });

            $('#tx').focusout(function () {
                chromeIssueFlag = true;
            });

        });

        document.onkeydown = checkKeycode

        function checkKeycode(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;

            if (keycode == 91 || keycode == 92) {
                isblurred = true;
                OpenJqueryPopup();
                return;
            }

            if (calFlag)
                return;
            ////assigning keystroke to radio buttons and checkbox
            if (keycode == 49 || keycode == 97) {
                AssignKeyStrokeToAnswers(0);
            }

            if (keycode == 50 || keycode == 98) {
                AssignKeyStrokeToAnswers(1);
            }

            if (keycode == 51 || keycode == 99) {
                AssignKeyStrokeToAnswers(2);
            }

            if (keycode == 52 || keycode == 100) {
                AssignKeyStrokeToAnswers(3);
            }

            if (keycode == 53 || keycode == 101) {
                AssignKeyStrokeToAnswers(4);
            }

            if (keycode == 54 || keycode == 102) {
                AssignKeyStrokeToAnswers(5);
            }
        }

        function AssignKeyStrokeToAnswers(selectedAnswer) {
            //check for radiobuttons
            if ($('#divD_Answers input:radio[name=RB]')[selectedAnswer] != undefined) {
                $('#divD_Answers input:radio[name=RB]')[selectedAnswer].checked = true;
                var rbValue = $('#divD_Answers input:radio[name=RB]:checked').val();
                EnableNextButton(rbValue.substring(rbValue.length - 1), '');
            }
            //check for checkboxes
            if ($('#divD_Answers input:checkbox[name^=CH_]')[selectedAnswer] != undefined) {
                $('#divD_Answers input:checkbox[name^=CH_]')[selectedAnswer].checked = true;
                var chkValue = $('#divD_Answers').find('input:checkbox[name^=CH_]').eq(selectedAnswer).attr('id');
                EnableNextButton(chkValue.substring(chkValue.length - 1), '');
            }
        }

    </script>
    <script type="text/jscript">
        function noBack() {
            window.history.forward(1);
        }
        setTimeout("noBack()", 0);
    </script>
    <script src="../JS/google.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openPopUp() {
            var NewHeight = screen.availHeight;
            $('#hfHeight').val(NewHeight);
            var winHeight = NewHeight * 0.81;
            $(document).ready(function (e) {
                var retVal = $("#popContent").dialog({
                    modal: true, width: 719, height: winHeight, resizable: false, dialogClass: 'introSkillModulePopup', draggable: false, position: "245,20",
                    onLoad: function () {
                        $("body").css("overflow", "hidden");
                    },
                    onClose: function () {
                        $("body").css("overflow", "");
                    },
                    create: function (event, ui) {
                        var widget = $(this).dialog("widget");
                        $(".ui-dialog-titlebar-close span", widget)
                            .removeClass("ui-icon-closethick")
                            .addClass("ui-icon-CloseImage");
                    }

                });
            });
        }

        function setPopupTitle(smScreenTitle) {

            $('#ui-id-1').html(smScreenTitle);
        }

        function closeIframe() {
            $('#popContent').dialog('close');
            return false;
        }

    </script>
    <style type="text/css">
        #ibCalc
        {
            height: 22px;
        }
        body
        {
            background-color: #cccccc;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -o-user-select: none;
        }
        
        @media print
        {
            .noprint
            {
                display: none;
            }
        }
    </style>

    <style type="text/css">
        div.popup_Titlebar
        {
            background-color: #0067A9;
            height: 29px;
            cursor: move;
        }
        
        .TitlebarLeft
        {
            float: left;
            padding-left: 5px;
            padding-top: 5px;
            font-family: verdana;
            font-weight: bold;
            font-size: 12px;
            color: #FFFFFF;
        }
        .TitlebarRight
        {
            background: url(../images/tab_close.gif);
            background-position: right;
            background-repeat: no-repeat;
            height: 15px;
            width: 16px;
            float: right;
            cursor: pointer;
            margin-right: 5px;
            margin-top: 5px;
        }
        .popup_Body
        {
            font-family: verdana;
            font-weight: bold;
            font-size: 12px;
            color: #000000;
            line-height: 15pt;
            clear: both;
        }
        .red
        {
            color: red;
            width: 32px;
            font-weight: bold;
            height: 25px;
            border:1px solid;
        }
        .blue
        {
            color: #1B1C78;
            width: 32px;
            height: 25px;
            border:1px solid;
        }
        .blue1
        {
            color: #1B1C78;
            width: 32px;
            font-weight: bold;
            height: 25px;
            border:1px solid;
        }
        .inputAnswerText
        {
            border: 1px solid #1B1C78;
            background-color: #f3f3ef;
            color: #181F6F;
            font: normal 12px Verdana;
        }
    </style>
</head>
<body id="body" runat="server" onselectstart="return false" onpaste="return false" 
    oncopy="return false;" oncut="return false;">
   <%--<script type="text/javascript">--%>
        
<%--    //    function validationComplete() {
    //        verificientWindow.resizeTo(1, 1);
    //        var contentDiv = document.getElementById("content");
    //        contentDiv.style.display = 'inline';
    //        var remaining = '<%= Remaining %>';
    //        startTimer(remaining, false);
    //        verificientWindow.blur();
    //        window.focus();
    //        var button = document.getElementById("btnNext");
    //        if (button != null) {
    //            button.focus();
    //        }

     //   }--%>

  <%-- </script>--%>
    <form id="myForm" runat="server" onmousedown="NoPromptOnlyIE();">
<%--        <div id="content">--%>
    <table align="center" border="0" cellspacing="0" cellpadding="0" class='noprint'>        
        <tr>
            <td>
                <input id="remaining" runat="server" type="hidden" />
                <div id="med_header_q_new" style="text-align: right; padding-right: 10px;">
                    &nbsp; &nbsp;
                </div>
                <div id="med_menu_qq_new">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ctrltable1">
                        <tr>
                            <td align="left" class="testname_new">
                                <asp:label ID="lblTestName" runat="server" Text="TestName"></asp:label>
                              
                            </td>
                            <td align="right">
                                <table width="300" border="0" cellspacing="0" cellpadding="0" style="color: #ffffff;
                                    font-size: 14px; font-weight:normal;">
                                    <tr>
                                        <td align="right" style="padding-bottom: 5px;">
                                            <asp:Image ID="imtimer" runat="server" ImageUrl="../images/timer.gif" />
                                            <asp:Label ID="lbltimer" runat="server" Text="Time Remaining:"></asp:Label>
                                            <input id="timer_up" type="hidden" runat="server" />&nbsp;
                                        </td>
                                        <td width="7%">
                                            <div id="timer" style="margin-top: 0px;">
                                            </div>
                                            <asp:HiddenField ID="mytimer" runat="server" Value="0" />
                                        </td>
                                        <td width="1%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="../images/item.gif" />&nbsp;&nbsp;
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:Label ID="lblQNumber" runat="server" Text="1 of 75 "></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="CalBar" runat="server" class="med_menu_qq_cal">
                    <img alt="" src="../images/cal.gif" runat="server" onclick='CalCulatorPopup();'
                        onmouseover="roll(this)" onmouseout="roll(this)" border="0" id="ibCalc" style="margin-top: 3px;" />
                </div>

                <div id="MessageRetire" runat="server" class="med_main_q_new">
                    <asp:Label ID="MessageRetireText" runat="server" Text=""></asp:Label>
                </div>
                 
                <div id="intro_main" runat="server" class="med_main_q_new">
                    <div id="intro_submain" runat="server" class="med_center" style="height: 115px">
                        <div runat="server" id="intro_title">
                        </div>
                        <div runat="server" class="med_question" id="intro_text">
                        </div>
                        <div id="intro_button" runat="server">
                            <asp:ImageButton ID="ibIntro_D" runat="server" ImageUrl="../images/btn_dis.gif" OnClick="IbIntroDClick"
                                AlternateText="Dismiss" Width="75" Height="25" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                Visible="False"></asp:ImageButton>
                            <asp:ImageButton ID="ibIntro_S" runat="server" ImageUrl="../images/btn_start.gif"
                                OnClick="IbIntroSClick" AlternateText="Start Test" onMouseOver="roll(this)" onMouseOut="roll(this)">
                            </asp:ImageButton>
                        </div>
                    </div>
                </div>
                <div id="tutorial_main" runat="server" class="med_main_q_new1">
                    <div id="Tutorial_title" runat="server" style="text-align: center">
                    </div>
                    <div id="Tutorial" runat="server">
                    </div>
                </div>
                <div id="question_main" runat="server" class="med_main_q_new">
                    <div id="D_QID" runat="server">
                    </div>
                    <div style="text-align: left">
                        <asp:Literal ID="Literal_Player" runat="server"></asp:Literal>
                    </div>
                    <div style="text-align: left;">
                        <img src="../Images/btn_inside_ex.gif" id="imgExhibit" runat="server" onclick="javscript:imgExhibitClick();UntileClick();"
                            visible="False" alt="" onmouseover="roll(this)" onmouseout="roll(this)" style="margin-top: 5px;" />
                    </div>
                    <div id='tabNotFocused' runat="server" style=" text-align:left;">
                        <div id="divStem" runat="server">
                        </div>
                        <div id="divD_Answers" runat="server" class="med_text" style="width: 84%; height: 1px">
                        </div>
                        <div id="divmatch" runat="server">
                        </div>
                        <div id="divhotspot_D" runat="server">
                        </div>
                    </div>
                    
                    <div>
                        <cc2:TabContainer ID="TabQuestion" runat="server" 
                            CssClass="ajax__tab_default" ScrollBars="Auto"
                            Visible="false" onactivetabchanged="TabQuestion_ActiveTabChanged" 
                            AutoPostBack="true">
                            <cc2:TabPanel ID="TabStandard" runat="server" HeaderText="Standard" ScrollBars="Auto"
                                TabIndex="0">
                                <HeaderTemplate>
                                    <b>
                                        <asp:Label ID="lblStandard" runat="server" Text="Standard"></asp:Label>
                                    </b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div id="stem" runat="server">
                                    </div>
                                    <div id="D_Answers" runat="server" class="med_text" style="width: 84%; height: 1px">
                                    </div>
                                    <div id="match" runat="server">
                                    </div>
                                    <div id="hotspot_D" runat="server">
                                    </div>
                                </ContentTemplate>
                            </cc2:TabPanel>
                            <cc2:TabPanel ID="TabAlternate" runat="server" HeaderText="Alternate" ScrollBars="Auto"
                                TabIndex="1">
                                <HeaderTemplate>
                                    <b>
                                        <asp:Label ID="lblAlternate" runat="server" Text="Alternate"></asp:Label></b>
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div id="altQuestion_main" runat="server">
                                        <ucALternateIntro:AlternateIntro ID="ucAlternateIntro" runat="server" readonly="true" />
                                    </div>
                                </ContentTemplate>
                            </cc2:TabPanel>
                        </cc2:TabContainer>
                    </div>
                    <div id="Explanation_Div" runat="server" visible="false" style="text-align:left;">
                        <asp:LinkButton ID="lnkExplanation" runat="server" OnClick="LnkExplanationClick">View Explanation</asp:LinkButton><br />
                        <br />
                        <div id="Explanation" runat="server" class="med_question">
                        </div>
                         <div id="SkillModuleLink_Div" runat="server" visible="false" style=" text-align:left;">
                            <asp:LinkButton ID="lbtnSkilModule" runat="server" onclick="lbtnSkilModule_Click"></asp:LinkButton>
                        </div>
                    </div>
                      
                   
                    <div id="remediation" runat="server" class="med_question">
                        <asp:Label ID="Label1" runat="server" Text="Topic Review" Font-Bold="True"></asp:Label><br />
                        <br />
                        <asp:PlaceHolder ID="Lippincott" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                
                <div id="divExhibit" runat="server">
                    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
                    </ajaxToolkit:ToolkitScriptManager>
                    <ajaxToolkit:DragPanelExtender ID="DragPanelExtender1" runat="server" TargetControlID="PanelExhibit"
                        DragHandleID="PanelExhibit">
                    </ajaxToolkit:DragPanelExtender>
                    <asp:Panel ID="PanelExhibit" runat="server" CssClass="PanelExhibit1">
                        <table width="100%" style="margin: 0px" align="center" class="inside_table" border="0">
                            <tr>
                                <td>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/tab_ex.gif" />
                                    &nbsp;<span style="color: #ffffff; font-size: 14px;" /><asp:Label ID="Label2" runat="server"
                                        Font-Bold="True" Text="Exhibit"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Image ID="Image3" runat="server" onclick="javscript:CloseClick()" ImageUrl="~/Images/tab_close.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_default"
                                        Height="114" OnClientActiveTabChanged="Tab_SelectionChanged" ScrollBars="Auto"
                                        Width="700">
                                        <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1" ScrollBars="Auto"><HeaderTemplate><b><asp:Label ID="ExhTitle1" runat="server"></asp:Label></b></HeaderTemplate><ContentTemplate><div id="Exhibit1" runat="server" class="med_text" style="width: 84%; height: 1px"></div></ContentTemplate></cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" ScrollBars="Auto"><HeaderTemplate><b><asp:Label ID="ExhTitle2" runat="server"></asp:Label></b></HeaderTemplate><ContentTemplate><div id="Exhibit2" runat="server" class="med_text" style="width: 84%; height: 1px"></div></ContentTemplate></cc2:TabPanel>
                                        <cc2:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3" ScrollBars="Auto"><HeaderTemplate><b><asp:Label ID="ExhTitle3" runat="server"></asp:Label></b></HeaderTemplate><ContentTemplate><div id="Exhibit3" runat="server" class="med_text" style="width: 84%; height: 1px"></div></ContentTemplate></cc2:TabPanel>
                                    </cc2:TabContainer>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <img id="btnClose" alt="" onclick="javscript:CloseClick()" onmouseout="roll(this)"
                                        onmouseover="roll(this)" src="../images/tab_close_btn.gif" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div id="med_bot_q_new">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ctrltable">
                        <tr>
                            <td align="left" style="padding-left: 2px;">
                                <a href="testreview.aspx"></a>
                                <asp:ImageButton ID="btnBackIncorrect" runat="server" ImageUrl="../images/pre_inc.gif"
                                    OnClick="BtnBackIncorrectClick" Visible="false" AlternateText="Previous" onMouseOver="roll(this)"
                                    onMouseOut="roll(this)" OnClientClick="javascript:NoPrompt();"/>
                                <asp:ImageButton ID="btnNextIncorrect" runat="server" ImageUrl="../images/next_inc.gif"
                                    OnClick="BtnNextIncorrectClick" Visible="false" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                    OnClientClick="Javascript:CloseClick();" />
                                <asp:ImageButton ID="ibSkip" runat="server" AlternateText="Skip Tutorial" ForeColor="White"
                                    onMouseOver="roll(this)" onMouseOut="roll(this)" OnClick="IbSkipClick" ImageUrl="../images/btn_skip.gif" OnClientClick="javascript:NoPrompt();"/>
                                <asp:ImageButton ID="btnQuit" runat="server" ImageUrl="../images/quit_new.gif" OnClick="BtnQuitClick"
                                    onMouseOver="roll(this)" onMouseOut="roll(this)"/>
                                <asp:ImageButton ID="ibSuspend" runat="server" ImageUrl="../images/suspend.gif" AlternateText="Suspend Test"
                                    OnClick="IbSuspendClick" onMouseOver="roll(this)" onMouseOut="roll(this)" OnClientClick="javascript:NoPrompt();" />
                            </td>
                            <td align="right" style="padding-right: 0px;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/lwww.jpg" ImageAlign="Left"
                                    Visible="False" />
                                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../images/backNav_new.gif"
                                    OnClick="BtnBackClick" AlternateText="Previous" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                    OnClientClick="Javascript:NoPrompt();" />
                                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../images/next_new.gif" OnClick="BtnNextClick"
                                    onMouseOver="roll(this)" onMouseOut="roll(this)" OnClientClick="Javascript:NoPrompt();"
                                    ToolTip="You cannot continue with this question unanswered." />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="HiddenTexts" runat="server">
                    <asp:TextBox ID="txtQuestionNumber" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="txtQuestionID" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="txtQuestionType" runat="server" style="display:none"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtA1" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="txtFileType" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="txtTest" runat="server" style="display:none"></asp:TextBox>
                    <input id="tabNumber" runat="server" type="hidden" />
                    <input id="txtA" runat="server" type="hidden" />
                    <input id="txtTestType" runat="server" type="hidden" />
                    <input id="ContinueTiming" value="1" type="hidden" runat="server" />
                    <input id="requireResponse" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtAction" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtUserTestID" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtQID" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtTimedTestQB" runat="server" type="hidden" />
                    <input id="hdProductId" runat="server" type="hidden" />
                    <input id="hdAction" runat="server" type="hidden" />
                    <input type="button" id="btnHidden" runat="server" onserverclick="btnHiddenQuitClick"  style="display:none" />
                    <br />
                    <br />
                    &nbsp;
                    <input id="txtADA" runat="server" type="hidden" />
                     <input id="txtSecondPerQuestion" runat="server" type="hidden" />
                    <asp:HiddenField ID="timerCount" runat="server" />
                    <asp:HiddenField ID="hdnDivStem" runat="server"/>
                    <asp:HiddenField ID="hdnExplanation" runat="server"/>
                    <asp:HiddenField id="hdnExplanationVisible" runat="server" value="1" />
                </div>
                <script type="text/javascript">
                    var ktabid = "1";
                    var tnumber = document.getElementById('tabNumber').value;
                    var msg = ktabid + " of " + tnumber;
                    if (document.getElementById('PanelExhibit') != undefined) {
                        if (document.getElementById('TabContainer1_TabPanel1_Exhibit1') != undefined) {
                            if (document.getElementById('TabContainer1_TabPanel1_Exhibit1').innerHTML.length != 0) {

                                // document.all.ExhibitPage.innerHTML = msg;
                                if (document.getElementById('imgExhibit') != null) {
                                    document.getElementById('imgExhibit').style.visibility = "visible";
                                }
                            }
                            else {
                                if (document.getElementById('imgExhibit') != null) {
                                    document.getElementById('imgExhibit').style.visibility = "hidden";
                                }
                            }

                        }
                        else {
                            if (document.getElementById('imgExhibit') != null) {
                                document.getElementById('imgExhibit').style.visibility = "hidden";
                            }
                        }
                        CloseClick();

                    }
                    else {
                        if (document.getElementById('imgExhibit') != null) {
                            document.getElementById('imgExhibit').style.visibility = "hidden";
                        }

                    }
                </script>
                   <div id='confirmCustom' style='display: none'>
                 
                    <p class='message'>
                    </p>
            
                </div>
                   <div id='ModalPopupDiv' style='width: 231px; height: 216px; display:none; position: absolute; top: 180px; left: 270px;background-color: #CCCCCC;color: Black;border-style: solid;border-color: #999999;border: 2px solid #000000;padding: 0px 0px 0px 0px;border-width: 1px;'>
                    <div class="popup_Titlebar" id="PopupHeader">
                        <div class="TitlebarLeft">
                            Calculator</div>
                        <div class="TitlebarRight" onclick="javascript:CloseCalPopup();">
                        </div>
                    </div>
                    <div class="popup_Body">
                     
    <script type="text/javascript">
        var Memory = 0;
        var Number1 = "";
        var Number2 = "";
        var NewNumber = "blank";
        var opvalue = "";

        function DisplayCalc(displaynumber) {
            document.getElementById('answer').value = displaynumber;
            AnswerFocus();
        }

        function MemoryClear() {
            Memory = 0;
            document.getElementById('mem').value = "";
            AnswerFocus();
        }

        function MemoryRecall(answer) {
            if (NewNumber != "blank") {
                Number2 += answer;
            } else {
                Number1 = answer;
            }
            NewNumber = "blank";
            DisplayCalc(answer);
        }

        function MemorySubtract(answer) {
            Memory = Memory - eval(answer);
            AnswerFocus();
        }

        function MemoryAdd(answer) {
            Memory = Memory + eval(answer);
            document.getElementById('mem').value = " M ";
            NewNumber = "blank";
            AnswerFocus();
        }

        function ClearCalc() {
            Number1 = "";
            Number2 = "";
            NewNumber = "blank";
            DisplayCalc("");
        }

        function Backspace(answer) {
            answerlength = answer.length;
            answer = answer.substring(0, answerlength - 1);
            if (Number2 != "") {
                Number2 = answer.toString();
                DisplayCalc(Number2);
            } else {
                Number1 = answer.toString();
                DisplayCalc(Number1);
            }
        }

        function CECalc() {
            Number2 = "";
            NewNumber = "yes";
            DisplayCalc("");
        }

        function CheckNumber(answer) {
            if (answer == ".") {
                Number = document.getElementById('answer').value;
                if (Number.indexOf(".") != -1) {
                    answer = "";
                }
            }

            if (NewNumber == "yes") {
                Number2 += answer;
                DisplayCalc(Number2);
            } else {
                if (NewNumber == "blank") {
                    Number1 = answer;
                    Number2 = "";
                    NewNumber = "no";
                } else {
                    Number1 += answer;
                }

                DisplayCalc(Number1);
            }
        }

        function AddButton(x) {
            if (x == 1) EqualButton();
            if (Number2 != "") {
                Number1 = parseFloat(Number1) + parseFloat(Number2);
            }

            NewNumber = "yes";
            opvalue = '+';
            DisplayCalc(Number1);
        }

        function SubButton(x) {
            if (x == 1) EqualButton();
            if (Number2 != "") {
                Number1 = parseFloat(Number1) - parseFloat(Number2);
            }

            NewNumber = "yes";
            opvalue = '-';
            DisplayCalc(Number1);
        }

        function MultButton(x) {
            if (x == 1) EqualButton();
            if (Number2 != "") {
                Number1 = parseFloat(Number1) * parseFloat(Number2);
            }

            NewNumber = "yes";
            opvalue = '*';
            DisplayCalc(Number1);
        }

        function DivButton(x) {
            if (x == 1) EqualButton();
            if (Number2 != "") {
                Number1 = parseFloat(Number1) / parseFloat(Number2);
            }

            NewNumber = "yes";
            opvalue = '/';
            DisplayCalc(parseFloat(Number1.toPrecision(16)));
        }

        function SqrtButton() {
            if (NewNumber == "yes") {
                Number1 = Number2
            }
            Number1 = Math.sqrt(Number1);
            NewNumber = "blank";
            DisplayCalc(Number1);
        }

        function PercentButton() {
            if (NewNumber != "blank") {
                Number2 *= .01;
                NewNumber = "blank";
                EqualButton();
            }
            AnswerFocus();
        }

        function RecipButton() {
            Number1 = 1 / Number1;
            NewNumber = "blank";
            DisplayCalc(Number1);
        }

        function NegateButton() {
            Number1 = parseFloat(-Number1);
            NewNumber = "no";
            DisplayCalc(Number1);
        }

        function EqualButton() {
            if (opvalue == '+') AddButton(0);
            if (opvalue == '-') SubButton(0);
            if (opvalue == '*') MultButton(0);
            if (opvalue == '/') DivButton(0);
            Number2 = "";
            opvalue = "";
            AnswerFocus();
        }

        function AnswerFocus() {
            if (browserCheck.indexOf("chrome") != -1 || browserCheck.indexOf("safari") != -1) {
                calFlag = true;
            }
            var answerText = document.getElementById("answer");
            answerText.focus();
        }
    </script>
    <div id="calculator" style="margin: 0px 0px 0px 0px;">
    <table border="1" cellpadding="0" cellspacing="0" style="width: 232px; height: 179px">
        <tr>
            <td align="center">
                <table border="0" style="width: 95%">
                    <tr>
                        <td colspan="6">
                            <input type="text" id="answer" name="answer" size="30" maxlength="30" onchange="CheckNumber(this.value)"
                                style="width: 215px; height: 23px;" class="inputAnswerText" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="text" id="mem" name="mem" size="3" maxlength="3" style="height: 25px; width: 32px;" readonly="readonly"/>
                        </td>
                        <td colspan="3" align="right">
                            <input type="button" id="backspace" name="backspace" class="red" value="Backspace" onclick="Backspace(document.getElementById('answer').value); return false;"
                                style="width: 85px; margin-right: 2px;" />
                        </td>
                        <td>
                            <input type="button" id="CE" name="CE" class="red" value="CE" onclick="CECalc(); return false;"
                                style="width: 30px; margin-left: 0px;" />
                        </td>
                        <td>
                            <input type="reset" id="C" name="C" class="red" value=" C " onclick="ClearCalc(); return false;"
                                style="margin-right: 4px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="button" id="MC" name="MC" class="red" value="MC" onclick="MemoryClear(); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc7" name="calc7" class="blue" value=" 7 " onclick="CheckNumber('7'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc8" name="calc8" class="blue" value=" 8 " onclick="CheckNumber('8'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc9" name="calc9" class="blue" value=" 9 " onclick="CheckNumber('9'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="divide" name="divide" class="blue1" value=" / " onclick="DivButton(1); return false;" />
                        </td>
                        <td>
                            <input type="button" id="sqrt" name="sqrt" class="blue1" value="sqrt" onclick="SqrtButton(); return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <input type="button" id="MR" name="MR" class="red"1" value="MR" onclick="MemoryRecall(Memory); return false;" />
                        </td>
                        <td class="style1">
                            <input type="button" id="calc4" name="calc4" class="blue" value=" 4 " onclick="CheckNumber('4'); return false;" />
                        </td>
                        <td class="style1">
                            <input type="button" id="calc5" name="calc5" class="blue" value=" 5 " onclick="CheckNumber('5'); return false;" />
                        </td>
                        <td class="style1">
                            <input type="button" id="calc6" name="calc6" class="blue" value=" 6 " onclick="CheckNumber('6'); return false;" />
                        </td>
                        <td class="style1">
                            <input type="button" id="multiply" name="multiply" class="blue1" value=" X " onclick="MultButton(1); return false;" />
                        </td>
                        <td class="style1">
                            <input type="button" id="percent" name="percent" class="blue1" value=" % " onclick="PercentButton(); return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="button" id="MS" name="MS" class="red" value="MS" onclick="MemorySubtract(document.getElementById('answer').value); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc1" name="calc1" class="blue" value=" 1 " onclick="CheckNumber('1'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc2" name="calc2" class="blue" value=" 2 " onclick="CheckNumber('2'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc3" name="calc3" class="blue" value=" 3 " onclick="CheckNumber('3'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="minus" name="minus" class="blue1" value=" - " onclick="SubButton(1); return false;" />
                        </td>
                        <td>
                            <input type="button" id="recip" name="recip" class="blue" value="1/x " onclick="RecipButton(); return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="button" id="Mplus" name="Mplus" class="red" value="M+" onclick="MemoryAdd(document.getElementById('answer').value); return false;" />
                        </td>
                        <td>
                            <input type="button" id="calc0" name="calc0" class="blue" value=" 0 " onclick="CheckNumber('0'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="negate" name="negate" class="blue1" value="+/- " onclick="NegateButton(); return false;" />
                        </td>
                        <td>
                            <input type="button" id="dot" name="dot" class="blue" value=" . " onclick="CheckNumber('.'); return false;" />
                        </td>
                        <td>
                            <input type="button" id="plus" name="plus" class="blue1" value=" + " onclick="AddButton(1); return false;" />
                        </td>
                        <td>
                            <input type="button" id="equal" name="equal" class="blue1" value=" = " onclick="EqualButton(); return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
<%--  </div>--%>
   <div id="popContent" style="display:none; overflow:hidden;" >
<asp:HtmlIframe frameborder='0' width='100%' height='100%' id='popupFrame' runat="server"></asp:HtmlIframe>   
    </div>
    <asp:HiddenField ID="hfHeight" runat="server" />
     <asp:HiddenField id="hdnHasAccessToSM" runat="server"/>
    </form>
</body>
</html>
