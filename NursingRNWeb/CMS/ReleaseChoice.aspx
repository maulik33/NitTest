<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_ReleaseChoice" Title="Kaplan Nursing" Codebehind="ReleaseChoice.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(4);
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="part2">
                <!-- code body -->
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="formtable">
                        <td colspan="2" class="headfont">
                            <b>Release Content Management</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            Choose the content category you want to Release to Production
                        </td>
                    </tr>
                    <tr class="datatablelabels">
                        <td colspan="2" align="left">
                            Content Categories
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 23px">
                            Content Questions & Remediations
                        </td>
                        <td align="left" style="height: 23px">
                            <asp:CheckBox ID="chkContent" runat="server" />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 23px">
                            Lippincot
                        </td>
                        <td align="left" style="height: 23px">
                            <asp:CheckBox ID="chkLippincot" runat="server" />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 23px">
                            Custom Tests/AVP Items
                        </td>
                        <td align="left" style="height: 23px">
                            <asp:CheckBox ID="chkTests" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </td>
            <td style="width: 300px">
            </td>
        </tr>
    </table>
</asp:Content>
