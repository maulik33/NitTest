<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordConfirmation.aspx.cs"
    Inherits="PasswordConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/front.css" rel="stylesheet" type="text/css" />
    <script src="js/main.js" type="text/javascript"></script>
    <script src="JS/google.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/jquery-1.4.3.min.js"></script>
    <link href="../css/jquery-ui-1.css" rel="stylesheet" type="text/css" />
    <link href="../css/ui_002.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function (e) {
            $("#dialog-message").bind("dialogclose", function (event, ui) {
                var val = $('#hdnIsResetMailsent').val();
                if (val == 'True') {
                    window.top.location.href = "S_Login.aspx"
                }
            });
        });
        function openPopUp(message) {

            $(document).ready(function (e) {
                $("#message").text(message);
                $("#dialog-message").dialog({
                    modal: true,
                    buttons: {
                        'Close': function () {
                            $(this).dialog("close");
                        }
                    }
                });

            });
        }
    </script>
    <style type="text/css">
        .ui-dialog .ui-dialog-buttonpane
        {
            text-align: center;
        }
        .ui-dialog .ui-dialog-buttonpane button
        {
            float: none;
            border: 0px none;
        }
        
        .ui-dialog .ui-button
        {
            color: #fff;
            width: 75px;
            background: #63639D url("../Temp/images/blank_button.jpg");
        }
        
        table tr td
        {
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 10px 0px 0px 20px;">
        <p style="text-align: left">
            Forgot your password? Enter your user name and email below. We will send instructions
            on how to reset your password.
        </p>
        <table style="margin-left: 60px;">
            <tr>
                <td>
                    User Name :
                </td>
            </tr>
            <tr>
                <td>
                    <asp:textbox runat="server" id="txtUserName" width="268px" maxlength="80">
                    </asp:textbox><br />
                    <asp:requiredfieldvalidator id="rfvUserName" runat="server" errormessage="Please enter user name. "
                        controltovalidate="txtUserName" display="Dynamic" setfocusonerror="True">
                    </asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td>
                    Email :
                </td>
            </tr>
            <tr>
                <td>
                    <asp:textbox runat="server" id="txtEmail" width="268px" maxlength="80">
                    </asp:textbox><br />
                    <asp:requiredfieldvalidator id="rfvEmail" runat="server" errormessage="Please enter email."
                        controltovalidate="txtEmail" display="Dynamic" setfocusonerror="True">
                    </asp:requiredfieldvalidator>
                    <asp:regularexpressionvalidator id="regexEmail" runat="server" controltovalidate="txtEmail"
                        errormessage="Please enter a valid email address." font-size="Small" validationexpression="^.+@.+\..+$"
                        width="150px" display="Dynamic" validationgroup="Form1">
                    </asp:regularexpressionvalidator>
                </td>
            </tr>
            <tr>
                <td style="padding: 15px 0px 0px 100px;">
                    <asp:imagebutton runat="server" id="btnSendResetPasswordEmail" onclick="btnSendResetPasswordEmail_Click"
                        imageurl="~/Temp/images/btn_submit.gif">
                    </asp:imagebutton>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-message" title="Password Reset" style="display: none; text-align: left;">
        <asp:hiddenfield runat="server" id="hdnIsResetMailsent">
        </asp:hiddenfield>
        <p id="message" style="padding-top: 10px; padding-left: 5px;">
        </p>
    </div>
    </form>
</body>
</html>
