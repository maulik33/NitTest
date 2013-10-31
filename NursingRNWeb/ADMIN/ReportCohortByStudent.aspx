<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="True" Inherits="ADMIN_ReportCohortByStudent" Codebehind="ReportCohortByStudent.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>Aggregate Reports > Cohort By Student </b><br/><br/></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">&nbsp; </td>
          </tr>
		  <tr>
			<td>

	  <table align="left" border="0" CLASS="datatable_rep">
      <tr CLASS="datatable2"> 
        <td colspan=11 align="left">
          <table width="100%">
            
              <tr>
                 <td> Institution:</td>
                 <td><asp:DropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged"> </asp:DropDownList></td>
                  <td rowspan="4" align="right">
                                       <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_pfv.gif" OnClick="ImageButton2_Click" /><br />
                      <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexcel.gif" style="margin-top:3px;" OnClick="ImageButton3_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."/><br />
                      <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif" style="margin-top:3px;" OnClick="ImageButton4_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."/> 
                  </td>
              </tr>
              <tr>
                 <td> Cohort:</td>
                 <td><asp:DropDownList ID="ddCohorts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohorts_SelectedIndexChanged"> </asp:DropDownList></td>
              </tr>
              <tr>
                 <td>  Test Type:</td>
                 <td><asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged"></asp:DropDownList></td>
              </tr>
              <tr>
                 <td>  Test Name:</td>
                 <td><asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged"> </asp:DropDownList></td>
              </tr>
            </table>
            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a this report"
                Visible="False" Width="347px"></asp:Label></td>
      </tr>
      <tr><td align="center">
          <asp:Label ID="lblN" runat="server" Text="N=" Font-Bold="True"></asp:Label>
          <asp:Label ID="lblStudentNumber" runat="server" Font-Bold="True"></asp:Label></td></tr>
      <tr CLASS="datatable2"> 
        <td>
            <asp:GridView ID="gvCohorts" width="100%"  runat="server" AutoGenerateColumns="False" DataKeyNames="CohortID,UserID" OnRowCommand="gvCohorts_RowCommand" CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting">
           	<RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels"/>
            <AlternatingRowStyle CssClass="datatable1a" />

            <Columns>
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                <asp:BoundField HeaderText="Overall % Correct" DataField="Percantage" SortExpression="Percantage" />
                <asp:ButtonField CommandName="Performance" Text="Student Performance Report" />
                <asp:ButtonField CommandName="Questions" Text="Student Results by Question" />
            </Columns>
           </asp:GridView>
       </td>
    </tr>
</table>
       
</td> 
</tr> 
</table> 
                      <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Temp/images/printbtn.gif"
                          OnClick="ImageButton1_Click" Visible="False" />

</asp:Content>
