<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true" CodeBehind="ReportMultiCampusReportCard.aspx.cs" Inherits="NursingRNWeb.ADMIN.ReportMultiCampusReportCard" title="Multi campus report card"%>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_tdReportMultiCampusReportCard');
        });

        function Validate() {
            var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var lbxStudent = document.getElementById('<%=lbxStudent.ClientID %>');
            var lbxProducts = document.getElementById('<%=lbxProducts.ClientID %>');
            var lbxTests = document.getElementById('<%=lbxTests.ClientID %>');

            if (lbxInstitution.selectedIndex == -1 || lbxInstitution.selectedIndex == 0) {
                alert('Select at least one Institution');
                return false;
            }

            if (lbxCohort.options.selectedIndex == -1 || lbxCohort.options.selectedIndex == 0) {
                alert('Select at least one Cohort');
                return false;
            }

            if (lbxStudent != null && lbxStudent.options.selectedIndex == -1 || lbxStudent.options.selectedIndex == 0) {
                alert('Select at least one Student');
                return false;
            }

            if (lbxProducts != null && (lbxProducts.options.selectedIndex == -1 || lbxProducts.options.selectedIndex == 0)) {
                alert('Select at least one Test Type');
                return false;
            }

            if (lbxTests != null && (lbxTests.options.selectedIndex == -1 || lbxTests.options.selectedIndex == 0)) {
                alert('Select at least one Test');
                return false;
            }

            return true;
        }
        window.onunload = function () {

        }
    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Multi-Campus Report Card</b>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="SubTextClass" align="left">
                Please make sure that you have selected the Institution, Test Type and Cohort. This
                will allow the student list to load. Once loaded either choose the "select all"
                option or pick individual students. Once you have selected the students the list
                of tests will load on the righthand side of the page. Select the test(s) you would
                like to view then press the "submit" button. The student and test lists may take
                some time to load depending on your browser and internet connection speed.<br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td align="left">
                            <table style="width: 100%;">
                              <tr runat="server" id="trProgramofStudy">
                                        <td class="reportParamLabel" >
                                            Program of Study 
                                            
                                        </td>
                                        <td colspan="4"><KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList></td>
                                    </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbxInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel" style="white-space: nowrap;">
                                        Test Type
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbxProducts" runat="server" AutoPostBack="True" Width="300px">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        <span>Cohort</span>
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" Width="300px">
                                        </KTP:KTPListBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel">
                                        Group
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxGroup" runat="server" AutoPostBack="True" Width="260px">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Students
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxStudent" runat="server" AutoPostBack="True" Width="300px">
                                        </KTP:KTPListBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel">
                                        Tests
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxTests" runat="server" Width="260px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();"/>&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrintPDF_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcelDataOnly_Click" OnClientClick="return Validate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile this report"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:GridView ID="grvResult" runat="server" CssClass="GridView1ChildStyle" Width="100%"
                                CellPadding="5" AutoGenerateColumns="False" AllowSorting="True" OnSorting="grvResult_Sorting"
                                OnRowDataBound="grvResult_RowDataBound">
                                <RowStyle CssClass="datatable2a" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="InstitutionName" HeaderText="Institution Name" SortExpression="InstitutionName" />
                                    <asp:TemplateField HeaderText="Last Name" SortExpression="Student.FullName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LastName" Text='<%#DataBinder.Eval(Container.DataItem,"Student.LastName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Name" SortExpression="Student.FirstName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="FirstName" Text='<%#DataBinder.Eval(Container.DataItem,"Student.FirstName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cohort Name" SortExpression="Cohort.CohortName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="CohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Name" SortExpression="Group.GroupName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="GroupName" Text='<%#DataBinder.Eval(Container.DataItem,"Group.GroupName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Type" SortExpression="Product.ProductName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="TestType" Text='<%#DataBinder.Eval(Container.DataItem,"Product.ProductName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Name" SortExpression="TestName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbuttonPerformance" runat="server" Text='<%# Eval("TestName") %>'
                                                ToolTip="Go to Student Performance Report">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuestionCount" HeaderText="Question Count" SortExpression="QuestionCount" />
                                    <asp:BoundField DataField="Correct" HeaderText="% Correct" SortExpression="Correct"
                                        DataFormatString="{0:#0.0}" />
                                    <asp:BoundField DataField="Rank" HeaderText="Percentile Rank" SortExpression="Ranking" />
                                    <asp:BoundField DataField="TestStyle" HeaderText="Test Style" SortExpression="TestStyle" />
                                    <asp:TemplateField HeaderText="Date & Time Taken" SortExpression="TestTaken">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTestTaken" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TimeUsed" HeaderText="Time Used" SortExpression="TimeUsed" />
                                    <asp:TemplateField HeaderText="Remediation Time" SortExpression="RemediationTime">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRemediationTime" runat="server" Text='<%# Eval("remediationTime") %>'
                                                ToolTip="Go to Remediation Time By Question Report">
                                            </asp:LinkButton>
                                            <asp:Label ID="lblNA" runat="server" Text="N/A" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <input id="hdnStudentId" type="hidden" runat="server" value='<%# Eval("Student.StudentId") %>' />
                                            <input id="hdnCohortID" type="hidden" runat="server" value='<%# Eval("Cohort.CohortID") %>' />
                                            <input id="hdnTestTypeId" type="hidden" runat="server" value='<%# Eval("Product.ProductId") %>' />
                                            <input id="hdnTestId" type="hidden" runat="server" value='<%# Eval("UserTestId") %>' />
                                            <input id="hdnTestName" type="hidden" runat="server" value='<%# Eval("TestName") %>' />
                                            <input id="hdnTestTaken" type="hidden" runat="server" value='<%# Eval("TestTaken") %>' />
                                            <input id="hdnInstitutionId" type="hidden" runat="server" value='<%# Eval("InstitutionId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="datatablelabels" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
        Visible="False" />
    <input type="hidden" id="hdnPreviousPage" value="/AdminHome.aspx" />
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Student.FullName|ASC" />
</asp:Content>

