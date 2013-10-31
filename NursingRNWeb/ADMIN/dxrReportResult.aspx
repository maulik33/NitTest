<%@ Page Language="C#" AutoEventWireup="true" Inherits="STUDENT_Launchdxr" Codebehind="dxrReportResult.aspx.cs" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="head.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td style="width: 871px">
                    <HD:Head ID="Head11" runat="server" />
                </td>
            </tr>
            <tr>
            <td>
            <br />
            
            <p>
                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="True" 
                    Font-Size="Medium" Width="80%"></asp:Label>
           
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
