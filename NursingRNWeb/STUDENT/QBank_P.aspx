<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="STUDENT.QBankP" Codebehind="QBank_P.aspx.cs" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css" />
    <script  type="text/javascript" src="../js/main.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
			 <table align="center"  border="0" cellspacing="0" cellpadding="0"><tr><td>
     <HD:Head ID="Head11" runat=server />
    <div id="med_main">

  <div id="med_center">

	<h2>NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg; Prep > <%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %> Qbank > Review Results</h2>
	
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="clear:both;">
	<tr>
   
	<td width="150" valign="top" bgcolor="#F6F6F9">
 <!-- left section --> 

    <div id="med_left_banner1">QBANK Navigation</div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Create"
            runat="server" OnClick="lb_Create_Click" CssClass="s8">Create Test</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_ListReview"
            runat="server" OnClick="lb_ListReview_Click" CssClass="s8">Previous Tests</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Analysis"
            runat="server" OnClick="lb_Analysis_Click" CssClass="s8">Cumulative Performance</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12">
        <asp:LinkButton ID="lb_Sample" runat="server" OnClick="lb_Sample_Click" CssClass="s8">NCLEX-<%= HttpUtility.HtmlEncode(QBankProgramofStudyName) %>&reg;Prep Sample Tests</asp:LinkButton></div>
     </td>
    <td style="width: 8px">&nbsp;</td>
	
	<td valign="top" align="left" style="height: 521px" >
    
		
<div id="Div1">

  <div id="Div2">
    
	<h2 style="margin-top:0px;"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></h2>
  <div id="topbutton">
 
  <a href="javascript:history.back();"><img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a></div>

<div id="med_center_banner3">OVERALL REPORT</div>
	 
	<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top:10px;clear:both;">
		<tr> 

			<td width="30%">
		   	<B>Overall Percent Correct: 
        <asp:Label ID="lblOPC" runat="server" Text=""></asp:Label></B>
				<script type="text/javascript">

					var fo = new FlashObject("Charts/FC_2_3_MSColumn2D.swf", "FC2Column", "150", "250", "7", "white", true);
					fo.addParam("allowScriptAccess", "always");
					fo.addParam("scale", "noScale");
					fo.addParam("menu", "false");
					fo.addVariable("dataURL", "<%=StrDataURL1%>");
					fo.addVariable("chartWidth","150");
					fo.addVariable("ChartHeight","250");
					fo.write("divchart");
			   </script>
			
		   
			</td>
			
			<td valign="top" width="70%">
			        <table width="95%" border="0" cellspacing="0" cellpadding="5" style="margin-top:100px;">
					<tr>
					<td colspan=2>
					<B>My Overall Correct</B></td>
					<td colspan="2" valign="bottom"><B>Answer Changes</B></td>

					</tr>
					<tr> 
					<td>Number correct:</td>
					<td>
						<asp:Label ID="lblNumberCorrect" runat="server" Text=""></asp:Label></td>
						<td>Correct to Incorrect:</td>
					<td>
						<asp:Label ID="lblC_I" runat="server" Text="12"></asp:Label></td>
					</tr>
					<tr> 
					<td>Number incorrect:</td>
					<td><asp:Label ID="lblNumberIncorrect" runat="server" Text=""></asp:Label></td>
					<td >Incorrect to Correct:</td>
					<td>
						<asp:Label ID="lblI_C" runat="server" Text="5"></asp:Label></td>
					</tr>
					<tr> 
					<td>Number not reached:</td>
					<td>
						<asp:Label ID="lblNotAnswered" runat="server" Width="40px"></asp:Label></td>
						<td>Incorrect to Incorrect:</td>
					<td>
						<asp:Label ID="lblI_I" runat="server" Text="6"></asp:Label></td>
					</tr>
					</table>

			</td>
		   </tr>
  </table>
    <div><b>Cumulative Score By Test</b></div>

    <div style="overflow-x:auto;width:600px">
        <script type="text/javascript">
            var fo = new FlashObject("Charts/FC_2_3_MSColumn3D.swf", "FC2Column", "<%=StrLength%>", "400", "7", "white", true);
				fo.addParam("allowScriptAccess", "always");
				fo.addParam("scale", "noScale");
				fo.addParam("menu", "false");
				fo.addVariable("dataURL", "<%=StrDataURL2%>");
				fo.addVariable("chartWidth", "<%=StrLength%>");
				fo.addVariable("ChartHeight","420");
				fo.write("divchart");
		</script>
	
    
    </div> 
    <!-- section A -->
    <div id="C1_Title" runat="server" class="med_center_banner5" style="padding-left:15px;"></div>
	<br/>
	<div id="C1" runat="server" >

    </div> 
     <!-- section B-->
    <div id="C2_Title" runat="server"  class="med_center_banner5" style="padding-left:15px;"></div>
	<br/>
	<div id="C2" runat ="server" >
        
    </div>
    <!-- section C-->
    <div id="C3_Title" runat="server"  class="med_center_banner5"  style="padding-left:15px;"></div>
	<br/>
	<div id="C3" runat="server" >

    </div>

 </div>

</div>
		

	</tr>

</table>
		
		<br/>
      <br/>
	

 </div>


</div>


<div id="med_bot">

</div>

  </td></tr></table>
    </form>
</body>
</html>
