<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCohortByTest" CodeBehind="ReportCohortByTest.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            var selectMode = document.getElementById('<%=hdnMode.ClientID%>').value;

            if (selectMode == "1") {
                ExpandContextMenu(1, 'ctl00_tdReportCohortByTest');
            }
            else {
                ExpandContextMenu(2, 'ctl00_tdReportCohortByTest1');
            }

        });

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var lbxGroup = document.getElementById('<%=lbxGroup.ClientID %>');
            var lbxProducts = document.getElementById('<%=lbxProducts.ClientID %>');
            var lbTests = document.getElementById('<%=lbxTests.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (lbxCohort != null && (lbxCohort.options.selectedIndex == -1
        || lbxCohort.options.selectedIndex == 0)) {
                alert('Select atleast one Cohort');
                return false;
            }

            if (lbxProducts != null && (lbxProducts.options.selectedIndex == -1
        || lbxProducts.options.selectedIndex == 0)) {
                alert('Select atleast one Test Type');
                return false;
            }

            if (lbTests != null && (lbTests.options.selectedIndex == -1 || lbTests.options.selectedIndex == 0)) {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Cohort by Test </b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="2">
                            <table width="100%" cellpadding="1" cellspacing="1" border="0">
                               <tr runat="server" id="trProgramofStudy">
                                    <td class="reportParamLabel">
                                       <p> 1. Program of Study <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" ShowNotSelected="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                     <% if (IsProgramofStudyVisible)
                                        { %>
                                       <p> 2. Institution <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span> </p>
                                    <%}
                                        else
                                        { %>
                                       <p> 1. Institution <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span> </p>
                                    <%} %>
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                    <% if (IsProgramofStudyVisible)
                                       { %>
                                      <p> 3. Cohort <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                      <%}
                                       else
                                       { %>
                                         <p> 2. Cohort <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                         <% } %>
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                            Width="275px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel" style="white-space:nowrap;">
                                     <% if (IsProgramofStudyVisible)
                                        { %>
                                      <p> 4. Test Type <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                      <%}
                                        else
                                        { %>
                                       <p> 3. Test Type <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                       <%} %>
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxProducts" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                            Width="275px">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                     <% if (IsProgramofStudyVisible)
                                        { %>
                                       <p>5. Group</p>
                                       <%}
                                        else
                                        { %>
                                        <p>4. Group</p>
                                        <% } %>
                                    </td>
                                    <td class="reportParamLabel">
                                        <KTP:KTPListBox ID="lbxGroup" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                            Width="275px">
                                        </KTP:KTPListBox>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel" style="white-space:nowrap;">
                                     <% if (IsProgramofStudyVisible)
                                        { %>
                                       <p>6. Test Name <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                      <%}
                                        else
                                        { %>
                                        <p>5. Test Name <span  style="color:red;font-weight:bold;position:relative;top:4px;left:1px;">*</span></p>
                                        <% } %>
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxTests" runat="server" SelectionMode="Multiple" Width="275px">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClientClick="return Validate();" OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrintPDF_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClick="btnPrintExcel_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClick="btnPrintExcelDataOnly_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvCohorts" Width="100%" runat="server" AutoGenerateColumns="False"
                                DataKeyNames="TestID,ProductID,CohortId,GroupID" OnRowCommand="gvCohorts_RowCommand"
                                CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting" OnRowDataBound="gvCohorts_RowDataBound">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="CohortName" HeaderText="Cohort" SortExpression="CohortName" />                                    
                                    <asp:TemplateField HeaderText="Test Name" SortExpression="TestName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbuttonPerformance" runat="server" Text='<%# Eval("TestName") %>'
                                                ToolTip="Go to Cohort Performance Report.">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NStudents" HeaderText="# of Students" SortExpression="NStudents" />
                                    <asp:BoundField HeaderText="% Correct" DataField="Percentage" SortExpression="Percentage" />
                                    <asp:BoundField HeaderText="Normed % Correct" DataField="NormedPercCorrect"  DataFormatString="{0:F1}" SortExpression="NormedPercCorrect" />
                                    <asp:ButtonField CommandName="Questions" Text="Test Results by Question" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
        OnClick="btnPrint_Click" Visible="False" />
    <input type="hidden" id="hdnPreviousPage" value="/AdminHome.aspx" />
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestsRemediationByStudent.rpt">
        </Report>
    </CR:CrystalReportSource>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="TestName|ASC" />
</asp:Content>
