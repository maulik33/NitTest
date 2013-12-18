<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportStudentReportCardByModule" Title="Student Report Card"
    CodeBehind="ReportStudentReportCardByModule.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(4, 'ctl00_tdReportStudentReportCardByModule');
        });

        function Validate() {
            var lbInstitution = document.getElementById('<%=lbInstitution.ClientID%>');
            var ddCohorts = document.getElementById('<%=ddCohorts.ClientID %>');
            var lbxStudent = document.getElementById('<%=lbxStudent.ClientID %>');
            var ddCase = document.getElementById('<%=ddCase.ClientID %>');
            var lbxModule = document.getElementById('<%=lbxModule.ClientID %>');

            if (lbInstitution.options.selectedIndex == -1) {
                alert('Select Institution');
                return false;
            }

            if (ddCohorts != null && ddCohorts.options[ddCohorts.selectedIndex].value == -1) {
                alert('Select Cohort');
                return false;
            }

            if (lbxStudent != null && lbxStudent.options.selectedIndex == -1) {
                alert('Select Student');
                return false;
            }

            if (ddCase != null && ddCase.options[ddCase.selectedIndex].value == -1) {
                alert('Select Case');
                return false;
            }

            if (lbxModule != null && lbxModule.options.selectedIndex == -1) {
                alert('Select Module');
                return false;
            }

            return true;
        }
    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Student Report Card By Module </b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            <table style="width: 100%;">
                                <tr>
                                <tr runat="server" id="trProgramofStudy">
                                        <td class="reportParamLabel" >
                                            Program of Study 
                                            
                                        </td>
                                        <td colspan="4"><KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList></td>
                                    </tr>
                                    <td class="reportParamTopLabel">
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbInstitution" runat="server" AutoPostBack="True" ShowNotSelected="false"
                                            ShowSelectAll="false" SelectionMode="Multiple">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Cohort
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddCohorts" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                            ShowSelectAll="false" Width="250px">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamLabel">
                                        Case
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddCase" runat="server" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Students
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxStudent" runat="server" AutoPostBack="True" ShowNotSelected="false"
                                            ShowSelectAll="true" SelectionMode="Multiple" Width="250px" />
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamTopLabel">
                                        Module
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxModule" runat="server" DataTextField="ModuleName" DataValueField="ModuleID"
                                            ShowNotSelected="false" ShowSelectAll="false" SelectionMode="Multiple">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();">
                                        </asp:ImageButton>&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrintPDF_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClientClick="return Validate();" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcelDataOnly_Click" />
                                    </td>
                                </tr>
                            </table>
                <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a this report"
                    Visible="False" Width="347px"></asp:Label>
            </td>
        </tr>
        <tr class="datatable2">
            <td>
                &nbsp;<asp:GridView ID="grvResult" runat="server" CssClass="GridView1ChildStyle"
                    Width="100%" CellPadding="5" AutoGenerateColumns="False" AllowSorting="True"
                    OnSorting="grvResult_Sorting">
                    <rowstyle cssclass="datatable2a" />
                    <alternatingrowstyle cssclass="datatable1a" />
                    <columns>
                                    <asp:TemplateField HeaderText="Last Name" SortExpression="Student.FullName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student.LastName")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First Name" SortExpression="Student.FirstName">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Student.FirstName")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Name" SortExpression="CaseStudy.CaseName">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CaseStudy.CaseName")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Module Name" SortExpression="Module.ModuleName">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Module.ModuleName")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Correct" HeaderText="% Correct" SortExpression="Correct"
                                        DataFormatString="{0:#0.0}" />
                                </columns>
                    <headerstyle cssclass="datatablelabels" />
                </asp:GridView>
                &nbsp;&nbsp;
            </td>
        </tr>
        </table> </td> </tr>
    </table>
    <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
        Visible="False" />
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Student.FullName|ASC" />
</asp:Content>
