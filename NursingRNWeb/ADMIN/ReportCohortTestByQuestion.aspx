<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCohortTestByQuestion" CodeBehind="ReportCohortTestByQuestion.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            var selectMode = document.getElementById('<%=hdnMode.ClientID%>').value;

            if (selectMode == "1") {
                ExpandContextMenu(1, 'ctl00_tdReportCohortTestByQuestion');
            }
            else {
                ExpandContextMenu(2, 'ctl00_tdReportCohortTestByQuestion1');
            }


        });

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var ddCohorts = document.getElementById('<%=ddCohorts.ClientID %>');
            var ddTests = document.getElementById('<%=ddTests.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (ddCohorts.selectedIndex == -1 || ddCohorts.options[ddCohorts.selectedIndex].value == '-1') {
                alert('Select Cohort');
                return false;
            }

            if (ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select Test Type');
                return false;
            }

            if (ddTests.options[ddTests.selectedIndex].value == '-1') {
                alert('Select Test Name');
                return false;
            }

            return true;
        }

    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Test Result by Question </b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td align="left">
                            <table width="100%" cellpadding="1" cellspacing="1">
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
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Cohort
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="ddCohorts" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                            ShowSelectAll="false">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Name
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddTests" runat="server" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="ImageButton2_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClick="ImageButton3_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClick="ImageButton4_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a this report"
                                Visible="False" Width="347px"></asp:Label><br />
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Print Cohort1-7"
                                Visible="False" />
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Print Cohort8-15"
                                Visible="False" />
                            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Print Cohort16-23"
                                Visible="False" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblStudentsNumber" runat="server" Visible="False" Font-Bold="True"></asp:Label>&nbsp;<asp:Panel
                                ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="gvCohorts" Width="100%" runat="server" AllowSorting="True" 
                                    OnSorting="gvCohorts_Sorting"  OnRowDataBound="gvCohorts_RowDataBound"
                                    AutoGenerateColumns="false">
                                    <RowStyle CssClass="datatable2a" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1a" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
        Visible="False"></asp:ImageButton>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        HasCrystalLogo="False" HasDrillUpButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
        Height="1327px" Width="773px" />
</asp:Content>
