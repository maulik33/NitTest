<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="True"
    Inherits="CMS_TestCategories" CodeBehind="TestCategories.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register TagPrefix="asp" Namespace="WebControls" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivTstCat');
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="1" width="100%" bgcolor="#FFFFFF">
                    <tr class="datatable2">
                        <td style="width: 117px; height: 26px" valign="top">
                        </td>
                        <td style="width: 494px; height: 26px">
                            <KTP:Messenger ID="Messenger1" runat="server" />
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                                    </td>
                                    <td align="left">
                                       <asp:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged" NotSelectedText="Selection Required"></asp:KTPDropDownList>
                                       <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblTestCategory" runat="server" Text="Test Type"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddTestCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTestCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblTest" runat="server" Text="Test"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddTest" runat="server" OnSelectedIndexChanged="ddTest_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            <asp:GridView ID="gvCat" runat="server" AutoGenerateColumns="False" Width="466px"
                                DataKeyNames="CategoryID" OnRowDataBound="gvCat_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Category" DataField="TableName" />
                                    <asp:TemplateField HeaderText="Student side">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_S" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Admin side">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_A" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
