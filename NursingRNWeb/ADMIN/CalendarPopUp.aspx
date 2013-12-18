<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarPopUp.aspx.cs" Inherits="NursingRNWeb.ADMIN.CalendarPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p align="center">
        <asp:Calendar ID="Calendar1" runat="server" CssClass="Normal" BackColor="White" BorderColor="#3366CC"
            BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana"
            Font-Size="8pt" ForeColor="#330099" Height="200px" Width="220px" 
            onselectionchanged="Calendar1_SelectionChanged">
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <TodayDayStyle BackColor="#D4D4FB" ForeColor="White" />
            <SelectorStyle BackColor="#D4D4FB" ForeColor="#663399" />
            <WeekendDayStyle BackColor="#eeeeee" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#eeeeee" />
            <DayHeaderStyle BackColor="#D4D4FB" ForeColor="#663399" Height="1px" />
            <TitleStyle BackColor="#330099" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                Font-Size="10pt" ForeColor="#eeeeee" Height="25px" />
        </asp:Calendar>
    </p>
    </form>
</body>
</html>
