<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRQBank.aspx.cs" EnableViewState="false" MaintainScrollPositionOnPostback="true"
    Inherits="NursingRNWeb.STUDENT.FRQBank" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="ASCX/FRBank.ascx" TagName="FRBank" TagPrefix="FRBank" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../CSS/front.css" rel="stylesheet" type="text/css" />
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
                            Focused Review > Search for Questions by Topic</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0"></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0"></a></div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear: both;">
                            <tr>
                                <td width="150" valign="top" bgcolor="#F6F6F9">
                                    <div id="med_left_banner1">
                                        Focused Review Navigation</div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lbReview" runat="server" CssClass="s8" onclick="lbReview_Click" >Focused Review Tests</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lbSearchRemediation" runat="server" CssClass="s8" 
                                            onclick="lbSearchRemediation_Click">Search Remediations</asp:LinkButton></div>
                                </td>
                                <td width="8">
                                    &nbsp;
                                </td>
                                <td valign="top" align="left" style="height: 521px">
                                    <div>
                                        <asp:Label runat="server" ID="StagingWarning" Visible="false" ForeColor="Red" Text="Customized Focused Review Tests cannot be created in this environment. Contact Technical Support for further Details.">
                                        </asp:Label>
                                    </div>
                                    <div id="med_center_banner5" style="padding-left: 15px; margin-bottom: 15px;">
                                        Test Style</div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-left: 15px; display: table; clear: both;">
                                                <asp:RadioButtonList ID="rblTutorMode" runat="server" RepeatLayout="Flow" Width="501px">
                                                    <asp:ListItem Value="0" Selected="True">Timed Test</asp:ListItem>
                                                    <asp:ListItem Value="1">Untimed </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div id="med_center_banner5" style="padding-left: 15px; margin-top: 15px; margin-bottom: 15px;">
                                                Question Reuse Mode</div>
                                            <div style="margin-left: 16px; display: table; clear: both;">
                                                <asp:RadioButtonList ID="rblMode" runat="server" RepeatDirection="Horizontal" Width="500px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Selected="True">Unused Only</asp:ListItem>
                                                    <asp:ListItem Value="2">Unused + Incorrect</asp:ListItem>
                                                    <asp:ListItem Value="4">Incorrect Only</asp:ListItem>
                                                    <asp:ListItem Value="3">All Items</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <FRBank:FRBank ID="FRBank" runat="server"/>
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
