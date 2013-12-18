<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="FRRemediation.aspx.cs"
    Inherits="STUDENT.FRRemediation" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="ASCX/FRBank.ascx" TagName="FRBank" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/main.js"></script>
    <script src="../JS/google.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                            Focused Review Tests > Search Remediations by Topic</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    alt="" onmouseout="roll(this)" border="0" /></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        alt="" src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0" /></a></div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear: both;">
                            <tr>
                                <td width="150" valign="top" bgcolor="#F6F6F9">
                                    <!-- left section -->
                                    <div id="med_left_banner1">
                                        REMEDIATION Navigation</div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lb_Create" runat="server" CssClass="s8" OnClick="lb_Create_Click">Search Remediations</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lb_ListReview" runat="server" CssClass="s8" OnClick="lb_ListReview_Click">Review Remediations</asp:LinkButton></div>
                                </td>
                                <td width="8">
                                    &nbsp;
                                </td>
                                <td valign="top" align="left" style="height: 521px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <uc1:FRBank ID="FRBank" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
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
