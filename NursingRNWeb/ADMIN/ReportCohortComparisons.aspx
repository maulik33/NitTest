<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCohortComparisons" CodeBehind="ReportCohortComparisons.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_tdReportCohortComparisons');
        });

        function prt(o) {
            o.style.display = 'none';
            window.print();
        }

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbxCohort = document.getElementById('<%=lbCohort.ClientID %>');
            var lbxProducts = document.getElementById('<%=lbProduct.ClientID %>');
            var lbxTests = document.getElementById('<%=lbTest.ClientID %>');
            var lbCategory = document.getElementById('<%=lbCategory.ClientID %>');
            var lbSubCategory = document.getElementById('<%=ListBox1.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (lbxCohort.options.selectedIndex == -1) {
                alert('Select at least one Cohort');
                return false;
            }

            if (lbxProducts != null && lbxProducts.options.selectedIndex == -1) {
                alert('Select at least one Test Type');
                return false;
            }

            if (lbxTests != null && lbxTests.options.selectedIndex == -1) {
                alert('Select at least one Test');
                return false;
            }

            if (lbCategory != null && lbCategory.options.selectedIndex == -1) {
                alert('Select at least one Category');
                return false;
            }

            return true;
        }
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Category Comparisons </b>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                &nbsp;<asp:Panel ID="Panel1" runat="server" Width="100%" Wrap="False">
                    <table align="left" border="0" class="datatable_rep" width="100%" cellpadding="8">
                        <tr class="datatable2">
                            <td align="left">
                                <table width="98%" border="0" cellspacing="5" cellpadding="0">
                                    <tr>
                                        <td class="reportParamTopLabel" colspan="2">
                                            <asp:Label ID="lblProgramOfStudy" runat="server" Text="Program of Study"></asp:Label>
                                            <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamTopLabel" colspan="4">
                                            Institution &nbsp;
                                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowNotSelected="True">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left">
                                            Select Cohort
                                        </td>
                                        <td align="left">
                                            Test Type/Test Program of Study
                                        </td>
                                        <td align="left">
                                            Select Test Type
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td>
                                            <KTP:KTPListBox ID="lbCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                Width="200" ShowNotSelected="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                             <KTP:KTPDropDownList ID="ddlProgramOfStudyForTestsAndCategories" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList>
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbProduct" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                Width="200" ShowSelectAll="false" ShowNotSelected="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td>
                                            Select Test
                                        </td>
                                        <td>
                                            Select Category
                                        </td>
                                        <td>
                                            Select SubCategory
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td>
                                            <KTP:KTPListBox ID="lbTest" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                Width="200px" ShowSelectAll="false" ShowNotSelected="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="lbCategory" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                Width="200px" ShowSelectAll="false" ShowNotSelected="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            <KTP:KTPListBox ID="ListBox1" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                                Width="200px" ShowSelectAll="false" ShowNotSelected="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <td colspan="4" align="center">
                                        <asp:ImageButton ImageUrl="~/Temp/images/btn_submit.gif" onMouseOver="roll(this)"
                                            onMouseOut="roll(this)" ID="btGo" runat="server" OnClick="btGo_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            Visible="False" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/btn_pfv.gif" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            OnClientClick="return Validate();" OnClick="ImageButton3_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            OnClientClick="return Validate();" OnClick="ImageButton4_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
                                            OnClientClick="return Validate();" OnClick="ImageButton1_Click" Visible="False" />&nbsp;&nbsp;&nbsp;
                                    </td>
                                </table>
                            </td>
                        </tr>
                      <%--   <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="Label" Visible="False">
                                </asp:Label>--%>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="padding: 8px;">
                <div id="D_Graph" runat="server">
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Right" Width="100%">
                        
                        <table width="100%">
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                           
                                <td>
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                                        HasCrystalLogo="False" HasDrillUpButton="False" HasSearchButton="False" Height="812px"
                                        Width="1181px" />
                                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                        <Report FileName="Report\CohortComparisons.rpt">
                                        </Report>
                                    </CR:CrystalReportSource>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                         <asp:Panel ID="printBtnPanel" runat="server" Visible="false">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/printbtn.gif" onclick="prt(this)" />
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
