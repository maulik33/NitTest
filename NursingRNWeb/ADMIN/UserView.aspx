<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_UserView" Codebehind="UserView.aspx.cs" %>

<%@ Register Src="~/ADMIN/Controls/Address.ascx" TagName="Address" TagPrefix="ucAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">

		  <tr> 
            <td colspan="2" class="headfont"><b>Add/Edit/View > Student Details</b></td>
          </tr>
<tr> 
<td colspan="2" align="left"></td>
</tr>

<tr> 
<td colspan="2">

           <table align="left" border="0" CLASS="datatable">

				 <tr class="datatablelabels"> 
					<td align="left" colspan="3">
                     <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Student successfully updated"></asp:Label>
					</td>
				  </tr>
				 <tr CLASS="datatable2"> 
				    
					 
					<td CLASS="datatable2" align="Left" width="25%">
                        User Name: &nbsp;
					</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblUserName" runat="server" Text="Text here" Width="180px"></asp:Label></td>
            	  
               <td rowspan="6" align="left" valign="middle" style="width: 199px">	
                        <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click" Width="101px">Edit</asp:LinkButton><br/><br/>
                        <asp:LinkButton ID="lbNew" runat="server" OnClick="lbNew_Click" Width="197px" Visible="False">Add Additional Student</asp:LinkButton>
               <asp:DropDownList ID="ddInstitution" runat="server" Enabled="False" Visible="False">
                       </asp:DropDownList>
                   <asp:DropDownList ID="ddCohort" runat="server" Enabled="False" Visible="False">
                       </asp:DropDownList>
                   <asp:DropDownList ID="ddGroup" runat="server" Enabled="False" Visible="False">
                       </asp:DropDownList></td>				  
				  
				  </tr>

				 <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
                        Password: &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblPassword" runat="server" Text="Auto Password goes here" Width="180px"></asp:Label></td>
				  </tr>
				 <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
                        E-mail: &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblEmail" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>
                  <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
                        Telephone: &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblPhone" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>
                  <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
                        Emergency Contact Person &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblEmergencyContact" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>
                   <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
                        Emergency Contact Phone &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblEmergencyPhone" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>
                  <tr class="datatable2">
                     <td CLASS="datatable2" align="Left">
                          <asp:Label ID="lblProgOfStudyTxt" runat="server" Text="Program of Study:" Width="200px"></asp:Label>
                        </td>
                     <td CLASS="datatable" align="Left">
                         &nbsp;<asp:Label ID="lblProgOfStudy" runat="server" Text="" Width="150px"></asp:Label>
                     </td>
                  </tr>
		         <tr CLASS="datatable2"> 
				    
					 
					<td CLASS="datatable2" align="Left" width="25%">
					
					Institution: &nbsp;&nbsp;</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp;<asp:Label ID="lblI" runat="server" Width="178px"></asp:Label></td>

            	  </tr>
				 <tr CLASS="datatable2">

 
					<td CLASS="datatable2" align="Left" style="height: 30px">Cohort: &nbsp;
									</td>
                   <td CLASS="datatable" align="Left" style="height: 30px">
                       &nbsp;<asp:Label ID="lblCohort" runat="server" Width="175px"></asp:Label></td>

					
				  </tr>
				 <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" align="Left">
					Group: &nbsp;
									</td>
                   <td CLASS="datatable" align="Left">
                       &nbsp<asp:Label ID="lblGroup" runat="server" Width="178px"></asp:Label></td>
<!-- 					<td CLASS="datatable">Pre-Populated</td>
					<td CLASS="datatable">Pre-Populated</td> -->

				  </tr>
                  <tr CLASS="datatable2">
			 
					<td CLASS="datatable2" style="width:100%;" colspan="3" align="Left">
					
									 <ucAddress:Address ID="ucAddress" runat="server" />
									</td>
                   

				  </tr>
 				 <tr class="formtableApply"> 
				 <td CLASS="datatable" colspan="3">
                     &nbsp;</td>
			    </tr>
          </table>
</td>
</tr>
</table>
</asp:Content> 