<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_ProgramView" EnableViewState="true" Codebehind="ProgramView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
		  <tr> 
            <td colspan="2" class="headfont"><b>Add/View/Edit > Program Details</b></td>
          </tr>
          <tr> 
            <td colspan="2" align="left"> </td>
          </tr>
		  <tr>
			<td>
			  <table align="left" border="0" class="formtable">
				  <tr class="datatablelabels"> 
					<td align="left" colspan="3">
                     <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Program successfully updated"></asp:Label>
					</td>
				  </tr>
                  <tr class="datatable2">
                        <td align="left" style="width: 243px">Program of Study:</td>
                        <td align="left" width="40%">
                            &nbsp;<asp:Label ID="lblProgramOfStudyName" runat="server"></asp:Label>
                        </td>
                        <td rowspan="3" valign="middle" align="left">
                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" Width="239px">Edit Program Name and Description</asp:LinkButton><br/>
                            <asp:LinkButton ID="lnkTests" runat="server" OnClick="lnkTests_Click">Add Tests</asp:LinkButton><br/>
                            <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click">Add Additional Program </asp:LinkButton>
                        </td>
                  </tr>
				  <tr class="datatable2">
					<td align="left" style="width: 243px">Program Name:</td>
					<td align="left" width="40%"><!--&nbsp;&nbsp;<input type="button" name="find" value="Find" onclick="submit_form('find');">-->
                        &nbsp;<asp:Label ID="lblProgramName" runat="server" Width="180px"></asp:Label></td>
				  <tr class="datatable2"> 
					<td align="left" style="height: 52px; width: 243px;">Program Description:</td>
					<td align="left" class="datatable" style="height: 52px">
                        &nbsp;<asp:Label ID="lblDescription" runat="server" Width="180px"></asp:Label></td>
				  </tr>			  
			  </table>
			</td>
		</tr>

      </table>
</asp:Content> 