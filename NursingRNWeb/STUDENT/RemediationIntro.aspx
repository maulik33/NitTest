<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="RemediationIntro.aspx.cs"
    Inherits="STUDENT.RemediationIntro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/STUDENT/ASCX/AlternateIntro.ascx" TagPrefix="ucALternateIntro"
    TagName="AlternateIntro" %>
<html>
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/timer.js"></script>
    <script type="text/javascript" src="../js/main1.js"></script>
    <script type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../js/js-security.js"></script>
    <script src="../JS/jquery-1.4.3.min.js" type="text/javascript"></script>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <script type="text/javascript">

        $(document).keydown(function (e) {
            var element = e.target.nodeName.toLowerCase();
            if ((e.keyCode == 18 && e.keyCode == 9) || (e.keyCode == 17 && e.keyCode == 9) || (e.keyCode == 16 && e.keyCode == 9) || (e.keyCode == 18) || (e.keyCode == 17) || (e.keyCode == 9) || (e.keyCode == 115) || (e.keyCode == 16)) {
                return false;
            }
            if (e.keyCode == 27) {
                CloseModelPopup();
            }
        });

        
    </script>
    <script type="text/javascript">
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

        function EnableNextButtonexp() {
            objButton = document.getElementById('btnNext');
            if (IsAudioPlayCompleted) objButton.disabled = false;
            return;
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
            var UserTestID = document.getElementById('txtRemID').value;

            ////PageMethods.saveRemediationTime(vAction, vTimer, QID, UserTestID);
        }
    </script>

    <script type="text/javascript">
        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;
            //alert("keycode: " + keycode);
            /////////////////////////////////////////////////radio button
            if (keycode == 49 || keycode == 97) {
                //alert("A pressed");
                if (document.all.RB_1 != undefined) {
                    document.all.RB_1.checked = true;
                    EnableNextButton(1, '');
                }

            }

            if (keycode == 50 || keycode == 98) {
                //alert("B pressed");
                if (document.all.RB_2 != undefined) {
                    document.all.RB_2.checked = true;
                    EnableNextButton(2, '');
                }
            }

            if (keycode == 51 || keycode == 99) {
                if (document.all.RB_3 != undefined) {
                    document.all.RB_3.checked = true;
                    EnableNextButton(3, '');
                }
            }

            if (keycode == 52 || keycode == 100) {
                if (document.all.RB_4 != undefined) {
                    document.all.RB_4.checked = true;
                    EnableNextButton(4, '');
                }
            }

            if (keycode == 53 || keycode == 101) {
                if (document.all.RB_5 != undefined) {
                    document.all.RB_5.checked = true;
                    EnableNextButton(5, '');
                }
            }

            if (keycode == 54 || keycode == 102) {
                if (document.all.RB_6 != undefined) {
                    document.all.RB_6.checked = true;
                    EnableNextButton(6, '');
                }
            }

            //////////////////////////////////check box


            if (keycode == 49 || keycode == 97) {
                if (document.all.CH_1 != undefined) {
                    document.all.CH_1.checked = true;
                    EnableNextButton(1, '');
                }

            }

            if (keycode == 50 || keycode == 98) {
                if (document.all.CH_2 != undefined) {
                    document.all.CH_2.checked = true;
                    EnableNextButton(2, '');
                }
            }

            if (keycode == 51 || keycode == 99) {
                if (document.all.CH_3 != undefined) {
                    document.all.CH_3.checked = true;
                    EnableNextButton(3, '');
                }
            }

            if (keycode == 52 || keycode == 100) {
                if (document.all.CH_4 != undefined) {
                    document.all.CH_4.checked = true;
                    EnableNextButton(4, '');
                }
            }

            if (keycode == 53 || keycode == 101) {
                if (document.all.CH_5 != undefined) {
                    document.all.CH_5.checked = true;
                    EnableNextButton(5, '');
                }
            }

            if (keycode == 54 || keycode == 102) {
                if (document.all.CH_6 != undefined) {
                    document.all.CH_6.checked = true;
                    EnableNextButton(6, '');
                }
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
</head>
<body id="body" runat="server" onselectstart="return false" onpaste="return false"
    oncopy="return false;" oncut="return false;">
    <form id="myForm" runat="server">
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
                                <asp:Label ID="lblTestName" runat="server" Text="TestName"></asp:Label>
                            </td>
                            <td align="right">
                                <table width="300" border="0" cellspacing="0" cellpadding="0" style="color: #ffffff;
                                    font-size: 14px;">
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
                    <img alt="" src="../images/cal.gif" runat="server" onclick="window.open('cal.html','cal','width=230,height=190,left=270,top=180')"
                        onmouseover="roll(this)" onmouseout="roll(this)" border="0" id="ibCalc" style="margin-top: 3px;" />
                </div>
                <div id="question_main" runat="server" class="med_main_q_new">
                    <div id="D_QID" runat="server">
                    </div>
                    <div style="text-align: left">
                        <asp:Literal ID="Literal_Player" runat="server"></asp:Literal>
                    </div>
                    <div id="Explanation_Div" runat="server" visible="false">
                        <asp:LinkButton ID="lnkExplanation" runat="server" OnClick="LnkExplanationClick">View Explanation</asp:LinkButton><br />
                        <br />
                        <div id="Explanation" runat="server" class="med_question">
                        </div>
                    </div>
                    <div id="remediation" runat="server" class="med_question">
                        <asp:Label ID="Label1" runat="server" Text="Topic Review" Font-Bold="True"></asp:Label><br />
                        <br />
                        <%--////To remove the commented line once Gokul confirms the UI without Lippincott
                        <asp:PlaceHolder ID="Lippincott" runat="server"></asp:PlaceHolder>--%>
                        <div id="Lippincott" runat="server" />
                    </div>
                </div>
                <div id="med_bot_q_new">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ctrltable">
                        <tr>
                            <td align="left" style="padding-left: 2px;">
                                <a href="testreview.aspx"></a>
                                <asp:ImageButton ID="btnQuit" runat="server" ImageUrl="../images/quit_new.gif" OnClick="BtnQuitClick"
                                    onMouseOver="roll(this)" onMouseOut="roll(this)" />
                            </td>
                            <td align="right" style="padding-right: 0px;">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/lwww.jpg" ImageAlign="Left"
                                    Visible="False" />
                                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../images/backNav_new.gif"
                                    OnClick="BtnBackClick" AlternateText="Previous" onMouseOver="roll(this)" onMouseOut="roll(this)" />
                                <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../images/next_new.gif" OnClick="BtnNextClick"
                                    onMouseOver="roll(this)" onMouseOut="roll(this)" />
                            </td>
                        </tr>
                    </table>
                </div>
                   <div id="intro_main" runat="server" class="med_main_q_new">
                    <div id="intro_submain" runat="server" class="med_center" style="height: 115px">
                        <div runat="server" id="intro_title">
                        </div>
                        <div runat="server" class="med_question" id="intro_text">
                        </div>
                        <div id="intro_button" runat="server">
                            <asp:ImageButton ID="ibIntro_S" runat="server" ImageUrl="~/Images/btn_end.gif"
                                                      OnClick="IbIntroSClick"  onmouseover="roll(this)" onmouseout="roll(this)" AlternateText="Start Test" >
                            </asp:ImageButton>
                        </div>
                    </div>
                </div>
                <div id="HiddenTexts" runat="server">
                    <asp:TextBox ID="txtRemediationNumber" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="txtQuestionID" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtQuestionType" runat="server" Visible="False"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtA1" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtFileType" runat="server" Visible="False"></asp:TextBox>
                    <input id="tabNumber" runat="server" type="hidden" />
                    <input id="txtA" runat="server" type="hidden" />
                    <input id="txtTestType" runat="server" type="hidden" />
                    <input id="ContinueTiming" value="1" type="hidden" runat="server" />
                    <input id="requireResponse" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtAction" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtRemID" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtQID" runat="server" type="hidden" style="width: 733px" />
                    <input id="txtTimedTestQB" runat="server" type="hidden" />
                    <input id="hdProductId" runat="server" type="hidden" />
                    <input id="hdAction" runat="server" type="hidden" />
                    <br />
                    <br />
                    &nbsp;
                    <input id="txtADA" runat="server" type="hidden" />
                    <asp:HiddenField ID="timerCount" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
