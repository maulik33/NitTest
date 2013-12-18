<%@ Page Language="C#" AutoEventWireup="true" Inherits="Service.LaunchDxr" Codebehind="Launchdxr.aspx.cs" %>

<head>
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/basic.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>

    <script src="../JS/google.js" type="text/javascript"></script>

</head>
<body>
    <form id="Form1" runat="server">
    <table id="header" border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
        <tr>
            <td width="172">
                &nbsp;
            </td>
            <td align="right" valign="bottom">
            </td>
        </tr>
    </table>
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <br />
                <p>
                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="True" Font-Size="Medium"
                        Width="80%"></asp:Label>
                </p>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <input id="btnClose" type="button" value="Close window" onclick="window.close()" />
            </td>
        </tr>
    </table>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <input type="hidden" id="cid" runat="server" />
        <input type="hidden" id="eid" runat="server" />
        <input type="hidden" id="ts" runat="server" />
        <input id="st" type="hidden" runat="server" />
    </div>
    </form>
</body>
</html> 