<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="LoginContentRelease" CodeBehind="LoginContentRelease.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 40px 20px 20px 20px;">
        <table class="datatable">
            <tr class="datatable1a">
                <td style="text-align: left; padding:8px 0px 8px 45px;">
                    <asp:Label ID="Label1" runat="server" Text="Login Page :"></asp:Label>
                
                    <asp:DropDownList ID="ddlLoginContentType" runat="server" OnSelectedIndexChanged="ddlLoginContentType_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="1">Admin Login</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">Student Login </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="datatable1a" align="left">
                <td style=" padding:5px;">
                    <asp:TextBox ID="txtContent" runat="server" Height="350px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
             <td class="datatable1a" style="text-align: Center; padding:10px;">
                    <asp:Button ID="btnpreview" Text="Preview" runat="server" OnClick="btnpreview_Click" />
                    <asp:Button ID="btnRelease" Text="Release" runat="server" OnClick="btnRelease_Click" />
                    <asp:Button ID="btnRevert" Text="Revert" runat="server" 
                        onclick="btnRevert_Click"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
