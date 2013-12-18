<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" Inherits="STUDENT.StudentInt" Codebehind="student_int.aspx.cs" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript" src="../js/main.js"></script>

    <script type="text/javascript" src="../js/js-security.js"></script>
    
   <script src="../JS/google.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <table id="med_main_f" border="0" cellspacing="18" cellpadding="0">
                    <tr>
                        <td align="center" valign="top">
                            <h2>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h2>
                            <br />
                            <div id="topbutton">
                                <a href="student_frt_rep_ana.asp"></a>&nbsp;<asp:ImageButton ID="btnAnalyze" runat="server"
                                    ImageUrl="../images/anatest.gif" onmouseover="roll(this)" onmouseout="roll(this)"
                                    OnClick="btnAnalyze_Click" />&nbsp;&nbsp; <a href="javascript:history.back();">
                                        <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                            onmouseout="roll(this)" border="0"></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                                src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                                onmouseout="roll(this)" border="0"></a></div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="8" style="margin-bottom: 8px;
                                clear: both;" bgcolor="E2DDF0">
                                <tr>
                                    <td style="width: 29%">
                                        <b>Test Type:</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="5%">
                                        &nbsp;
                                    </td>
                                    <td width="20%">
                                        <b>Test Name:</b>
                                    </td>
                                    <td width="54%">
                                        <asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div id="med_center_banner2_l">
                                <img src="../images/icon_type3.gif" width="13" height="13">
                                Review Results
                            </div>
                            <asp:GridView EnableViewState="false" Style="clear: both;" ID="gvIntegrated" OnRowDataBound="gvIntegrated_RowDataBound"
                                OnRowCommand="gvIntegrated_RowCommand" DataKeyNames="QID,TypeOfFileID,RemediationID,TopicTitle,Correct"
                                runat="server" AutoGenerateColumns="False" CssClass="GridView1ChildStyle" Width="100%"
                                CellPadding="2" BorderColor="#503792" GridLine="Both">
                                <RowStyle CssClass="Gridrow2" />
                                <HeaderStyle CssClass="Gridheader" />
                                <AlternatingRowStyle CssClass="Gridrow1" />
                            </asp:GridView>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hfcategories"/>
    </form>
</body>
</html>
