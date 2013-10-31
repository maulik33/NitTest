<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.StudentChangePassword" Codebehind="ChangePassword.aspx.cs" %>

<%@ Register TagName="head" TagPrefix="uc1" Src="~/Student/ASCX/head.ascx" %>
<html>
<head>
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>
</head>
<body>
    <form id="Form1" runat="server" autocomplete="off" method="post">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 871px">
                <uc1:head ID="NursingHeader" runat="server" />
                <table id="med_main_f" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center" valign="top">
                            <table border="0" cellspacing="0" cellpadding="10" style="border: solid 1px #333399;
                                margin-top: 120px;">
                                <tr>
                                    <td align="left" style="width: 296px">
                                        <asp:Label ID="LblMessage" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px">
                                        <asp:Label ID="LblOldPassword" runat="server" Text="Old Password: " Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px">
                                        <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" Width="285px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px">
                                        <asp:Label ID="LblPassword" runat="server" Text="New Password: " Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px">
                                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="285px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px;">
                                        <asp:Label ID="LblCpassword" runat="server" Text="Confirm New Password: " Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 296px;">
                                        <asp:TextBox ID="TxtCpassword" runat="server" TextMode="Password" Width="287px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 296px; height: 26px">
                                        <asp:ImageButton ID="BtnSave" runat="server" ImageUrl="../images/btn_save.gif" onMouseOver="roll(this)"
                                            onMouseOut="roll(this)" OnClick="BtnSave_Click" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <a href="user_home.aspx">
                                            <img style="border: 0;" alt="" src="../Images/backtohome_over.gif" /></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
