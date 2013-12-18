<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_ProgramEdit" EnableViewState="true" Codebehind="ProgramEdit.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        ExpandContextMenu(1, 'ctl00_Div4');
    });
</script>
 <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
		  <tr> 
            <td colspan="2" class="headfont">
                <B><asp:Label ID="lblTitle" runat="server" Text="Edit > Program" Width="448px"></asp:Label></B></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit a Program"
                    Width="450px"></asp:Label></td>
          </tr>
		  <tr>
			<td>
			  <table align="left" border="0" class="formtable">
				  <tr class="datatablelabels"> 
					<td align="left" colspan="2">
                     Enter details in fields below<br/>
                         <asp:Label ID="lblErrorMsg" runat="server" EnableViewState="False" ForeColor="Red" ></asp:Label>
					</td>
				  </tr>
                  <tr class="datatable2"> 
					<td align="left" width="25%">Program of Study:</td>
                    <td style="text-align:left;padding-left:8px;" class="datatable">
                        <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" NotSelectedText="Selection Required"></KTP:KTPDropDownList>
                        <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramOfStudy" 
                        ErrorMessage="*Required Field" ValidationGroup="Form1" Display="Static" InitialValue="-1">
                        </asp:RequiredFieldValidator>
                        <asp:Label ID="lblProgramOfStudyName" runat="server" Visible="false"></asp:Label>
                        <asp:HiddenField ID="hfProgramOfStudyId" runat="server" />
                    </td>
                  </tr>
				  <tr class="datatable2"> 
					<td style="width:25%;text-align:left;">Program Name:</td>
					<td align="left" class="datatable"><!--&nbsp;&nbsp;<input type="button" name="find" value="Find" onclick="submit_form('find');">-->
                        &nbsp;<asp:TextBox ID="txtProgramName" runat="server" Width="302px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramName"
                            ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
					</td>
				  </tr>
				  <tr class="datatable2"> 
					<td align="left">Program Description:</td>
					<td align="left" class="datatable">
                        &nbsp;<asp:TextBox ID="txtProgramD" runat="server" Width="290px"></asp:TextBox></td>
				  </tr>	
                   <tr runat="server" id="trCopy" Visible="False"> 
					<td colspan="2" align="left">
						<asp:Label ID="lblCopyDetail" runat="server"></asp:Label>
					</td>
				  </tr>	
                  		  
				  <tr class="formtableApply"> 
					<td align="center" colspan="2">
					<asp:imagebutton id="addbtn" runat="server" ImageUrl="~/Temp/images/btn_save.gif" width="75" height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)" OnClick="addbtn_Click" ValidationGroup="Form1" Visible="false" ></asp:imagebutton>
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btDelete" runat="server" OnClick="btDelete_Click" AlternateText="Delete Program" ImageUrl="~/Temp/images/btn_del.gif" onMouseOver="roll(this)" onMouseOut="roll(this)" Visible="false" />
                        
					    <asp:Button ID="btnCopy" runat="server" Text="Copy" Visible ="False" OnClick="btnCopy_Click" ValidationGroup="Form1"/>
                        
					</td>
				  </tr>
			  </table>
			</td>
		</tr>

      </table>
</asp:Content> 
