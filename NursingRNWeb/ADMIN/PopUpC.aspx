<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_PopUpC" Codebehind="PopUpC.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Kaplan Nursing</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Calendar ID="calDate" OnSelectionChanged="Change_Date" Runat="server" BackColor="#F0F0FE" BorderColor="#D4D4FB" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px" >
        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
        <TodayDayStyle BackColor="#D4D4FB" ForeColor="White" />
        <SelectorStyle BackColor="#D4D4FB" />
        <OtherMonthDayStyle ForeColor="#CC9966" />
        <NextPrevStyle Font-Size="9pt" ForeColor="#F0F0FE" />
        <DayHeaderStyle BackColor="#D4D4FB" Font-Bold="True" Height="1px" />
        <TitleStyle BackColor="#330099" Font-Bold="True" Font-Size="9pt" ForeColor="#F0F0FE" />
    </asp:Calendar>
    <input type="hidden" id="control" runat="server" />
    </div>
    </form>
</body>
</html>
