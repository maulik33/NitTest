<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCaseComparisonsByModule" CodeBehind="ReportCaseComparisonByModule.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function prt(o) {
            o.style.display = 'none';
            window.print();
        }
        $(document).ready(function () {
            ExpandContextMenu(4, 'ctl00_tdReportCaseComparisonByModule');
        });

        function Validate(arg1) {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbCohort = document.getElementById('<%=lbCohort.ClientID %>');
            var lbCase = document.getElementById('<%=lbCase.ClientID %>');
            var lbModule = document.getElementById('<%=lbModule.ClientID %>');
            var lbCategory = document.getElementById('<%=lbCategory.ClientID %>');
            var lbSubCategory = document.getElementById('<%=lbSubCategory.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }
            var s = lbCohort.options.selectedvalue;
            if (lbCohort.options.selectedIndex == -1) {
                alert('Select atleast one Cohort');
                return false;
            }

            if (lbCase.options.selectedIndex == -1) {
                alert('Select atleast one Case');
                return false;
            }

            if (lbModule.options.selectedIndex == -1) {
                alert('Select atleast one Module');
                return false;
            }

            if (lbCategory.options.selectedIndex == -1) {
                alert('Select atleast one Category');
                return false;
            }

            if (arg1 == "Go") {
                if (lbSubCategory.options.selectedIndex == -1) {
                    alert('Select atleast one Sub Category');
                    return false;
                }
            }




            return true;
        }

    </script>
    <table border="0" cellpadding="2" cellspacing="1" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Case Comparison By Module </b>
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
                                 <tr runat="server" id="trProgramofStudy">
                                    <td colspan="4" class="reportParamLabel">
                                            Program of Study                                
                                        <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" ShowNotSelected="false">
                                            </KTP:KTPDropDownList>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="reportParamLabel">
                                            Institution &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                                ShowSelectAll="true">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left" class="reportParamLabel">
                                            Select Cohort
                                        </td>
                                        <td align="left" class="reportParamLabel">
                                            Select Case
                                        </td>
                                        <td align="left" class="reportParamLabel">
                                            Select Module
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="left">
                                            <KTP:KTPListBox ID="lbCohort" runat="server" SelectionMode="Multiple" Width="200"
                                                ShowNotSelected="false" ShowSelectAll="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td align="left">
                                            <KTP:KTPListBox ID="lbCase" runat="server" SelectionMode="Multiple" Width="200" ShowNotSelected="false"
                                                ShowSelectAll="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td align="left">
                                            <KTP:KTPListBox ID="lbModule" runat="server" SelectionMode="Multiple" Width="200"
                                                ShowNotSelected="false" ShowSelectAll="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Select Category
                                        </td>
                                        <td align="left">
                                            Select SubCategory
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <KTP:KTPListBox ID="lbCategory" runat="server" SelectionMode="Multiple" Width="200"
                                                ShowNotSelected="false" ShowSelectAll="false" AutoPostBack="True">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td align="left">
                                            <KTP:KTPListBox ID="lbSubCategory" runat="server" SelectionMode="Multiple" Width="200"
                                                ShowNotSelected="false" ShowSelectAll="false">
                                            </KTP:KTPListBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:ImageButton ID="btGo" runat="server" 
                                                ImageUrl="~/Temp/images/btn_submit.gif" OnClick="btGo_Click" 
                                                OnClientClick="return Validate('Go');" onMouseOut="roll(this)" 
                                                onMouseOver="roll(this)" />
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                                ImageUrl="~/Images/btn_pfv.gif" Visible="False" />
                                            &nbsp;&nbsp;
                                            <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/btn_pfv.gif" />
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                                ImageUrl="~/Images/btn_toexcel.gif" OnClick="ImageButton3_Click" 
                                                OnClientClick="return Validate('Excel');" 
                                                ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton4" runat="server" 
                                                ImageUrl="~/Images/btn_toexceldata.gif" OnClick="ImageButton4_Click" 
                                                OnClientClick="return Validate('ExcelData');" 
                                                ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/Temp/images/printbtn.gif" Visible="False" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                         <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="Label" Visible="False">
                                </asp:Label>
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
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Right" Width="100%" Visible="false">
                        <table width="100%">
                            <tr>
                                <td align="left" colspan="3">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/printbtn.gif" onclick="prt(this)" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
