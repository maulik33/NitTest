<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_Upload" Title="Upload" Codebehind="Upload.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
   <table >
  <tr class="datatable2"> 
					<td align="left" width="25%"><asp:Label ID="Label3" runat="server" Text="Test ID:" ></asp:Label></td>
					<td align="left" class="datatable">
                        &nbsp;<asp:Label ID="lblTestID" runat="server"></asp:Label></td>
				  </tr>
				  <tr class="datatable2"> 
					<td align="left" width="25%"><asp:Label ID="Label5" runat="server" Text="Test Name:" ></asp:Label></td>
					<td align="left" class="datatable">
                        &nbsp;<asp:Label ID="lblTestName" runat="server"></asp:Label>
                    </td>
				  </tr>
	</table> 
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Please select a file:"></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label><br />
    <br />
    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />&nbsp;
</asp:Content>

