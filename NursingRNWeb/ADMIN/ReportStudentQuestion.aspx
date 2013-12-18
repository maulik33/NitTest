<%@ Page Language="C#" MasterPageFile="~/Admin/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportStudentQuestions" CodeBehind="ReportStudentQuestion.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function Validate() {
            var ddTests = document.getElementById('<%=ddTests.ClientID%>');

            if (ddTests.options[ddTests.selectedIndex].value == '-1') {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }
        $(document).ready(function () {
            var selectMode = document.getElementById('<%=hdnMode.ClientID%>').value;
            if (selectMode == "1") {
                ExpandContextMenu(3, 'ctl00_tdReportMultiCampusReportCard');
            }
            else {
                ExpandContextMenu(1, 'ctl00_tdReportStudentReportCard');
            }
        });
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Remediation Time by Question Report</b><br />
                <br />
                <asp:Label ID="llsn1" runat="server" Text="Student Name: "></asp:Label>
                <asp:Label ID="lblName" runat="server" Width="155px"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp;
                <asp:LinkButton ID="btn" runat="server" OnClick="btn_Click">Student Performance Report</asp:LinkButton>
                &nbsp; &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            Test Type:&nbsp;<KTP:KTPDropDownList ID="ddProducts" runat="server" ShowSelectAll="false"
                                AutoPostBack="True" />
                            &nbsp;&nbsp;&nbsp; &nbsp;Test Name:
                            <KTP:KTPDropDownList ID="ddTests" runat="server" ShowSelectAll="false" />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" colspan="11">
                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                OnClick="ImageButton2_Click" OnClientClick="return Validate();" />
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                OnClick="ImageButton3_Click" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                OnClick="ImageButton4_Click" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" style="height: 19px">
                            <table align="left" border="0" class="datatable_rep">
                                <tr class="datatable1">
                                    <td class="datatable" nowrap="nowrap" align="left" style="width: 182px">
                                        <b>Total Question:&nbsp; </b>
                                    </td>
                                    <td class="datatable" nowrap="nowrap" align="left">
                                        <b>
                                            <asp:Label ID="lblTotalN" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="datatable" nowrap="nowrap" style="width: 182px">
                                        <b>
                                            <asp:Label ID="Label1" runat="server">Total Time Remediated:</asp:Label>&nbsp;
                                        </b>
                                    </td>
                                    <td class="datatable" nowrap="nowrap" align="left">
                                        <b>
                                            <asp:Label ID="lblTotalR" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvIntegrated" runat="server" AutoGenerateColumns="False" Visible="False"
                                OnRowDataBound="gvIntegrated_RowDataBound" CellPadding="3" AllowSorting="True"
                                OnSorting="gvIntegrated_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="Q.ID" Visible="False" SortExpression="QuestionId" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" Visible="False" SortExpression="TestName" />
                                    <asp:BoundField HeaderText="Correct" DataField="Correct" SortExpression="Correct" />
                                    <asp:BoundField DataField="TopicTitle" HeaderText="Remediation Topic" SortExpression="TopicTitle" />
                                    <asp:BoundField DataField="TimeSpendForRemedation" HeaderText="Second Remediated"
                                        SortExpression="TimeSpendForRemedation" />
                                </Columns>
                            </asp:GridView>
                            &nbsp;
                            <asp:GridView ID="gvFocus" runat="server" AutoGenerateColumns="False" Visible="False"
                                OnRowDataBound="gvFocus_RowDataBound" Width="100%" CellPadding="3" AllowSorting="True"
                                OnSorting="gvFocus_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="Q.ID" Visible="False" SortExpression="QuestionId" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" Visible="False" SortExpression="TestName" />
                                    <asp:BoundField HeaderText="Correct" DataField="Correct" SortExpression="Correct" />
                                    <asp:BoundField DataField="TopicTitle" HeaderText="Remediation Topic" SortExpression="TopicTitle" />
                                    <asp:BoundField DataField="TimeSpendForExplanation" HeaderText="Time Reviewed" SortExpression="TimeSpendForExplanation" />
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvNCLEX" runat="server" AutoGenerateColumns="False" CssClass="GridView1ChildStyle"
                                DataKeyNames="QID,TypeOfFileID,RemediationID,TopicTitle,Correct" Visible="False"
                                OnRowDataBound="gvNCLEX_RowDataBound" AllowSorting="True" OnSorting="gvNCLEX_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="Question ID" SortExpression="QuestionId" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" Visible="False" SortExpression="TestName" />
                                    <asp:BoundField DataField="Correct" HeaderText="Correct" SortExpression="Correct" />
                                    <asp:BoundField DataField="ClientNeeds" HeaderText="Client Needs" SortExpression="ClientNeeds" />
                                    <asp:BoundField DataField="ClientNeedCategory" HeaderText="Client Need Category"
                                        SortExpression="ClientNeedCategory" />
                                    <asp:BoundField DataField="NursingProcess" HeaderText="Nursing Process" SortExpression="NursingProcess" />
                                    <asp:BoundField DataField="Demographic" HeaderText="Demographic" SortExpression="Demographic" />
                                    <asp:BoundField DataField="Concepts" HeaderText="Concepts" SortExpression="Concepts" />
                                    <asp:BoundField DataField="TimeSpendForExplanation" HeaderText="Time Reviewed" SortExpression="TimeSpendForExplanation" />
                                </Columns>
                            </asp:GridView>
                            &nbsp;
                        </td>
                    </tr>
                    <!-- end report body -->
                </table>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        HasCrystalLogo="False" HasDrillUpButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
        Height="812px" Width="1181px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="Report\StudentQuestion.rpt">
        </Report>
    </CR:CrystalReportSource>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="QuestionId|ASC" />
</asp:Content>
