<%@ Control Language="C#" AutoEventWireup="true" Inherits="AdminSendMailMenu" Codebehind="AdminSendMailMenu.ascx.cs" %>
<fieldset style="background-color:#D16587"><%--<legend>Send Email</legend>--%>
<table id="tblSendEmail" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="10%">&nbsp;</td>
            <td width="80%" class="part2">&nbsp;
                <table  width="90%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" class="parta7"> <b><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/EmailReceiver1.aspx" Text="Send an Email" Font-Bold="true" Font-Underline="True" /></b></td>
                    </tr>                    
                </table>
            </td>
            <td width="10%">&nbsp;</td>
        </tr>
  </table>
</fieldset>