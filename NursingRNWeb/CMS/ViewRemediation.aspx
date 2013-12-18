<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_ViewRemediation" Title="Kaplan Nursing" Codebehind="ViewRemediation.aspx.cs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript" src="../js/main1.js"></script>

    <table width="100%">
        <tr>
            <td align="left">
              <asp:Label ID="errorMessage" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <div ID="remediation" runat="server" class="med_question">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Topic Review"></asp:Label><br />
                    <br />
                    <asp:PlaceHolder ID="Lippincott" runat="server"></asp:PlaceHolder>
                </div>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" /></td>
        </tr>
    </table>
</asp:Content>