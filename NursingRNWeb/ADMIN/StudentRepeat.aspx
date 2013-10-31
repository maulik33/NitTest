<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRepeat.aspx.cs"
    Inherits="NursingRNWeb.ADMIN.StudentRepeat" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Student Repeat</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript">
        var closeValue = new Object();
        closeValue.expiryDate = "";
        closeValue.closed = "BrowserClose";
        function OnMove() {
            var date = new Date();
            var today = new Date((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
            var repeatDate = document.getElementById('<%=txtRepeatDate.ClientID%>').value;
            var validDate = Date.parse(repeatDate);
            if (repeatDate != "") {
                if (isNaN(validDate)) {
                    document.getElementById('<%=lblMsg.ClientID%>').style.display = "inline";
                      document.getElementById('<%=lblMsg.ClientID%>').innerHTML = "Please select a valid date";
                      return false;
                  }
                var rDate = new Date(repeatDate);
                if (rDate < today) {
                    document.getElementById('<%=lblMsg.ClientID%>').style.display = "inline";
                    document.getElementById('<%=lblMsg.ClientID%>').innerHTML = "Please set selected date to be equal or greater than current date.";
                    return false;
                }
            }
            closeValue.expiryDate = document.getElementById('<%=txtRepeatDate.ClientID%>').value;
            closeValue.closed = "OK";
            return true;
        }

        function winClose() {
            window.returnValue = closeValue;
            window.close();
        }
    </script>
</head>
<body onbeforeunload="winClose()">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="2" cellpadding="3">
        <tr>
            <td align="left" style="border-bottom: 1px solid #330099;">
               <b> Repeat </b>
            </td>
        </tr>
        <tr class="datatable1">
            <td style="border-bottom: 1px solid #330099;">
                 <div style="margin-left: 10px">
                <br /><b>
                    Is this a repeating student? If yes, please
                    <br />
                    provide an expiration date, otherwise
                    <br />
                    leave date blank and click on Move.</b>
                <br /><br/>
                <asp:TextBox ID="txtRepeatDate" runat="server" Style="cursor: text;"></asp:TextBox>
                <asp:Image ID="imgRepeatDate" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                <br />
                 <div style="margin-left: 20px">
                <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Style="display: none;" /></div><br/>
                <asp:Button ID="btnMove" runat="server" OnClientClick="return OnMove()" Text="MOVE" /></div><br/><br/>
            </td>
        </tr>
       </table>
    </form>
</body>
</html>
