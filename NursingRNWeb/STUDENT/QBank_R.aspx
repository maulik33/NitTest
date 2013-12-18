<%@ Page Language="C#"   AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.QBank_R" Codebehind="QBank_R.aspx.cs" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css">
    <script  type="text/javascript" src="../js/main.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
		 <table align="center"  border="0" cellspacing="0" cellpadding="0"><tr><td>
     <HD:Head ID="Head11" runat="server" />
    <div id="med_main">

  <div id="med_center">

	<h2>NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg; Prep > <%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %> Qbank > Review Results</h2>
	 <div id="topbutton">
 
  <a href="javascript:history.back();"><img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear:both;">
	<tr>
   
	<td width="150" valign="top" bgcolor="#F6F6F9">
 <!-- left section --> 

    <div id="med_left_banner1"><%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %> QBANK Navigation</div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Create"
            runat="server" OnClick="lb_Create_Click" CssClass="s8">Create Test</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_ListReview"
            runat="server" OnClick="lb_ListReview_Click" CssClass="s8">Previous Tests</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Analysis"
            runat="server" OnClick="lb_Analysis_Click" CssClass="s8">Cumulative Performance</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Sample"
            runat="server" OnClick="lb_Sample_Click" CssClass="s8">NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg; Prep Sample Tests</asp:LinkButton></div>
     </td>
    <td width="8">&nbsp;</td>
	
	<td valign="top" align="left" style="height: 521px">
    
		
<div id="Div1">

  <div id="Div2">
    
	
	<div id="med_center_banner2_l">
	<img src="../images/icon_type3.gif" width="13" height="13"> TEST ANALYSIS
	</div>
      <asp:GridView EnableViewState="false" ID="gvList" runat="server" AutoGenerateColumns="False" DataKeyNames="UserTestId,TestId,QuizOrQBank" OnRowDataBound="gvList_RowDataBound"
          Width="100%" OnRowCommand="gvList_RowCommand" cellpadding="3" AllowSorting="true"  BorderColor="#503792" GridLine ="Both" OnSorting="gvList_Sorting" style="clear:both;">
            
			<RowStyle CssClass="Gridrow2" />
            <HeaderStyle CssClass="Gridheader"/>
            <AlternatingRowStyle CssClass="Gridrow1" />

          <Columns>
              <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression ="TestName">
                  <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="TestStarted" HeaderText="Date &amp; Time" SortExpression ="TestStarted"  />
              <asp:BoundField HeaderText="Score" DataField="PercentCorrect"/>
              <asp:TemplateField HeaderText="View">
                  <ItemTemplate>
                      <asp:LinkButton ID="lb1" runat="server"></asp:LinkButton>
                      <asp:LinkButton ID="lb2" runat="server"></asp:LinkButton>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="TestStatus" HeaderText="Status " />
              <asp:BoundField HeaderText="# of ?" DataField="QuestionCount" />
          </Columns>
      </asp:GridView>
      <br />
      <asp:GridView  EnableViewState="false" ID="gv_Sample" runat="server" AutoGenerateColumns="False" DataKeyNames="UserTestId,TestId,QuizOrQBank" OnRowDataBound="gv_Sample_RowDataBound"
          Width="100%" OnRowCommand="gv_Sample_RowCommand" cellpadding="3" BorderColor="#503792" GridLine ="Both" AllowSorting="True" OnSorting="gv_Sample_Sorting">
          <Columns>
              <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression ="TestName">
                  <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="TestStarted" HeaderText="Date &amp; Time" SortExpression ="TestStarted"/>
              <asp:BoundField HeaderText="Score" DataField="PercentCorrect" />
              <asp:TemplateField HeaderText="View">
                  <ItemTemplate>
                      <asp:LinkButton ID="lb1" runat="server"></asp:LinkButton>
                      <asp:LinkButton ID="lb2" runat="server"></asp:LinkButton>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="TestStatus" HeaderText="Status " />
              <asp:BoundField HeaderText="# of ?" DataField="QuestionCount" />
          </Columns>
          <RowStyle CssClass="Gridrow2" />
          <HeaderStyle CssClass="Gridheader"/>
          <AlternatingRowStyle CssClass="Gridrow1" />
      </asp:GridView>

</div>
</div>
		

	</tr>

</table>
		
		<br/><br/>
	

 </div>


</div>


<div id="med_bot">

</div>
  </td></tr></table>


    </form>
</body>
</html>
