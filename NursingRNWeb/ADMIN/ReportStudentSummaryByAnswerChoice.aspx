<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportStudentSummaryByAnswerChoice" Title="Student Summary by Answer Choice"
    CodeBehind="ReportStudentSummaryByAnswerChoice.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(5, 'ctl00_tdReportStudentSummaryByAnswerChoice');
        });

        function Validate() {
            var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');
            var ddTest = document.getElementById('<%=ddTest.ClientID %>');

            if (lbxInstitution.options.selectedIndex == 0) {
                alert('Select Institution');
                return false;
            }

            if (lbxCohort != null && lbxCohort.options.selectedIndex == -1) {
                alert('Select atleast one Cohort');
                return false;
            }

            if (ddProducts != null && ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select atleast one Test Type');
                return false;
            }

            if (ddTest != null && ddTest.options[ddTest.selectedIndex].value == '-1') {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }

    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Student Summary By Answer Choice</b><br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep" width="100%">
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
                                        <KTP:KTPListBox ID="lbxInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false"
                                            SelectionMode="Multiple" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Cohort
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            ShowNotSelected="false" SelectionMode="Multiple" Width="275px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowSelectAll="false"
                                            ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamLabel">
                                        Tests
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddTest" runat="server" ShowSelectAll="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" OnClientClick="return Validate();" PostBackUrl="~/ADMIN/ReportStudentSummaryByAnswerChoice.aspx" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr class="datatable2">
                    <td style="text-align: center">
                        &nbsp;
                        <asp:Label ID="lblN" runat="server"></asp:Label>
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Visible="False" CellPadding="5"
                            Width="700px" Style="min-height: 100px; width=100%;">
                            <asp:GridView ID="grvResult" AllowSorting="True" runat="server" Visible="False" Height="100%"
                                OnSorting="grvResult_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
         </tr>
    </table>
</asp:Content>
