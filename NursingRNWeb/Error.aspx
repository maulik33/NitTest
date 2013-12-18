<%@ Page Language="C#" AutoEventWireup="true" Inherits="Error" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Kaplan Nursing</title>
    <link href="CSS/basic.css" rel="stylesheet" type="text/css" />
    <script src="JS/google.js" type="text/javascript"></script>
    <script src="JS/main.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" action="Error.aspx">
    <!-- start banner table -->
    <table cellpadding="0" cellspacing="0" border="0" align="center">
        <tr>
            <td>
                <table id="header" border="0" cellpadding="0" cellspacing="0" width="100%">
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
                <!-- end banner table -->
                <!-- start content table -->
                <table id="content" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table width="80%" border="0" cellpadding="0" cellspacing="0" align="center">
                                <tr>
                                    <td style="line-height: 25pt;">
                                        <b>Unfortunately, the function you are attempting cannot be completed. Please log back
                                            into the system and try again.<br />
                                            We apologize for the inconvenience.<br />
                                            Kaplan Nursing Team.</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <a id="login" runat="server" style="text-decoration: underline" href="S_Login.aspx">
                                            <b>Go to login page</b></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- end content table -->
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
