<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCohortPerformance" CodeBehind="ReportCohortPerformance.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
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
        function prt(o) {
            o.style.display = 'none';
            window.print();
        }

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var lbxProducts = document.getElementById('<%=ddProducts.ClientID %>');
            var lbxTests = document.getElementById('<%=ddTests.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (lbxCohort.options.selectedIndex == 0) {
                alert('Select atleast one Cohort');
                return false;
            }

            if (lbxProducts != null && (lbxProducts.options[lbxProducts.selectedIndex].value == '-1')) {
                alert('Select atleast one Test Type');
                return false;
            }

            if (lbxTests != null && lbxTests.options[lbxTests.selectedIndex].value == '-1') {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont" style="width: 943px">
                <b>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Temp/images/header_print.JPG" /><br />
                    Cohort Performance Report </b>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            <asp:Panel ID="Panel1" runat="server">
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
                                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamTopLabel">
                                            Cohort
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                ShowNotSelected="true" ShowSelectAll="true" Width="250px">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="reportParamTopLabel">
                                            Group
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbxGroup" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                ShowNotSelected="true" ShowSelectAll="true" Width="250px">
                                            </KTP:KTPListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamLabel">
                                            Test Type
                                        </td>
                                        <td>
                                            <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                                ShowSelectAll="true" />
                                        </td>
                                        <td>
                                        </td>
                                        <td class="reportParamLabel">
                                            Test Name
                                        </td>
                                        <td>
                                            <KTP:KTPDropDownList ID="ddTests" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                                ShowSelectAll="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamLabel">
                                            Date From
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateFrom" runat="server">
                                            </asp:TextBox>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                        </td>
                                        <td>
                                        </td>
                                        <td class="reportParamLabel">
                                            Date To
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDateTo" runat="server">
                                            </asp:TextBox>
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_search.gif"
                                                OnClientClick="return Validate();" OnClick="ImageButton1_Click1" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                                            <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/btn_pfv.gif" />&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                                Style="margin-top: 3px;" OnClientClick="return Validate();" OnClick="ImageButton3_Click"
                                                ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                                Style="margin-top: 3px;" OnClientClick="return Validate();" OnClick="ImageButton4_Click"
                                                ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="Label" Visible="False">
                                </asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr runat="server">
                        <td align="left" colspan="3" valign="bottom">
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:Label ID="lblTitle" runat="server" Text="Performance Comparison for School"
                                    Font-Bold="True"></asp:Label>
                                <br />
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Overall Performance Analysis:">
                                </asp:Label>
                                <asp:Label ID="lbltest" runat="server" Font-Bold="True"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Right" Width="100%">
                                &nbsp;
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/printbtn.gif" onclick="prt(this)" />
                                <asp:Panel ID="Panel4" runat="server" Height="50px" HorizontalAlign="Left" Width="100%">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr id="TableTitle" runat="server">
                        <td align="left" valign="middle" width="10%">
                            <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                <tr>
                                    <td align="center" rowspan="2" valign="bottom">
                                        <div id="divchart">
                                        </div>
                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                    </td>
                                    <td colspan="2" valign="bottom">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 260px" align="left" valign="bottom">
                            <table cellspacing="10" style="margin-bottom: 25px;">
                                <tr>
                                    <td colspan="2" valign="bottom" align="left">
                                        <b>&nbsp</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" nowrap="nowrap">
                                        <asp:Label ID="Label3" runat="server" Text="Number correct:"></asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:Label ID="lblNumberCorrect" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" nowrap="nowrap">
                                        <asp:Label ID="Label2" runat="server" Text="Number incorrect:"></asp:Label>
                                    </td>
                                    <td style="height: 17px" align="left">
                                        <asp:Label ID="lblNumberIncorrect" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" nowrap="nowrap">
                                        <asp:Label ID="Label1" runat="server" Text="Number not reached:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblNotAnswered" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td rowspan="2" align="left" valign="bottom">
                            <table cellspacing="10" style="margin-bottom: 25px;">
                                <tr>
                                    <td colspan="2" valign="bottom" align="left">
                                        <b>Answer Changes</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Correct to Incorrect:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblC_I" runat="server" Text="12"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Incorrect to Correct:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblI_C" runat="server" Text="5"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Incorrect to Incorrect:
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblI_I" runat="server" Text="6"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblM" Visible="false" runat="server" Text="There is not enough data to compile a student report">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 8px;">
                <div id="D_Graph" runat="server" style="background-color: White;">
                </div>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true">
    </CR:CrystalReportViewer>
</asp:Content>
