<%@ Page Language="C#" AutoEventWireup="true" Inherits="STUDENT.Analysis" EnableViewState="false"  Codebehind="Analysis.aspx.cs" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>
    <script src="../JS/google.js" type="text/javascript"></script>  
  
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h2>
                        <br />
                        <div id="topbutton">
                            <a href="student_frt_rep_ana.asp"></a>&nbsp;<asp:ImageButton ID="btnReview" runat="server"
                                ImageUrl="../images/reviewtest.gif" OnClick="btnReview_Click" onmouseover="roll(this)"
                                onmouseout="roll(this)" />&nbsp;&nbsp; <a href="javascript:history.back();">
                                    <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0"></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                            src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                            onmouseout="roll(this)" border="0"></a></div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="8" style="margin-bottom: 8px;
                            clear: both;" bgcolor="E2DDF0">
                            <tr>
                                <td style="width: 29%">
                                    <b>Test Type:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%">
                                    &nbsp;
                                </td>
                                <td width="20%">
                                    <b>Test Name:</b>
                                </td>
                                <td width="54%">
                                    <asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div id="med_center_banner3">
                            OVERALL REPORT</div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="8" style="clear: both;">
                            <tr>
                                <td align="left" colspan="3" rowspan="1" valign="top">
                                    <asp:Label ID="lblPR" runat="server" Font-Bold="True" Text="Percentile Ranking:"></asp:Label>
                                    &nbsp;<asp:Label ID="lblRanking" runat="server" Text="0"></asp:Label>
                                    <br />
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%">
                                    <b>Overall Percent Correct:
                                        <asp:Label ID="lblOPC" runat="server" Text=""></asp:Label></b>
                                    <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="19%" rowspan="4" style="height: 107px">

                                                <script type="text/javascript">

				var fo = new FlashObject("Charts/FC_2_3_MSColumn2D.swf", "FC2Column", "150", "250", "7", "white", true);
				fo.addParam("allowScriptAccess", "always");
				fo.addParam("scale", "noScale");
				fo.addParam("menu", "false");
				fo.addVariable("dataURL", "<%= DataURL%>");
				fo.addVariable("chartWidth","150");
				fo.addVariable("ChartHeight","250");
				fo.write("divchart");
                                                </script>

                                            </td>
                                            <td style="height: 107px; vertical-align: bottom;">
                                                <table border="0" cellspacing="0" cellpadding="4" width="100%" style="margin-bottom: 55px;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <img src="../Temp/images/baricon.gif" width="12" height="12">
                                                            <b>My Overall Correct</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px; width: 118px;">
                                                            Number correct:
                                                        </td>
                                                        <td style="width: 40px; height: 23px;">
                                                            <asp:Label ID="lblNumberCorrect" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 118px">
                                                            Number incorrect:
                                                        </td>
                                                        <td style="width: 40px">
                                                            <asp:Label ID="lblNumberIncorrect" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 118px">
                                                            Number not reached:
                                                        </td>
                                                        <td style="width: 40px">
                                                            <asp:Label ID="lblNotAnswered" runat="server" Width="40px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="height: 107px; vertical-align: bottom;">
                                                <table border="0" cellspacing="0" cellpadding="4" width="100%" style="margin-bottom: 55px;">
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>Answer Changes</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Correct to Incorrect:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblC_I" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Incorrect to Correct:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblI_C" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Incorrect to Incorrect:
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblI_I" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div id="D_Graph" runat="server" style="clear: both;">
                        </div>
                    </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
