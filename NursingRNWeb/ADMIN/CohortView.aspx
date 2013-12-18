<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_CohortView" EnableViewState="false" Codebehind="CohortView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
		  <tr> 
            <td colspan="2" class="headfont"><b>Add/View/Edit > Cohort Details</b></td>
          </tr>
          <tr> 
            <td colspan="2" align="left"> </td>
          </tr>
		  <tr>
			<td>
			  <table align="left" border="0" class="formtable">
				  <tr class="datatablelabels"> 
					<td align="left" colspan="3">
                     <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Cohort successfully updated"></asp:Label>
					</td>
				  </tr>
                    <tr class="datatable2" id="trProgramofStudy" runat="server" visible="false"> 
					<td align="left">Program of Study:</td>
					<td align="left" class="datatable">
                     &nbsp;<asp:Label ID="lblProgramofStudy" runat="server" Text="Text here" Width="180px"></asp:Label></td>
                     <td style=" border:0px"></td>
				  </tr>	
				  <tr class="datatable2"> 
					<td align="left" width="25%">Cohort Name:</td>
					<td align="left"><!--&nbsp;&nbsp;<input type="button" name="find" value="Find" onclick="submit_form('find');">-->
                        &nbsp;<asp:Label ID="lblCohortName" runat="server" Text="Text here" Width="180px"></asp:Label>
					</td>
				    <td rowspan="5" align="left">
                        <asp:LinkButton ID="lbEdit" runat="server" Width="107px" OnClick="lbEdit_Click">Edit</asp:LinkButton><br/>
                        <asp:LinkButton ID="lbAssign" runat="server" OnClick="lbAssign_Click">Assign Program</asp:LinkButton><br/>
					  <asp:LinkButton ID="lbNew" runat="server" OnClick="lbNew_Click">Add Additional Cohort</asp:LinkButton>
                     </td>
				 </tr>
				  <tr class="datatable2"> 
					<td align="left">Description:</td>
					<td align="left" class="datatable">
                     &nbsp;<asp:Label ID="lblDescription" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>	
				  <tr class="datatable2"> 
					<td align="left">Institution:</td>
					<td align="left" class="datatable">
                     &nbsp;<asp:Label ID="lblLocation" runat="server" Text="Text here" Width="180px"></asp:Label></td>
				  </tr>	

				  <tr class="datatable2"> 
					<td align="left">Cohort Start Date:</td>
					<td align="left" class="datatable">
                        &nbsp;<asp:Label ID="lblSD" runat="server" Text="Text here" Width="180px"></asp:Label>&nbsp;
					</td>
				  </tr>	
				  <tr class="datatable2"> 
					<td align="left">Cohort End Date:</td>
					<td align="left" class="datatable">
                        &nbsp;<asp:Label ID="lblED" runat="server" Text="Text here" Width="180px"></asp:Label>&nbsp;
					</td>
				  </tr>					  
					
					
			  </table>
			</td>
		</tr>

      </table>

</asp:Content> 