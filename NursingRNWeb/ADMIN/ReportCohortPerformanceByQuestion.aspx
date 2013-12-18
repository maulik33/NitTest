<%@ Page Language="C#" MasterPageFile="~/Admin/ReportMaster.master" AutoEventWireup="True" Inherits="ADMIN_ReportCohortPerformanceByQuestion" Codebehind="ReportCohortPerformanceByQuestion.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Aggregate Reports > Cohort Performance by Question </b>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 81px">
                                        Institution:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" rowspan="4">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="ImageButton2_Click" /><br />
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            OnClick="ImageButton3_Click" Style="margin-top: 3px" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." /><br />
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            OnClick="ImageButton4_Click" Style="margin-top: 3px" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 81px">
                                        Cohort:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddCohorts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohorts_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 81px">
                                        Test Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 81px">
                                        <asp:Label ID="Label1" runat="server" Text="Test Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a this report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" Visible="False">
    </asp:GridView>
    <br>
    <asp:GridView ID="gvIntegrated" runat="server" AutoGenerateColumns="False" CssClass="GridView1ChildStyle"
        DataKeyNames="QID,TestID" OnRowCommand="gvIntegrated_RowCommand" OnRowDataBound="gvIntegrated_RowDataBound"
        Width="100%" CellPadding="5">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
    </asp:GridView>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
</asp:Content>
