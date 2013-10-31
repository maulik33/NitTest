<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="FRRemediationReview.aspx.cs"
    Inherits="STUDENT.FRRemediationReview" %>

<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <HD:Head ID="Head11" runat="server" />
                <div id="med_main">
                    <div id="med_center">
                        <h2>
                            Focused Review Tests > Review Remediations</h2>
                        <div id="topbutton">
                            <a href="javascript:history.back();">
                                <img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)"
                                    onmouseout="roll(this)" border="0"></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img
                                        src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)"
                                        onmouseout="roll(this)" border="0"></a></div>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear: both;">
                            <tr>
                                <td width="150" valign="top" bgcolor="#F6F6F9">
                                    <!-- left section -->
                                    <div id="med_left_banner1">
                                        REMEDIATION Navigation</div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lb_Create" runat="server" CssClass="s8" OnClick="lb_Create_Click">Search Remediations</asp:LinkButton></div>
                                    <div class="menubar" onmouseover="this.className='menubar_over'" onmouseout="this.className='menubar'">
                                        <img src="../Temp/images/ln-bullet.gif" width="10" height="12" alt="" /><asp:LinkButton
                                            ID="lb_ListReview" runat="server" CssClass="s8" OnClick="lb_ListReview_Click">Review Remediations</asp:LinkButton></div>
                                </td>
                                <td width="8">
                                    &nbsp;
                                </td>
                                <td valign="top" align="left" style="height: 521px">
                                    <div id="Div1">
                                        <div id="Div2">
                                            <div id="med_center_banner2_l">
                                                <img src="../images/icon_type3.gif" width="13" height="13">
                                                Remediation Review
                                            </div>
                                            <asp:GridView EnableViewState="false" ID="gvList" runat="server" DataKeyNames="ReviewRemId"
                                                AutoGenerateColumns="False" EmptyDataText="No Records to display" OnRowCreated="gvList_RowCreated"
                                                OnRowCommand="gvList_RowCommand" OnRowDataBound="gvList_RowDataBound" Width="100%"
                                                CellPadding="3" OnRowDeleting="gvList_RowDeleting" BorderColor="#503792" AllowSorting="true"
                                                OnSorting="gvList_Sorting" GridLine="Both" Style="clear: both;">
                                                <RowStyle CssClass="Gridrow2" />
                                                <HeaderStyle CssClass="Gridheader" />
                                                <AlternatingRowStyle CssClass="Gridrow1" />
                                                <Columns>
                                                    <asp:BoundField DataField="ReviewRemName" HeaderText="Remediation Name" SortExpression="ReviewRemName">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Date &amp; Time" SortExpression="CreateDate" />
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbReview" runat="server" CausesValidation="false" CommandName="Review"
                                                                CommandArgument='<%#Eval("ReviewRemId")+","+Eval("NoOfRemediations")%>'>Review</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                                                CommandArgument='<%#Eval("ReviewRemId") %>'>Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="#" DataField="NoOfRemediations" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="ReviewRemName|ASC" />
                                        </div>
                                    </div>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </div>
                </div>
                <div id="med_bot">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
