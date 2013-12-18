<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ReportTestsScheduledbyDate.aspx.cs" Inherits="NursingRNWeb.ADMIN.ReportTestsScheduledbyDate" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div21');
        });
        function Validate() {
            var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var lbxProduct = document.getElementById('<%=lbxProducts.ClientID %>');
              
            if (lbxInstitution != null && lbxInstitution.selectedIndex == '-1' ) {
                alert('Select Institution');
                return false;
            }

            if (lbxProduct != null && (lbxProduct.options.selectedIndex == -1 || lbxProduct.options.selectedIndex == 0)) {
                alert('Select atleast one Test Type');
                return false;
            }

            if (lbxCohort != null && (lbxCohort.options.selectedIndex == -1 || lbxCohort.options.selectedIndex == 0)) {
                alert('Select atleast one Cohort');
                return false;
            }

            return true;
        }
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Tests Scheduled by Date</b>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="headfont">
                <asp:Label ID="lblErrorMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td>
                            <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <tr runat="server" id="trProgramofStudy">
                                    <td class="reportParamLabel" >
                                        Program of Study 
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true"  ShowNotSelected="false"></KTP:KTPDropDownList> 
                                    </td>
                                 </tr>
                                <tr>
                                    <td class="reportParamTopLabel" >
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbxInstitution" runat="server" SelectionMode="Multiple" AutoPostBack="True"
                                            ShowNotSelected="false" ShowSelectAll="true">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel" style="white-space: nowrap;">
                                        Test Type
                                    </td>
                                    <td class="reportParamLabel">
                                        <KTP:KTPListBox ID="lbxProducts" runat="server"  SelectionMode="Multiple"
                                            Width="275px">
                                        </KTP:KTPListBox>
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Cohort
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                            Width="275px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel">
                                        Group
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxGroup" runat="server" SelectionMode="Multiple" Width="275px">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel" style="padding-top: 7px;">
                                        Date From
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                                        <asp:Image ID="imgDateFrom" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel" style="padding-top: 7px;">
                                        Date To
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateTo" runat="server" Style="cursor: text;"></asp:TextBox>
                                        <asp:Image ID="imgDateTo" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClientClick="return Validate();" Style="height: 25px" OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClientClick="return Validate();" OnClick="btnPrintPDF_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcelDataOnly_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvTestSchedule" Width="100%" runat="server" AutoGenerateColumns="False"
                                CellPadding="5" AllowSorting="True" OnSorting="gvTestSchedule_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="InstitutionName" HeaderText="Institution" SortExpression="InstitutionName" />
                                    <asp:BoundField DataField="CohortName" HeaderText="Cohort" SortExpression="CohortName" />
                                    <asp:BoundField DataField="StartDate" HeaderText="Time/Date" SortExpression="StartDate" />
                                    <asp:BoundField DataField="TestType" HeaderText="Test Type" SortExpression="TestType" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression="TestName" />
                                    <asp:BoundField DataField="NumberOfStudents" HeaderText="# of Students" SortExpression="NumberOfStudents" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="CohortName|ASC" />
                </table>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestScheduleByDate.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
