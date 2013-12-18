<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" Inherits="STUDENT.StudentUserHome"
    MaintainScrollPositionOnPostback="true" CodeBehind="user_home.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.4.3.min.js"></script>
    <link href="../css/jquery-ui-1.css" rel="stylesheet" type="text/css" />
    <link href="../css/ui_002.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script src="../JS/google.js" type="text/javascript"></script>
    <script type="text/javascript">
        function keepMeAlive(imgName) {
            myImg = document.getElementById(imgName);

            if (myImg) {
                myImg.src = myImg.src.replace(/\?*?.*$/, '?' + Math.random());
            }
        }

        window.setInterval("keepMeAlive('keepAliveIMG')", 1200000);

        function openPopUp(testId) {

            var NewHeight = screen.availHeight;
            $('#hfHeight').val(NewHeight);
            var winHeight = NewHeight * 0.79;
            $(document).ready(function (e) {
                var retVal = $("#popContent").dialog({ modal: true, width: 719, height: winHeight, resizable: false, draggable: false, position: "245,20" });
            });
        }

        function setPopupTitle(smScreenTitle) {

            $('#ui-dialog-title-popContent').html(smScreenTitle);
        }       
    </script>
</head>
<body onload="init('expend1');">
    <form id="form1" runat="server">
    <img alt="missing" id="keepAliveIMG" width="1" height="1" src="../Images/blank.jpg" />
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 871px">
                <HD:Head ID="Head11" runat="server" />
                <table id="med_main_f" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <table width="94%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        <div id="top_info" runat="server">
                                            
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="94%" border="0" cellspacing="0" cellpadding="3" class="school">
                                <tr>
                                    <td class="med_center_banner2_2">
                                        Nursing School Success
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 30px; text-align: left">
                                        <img src="../images/bull.gif" width="7" height="9" alt="" />
                                        <a href="#" class="s7" onclick="toggle('expend1');">How to Study</a>
                                    </td>
                                </tr>
                                <tr id="expend1">
                                    <td class="poplinks">
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_ORI','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Orientation:</a>
                                        Find out what study skills workshops are available to you.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_CLA','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Classes:</a>
                                        Learn how to make the most of attending classes using techniques such as active
                                        listening.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_TAK','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Taking
                                            Notes:</a> Find out how to take notes in the most effective manner possible.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_STU','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Studying:</a>
                                        Learn how to study in the manner that works best for you.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_REM','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Remembering:</a>
                                        Find out what you can do to improve your retention of information.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_REA','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Reading
                                            Textbooks:</a> Learn how to read your textbooks efficiently.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_TIM','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Time
                                            Management:</a> Learn how to make the best use of your time.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_STR','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Stress
                                            Reduction:</a> Learn about ways you can work through stressful situations.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_PRE','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Preparing
                                            for the Exam:</a> Learn ways to help you get ready for success on exams.<br />
                                        <img src="../images/bluear.gif" alt="" />
                                        &nbsp;<a href="#" class="s9" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=INTTEST_BIB','Nursing','width=700,height=500,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Bibliography:</a>
                                        Find the sources for the study skills workshops.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 30px; text-align: left">
                                        <img src="../images/bull.gif" width="7" height="9" alt="" />
                                        <a href="#" class="s7" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXLODW09','Nursing','width=750,height=525,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">
                                            Dosage & Calculation Workshop</a>
                                    </td>
                                </tr>
                            </table>
                            <!-- section D -->
                            <br />
                            <table width="94%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 20px;
                                margin-left: 10px;">
                                <tr>
                                    <td valign="top" style="width: 50%">
                                        <div id="session_left_top" runat="server">
                                            <div style="padding-top: 45px; padding-left: 25px; text-align: left">
                                                <img src="../images/bull.gif" width="7" height="9" alt="" />
                                                <asp:LinkButton ID="lb_IT" runat="server" OnClick="lb_IT_Click" Width="150px" CssClass="s7">Take Available Tests</asp:LinkButton><br />
                                                <img src="../images/bull.gif" width="7" height="9" alt="" />
                                                <asp:LinkButton ID="lbl_R_I" runat="server" OnClick="lbl_R_I_Click" Width="154px"
                                                    CssClass="s7">Review Results</asp:LinkButton><br />
                                            </div>
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <div id="session_right_top">
                                            <div style="padding-top: 45px; padding-left: 25px; text-align: left">
                                                <img src="../images/bull.gif" width="7" height="9" alt="" />
                                                <asp:LinkButton ID="lbFRTest" runat="server" OnClick="lbFRTest_Click" CssClass="s7">Take Available Tests</asp:LinkButton><br />
                                                <img src="../images/bull.gif" width="7" height="9" alt="" />
                                                <asp:LinkButton ID="lbCreateFRQBankTest" runat="server" OnClick="lbCreateFRQBankTest_Click"
                                                    CssClass="s7">Search for Questions by Topic</asp:LinkButton><br />
                                                <img src="../images/bull.gif" width="7" height="9" alt="" />
                                                <asp:LinkButton ID="lbFRCreateOwnTest" runat="server" OnClick="lbFRCreateOwnTest_Click"
                                                    CssClass="s7">Search for Remediations by Topic</asp:LinkButton><br />
                                                <img src="../images/bull.gif" width="7" height="9" alt="bull" />
                                                <asp:LinkButton ID="lbl_R_F" runat="server" OnClick="lbl_R_F_Click" CssClass="s7">Review Test Results</asp:LinkButton></div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 50%">
                                        <div id="session_left_bottom">
                                            <div id="CaseID" style="padding-top: 45px; padding-left: 25px; text-align: left"
                                                runat="server">
                                                <asp:Panel CssClass="headerCaseStudy" runat="server" ID="pnlCaseStudy">
                                                <div>
                                                    <b>Case Studies&nbsp;</b>
                                                </div>
                                                <KTP:KTPDropDownList ID="ddCaseID" runat="server" Width="223px" CssClass="studenthome_cr_select"
                                                    OnSelectedIndexChanged="ddCaseID_SelectedIndexChanged" AutoPostBack="True">
                                                </KTP:KTPDropDownList>
                                                </asp:Panel>
                                            </div>
                                            <div id="SkillsID" style="padding-top: 15px; padding-left: 25px; text-align: left"
                                                runat="server">
                                                <b>Essential Nursing Skills&nbsp;</b>
                                                <div>
                                                    <KTP:KTPDropDownList ID="ddSkills" runat="server" Width="223px" CssClass="studenthome_cr_select"
                                                        OnSelectedIndexChanged="ddSkills_SelectedIndexChanged" AutoPostBack="True" CausesValidation="True">
                                                    </KTP:KTPDropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="session_right_bottom">
                                            <div style="padding-top: 45px; padding-left: 25px; text-align: left">
                                                <asp:Image ID="image7" runat="server" ImageUrl="../images/bull.gif" Visible="False" />
                                                <asp:HyperLink ID="lbl_Workshop" runat="server" Width="154px" CssClass="s7" NavigateUrl="#"
                                                    onClick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAVTTW','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')"
                                                    Visible="False">Test Taking Workshop</asp:HyperLink>
                                                <asp:Image ID="imagez1" runat="server" ImageUrl="../images/bull.gif" />
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="s7" Text="NCLEX Review"
                                                     OnClick="LinkButton2_Click">
                                                </asp:LinkButton><br />
                                                <asp:Image ID="image1" runat="server" ImageUrl="../images/bull.gif" />
                                                <asp:LinkButton ID="lbl_QBank" runat="server" OnClick="lbl_QBank_Click" 
                                                    CssClass="s7">Qbank</asp:LinkButton><br /><asp:Panel ID="pnQuestionTrainer" runat="server" Visible="false">
                                                <asp:Image ID="image2" runat="server" ImageUrl="../images/bull.gif" />
                                                <asp:LinkButton ID="lbl_QT" runat="server" OnClick="lbl_QT_Click" CssClass="s7" >Question Trainer Tests</asp:LinkButton><br /></asp:Panel>
                                                <asp:Image ID="image3" runat="server" ImageUrl="../images/bull.gif" Visible="False" />
                                                <asp:HyperLink ID="lnk_RC" runat="server" CssClass="s7" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV02','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')"
                                                     NavigateUrl="#" Visible="False">Review of Content</asp:HyperLink>
                                                <asp:Image ID="image4" runat="server" ImageUrl="../images/bull.gif" Visible="False" />
                                                <asp:HyperLink ID="lnk_QR" runat="server" CssClass="s7" NavigateUrl="#" 
                                                    onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV03','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')"
                                                    Visible="False">Review of Questions</asp:HyperLink>
                                                <asp:Image ID="image5" runat="server" ImageUrl="../images/bull.gif" Visible="False" />
                                                <asp:HyperLink ID="lnk_video" runat="server" CssClass="s7" NavigateUrl="#" 
                                                    onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV05','Nursing','width=600,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')"
                                                    Visible="False">Video Clips</asp:HyperLink>
                                             </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    <div id="popContent" style="display: none">
        <iframe frameborder='0' width='100%' height='100%' id='popupFrame' runat="server">
        </iframe>
    </div>
    <asp:HiddenField ID="hfHeight" runat="server" />
    </form>
</body>
</html>
