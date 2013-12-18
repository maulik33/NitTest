<%@ Page Language="C#" AutoEventWireup="true" Inherits="AdminLogin" CodeBehind="A_Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Kaplan Nursing</title>
    <link href="CSS/basic.css" rel="stylesheet" type="text/css" />
    <script src="JS/main.js" type="text/javascript"></script>
    <script src="JS/google.js" type="text/javascript"></script>
    <link rel="Shortcut Icon" href="favicon.ico" />
    <style type="text/css">
        html
        {
            display: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        if (self == top) {
            document.documentElement.style.display = 'block';
        }
        else {
            top.location = self.location;
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server" method="Post" autocomplete="off">
    <table cellpadding="0" cellspacing="0" border="0" align="center">
        <tr>
            <td>
                <table id="header" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="172">
                            &nbsp;
                        </td>
                        <td align="right" valign="bottom">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="content" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="part2">
                            <table width="15%" border="0" cellspacing="0" cellpadding="2" style="margin-top: 60px;"
                                align="center">
                                <tr>
                                    <td class="titlefont" style="background-color: #D9EBED; width: 441px;">
                                        Admin Login
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="25" style="width: 275px; text-align: justify">
                                        <br />
                                        <br />
                                        <b>User Name</b><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtUserName"
                                            ErrorMessage="* Please enter your user name" Display="dynamic" runat="server"
                                            ValidationGroup="Form1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 275px">
                                        <asp:TextBox ID="TxtUserName" runat="server" Width="268px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="25" style="width: 275px">
                                        <b>Password</b><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtPassword"
                                            ErrorMessage="* Please enter your password" Display="dynamic" runat="server"
                                            ValidationGroup="Form1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 275px">
                                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="268px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 275px">
                                        &nbsp;
                                        <br />
                                        <asp:Label ID="Msg" runat="server" ForeColor="Red" Text="Label" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom" style="width: 275px">
                                        <asp:ImageButton ID="BtnLogIn" runat="server" ImageUrl="images/btn_login.gif" OnClick="btnLogIn_Click"
                                            ValidationGroup="Form1" onMouseOver="roll(this)" onMouseOut="roll(this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="50" style="width: 275px">
                                        <asp:Label ID="LblResult" Visible="False" runat="server" Font-Bold="True" Width="271px"></asp:Label>
                                        <br />
                                        <br />
                                        <div runat="server" id="divLoginContent">
                                        </div>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblApplicationRestartDate" runat="server" class="WhiteText"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
