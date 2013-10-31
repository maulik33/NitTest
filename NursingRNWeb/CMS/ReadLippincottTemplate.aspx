<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_ReadLippincottTemplate" Codebehind="ReadLippincottTemplate.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
        <tr>
            <td align="left" colspan="2">
                <asp:Label ID="errorMessage" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 340px">
                <asp:ListBox ID="ListBox1" runat="server" Height="416px" Width="328px" SelectionMode="Multiple"></asp:ListBox></td>
            <td align="left">
                &nbsp;<br />
                <asp:Button ID="btnSelectAll" runat="server" Text="Select All" 
                    onclick="btnSelectAll_Click" />
                <br />
                <br />
                <asp:Button ID="btnRead" runat="server" Text="Read" onclick="btnRead_Click" />
                <br />
                <br />
                <asp:Button ID="btnBackToLippincott" runat="server" Text="Back To Lippincott" 
                    onclick="btnBackToLippincott_Click" /></td>
        </tr>
        <tr>
            <td align="left" style="width: 340px">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 340px">
            </td>
        </tr>
    </table>
</asp:Content>