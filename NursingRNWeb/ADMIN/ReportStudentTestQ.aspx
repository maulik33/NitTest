<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Admin/ReportMaster.master" Inherits="ADMIN_ReportStudentTestQ" Codebehind="ReportStudentTestQ.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	
	<!-- report body -->

	<table border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont" style="height: 57px"><b>Student Reports > Overall Student Performance by Question</b><br/><br/>
                Student Name:
                <asp:Label ID="lblName" runat="server" Width="155px"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; <a
                    href="report_question_student.aspx"></a></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">&nbsp;</td>
          </tr>
		  <tr>
			<td>

	    <table align="left" border="0" CLASS="datatable_rep">

      <tr CLASS="datatable2"> 
        <td colspan=11 align="left"> 
		 Test Type: &nbsp; &nbsp;&nbsp;<asp:DropDownList ID="ddProducts"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp; Test Name: &nbsp; &nbsp;&nbsp;<asp:DropDownList ID="ddTests"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged">
            </asp:DropDownList>
		  </td>
      </tr>
	 </table>

	 	</td>
  </tr>

</table><br/>
<asp:GridView ID="gvIntegrated" OnRowDataBound="gvIntegrated_RowDataBound"  DataKeyNames="QID,TypeOfFileID,RemediationID,TopicTitle,Correct" runat="server"  AutoGenerateColumns="False" CssClass="GridView1ChildStyle" Width="100%" OnRowCommand="gvIntegrated_RowCommand" cellpadding="5">
     
             <RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels"/>
            <AlternatingRowStyle CssClass="datatable1a" />

    </asp:GridView>
<br/>
	
	

    <!-- section A -->
   

	

   <!-- end content table -->
</asp:Content>

