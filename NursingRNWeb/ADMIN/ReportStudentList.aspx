<%@ Page Language="C#" MasterPageFile="~/Admin/ReportMaster.master" AutoEventWireup="True" Inherits="ADMIN_ReportStudentList" Codebehind="ReportStudentList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" >

    $(document).ready(function () {
        ExpandContextMenu(3);
    });

</script>

	<!-- report body -->

	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>
                <asp:Label ID="lblTitle" runat="server" Text="Student Reports > Remediation Time by Question Report"></asp:Label> </b><br/><br/></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">&nbsp;</td>
          </tr>
		  <tr>
			<td style="height: 101px">

	    <table align="left" border="0" CLASS="datatable">
                  <tr CLASS="datatable1"> 
                    <td colspan=5 align="right"> <table border="0" cellpadding="0" cellspacing="0">
                        <tr> 
                          <td align="right" style="height: 23px"> 
                              <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
                          </td>
                          <td style="height: 23px"> <asp:ImageButton ID="seabtn" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                            onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="seabtn_Click" /></td>
                        </tr>
                      </table></td>
                  </tr>
                  <tr CLASS="datatable2"> 
                    <td colspan=5 align="left"> 
                    
                       <table >
                       <tr> 
                       <td> Select Institution:</td>
                        <td><asp:DropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                        </asp:DropDownList></td>
                       </tr>
                        <tr> 
                       <td>Select Cohort:</td>
                        <td><asp:DropDownList ID="ddCohort"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohort_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList><asp:ListBox ID="lbxCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple" OnSelectedIndexChanged="lbxCohort_SelectedIndexChanged" Visible="False"></asp:ListBox></td>
                       </tr>
                        <tr> 
                       <td>Select Group:</td>
                        <td> <asp:DropDownList ID="ddGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddGroup_SelectedIndexChanged">
                        </asp:DropDownList></td>
                       </tr>
                       </table>
                        <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile the report"
                            Visible="False" Width="347px"></asp:Label></td>
                  </tr>
                </table>

	<!-- end report body -->

	</td>
  </tr>

</table>
 <asp:GridView ID="gridUsers" Width="100%" runat="server" AutoGenerateColumns="False" OnRowCommand="gridUsers_RowCommand" OnRowDataBound="gridUsers_RowDataBound" cellpadding="5" AllowSorting="True" OnSorting="gridUsers_Sorting">
     
             <RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels"/>
            <AlternatingRowStyle CssClass="datatable1a" />

     <Columns>
         <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
         <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
         <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
         <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
         <asp:ButtonField HeaderText="View" CommandName="View" Text="View" />
     </Columns>
     </asp:GridView>
</asp:Content> 