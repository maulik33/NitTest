<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="True" Inherits="ADMIN_ReportCohortByStudent" Codebehind="ReportCohortByStudentRemediated.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>Aggregate Reports > Remediation Time by Cohort</b><br/><br/></td>
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
      <tr CLASS="datatable2"> 
        <td align="center">
            &nbsp;<asp:Label ID="lblStudentNumber" runat="server" Font-Bold="True"></asp:Label>
            <asp:GridView ID="gvRemediated" width="100%"  runat="server" AutoGenerateColumns="False" CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting">
           	<RowStyle CssClass="datatable2a" />
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="Remediation" HeaderText="Total Time Remediated" SortExpression="Remediation" DataFormatString="{0:T}" >
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="datatablelabels"/>
                <AlternatingRowStyle CssClass="datatable1a" />
            </asp:GridView>
            &nbsp;&nbsp;
            <asp:GridView ID="gvExplanation" width="100%"  runat="server" AutoGenerateColumns="False" CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting">
                <RowStyle CssClass="datatable2a" />
                <Columns>
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="Explanation" HeaderText="Total Time Explanation" SortExpression="Explanation" >
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                </Columns><HeaderStyle CssClass="datatablelabels"/>
                <AlternatingRowStyle CssClass="datatable1a" />
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
