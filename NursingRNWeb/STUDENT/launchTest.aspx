<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
   <script src="../JS/google.js" type="text/javascript"></script>
</head>
<body>
    <%--<form id="Form1" method="post" action="http://jasp.kap.com:4000/gogo.aspx">--%>
    <form id="Form1" method="post" action="http://jasperqa.kaplan.com/receiveCustomer.aspx">
    <%--<form id="Form1" method="post" action="http://jasper.kaptest.com/receiveCustomer.aspx">--%>
    <div id="loader">
        Loading quiz...</div>
    <input type="hidden" name="USERID" value="<%=Request.QueryString["ProfileID"] %>" /><br />
    <input type="hidden" id="PRODUCTCODE" name="PRODUCTCODE" value="<%=Request.QueryString["productCode"]%>" /><br />
    <input type="hidden" id="ASSETID" name="ASSETID" value="<%=Request.QueryString["assetCode"]%>" /><br />
    <input type="hidden" id="INSTANCEID" name="INSTANCEID" value="<%=Request.QueryString["enrollmentID"]%>" /><br />
    <input type="hidden" id="COMMAND" name="COMMAND" value="start" /><br />
    <input type="hidden" id="EXPIRES" name="EXPIRES" value="2010-01-01" /><br />
    <input type="hidden" id="LOGIN" name="LOGIN" value="plstest1" /><br />
    <!--<input type="submit" value="submit">-->

    <script type="text/javascript">

   document.forms[0].submit();

    </script>

    </form>
</body>
</html>
