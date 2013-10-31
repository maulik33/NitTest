<%@ Control Language="C#" AutoEventWireup="true" Inherits="ADMIN_Controls_AdminMainRightMenu"
    CodeBehind="AdminMainRightMenu.ascx.cs" %>
<asp:ImageButton ID="btnMainMenu" runat="server" OnClick="btnMainMenu_Click" ImageUrl="~/Images/mainmenu.bmp"
    Width="86px" Height="30px" Visible="false" />
<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/EmailReceiver1.aspx"
    ImageUrl="~/Images/sendemail.bmp" />
<asp:ImageButton ID="btnLogout" runat="server" OnClick="btnLogout_Click" ImageUrl="~/Images/logout.bmp" />