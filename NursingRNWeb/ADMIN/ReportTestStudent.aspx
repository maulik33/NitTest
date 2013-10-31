<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true" Inherits="ADMIN_ReportTestStudent" Codebehind="ReportTestStudent.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
           
            var selectMode = document.getElementById('<%=hdnMode.ClientID%>').value;
            if (selectMode == "1") {
                ExpandContextMenu(3, 'ctl00_tdReportMultiCampusReportCard');
            }
            else {
                ExpandContextMenu(1, 'ctl00_tdReportStudentReportCard');
            }

        });
        function prt(o) {
            o.style.display = 'none';
            window.print();
        }

        function Validate() {
            var ddTests = document.getElementById('<%=ddTests.ClientID%>');

            if (ddTests.options[ddTests.selectedIndex].value == '-1') {
                alert('Select atleast one Test');
                return false;
            }

            return true;
        }

    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont" style="height: 57px">
                <b>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Temp/images/header_nur2.jpg" /><br />
                    Student Performance Report by Test<br />
                    <asp:Label ID="Label1" runat="server" Text="Student Name:"></asp:Label>
                    <asp:Label ID="lblName" runat="server" Width="155px"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text="Test Name:" Visible="False"></asp:Label>
                    <br />
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:Panel ID="Panel5" runat="server" Width="100%" HorizontalAlign="Right">
                            <asp:LinkButton ID="btn" runat="server" OnClick="btn_Click">Remediation Time by Question Report</asp:LinkButton></asp:Panel>
                                <table align="left" border="0" class="datatable_rep">
                                    <tr class="datatable2">
                                        <td align="left">
                                            Test Type: &nbsp; &nbsp;&nbsp;<KTP:KTPDropDownList ID="ddProducts" runat="server" 
                                                AutoPostBack="True" ShowNotSelected="false" ShowSelectAll="true" />
                                            &nbsp;&nbsp;&nbsp; Test Name: &nbsp; &nbsp;&nbsp;<KTP:KTPDropDownList ID="ddTests" runat="server" ShowSelectAll="false" />
                                        </td>
                                    </tr>
                                    <tr class="datatable2">
                                        <td align="left" style="height: 27px">
                                            <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                                OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                                OnClientClick="return Validate();" OnClick="ImageButton2_Click" Visible="False" />
                                            <asp:Image ID="Image111" runat="server" ImageUrl="~/Images/btn_pfv.gif"  />
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                                OnClientClick="return Validate();" OnClick="ImageButton3_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                                OnClientClick="return Validate();" OnClick="ImageButton4_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
                                                OnClientClick="return Validate();" OnClick="ImageButton1_Click" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Right" Width="100%">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/printbtn.gif" onclick="prt(this)" /></asp:Panel>
                </b>
            </td>
        </tr>
    </table>
    <!-- section A -->
    <table width="100%" border="0" cellspacing="0" cellpadding="10">
        <tr>
            <td align="left" colspan="2">
                <div id="med_center_banner3">
                    OVERALL REPORT</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="lblPR" runat="server" Text="Percentile Ranking:" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblRanking" runat="server" Text="31" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblPP" runat="server" Font-Bold="True" Text="Probability of Passing NCLEX:"
                    Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="lblProbability" runat="server" Text="0" Visible="False"></asp:Label>
                <br />
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <table width="100%" border="0" cellspacing="0" cellpadding="8" style="clear: both;">
                    <tr>
                        <td width="50%">
                            <b>Overall Percent Correct:
                                <asp:Label ID="lblOPC" runat="server" Text=""></asp:Label></b>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="19%">
                                        <script type="text/javascript">

                                            var fo = new FlashObject("Charts/FC_2_3_MSColumn2D.swf", "FC2Column", "150", "250", "7", "white", true);
                                            fo.addParam("allowScriptAccess", "always");
                                            fo.addParam("scale", "noScale");
                                            fo.addParam("menu", "false");
                                            fo.addVariable("dataURL", "<%=dataURL%>");
                                            fo.addVariable("chartWidth", "150");
                                            fo.addVariable("ChartHeight", "250");
                                            fo.write("divchart");
                                        </script>
                                    </td>
                                    <td valign="bottom">
                                        <table border="0" cellspacing="0" cellpadding="5" width="90%" align="right" style="margin-bottom: 55px">
                                            <tr>
                                                <td>
                                                    <img src="../temp/images/baricon.gif" width="12" height="12">
                                                    <b>My Overall Correct</b>
                                                </td>
                                                <td style="width: 40px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Number correct:
                                                </td>
                                                <td style="width: 40px">
                                                    <asp:Label ID="lblNumberCorrect" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Number incorrect:
                                                </td>
                                                <td style="width: 40px">
                                                    <asp:Label ID="lblNumberIncorrect" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Number not reached:
                                                </td>
                                                <td style="width: 40px">
                                                    <asp:Label ID="lblNotAnswered" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="bottom">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="5" style="margin-bottom: 55px;">
                                            <tr>
                                                <td colspan="2" valign="bottom">
                                                    <b>Answer Changes</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Correct to Incorrect:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblC_I" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Incorrect to Correct:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblI_C" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Incorrect to Incorrect:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblI_I" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="D_Title" runat="server" class="med_center_banner5" style="padding-left: 15px;">
                </div>
                <br />
                <div id="D_Graph" runat="server">
                </div>
            </td>
        </tr>
    </table>
    <!-- end report body -->
    <!-- end content table -->
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            Height="1039px" Width="901px" HasCrystalLogo="False" HasDrillUpButton="False" 
            HasSearchButton="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Report\TestStudent.rpt">
            </Report>
        </CR:CrystalReportSource>
</asp:Content>
