<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" EnableViewState="false" Inherits="STUDENT.TestReview" CodeBehind="TestReview.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/main.js"></script>
    <script type="text/javascript" src="../js/js-security.js"></script>
    <script type="text/javascript" src="../JS/google.js"></script>
    <script type="text/javascript" src="../JS/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <script type="text/javascript">

        var selectedTestId;
        var userId;
        function GridViewAllTestDialogBox(lnk, launchProctorTrack) {
            selectedTestId = GetSelectedTestId(lnk, 4);
            if (selectedTestId == 9 || selectedTestId == 504) {
                var msg1 = "This test is 180 questions and approximately 3.5 hours long. Please do not hit the Quit button or exit the test until complete as these actions will immediately close and score your test."
                var msg2 = "Please hit OK to begin this exam. If you would like to exit now and start this test at a later time, please hit Cancel."
                var answer = confirm(msg1 + "\n" + "\n" + msg2);
                if (!answer) {
                    return answer;
                }
            }
            if (launchProctorTrack) {
                __doPostBack('<%= GridViewAllTest.ClientID %>', "launchSecuredTest:" + selectedTestId);
                return false;
            }
        }
        
        function GridViewSuspendedTestDialogBox(lnk, urlString) {
            selectedTestId = GetSelectedTestId(lnk, 2);
           LaunchProctorTrack(urlString, selectedTestId);
            return false;
        }
        
        function GetSelectedTestId(lnk, cellNumber)
        {
            var row = lnk.parentNode.parentNode;
            var elem = row.cells[cellNumber].querySelectorAll(".HdnTestid");
            selectedTestId = elem[0].innerText || elem[0].textContent; //innerText for IE, textContent for FF
            return selectedTestId;

        }

    </script>
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <table id="med_main_f" border="0" cellspacing="18" cellpadding="0">
                    <tr>
                        <td align="center" valign="top">
                            <h2>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h2>
                            <br />
                            <div id="topbutton">
                                <a href="javascript:history.back();">
                                    <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" alt="" border="0" /></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                            src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                            onmouseout="roll(this)" border="0" alt="" /></a>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div id="divFocus" runat="server" style="visibility: hidden; position: absolute">
                                <div id="divCreateTest" class="med_center_banner2_l" runat="server">
                                    <img src="../images/icon_type3.gif" width="13" height="13" alt="" />
                                    FOCUSED REVIEW RESOURCES
                                </div>
                                <div>
                                    <asp:Table Style="clear: both;" ID="Table1" runat="server" Width="100%" BackColor="#ECE9D8"
                                        CellSpacing="1" CssClass="nctable" CellPadding="2">
                                        <asp:TableRow ID="TableRow1" runat="server">
                                            <asp:TableCell ID="TableCell1" runat="server" BackColor="#ffffff">Create Your Own Test</asp:TableCell>
                                            <asp:TableCell ID="TableCell2" runat="server" Width="50%" HorizontalAlign="Right" BackColor="#ffffff">
                                                <asp:LinkButton CssClass="s2" ID="Lb11" runat="server" Text="Go" OnClick="lbCustomTest_Click" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="TableRow2" runat="server">
                                            <asp:TableCell ID="TableCell3" runat="server" BackColor="#eeeeee">Search Remediations</asp:TableCell>
                                            <asp:TableCell ID="TableCell4" runat="server" Width="50%" HorizontalAlign="Right"
                                                BackColor="#eeeeee">
                                                <asp:LinkButton CssClass="s2" ID="Lb12" runat="server" Text="Go" OnClick="lbCreateTest_Click" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </div>
                            <div id="med_center_banner2_l">
                                <img src="../images/icon_type3.gif" width="13" height="13" alt="" />
                                TAKE AVAILABLE TESTS</div>
                            <div id="AvailableStandardTests" class="med_center_banner2_l" style="visibility: hidden;
                                position: absolute" runat="server">
                                <p class="med_Center_banner_P"><b>STANDARD TESTS</b></p>
                            </div>
                            <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewAllTest" runat="server"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="TestId" EmptyDataText="No Test Results"
                                OnRowCommand="GridViewAllTestRowCommand" OnRowDataBound="GridViewAllTestRowDataBound"
                                CellPadding="3" BorderColor="#503792" GridLines="Both">
                                <rowstyle cssclass="Gridrow2" />
                                <headerstyle cssclass="Gridheader" />
                                <alternatingrowstyle cssclass="Gridrow1" />
                                <columns>                   
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" ItemStyle-HorizontalAlign="left" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><asp:Label ID="Label1" Text="Available" runat="server"></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StartDateAll" HeaderText="From" ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="EndDateAll" HeaderText="To" ItemStyle-HorizontalAlign="left" />
                                    <asp:TemplateField ShowHeader="False">  
                                        <ItemTemplate>
                                            <asp:Label ID="HdnTestid" CssClass="HdnTestid"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TestId")%>' style="display:none;" ></asp:Label>
                                            <asp:LinkButton ID="GridViewAllTestLinkButton" runat="server" CausesValidation="false" CommandName="TakeTest" Text="Take the Test"  CommandArgument='<%# Container.DataItemIndex %>' ControlStyle-CssClass="s2"></asp:LinkButton>                                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 </columns>
                            </asp:GridView>
                            <br />
                            <div id="AvailableCustomTests" style="visibility: hidden; position: absolute" runat="server">
                                <div class="med_center_banner2_l">
                                     <p class="med_Center_banner_P"><b>CUSTOM TESTS</b></p></div>
                                <br />
                                <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewAvailableCustomTest"
                                    runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="UserTestId"
                                    EmptyDataText="No Test Results" OnRowCommand="GridViewCustomTestRowCommand" OnRowDataBound="GridViewAllTestRowDataBound"
                                    CellPadding="3" BorderColor="#503792" GridLines="Both">
                                    <rowstyle cssclass="Gridrow2" />
                                    <headerstyle cssclass="Gridheader" />
                                    <alternatingrowstyle cssclass="Gridrow1" />
                                    <columns>
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" ItemStyle-HorizontalAlign="left" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><asp:Label ID="lblStatusVal" Text="Available" runat="server"></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TestStarted" HeaderText="Date & Time" ItemStyle-HorizontalAlign="left" />
                                    <asp:ButtonField Text="Take the Test" CommandName="TakeCustomTest" ControlStyle-CssClass="s2" />
                                </columns>
                                </asp:GridView>
                                <br />
                            </div>
                            <div id="SuspendedTests" class="med_center_banner2_l" runat="server">
                                <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
                                VIEW SUSPENDED TESTS</div>
                            <div id="SuspendedStandardTests" class="med_center_banner2_l" style="visibility: hidden;
                                position: absolute" runat="server">
                                 <p class="med_Center_banner_P"><b>STANDARD TESTS</b></p>
                            </div>
                            <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewSuspendedTests"
                                runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Test Results"
                                DataKeyNames="TestId,UserTestId,SuspendType" OnRowCommand="GridViewSuspendedTestsRowCommand"
                                BorderColor="#503792" GridLines="Both" CellPadding="3" OnRowDataBound="GridViewSuspendedTestsRowDataBound">
                                <columns>
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TestStarted" HeaderText="Date Taken" />
                                     <asp:TemplateField ShowHeader="False">  
                                        <ItemTemplate>
                                            <asp:Label ID="HdnTestidSuspended" CssClass="HdnTestid"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TestId")%>' style="display:none;" ></asp:Label>
                                            <asp:LinkButton ID="GridViewSuspendedTestLinkButton" runat="server" CausesValidation="false" CommandName="Resume" Text="Resume"  CommandArgument='<%# Container.DataItemIndex %>' ControlStyle-CssClass="s2"></asp:LinkButton>                                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </columns>
                                <rowstyle cssclass="Gridrow2" />
                                <headerstyle cssclass="Gridheader" />
                                <alternatingrowstyle cssclass="Gridrow1" />
                            </asp:GridView>
                            <br />
                            <div id="SuspendedCustomizedTests" style="visibility: hidden;position: absolute" runat="server">
                                <div class="med_center_banner2_l">
                                    <p class="med_Center_banner_P"> <b>CUSTOM TESTS</b></p></div>
                                <br />
                            <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewSuspendedCustomTests"
                                runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Test Results"
                                DataKeyNames="TestId,UserTestId,SuspendType" OnRowCommand="GridViewSuspendedCustomRowCommand"
                                BorderColor="#503792" GridLines="Both" CellPadding="3" Visible="false">
                                <columns>
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TestStarted" HeaderText="Date Taken" />
                                    <asp:ButtonField Text="Resume" CommandName="Resume">
                                        <ControlStyle CssClass="s2" />
                                    </asp:ButtonField>
                                </columns>
                                <rowstyle cssclass="Gridrow2" />
                                <headerstyle cssclass="Gridheader" />
                                <alternatingrowstyle cssclass="Gridrow1" />
                            </asp:GridView>
                            <br />
                            </div>
                            <div id="med_center_banner2_l_1" class="med_center_banner2_l">
                                    <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
                                    VIEW TEST RESULTS</div>
                                <div id="TakenStandardTest" class="med_center_banner2_l" style="visibility: hidden;
                                    position: absolute" runat="server">
                                    <p class="med_Center_banner_P"> <b>STANDARD TESTS</b></p>
                                </div>
                                <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewTakenTests"
                                    runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Test Results"
                                    DataKeyNames="TestId,UserTestId,SuspendType" OnRowDataBound="GridViewTakenTestsRowDataBound"
                                    OnRowCommand="GridViewTakenTestsRowCommand" BorderColor="#503792" GridLines="Both"
                                    CellPadding="3">
                                    <rowstyle cssclass="Gridrow2" />
                                    <headerstyle cssclass="Gridheader" />
                                    <alternatingrowstyle cssclass="Gridrow1" />
                                    <columns>
                                        <asp:BoundField DataField="TestName" HeaderText="Test Name" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="TestStarted" HeaderText="Date Taken" />
                                        <asp:ButtonField Text="Review Results" CommandName="Review" ControlStyle-CssClass="s2" />
                                    </columns>
                                </asp:GridView>
                                <br />
                                <div id="TakenCustomTest" style="visibility: hidden;position: absolute" runat="server">
                                    <div class="med_center_banner2_l">
                                    <p class="med_Center_banner_P" ><b>CUSTOM TESTS</b></p></div>
                                <br />                            
                                <asp:GridView EnableViewState="false" Style="clear: both;" ID="GridViewTakenCustomTests"
                                    runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Test Results"
                                    DataKeyNames="TestId,UserTestId,SuspendType" OnRowDataBound="GridViewTakenTestsRowDataBound"
                                    OnRowCommand="GridViewTakenCustomTestsRowCommand" BorderColor="#503792" GridLines="Both"
                                    CellPadding="3">
                                    <rowstyle cssclass="Gridrow2" />
                                    <headerstyle cssclass="Gridheader" />
                                    <alternatingrowstyle cssclass="Gridrow1" />
                                    <columns>
                                        <asp:BoundField DataField="TestName" HeaderText="Test Name" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField DataField="TestStarted" HeaderText="Date Taken" />
                                        <asp:ButtonField Text="Review Results" CommandName="Review" ControlStyle-CssClass="s2" />
                                    </columns>
                                </asp:GridView>
                                <br />
                                </div>
                        </td>
                    </tr>
                </table>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
