<%@ Page Language="VB" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="false" Inherits="ADMIN_P_CohortByStudent"
    Title="Untitled Page" Codebehind="P_TestRemediationByStudent.aspx.vb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <div align="left">
        <table>
            <tr>
                <td>
                    Institution:
                </td>
                <td>
                    <asp:DropDownList ID="ddInstitution" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Cohort:
                </td>
                <td>
                    <asp:DropDownList ID="ddCohorts" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Test Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Test Name:
                </td>
                <td>
                    <asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" Visible="False">
        </asp:GridView>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            Height="50px" Width="350px" HasCrystalLogo="False" HasDrillUpButton="False"
            HasSearchButton="False" HasToggleGroupTreeButton="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="ADMIN\Report\TestsRemediationByStudent.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
--%></asp:Content>
