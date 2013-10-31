<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_ReportCohortResultByModule" CodeBehind="ReportCohortResultByModule.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function prt(o) {
            o.style.display = 'none';
            window.print();
        }

        $(document).ready(function () {
            ExpandContextMenu(4, 'ctl00_tdReportCohortResultByModule');
        });

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var lbCohort = document.getElementById('<%=lbxCohort.ClientID %>');
            var ddCase = document.getElementById('<%=ddCase.ClientID %>');
            var ddModule = document.getElementById('<%=ddModule.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (lbCohort != null && (lbCohort.options.selectedIndex == -1)) {
                alert('Select Cohort');
                return false;
            }

            if (ddCase != null && ddCase.options[ddCase.selectedIndex].value == '-1') {
                alert('Select Case');
                return false;
            }

            if (ddModule != null && ddModule.options[ddModule.selectedIndex].value == '-1') {
                alert('Select Module');
                return false;
            }

            return true;
        }
    </script>
    <table align="left" border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont" style="width: 943px">
                <b>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Temp/images/header_print.JPG" />
                    <p>Cohort Results by Module</p></b>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            <asp:Panel ID="Panel1" runat="server">
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
                                        <td class="reportParamLabel">
                                            Institution:
                                        </td>
                                        <td colspan="4">
                                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false"
                                                ShowNotSelected="true">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamTopLabel">
                                            Cohort:
                                        </td>
                                        <td colspan="4">
                                            <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" Width="300px" SelectionMode="Multiple"
                                                ShowNotSelected="false" ShowSelectAll="true">
                                            </KTP:KTPListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="reportParamLabel">
                                            Module:
                                        </td>
                                        <td>
                                            <KTP:KTPDropDownList ID="ddModule" runat="server" AutoPostBack="True" ShowNotSelected="true"
                                                ShowSelectAll="true">
                                            </KTP:KTPDropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td class="reportParamLabel">
                                            Case:
                                        </td>
                                        <td>
                                            <KTP:KTPDropDownList ID="ddCase" runat="server" AutoPostBack="True" ShowNotSelected="true">
                                            </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" align="center">
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                                OnClick="btnSubmit_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                            <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/btn_pfv.gif" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                                OnClientClick="return Validate();" Style="margin-top: 3px;" OnClick="ImageButton3_Click"
                                                ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                            &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                                OnClientClick="return Validate();" Style="margin-top: 3px;" OnClick="ImageButton4_Click"
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
                            &nbsp;
                        </td>
                        <td rowspan="2" align="left" valign="bottom">
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
