<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" Inherits="Login"
    CodeBehind="S_Login.aspx.cs" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Kaplan Nursing</title>
    <link href="CSS/basic.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.ico" />
    <script src="js/main.js" type="text/javascript"></script>
    <script src="JS/google.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/jquery-1.4.3.min.js"></script>
    <link href="../css/jquery-ui-1.css" rel="stylesheet" type="text/css" />
    <link href="../css/ui_002.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
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
        
        function openPopUp() {
            var NewHeight = screen.availHeight;
            $('#hfHeight').val(NewHeight);
            $(document).ready(function (e) {
                var retVal = $("#popContent").dialog({ modal: true, width: 450, height: 250, resizable: false, draggable: false, position: "245,20" });
            });
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
                            <table width="15%" border="0" cellspacing="0" cellpadding="2" align="center" style="margin-top: 60px;">
                                <tr>
                                    <td class="titlefont" style="background-color: #D4D7EA; width: 275px;">
                                        Student Login
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="25" style="width: 275px; text-align: justify !important;">
                                        <br />
                                        Welcome to your Integrated Testing Program home page. Please 
                                        enter your login information below to use all your program’s features.
                                        <br />
                                        <br />
                                        <b>User Name</b><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtUserName"
                                            ErrorMessage="* Please enter your user name" Display="dynamic" runat="server"
                                            ValidationGroup="Form1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 275px">
                                        <asp:TextBox ID="TxtUserName" runat="server" Width="268px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" height="25" style="width: 275px">
                                        <b>Password</b><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtPassword"
                                            ErrorMessage="* Please enter your password" Display="dynamic" runat="server"
                                            ValidationGroup="Form1">
                                        </asp:RequiredFieldValidator>
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
                                        <asp:Label ID="Msg" runat="server" ForeColor="Red" Text="Label" Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom" style="width: 275px">
                                        <asp:ImageButton ID="BtnLogIn" runat="server" ImageUrl="images/btn_login.gif" OnClick="BtnLogIn_Click"
                                            ValidationGroup="Form1" onmouseover="this.src='images/btn_login_over.gif';" onmouseout="this.src='images/btn_login.gif';" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 5px;">
                                        <asp:LinkButton runat="server" ID="ForgotPassword" OnClick="ForgotPassword_Click">Forgot your password?</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="50" style="width: 275px">
                                        <asp:Label ID="LblResult" Visible="False" runat="server" Font-Bold="True" Width="271px">
                                        </asp:Label>
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
    <div id="popContent" style="display: none" title="Forgot your password?">
        <iframe frameborder='0' width='100%' height='100%' id='popupFrame' runat="server">
        </iframe>
    </div>
    </form>
</body>
</html>
