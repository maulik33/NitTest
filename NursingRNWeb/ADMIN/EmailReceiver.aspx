<%@ Page Language="VB" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="false" Inherits="ADMIN_EmailReceiver" title="Kaplan Nursing" Codebehind="EmailReceiver.aspx.vb" %>

<%--<%@ Register Assembly="WebControlLibrary" Namespace="WebControlLibrary.ZYH" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    &nbsp;
    <table width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="Receiver Group Type" />
                        <asp:BoundField HeaderText="Receiver Name" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">Institustion</asp:ListItem>
                    <asp:ListItem Value="1">Cohort</asp:ListItem>
                    <asp:ListItem Value="2">Group</asp:ListItem>
                    <asp:ListItem Value="3">Student</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Institution ID" />
                    <asp:BoundField HeaderText="Name" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
        <asp:View ID="View3" runat="server">
        </asp:View>
        <asp:View ID="View4" runat="server">
        </asp:View>
    </asp:MultiView></td>
        </tr>
    </table>
--%></asp:Content>

