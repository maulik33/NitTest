<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.IntroError" Codebehind="IntroError.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/timer.js"></script>

    <script type="text/javascript" src="../js/main1.js"></script>

    <script type="text/javascript" src="../js/js-security.js"></script> 
</head>
<body>
     <table align="center">
        <tr>
            <td>
                <form id="myForm" runat="server">
                        <input id="txtADA" runat="server" type="hidden" />
                        <input id="requireResponse" runat="server" type="hidden" style="width: 733px" />
                        <input id="ContinueTiming" value="1" type="hidden" runat="server" />
                        <input id="txtTestType" runat="server" type="hidden" />
                        <input id="txtA" runat="server" type="hidden" />
                        <input id="tabNumber" runat="server" type="hidden" />
                    <input id="remaining" runat="server" type="hidden" />
                    <div id="med_header_q" style="text-align: right; padding-right: 10px;">
                        &nbsp; &nbsp;<asp:Label ID&nbsp; &nbsp;<asp:Label ID="lblTestName" runat="server" Text="TestName" Width="600px"
                            CssClass="testname"></asp:Label>
                    </div>
                    <div id="med_menu_qq">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ctrltable1">
                            <tr>
                                <td align="left" style="padding-top: 3px; height: 31px;">
                                    <asp:ImageButton ID="btnQuit" runat="server" ImageUrl="../images/quit.gif" OnClick="btnQuit_Click"
                                        onMouseOver="roll(this)" onMouseOut="roll(this)" />
                                  </td>
                                <td align="right">
                                    <table width="50%" border="0" cellspacing="0" cellpadding="3">
                                        <tr>
                                            <td align="right">
                                                <input id="timer_up" type="hidden" runat="server" /></td>
                                            <td width="5%">
                                                <div id="timer" style="margin-top: 5px;">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="MessageRetire" runat="server"  class="med_main_q">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" 
                            ForeColor="Red" Text="No questions are available for the test."></asp:Label>
                    </div>
                    
                   
                    <div id="med_bot_q">
                    </div>
                </form>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        var ktabid = "1";
        var tnumber = document.all.tabNumber.value;
        var msg = ktabid + " of " + tnumber;
        if (document.all.PanelExhibit != undefined) {
            if (document.all.TabContainer1_TabPanel1_Exhibit1 != undefined) {


                if (document.all.TabContainer1_TabPanel1_Exhibit1.innerHTML.length != 0) {

                    document.all.ExhibitPage.innerHTML = msg;
                    if (document.all.imgExhibit != null) {
                        document.all.imgExhibit.style.visibility = "visible";
                    }


                }
                else {
                    if (document.all.imgExhibit != null) {
                        document.all.imgExhibit.style.visibility = "hidden";
                    }
                }

            }
            else {
                if (document.all.imgExhibit != null) {
                    document.all.imgExhibit.style.visibility = "hidden";
                }
            }
            CloseClick();

        }
        else {
            if (document.all.imgExhibit != null) {
                document.all.imgExhibit.style.visibility = "hidden";
            }

        }





</script>
</body>
</html>
