<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportRemediationByTest" Title="Tests Remediation report" CodeBehind="ReportRemediationByTest.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(1, 'ctl00_tdReportRemediationByTest');
        });

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbCohort = document.getElementById('<%=lbCohorts.ClientID %>');
            var ddGroup = document.getElementById('<%=ddGroup.ClientID %>');
            var ddStudent = document.getElementById('<%=ddStudents.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');
            
            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (lbCohort != null && lbCohort.options.selectedIndex == -1 || lbCohort.options.selectedIndex == 0) {
                alert('Select Cohort');
                return false;
            }

            if (ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select Test Type');
                return false;
            }

            if (ddStudent.options[ddStudent.selectedIndex].value == '-1') {
                alert('Select Student');
                return false;
            }

            return true;
        }
    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Remediation Time</b><br/>               
            </td>
        </tr>        
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep" cellpadding="0" width="100%">
                    <tr class="datatable2">
                        <td colspan="2" align="left">
                            <table width="100%">
                             <tr runat="server" id="trProgramofStudy">
                                    <td class="reportParamLabel">
                                        Program of Study
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" ShowNotSelected="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Cohort
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbCohorts" runat="server" AutoPostBack="True" ShowSelectAll="false" SelectionMode="Multiple" ShowNotSelected="true">
                                        </KTP:KTPListBox>
                                      
                                    </td>
                                    <td></td>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Group
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddGroup" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td></td>
                                    <td>
                                        Student
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddStudents" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;                                    
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClientClick="return Validate();" OnClick="btnPDF_Click" />&nbsp;&nbsp;&nbsp;                                  
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            OnClientClick="return Validate();" Style="margin-top: 3px;" OnClick="btnExcel_Click"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            OnClientClick="return Validate();" Style="margin-top: 3px;" OnClick="btnExcelOnlyData_Click"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                    </tr>
                </table>
                <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile this report"
                    Visible="False" Width="347px"></asp:Label>
            </td>
            </tr>
            <tr class="datatable2">
                <td>
                    &nbsp;&nbsp;
                    <asp:GridView ID="grvResult" runat="server" AutoGenerateColumns="False" CellPadding="5"
                        Width="100%" AllowSorting="True" OnSorting="gvResult_Sorting">
                        <columns>
                                    <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression="TestName" />
                                    <asp:BoundField DataField="RemediationOrExplaination" DataFormatString="{0:F2}" HeaderText="Remediation Time"
                                        ReadOnly="True" SortExpression="RemediationOrExplaination" />
                                </columns>
                        <rowstyle cssclass="datatable2a" />
                        <headerstyle cssclass="datatablelabels" />
                        <alternatingrowstyle cssclass="datatable1a" />
                    </asp:GridView>
                    &nbsp;&nbsp;
                    <asp:GridView ID="grvExplanation" runat="server" AutoGenerateColumns="False" Visible="False"
                        Width="100%" AllowSorting="True" OnSorting="grvExplanation_sorting">
                        <rowstyle cssclass="datatable2a" />
                        <columns>
                                    <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression="TestName" />
                                    <asp:BoundField DataField="RemediationOrExplaination" DataFormatString="{0:F2}" HeaderText="Explanation Time"
                                        SortExpression="RemediationOrExplaination" />
                                </columns>
                        <headerstyle cssclass="datatablelabels" />
                        <alternatingrowstyle cssclass="datatable1a" />
                    </asp:GridView>
                </td>
            </tr>
            </table> </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestsRemediationByStudent.rpt">
        </Report>
    </CR:CrystalReportSource>
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
        Visible="False" />
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="CohortName|ASC" />
</asp:Content>
