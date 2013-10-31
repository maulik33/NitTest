<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_Norm" Title="Kaplan Nursing" CodeBehind="Norm.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivNorm');
        });
    </script>
    <table cellpadding="0" cellspacing="0" width="100%" class="datatable1">
        <tr>
            <td align="left" colspan="2" style="width: 494px;">
                <asp:Label ID="errorMessage" runat="server" EnableViewState="false" Visible="false"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr class="datatable2"> 
            <td class="normtext">
                Program of study
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" NotSelectedText="Selection Required"
                    OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td class="normtext">
                Test Type
            </td>
            <td align="left" style="height: 27px">
                <KTP:KTPDropDownList ID="ddTestType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTestType_SelectedIndexChanged"
                    ShowNotSelected="true">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td class="normtext">
                <asp:Label ID="Label1" runat="server" Text="Test"></asp:Label>
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddTest" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTest_SelectedIndexChanged"
                    ShowNotSelected="true">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr class="datatable2">
            <td class="normtext">
                <asp:Label ID="Label2" runat="server" Text="Category"></asp:Label>
            </td>
            <td align="left">
                <KTP:KTPDropDownList ID="ddCategory" runat="server" AutoPostBack="true" ShowNotSelected="true"
                    OnSelectedIndexChanged="ddCategory_SelectedIndexChanged">
                </KTP:KTPDropDownList>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style=" border-bottom:1px solid; padding:4px 4px 4px 8px;">
                <asp:Label ID="Label3" runat="server" Text="Norm value"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="padding-top:4px;">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
