<%@ Page Language="C#" AutoEventWireup="True" Inherits="STUDENT.STUDENT_AccessDenied" Codebehind="AccessDenied.aspx.cs" %>
<%@ Register TagPrefix="KTP" Namespace="WebControls" Assembly="NursingRNWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>
                <KTP:Messenger ID="Messenger1" runat="server" />
            </h2>
            <br />
        </div>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/STUDENT/user_home.aspx">Home</asp:LinkButton>
    </form>
</body>
</html>
