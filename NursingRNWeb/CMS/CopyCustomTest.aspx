<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_CopyCustomTest" Codebehind="CopyCustomTest.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
		  <tr> 
            <td colspan="2" class="headfont">
                <B>Copy Custom Test</B></td>
          </tr>
          <tr> 
            <td colspan="2" align="left">
                Use this page to copy custom test"
            </td>
          </tr>
		  <tr>
			<td>
			  <table align="left" border="0" class="formtable">
				  <tr class="datatablelabels"> 
					<td align="left" colspan="2">
                     Enter details in fields below&nbsp;<br />
                     <asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red" ></asp:Label>
					</td>
				  </tr>
                  <tr class="datatable2">
                    <td align="left" width="25%">Program of Study:</td>
                    <td align="left" class="datatable">
                        <asp:Label ID="lblProgramOfStudyName" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnProgramOfStudyId" runat="server" />
                     </td>
                  </tr>
				  <tr class="datatable2"> 
					<td align="left" width="25%">New Test Name:</td>
					<td align="left" class="datatable">
					<asp:TextBox ID="TextBox1" runat="server" Width="272px"></asp:TextBox>
					</td>
				  </tr>
				   <tr class="datatable2"> 
					<td align="left" width="25%"><asp:Label ID="Label1" runat="server" Text="Test Type:" ></asp:Label></td>
					<td align="left" class="datatable">
                      <table>
                        <tr class="datatable2">
                            <td align="left" width="53%">
                        <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                         </td>
                            <td align="right" width="25%">
                            <asp:Label runat="server" Text="Seconds/Question"></asp:Label>&nbsp;&nbsp; 
                            </td>
                            <td>
                            <asp:DropDownList ID="ddSecondsPerQuestion" runat="server" Width="100%">
                           <asp:ListItem value="72" Selected="true">1x</asp:ListItem>
                           <asp:ListItem value="108">1.5x</asp:ListItem>
                           <asp:ListItem value="144">2x</asp:ListItem>
                           <asp:ListItem value="216">3x</asp:ListItem>
                            <asp:ListItem value="0">Untimed</asp:ListItem>
                           </asp:DropDownList>         
                                        </td>
                        </tr>
                        </table>
                    </td>
				  </tr>
				  <tr class="datatable2"> 
					<td align="left" width="25%"></td>
					<td align="left" class="datatable">
                        &nbsp;
                        <asp:CheckBox ID="chbFRDefault" runat="server" Enabled="false" />
                        Add to Default Group</td>
				  </tr>				  
				  <tr> 
					<td colspan="2" align="left">
						<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
					</td>
				  </tr>				  
				  <tr class="formtableApply"> 
					<td align="center" colspan="2">
					 <asp:Button ID="Button1" runat="server" Text=" Copy " onclick="Button1_Click" />
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="Cancel" onclick="Button2_Click" />
					</td>
				  </tr>

			  </table>
			</td>
		</tr>

      </table>
</asp:Content>
