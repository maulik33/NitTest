<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.StudentListOfReviews" Codebehind="ListOfReviews.aspx.cs" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
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
                            Test Results</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0" alt="" /></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0" alt="" /></a>
                        </div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="8" style="margin-bottom: 8px;
                            clear: both;" bgcolor="E2DDF0">
                            <tr>
                                <td width="30%">
                                    <b>Choose Test Type</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdProductsSelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%">
                                    &nbsp;
                                </td>
                                <td width="20%">
                                    &nbsp;
                                </td>
                                <td width="54%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div id="med_center_banner2_l">
                            <img src="../images/icon_type3.gif" width="13" height="13" alt="" />
                            TEST ANALYSIS
                        </div>
                        <asp:GridView Style="clear: both;" ID="gvList" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="UserTestId,TestId,QuizOrQBank" OnRowDataBound="gvList_RowDataBound"
                            Width="100%" OnRowCommand="gvList_RowCommand" CellPadding="3" BorderColor="#503792"
                            AllowSorting="True" GridLine="Both" OnSorting="gvList_Sorting">
                            <RowStyle CssClass="Gridrow2" />
                            <HeaderStyle CssClass="Gridheader" />
                            <AlternatingRowStyle CssClass="Gridrow1" />
                            <Columns>
                                <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression="TestName">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TestStarted" SortExpression="TestStarted" HeaderText="Date &amp; Time" />
                                <asp:BoundField HeaderText="% Correct" DataField="PercentCorrect" />
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lb1" runat="server"></asp:LinkButton>
                                        <asp:LinkButton ID="lb2" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TestStatus" HeaderText="Status" />
                                <asp:BoundField HeaderText="# of ?" DataField="QuestionCount" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Table ID="Table5" runat="server" BackColor="#ECE9D8" CellPadding="2" CellSpacing="1"
                            class="nctable" Style="clear: both;" Width="100%">
                        </asp:Table>
                    </div>
                </div>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="TestName|ASC" />
    </form>
</body>
</html>
