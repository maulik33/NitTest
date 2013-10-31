<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true" 
    Inherits="ADMIN_ReportInstitutionTestByQuestion" CodeBehind="ReportInstitutionTestByQuestion.aspx.cs"%>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_tdReportInstitutionTestByQuestion');
        });

        function Validate() {
            var lbInstitution = document.getElementById('<%=lbInstitution.ClientID%>');
            var lbCohort = document.getElementById('<%=lbCohort.ClientID %>');
            var ddTests = document.getElementById('<%=ddTests.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');

            if (lbInstitution.options.selectedIndex == -1 || lbInstitution.options.selectedIndex == 0) {
                alert('Select Institution');
                return false;
            }

            if (lbCohort != null && lbCohort.options.selectedIndex == -1) {
                alert('Select Cohort');
                return false;
            }

            if (ddProducts != null && ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select Test Type');
                return false;
            }

            if (ddTests != null && ddTests.options[ddTests.selectedIndex].value == '-1') {
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
                <b>Institutional Test Result by Question </b>
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
                                     <KTP:KTPListBox ID="lbInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false"
                                            SelectionMode="Multiple" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamTopLabel">
                                        Cohort
                                    </td>
                                    <td colspan="4">
                                     <KTP:KTPListBox ID="lbCohort" runat="server" AutoPostBack="True"  SelectionMode="Multiple" ShowNotSelected="false" width="275px"/>
                                          </KTP:KTPDropDownList>
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
                                        <KTP:KTPDropDownList ID="ddTests" runat="server" AutoPostBack="True" ShowNotSelected="true">
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
                            <asp:Button ID="Button1" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort1-7" CommandArgument="1"
                                Visible="False" />
                            <asp:Button ID="Button2" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort8-14" CommandArgument="2"
                                Visible="False" />
                            <asp:Button ID="Button3" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort15-21" CommandArgument="3"
                                Visible="False" />
                            <asp:Button ID="Button4" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort22-28" CommandArgument="4"
                                Visible="False" />   
                            <asp:Button ID="Button5" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort29-35" CommandArgument="5"
                                Visible="False" />
                            <asp:Button ID="Button6" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort36-42" CommandArgument="6"
                                Visible="False" />
                            <asp:Button ID="Button7" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort43-49" CommandArgument="7"
                                Visible="False" />
                            <asp:Button ID="Button8" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort50-56" CommandArgument="8"
                                Visible="False" />
                            <asp:Button ID="Button9" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort57-63" CommandArgument="9"
                                Visible="False" />
                            <asp:Button ID="Button10" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort64-70" CommandArgument="10"
                                Visible="False" />
                            <asp:Button ID="Button11" runat="server" OnClick="PrintCohort_Click" Text="Print Cohort71-75" CommandArgument="11"
                                Visible="False" />
                                &nbsp;
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
