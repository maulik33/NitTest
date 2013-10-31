<%@ Page Language="C#" MasterPageFile="~/Admin/ReportMaster.master" AutoEventWireup="true" Inherits="ADMIN_ReportHome" Codebehind="ReportHome.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>Aggregate Reports > By Cohort Summary</b><br/><br/></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">&nbsp; </td>
          </tr>
		  <tr>
			<td>

	    <table align="left" border="0" CLASS="datatable_rep">
      <tr CLASS="datatable2"> 
        <td colspan=11 align="left"> 
		Select Cohort: &nbsp; <select name="select">
            <option>All</option>
            <option>Cohort A</option>
            <option>Cohort B</option>
            <option>Cohort C</option>
          </select> &nbsp;&nbsp;&nbsp; 
		 Test Category: &nbsp; <select name="select">
            <option>All</option>
            <option>Category A</option>
            <option>Category B</option>
            <option>Category C</option>
          </select>
          &nbsp;&nbsp;&nbsp; Test Name: &nbsp; <select name="select">
            <option>Choose Test</option>
            <option>Test Name A</option>
            <option>Test Name B</option>
            <option>Test Name C</option>
          </select>
		  </td>
      </tr>
      <tr CLASS="datatable2"> 

      </tr>
      <tr CLASS="datatablelabels"> 
        <td align="center" class="datatablelabels"><b>Cohort</b></a> </td>
        <td align="center" class="datatablelabels"><b>% Correct</b></a> </td>
        <td align="center" class="datatablelabels"></td>

      </tr>
      <tr CLASS="datatable2"> 
        <td CLASS="datatable">Cohort 1 </td>
        <td CLASS="datatable">56%</td>
        <td CLASS="datatable"><a href="report_coho_per.asp">Performance Report</a></td>
 
      </tr>
      <tr CLASS="datatable1"> 
        <td CLASS="datatable">Cohort 2 </td>
        <td CLASS="datatable">78%</td>
        <td CLASS="datatable"><a href="report_coho_per.asp">Performance Report</a></td>
 
      </tr>
      <tr CLASS="datatable2"> 
        <td CLASS="datatable">Cohort 3</td>
        <td CLASS="datatable">62%</td>
        <td CLASS="datatable"><a href="report_coho_per.asp">Performance Report</a></td>
 
      </tr>
      <tr CLASS="datatable1"> 
        <td CLASS="datatable">Cohort 4</td>
        <td CLASS="datatable">92%</td>
        <td CLASS="datatable"><a href="report_coho_per.asp">Performance Report</a></td>
     </tr> 
    </table> 
	<!-- end report body -->

          </td>
         </tr>
       </table> 
</asp:Content> 


