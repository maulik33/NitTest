<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/ADMIN/ReportMaster.master" Inherits="ADMIN_ReportBycohort" Codebehind="ReportByCohort.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--

	
	

	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>Aggregate Reports > Tests by Cohort </b><br/><br/></td>
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
                  <td rowspan="3" align="right">
                      <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_pfv.gif" OnClick="ImageButton1_Click1" /><br />
                      <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_toexcel.gif" style="margin-top:3px;" OnClick="ImageButton2_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."/><br />
                      <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexceldata.gif" style="margin-top:3px;" OnClick="ImageButton3_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."/>
                      </td>
                  <td>
                  </td>
              </tr>
               <tr>
                 <td>  Test Type:</td>
                 <td><asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
            </asp:DropDownList></td>
                   <td>
                   </td>
              </tr>
               <tr>
                 <td style="height: 27px">  Test Name:</td>
                 <td style="height: 27px"><asp:DropDownList ID="ddTests" runat="server" 
                         AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged" 
                         Visible="False">
            </asp:DropDownList>&nbsp;
                     <asp:ListBox ID="lbTests" runat="server" AutoPostBack="True" 
                         onselectedindexchanged="ListBox1_SelectedIndexChanged" SelectionMode="Multiple">
                     </asp:ListBox>
                 </td>
                   <td style="height: 27px">
                       </td>
              </tr>
            </table>
            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                Visible="False" Width="347px"></asp:Label></td> 
      </tr>
     
      <tr>
        <td>
            <asp:GridView Width="100%" ID="gvCohorts" runat="server" AutoGenerateColumns="False" DataKeyNames="CohortID" OnRowCommand="gvCohorts_RowCommand" OnRowDataBound="gvCohorts_RowDataBound" CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting">
           	<RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels"/>
            <AlternatingRowStyle CssClass="datatable1a" />
            <Columns>
                <asp:BoundField DataField="CohortName" HeaderText="Cohort" SortExpression="CohortName" />
                <asp:BoundField DataField="NStudents" HeaderText="# Students" SortExpression="NStudents" />
                <asp:BoundField HeaderText=" % Correct" DataField="Percantage" SortExpression="Percantage" />
                <asp:ButtonField CommandName="Performance" Text="Cohort Performance Report" />
            </Columns>
           </asp:GridView>
       </td>
    </tr>
</table>
       
</td> 
</tr> 
</table> 
                      <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Show Preview" Visible="False" />
    &nbsp;
                       <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Direct Print" Visible="False" />
--%>
</asp:Content>
