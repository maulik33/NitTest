<%@ Page Language="VB" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="false" Inherits="ADMIN_EmailQueue" title="Kaplan Nursing" Codebehind="EmailQueue.aspx.vb" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table width="100%">
        <tr>
            <td align="left" style="height: 17px">
                <asp:Label ID="Label1" runat="server" Text="List after this date :"></asp:Label></td>
            <td align="left" style="height: 17px">
                <KTP:Calendar ID="Calendar1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="Label2" runat="server" Text="List Before this date :"></asp:Label></td>
            <td align="left">
                <KTP:Calendar ID="Calendar2" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="left">
                <asp:Button ID="Button2" runat="server" Text="Schedule Email" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Queue ID" />
                        <asp:BoundField HeaderText="Sender" />
                        <asp:BoundField HeaderText="Email Title" />
                        <asp:BoundField HeaderText="Sending State" />
                        <asp:BoundField HeaderText="Sent Date" />
                        <asp:BoundField HeaderText="Scheduled Date" />
                        <asp:ButtonField Text="Cancel Send" />
                        <asp:ButtonField Text="Retry" />
                        <asp:ButtonField Text="Edit" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

