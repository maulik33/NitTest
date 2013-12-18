<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="admin_BackupData" Codebehind="BackupData.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>

        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Are you sure you want to release to production?"></asp:Label><br />
        <br />
        <br />

        <asp:Button ID="Button1" runat="server" Text="Release to Production" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" />
     
        <br />
        <br />
        <asp:Label ID="Label4" runat="server"></asp:Label><br />


</div>
</asp:Content>