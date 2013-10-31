<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditTrailView.aspx.cs" Inherits="NursingRNWeb.ADMIN.AuditTrailView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Audit Trail</title>
    <link href="../CSS/basic1.css" rel="stylesheet" type="text/css" />
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="auditTrailForm" runat="server">
    <div style="padding-top: 20px;">
      
     <table align="left" border="0" class="datatable">
         <tr>
         <td class="headfont">
            <asp:Label ID="lblStudentIDTxt" runat="server" Text="Student ID :"></asp:Label>
            <asp:Label ID="lblStudentId" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
           <td align="left">
              <asp:GridView ID="gridAuditTrails" runat="server" AllowSorting="False" BackColor="White"
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="2" AllowPaging="false"
                    AutoGenerateColumns="False" DataKeyNames="AuditTrailId" CssClass="data1" Width="100%">
                    <RowStyle CssClass="datatable2a" />
                    <HeaderStyle CssClass="datatablelabels"/>
                    <AlternatingRowStyle CssClass="datatable1a" />
                    <Columns>
                        <asp:BoundField DataField="StudentUserName" HeaderText="Student User Name" SortExpression="StudentUserName">
                          <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="FromInstitution" HeaderText="From Institution" SortExpression="FromInstitution">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="FromCohort" HeaderText="From Cohort" SortExpression="FromCohort">
                            <ItemStyle HorizontalAlign="Left" Width="8%"/>
                        </asp:BoundField>
                         <asp:BoundField DataField="FromGroup" HeaderText="From Group" SortExpression="FromGroup">
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ToInstitution" HeaderText="To Institution" SortExpression="ToInstitution">
                            <ItemStyle HorizontalAlign="Left" Width="20%"/>
                        </asp:BoundField>
                         <asp:BoundField DataField="ToCohort" HeaderText="To Cohort" SortExpression="ToCohort">
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="ToGroup" HeaderText="To Group" SortExpression="ToGroup">
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="DateMoved" HeaderText="Date Moved" SortExpression="DateMoved">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AdminUserName" HeaderText="Admin User Name" SortExpression="AdminUserName">
                          <ItemStyle HorizontalAlign="Left" Width="6%"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="AdminUserId" HeaderText="Admin User ID" SortExpression="AdminUserID">
                          <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
           </tr>
          <tr>
           <b>
              <asp:Label ID="lblNoDataAvailable" Style="font-size: 16px; vertical-align:central;" runat="server" Text="No Audit Trail Data Available for this student." Width="482px" Visible="False"></asp:Label>
            </b> 
         </tr>
     </table>
    </div>
    </form>
</body>
</html>
