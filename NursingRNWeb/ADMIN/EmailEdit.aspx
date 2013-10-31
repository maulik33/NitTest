<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_EmailEdit" Title="Kaplan Nursing" Codebehind="EmailEdit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
        function Validate() {
            var TextBox1 = document.getElementById('<%=TextBox1.ClientID%>');
            var TextBox2 = document.getElementById('<%=TextBox2.ClientID%>');

            //$('Messenger1').innerHTML = '';

            if ($('TextBox1').val == '') {
                //$('Messenger1').innerHTML = "<span style='color:Red;'><li>Please enter Email title.</li></span>";
                alert('Please enter Email title.');
                return false;
            }

            if ($('TextBox2').val == '') {
                //$('Messenger1').innerHTML = "<span style='color:Red;'><li>Please enter Email content.</li></span>";
                alert('Please enter Email content.');
                return false;
            }

            return true;
        }
</script>

    <div align="left">
        <asp:Label ID="Messenger1" runat="server" />
        <table width="100%" align="left">
            <tr>
                <td align="left" style="height: 18px">
                </td>
                <td class="headfont" align="left">
                    <b><asp:Label ID="Label3" runat="server" Text="Edit Email"></asp:Label></b></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label1" runat="server" Text="Title :"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="TextBox1" runat="server" Width="699px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label2" runat="server" Text="Content :"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="702px" Height="185px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left">
                </td>
                <td align="left">
                    &nbsp;<asp:Button ID="Button2" runat="server" Text="Save" Width="62px" OnClientClick = "return Validate();" OnClick="Button2_Click" />
                    <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="Button1_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
