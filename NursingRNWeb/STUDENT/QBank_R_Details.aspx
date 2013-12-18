<%@ Page Language="C#" AutoEventWireup="true" Inherits="STUDENT.QBankRDetails" Codebehind="QBank_R_Details.aspx.cs" %>
<%@ Register TagPrefix="HD" TagName="Head" Src="~/Student/ASCX/head.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Kaplan Nursing</title>
    <link href="../css/front.css" rel="stylesheet" type="text/css">
    <script  type="text/javascript" src="../js/main.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
     <HD:Head ID="Head11" runat=server />
    <div id="med_main">

  <div id="med_center">

	<h2>NCLEX-RN&reg; Prep > Qbank > Review Results</h2>
	
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
   
	<td width="150" valign="top" bgcolor="#F6F6F9">
 <!-- left section --> 

    <div id="med_left_banner1">QBANK Navigation</div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Create"
            runat="server" OnClick="lb_Create_Click">Create Test</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_ListReview"
            runat="server" OnClick="lb_ListReview_Click">Previous Tests</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12"><asp:LinkButton ID="lb_Analysis"
            runat="server" OnClick="lb_Analysis_Click">Cumulative Performance</asp:LinkButton></div>
	<div class="menubar" onMouseOver="this.className='menubar_over'" onMouseOut="this.className='menubar'"><img src="../Temp/images/ln-bullet.gif" width="10" height="12">
        <asp:LinkButton ID="lb_Sample" runat="server" OnClick="lb_Analysis_Click">NCLEX-RN &reg; Prep Sample Tests</asp:LinkButton></div>
     </td>
    <td style="width: 8px">&nbsp;</td>
	
	<td valign="top" align="left" style="height: 521px">
    
		
<div id="Div1">

  <div id="Div2">
    
	<h2>
        <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h2>
  <br/>
  <div id="topbutton">
  <a href="student_frt_rep_ana.asp"></a>&nbsp;<asp:ImageButton ID="btnReview" runat="server"
      ImageUrl="../images/reviewtest.gif" OnClick="btnReview_Click" />&nbsp;&nbsp;
  <a href="javascript:history.back();"><img src="../images/backNav_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a>&nbsp;&nbsp;&nbsp;<a href="user_home.aspx"><img src="../images/backtohome_over.gif" width="75" height="25" onmouseover="roll(this)" onmouseout="roll(this)" border=0 ></a></div>

<div id="med_center_banner3">OVERALL REPORT</div>
	
	<table width="100%" border="0" cellspacing="0" cellpadding="8">
	<tr> 
	<td width="50%">
	<B>Overall Percent Correct: 
        <asp:Label ID="lblOPC" runat="server" Text=""></asp:Label></B>
   
    
		<table width="90%" border="0" cellspacing="0" cellpadding="0">
		<tr> 
		<td width="19%" rowspan="4">
       
            <script type="text/javascript">

				var fo = new FlashObject("Charts/FC_2_3_MSColumn2D.swf", "FC2Column", "150", "250", "7", "white", true);
				fo.addParam("allowScriptAccess", "always");
				fo.addParam("scale", "noScale");
				fo.addParam("menu", "false");
				fo.addVariable("dataURL", "<%=strDataURL1%>");
				fo.addVariable("chartWidth","150");
				fo.addVariable("ChartHeight","250");
				fo.write("divchart");
		   </script>
        
       
        </td>
		<td><br/>
		<br/>
		<br/>
		<br/><br/><br/>
		<img src="../Temp/images/baricon.gif" width="12" height="12"> <B>My Overall Correct</B></td>
		<td style="width: 40px">&nbsp;</td>
		</tr>
		<tr> 
		<td>Number correct:</td>
		<td style="width: 40px">
            <asp:Label ID="lblNumberCorrect" runat="server" Text=""></asp:Label></td>
		</tr>
		<tr> 
		<td>Number incorrect:</td>
		<td style="width: 40px"><asp:Label ID="lblNumberIncorrect" runat="server" Text=""></asp:Label></td>
		</tr>
		<tr> 
		<td>Number not reached:</td>
		<td style="width: 40px">
            <asp:Label ID="lblNotAnswered" runat="server" Width="40px"></asp:Label></td>
		</tr>
		</table>
	</td>
	<td align="left">
	
	</td>
	<td rowspan="2" valign="top" align="center"> <div style="text-align:left;margin-left:30px;"> 
		<table width="55%" border="0" cellspacing="0" cellpadding="5" style="margin-top:50px;">
		  <tr> 
			<td colspan="2" height="120" valign="bottom"><B>Answer Changes</B></td>
		  </tr>
		<tr> 
		<td>Correct to Incorrect:</td>
		<td>
            <asp:Label ID="lblC_I" runat="server" Text="12"></asp:Label></td>
		</tr>
		<tr> 
		<td >Incorrect to Correct:</td>
		<td>
            <asp:Label ID="lblI_C" runat="server" Text="5"></asp:Label></td>
		</tr>
		<tr> 
		<td>Incorrect to Incorrect:</td>
		<td>
            <asp:Label ID="lblI_I" runat="server" Text="6"></asp:Label></td>
		</tr>
		</table>
	</div>
	</td>
	</tr>
	</table>
    <div>
   
    </div> 
   <div id="D_Graph" runat="server" >

 </div>

</div>
		

	</tr>

</table>
		
		<br/><br/>
	

 </div>


</div>


<div id="med_bot">

</div>


    </form>
</body>
</html>
