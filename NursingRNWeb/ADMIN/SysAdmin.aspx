<%@ Page Title="System Administrator Console" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master"
    AutoEventWireup="true" CodeBehind="SysAdmin.aspx.cs" Inherits="NursingRNWeb.ADMIN.SysAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="MessageLabel" ForeColor="Red" ViewStateMode="Disabled"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView runat="server" ID="CheckSystemResultsGridView" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Key" HeaderText="Key" />
                        <asp:BoundField DataField="Value" HeaderText="Value" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                PassCode
            </td>
            <td>
                <asp:TextBox runat="server" ID="PassCodeTextBox" TextMode="Password" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button Text="Submit" runat="server" ID="SubmitButton" OnClick="SubmitButton_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
