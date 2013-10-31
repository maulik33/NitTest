<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.STUDENT_CaseStudies" Codebehind="CaseStudies.aspx.cs" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <![if !IE]>
    <link href="../css/not_ie.css" rel="stylesheet" type="text/css" />
    <![endif]>
    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../js/js-security.js"></script>
    <script src="../JS/google.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head1" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                            Case Studies</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0" alt=""/></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0" alt="" /></a>
                        </div>
                        <div id="med_center_banner2_l">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt=""/>
                            Case Studies
                        </div>
                        <br />
                        <asp:PlaceHolder ID="tbCase" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
