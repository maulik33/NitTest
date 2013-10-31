<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportPerformanceByQuestion" Title="Summary Performance by Question Report"
    CodeBehind="ReportPerformanceByQuestion.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(5, 'ctl00_tdReportPerformanceByQuestion');
        });

        function Validate() {
            var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');
            var ddTest = document.getElementById('<%=ddTests.ClientID %>');


            if (lbxInstitution.options.selectedIndex == -1) {
                alert('Select Institution');
                return false;
            }

            if (lbxCohort.options.selectedIndex == -1 || lbxCohort.options.selectedIndex == 0) {
                alert('Select atleast one Cohort');
                return false;
            }

            if (ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select atleast one Test Type');
                return false;
            }

            if (ddTest.options[ddTest.selectedIndex].value == '-1') {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }

    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Student Performance by Question&nbsp;</b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td align="left">
                            <table width="100%" >
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
                                            ShowNotSelected="false" SelectionMode="Multiple" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Cohort
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            SelectionMode="Multiple" width="275px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowSelectAll="true"
                                            ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Name
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddTests" runat="server" ShowSelectAll="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClientClick="return Validate();" OnClick="ImageButton3_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblN" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                            <asp:GridView ID="gvResult" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                CellPadding="5" OnSorting="gvResult_Sorting">
                                <rowstyle cssclass="datatable2a" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1a" />
                                <columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="Questions" SortExpression="QuestionId" />
                                    <asp:BoundField DataField="Answer" HeaderText="Answer" SortExpression="Answer" />
                                    <asp:BoundField DataField="Total1" HeaderText="Total1" SortExpression="Total1" />
                                    <asp:BoundField DataField="Total2" HeaderText="Total2" SortExpression="Total2" />
                                    <asp:BoundField DataField="Total3" HeaderText="Total3" SortExpression="Total3" />
                                    <asp:BoundField DataField="Total4" HeaderText="Total4" SortExpression="Total4" />
                                    <asp:BoundField DataField="TotalN" HeaderText="Total N" SortExpression="TotalN" />
                                    <asp:BoundField DataField="TotalNumberCorrect" HeaderText="Total # Correct" SortExpression="TotalNumberCorrect" />
                                    <asp:BoundField DataField="TotalNumberWrong" HeaderText="Total # Wrong" SortExpression="TotalNumberWrong" />
                                    <asp:BoundField DataField="CorrectPercent" HeaderText="% Correct" SortExpression="CorrectPercent"
                                        DataFormatString="{0:0.0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </columns>
                            </asp:GridView>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="QuestionId|ASC" />
</asp:Content>
