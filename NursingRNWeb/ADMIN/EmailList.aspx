<%@ Page Language="VB" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="false" Inherits="ADMIN_EmailList" title="Kaplan Nursing" Codebehind="EmailList.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="508px">
        <Columns>
            <asp:BoundField HeaderText="Email Title" />
            <asp:BoundField HeaderText="Create Date" />
            <asp:BoundField HeaderText="Sent Date" />
            <asp:BoundField HeaderText="Owner" />
            <asp:ButtonField Text="Edit" />
            <asp:ButtonField Text="View" />
            <asp:ButtonField Text="Send" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="Show all Preset, custom preset, and custom email. "></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Add New" />
</asp:Content>

