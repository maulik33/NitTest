<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    CodeBehind="ReportEnglishForNursingTracking.aspx.cs" Inherits="NursingRNWeb.ADMIN.ReportEnglishForNursingTracking" %>

<%@ Register Assembly="NursingRNWeb" Namespace="WebControls" TagPrefix="KTP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <script type="text/javascript">

            $(document).ready(function () {
                ExpandContextMenu(1, 'ctl00_tdReportEnglishForNursingTracking');
            });

            function Validate() {
                var lbxInstitution = document.getElementById('<%=lbxInstitution.ClientID%>');
                var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
                var lbxStudent = document.getElementById('<%=lbxStudent.ClientID %>');
                var lbxTests = document.getElementById('<%=lbxTests.ClientID %>');
                var lbxQid = document.getElementById('<%=lbxQid.ClientID %>');

                if (lbxInstitution.options[lbxInstitution.selectedIndex].value == '-1') {
                    alert('Select Institution');
                    return false;
                }

                if (lbxCohort.options.selectedIndex == 0) {
                    alert('Select atleast one Cohort');
                    return false;
                }

                if (lbxStudent != null && lbxStudent.options.selectedIndex == 0) {
                    alert('Select atleast one Student');
                    return false;
                }

                if (lbxTests != null && lbxTests.options.selectedIndex == -1) {
                    alert('Select atleast one Test');
                    return false;
                }

                if (lbxQid != null && lbxQid.options.selectedIndex == -1) {
                    alert('Select atleast one Qid');
                    return false;
                }
                return true;
            }
        </script>
        <table border="0" cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td class="headfont">
                    <b>English For Nursing Tracking </b>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="left" border="0" class="datatable_rep">
                        <tr class="datatable2">
                            <td align="left">
                                <table style="width: 100%;">
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
                                        <td class="reportParamTopLabel">
                                            Institution
                                        </td>
                                        <td colspan="4">
                                            <KTP:KTPListBox ID="lbxInstitution" runat="server" AutoPostBack="True" Width="300px">
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
                                            Students
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbxStudent" runat="server" AutoPostBack="True" Width="260px">
                                            </KTP:KTPListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamTopLabel">
                                            Tests
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbxTests" runat="server" AutoPostBack="True" Width="300px">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="reportParamTopLabel">
                                            QId
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbxQid" runat="server" Width="260px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                                OnClick="btnSubmit_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
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
                                    CellPadding="5" AutoGenerateColumns="False" AllowSorting="True" 
                                    onsorting="grvResult_Sorting">
                                    <RowStyle CssClass="datatable2a" />
                                    <AlternatingRowStyle CssClass="datatable1a" />
                                    <Columns>
                                        <asp:BoundField DataField="InstitutionName" HeaderText="Institution" SortExpression="InstitutionName" />
                                        <asp:BoundField DataField="CohortName" HeaderText="Cohort" SortExpression="CohortName" />
                                        <asp:TemplateField HeaderText="Last Name" SortExpression="Student.LastName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="LastName" Text='<%#DataBinder.Eval(Container.DataItem,"Student.LastName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name" SortExpression="Student.FirstName">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="FirstName" Text='<%#DataBinder.Eval(Container.DataItem,"Student.FirstName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserAction" HeaderText="Test/Rem" SortExpression="UserAction" />
                                        <asp:BoundField DataField="TestName" HeaderText="Test" SortExpression="TestName" />
                                        <asp:BoundField DataField="QuestionId" HeaderText="QId" SortExpression="QuestionId" />
                                        <asp:BoundField DataField="AltTabClickedDate" HeaderText="Test Taken/Alt Txt Access"
                                            SortExpression="AltTabClickedDate" />
                                        <asp:BoundField DataField="Correct" HeaderText="Correct" SortExpression="Correct" />
                                    </Columns>
                                    <HeaderStyle CssClass="datatablelabels" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--<asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
            Visible="False" />--%>
        <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Student.LastName|ASC" />
    </div>
</asp:Content>
