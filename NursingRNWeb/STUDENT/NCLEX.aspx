<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true"
    Inherits="STUDENT.Nclex" Codebehind="Nclex.aspx.cs" %>

<%@ Register TagPrefix="ucHead" TagName="NursingHeader" Src="~/Student/ASCX/head.ascx" %>
<html>
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/main.js"></script>
    <script src="../JS/google.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <ucHead:NursingHeader ID="StudentHeader" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                           NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName)%> &reg; Prep</h2>
                        <br />
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0" alt="" /></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0" alt="" /></a></div>
                        <div id="med_center_banner2_l" runat="server">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt="" />NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName)%> &reg;
                            Qbank
                        </div>
                        <div runat="server" id="FirstGo" style="margin-top: 0px;">
                            <asp:Table Style="clear: both;" ID="Table1" runat="server" Width="100%" BackColor="#ECE9D8"
                                CellSpacing="1" CssClass="nctable" CellPadding="2">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server" BackColor="#ffffff">Qbank</asp:TableCell>
                                    <asp:TableCell runat="server" Width="50%" HorizontalAlign="Right" BackColor="#ffffff">
                                        <asp:LinkButton CssClass="s2" ID="Lb11" CommandName="Go_1_1" OnClick="LbNclexClick"
                                            runat="server" Text="Go" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server" BackColor="#eeeeee">Qbank Sample Tests</asp:TableCell>
                                    <asp:TableCell runat="server" Width="50%" HorizontalAlign="Right" BackColor="#eeeeee">
                                        <asp:LinkButton CssClass="s2" ID="Lb12" CommandName="Go_1_2" OnClick="LbNclexClick"
                                            runat="server" Text="Go" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div id="med_center_banner2_l_1" runat="server" class="med_center_banner2_l">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt="" />Question Trainer
                            Tests
                        </div>
                        <asp:Table Style="clear: both;" ID="Table2" runat="server" Width="100%" BackColor="#ECE9D8"
                            CellSpacing="1" CssClass="nctable" CellPadding="2">
                            <asp:TableRow ID="TableRow1" runat="server">
                                <asp:TableCell BackColor="#ffffff" ID="TableCell1" Width="50%" HorizontalAlign="Left"
                                    runat="server">Question Trainer</asp:TableCell>
                                <asp:TableCell BackColor="#ffffff" ID="TableCell2" Width="50%" HorizontalAlign="Right"
                                    runat="server">
                                    <asp:LinkButton CssClass="s2" ID="Lb21" CommandName="Go_2_1" OnClick="LbNclexClick"
                                        runat="server">Go</asp:LinkButton></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <div class="med_center_banner2_l">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt="" />
                            NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName)%> &reg; Review</div>
                        <asp:PlaceHolder ID="AvpItems" runat="server"></asp:PlaceHolder>
                        <div class="med_center_banner2_l">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt="" />
                            NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName)%> &reg; Exam Tests</div>
                        <asp:Table Style="clear: both;" ID="Table4" runat="server" Width="100%" BackColor="#ECE9D8"
                            CellSpacing="1" CssClass="nctable" CellPadding="2">
                            <asp:TableRow ID="TableRow6" runat="server" BackColor="White">
                                <asp:TableCell BackColor="White" ID="TableCell11" runat="server">Diagnostic</asp:TableCell>
                                <asp:TableCell BackColor="White" ID="TableCell12" runat="server" Width="50%" HorizontalAlign="Right">
                                    <asp:LinkButton CssClass="s2" ID="Lb41" CommandName="Go_4_1" OnClick="LbNclexClick"
                                        runat="server">Go</asp:LinkButton></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow7" runat="server" BackColor="#ECE9D8" CssClass="nctable">
                                <asp:TableCell BackColor="#EEEEEE" ID="TableCell13" runat="server">Readiness</asp:TableCell>
                                <asp:TableCell BackColor="#EEEEEE" ID="TableCell14" runat="server" Width="50%" HorizontalAlign="Right">
                                    <asp:LinkButton CssClass="s2" ID="Lb42" CommandName="Go_4_2" OnClick="LbNclexClick"
                                        runat="server">Go</asp:LinkButton></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    <asp:Table Style="clear: both" ID="Table3Sdsdsd" class="nctable" runat="server" CellPadding="2"
        CellSpacing="1" BackColor="#ECE9D8" Width="100%" Visible="False">
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell BackColor="White" ID="TableCell3" runat="server">Test Taking Workshop</asp:TableCell>
            <asp:TableCell BackColor="White" ID="TableCell4" runat="server" Width="50%" HorizontalAlign="Right">
                <asp:HyperLink ID="LblWorkshop" runat="server" Width="154px" CssClass="s2" NavigateUrl="#"
                    onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV11','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Go</asp:HyperLink></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server" BackColor="#ECE9D8" CellSpacing="1" class="nctable"
            CellPadding="2">
            <asp:TableCell BackColor="#EEEEEE" ID="TableCell5" runat="server">Review of Content</asp:TableCell>
            <asp:TableCell BackColor="#EEEEEE" ID="TableCell6" runat="server" Width="50%" HorizontalAlign="Right">
                <asp:HyperLink ID="LnkRc" runat="server" CssClass="s2" onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV02','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')"
                    Width="154px" NavigateUrl="#">Go</asp:HyperLink></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server" BackColor="#ECE9D8" CellSpacing="1" class="nctable"
            CellPadding="2">
            <asp:TableCell BackColor="White" ID="TableCell7" runat="server">Review of Questions</asp:TableCell>
            <asp:TableCell BackColor="White" ID="TableCell8" runat="server" Width="50%" HorizontalAlign="Right">
                <asp:HyperLink ID="LnkQr" runat="server" CssClass="s2" NavigateUrl="#" Width="154px"
                    onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV03','Nursing','width=750,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Go</asp:HyperLink></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" runat="server" BackColor="#ECE9D8" CellSpacing="1" class="nctable"
            CellPadding="2">
            <asp:TableCell BackColor="#EEEEEE" ID="TableCell9" runat="server">Video Clips</asp:TableCell>
            <asp:TableCell BackColor="#EEEEEE" ID="TableCell10" runat="server" Width="50%" HorizontalAlign="Right">
                <asp:HyperLink ID="LnkVideo" runat="server" CssClass="s2" NavigateUrl="#" Width="154px"
                    onclick="window.open('http://www.kaplanlogin.com/dl/ReceiveCustomer.asp?productCode=NCLEXAV05','Nursing','width=600,height=550,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no')">Go</asp:HyperLink></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </form>
</body>
</html>
