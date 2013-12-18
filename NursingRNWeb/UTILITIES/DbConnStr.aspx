<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DbConnStr.aspx.cs" Inherits="NursingRNWeb.UTILITIES.DbConnStr" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Active Databases</title>
</head>
<body>
    <form id="form1" runat="server">
   <div id="accountLoginDiv" runat="server">
          <table>
              <tr>
                 <asp:label Text="Enter your KEC Credentials." Style="font-size:20px;font-weight: bold" runat="server"></asp:label>
             </tr>
              <tr>
                  <td style="width: 110px">
                     <asp:label Text="KEC UserName" runat="server"></asp:label>
                   </td>
                  <td>
                    <asp:TextBox ID ="txtUserName" runat="server"></asp:TextBox> 
                  </td>
            </tr>
            <tr>
                <td style="width: 110px" >
                    <asp:label Text="KEC Password  " runat="server"></asp:label>
                </td>
                <td>
                  <asp:TextBox ID ="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
               </td>
            </tr>
               <tr>
                <td style="width: 110px"></td>
               <td>
                   <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"  runat="server"></asp:Button>
               </td>
            </tr>
          </table>  
           <asp:label ID="errMsgLbl" Text="Invalid Domain User. Please Enter Valid KEC Credentials." Style="font-size:18px;color: red" Visible="False" runat="server"></asp:label>
          
        </div>
        <div id="displayStringDiv" runat="server" Visible="False">
        <table cellspacing="2" style="padding:10% 10% 0 10%;width:100%">
        <tr>
            <th>KEY</th>
            <th>Value</th>
        </tr>
        <% foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
           {
               
               if (item.Name.Equals("OraAspNetConString"))
               {
                   continue;
               }
               if (string.IsNullOrEmpty(item.ProviderName) && !string.IsNullOrEmpty(item.ConnectionString))
               {
                   var builder = new SqlConnectionStringBuilder(item.ConnectionString);
                   string output = "Data Source = " + builder.DataSource + "; Initial Catalog = " + builder.InitialCatalog;
                   %>
                           
                    <tr align="left">
                        <td >                
                            <%= item.Name%>                
                        </td>
                        <td >
                            <%= output%>
                        </td>
                    </tr>
        <% }
           }%>
        </table>
      </div>
    </form>
</body>
</html>
