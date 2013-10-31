<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_EditR" Codebehind="EditR.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <table>
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnEdit" runat="server" Text="Save" OnClick="btnEdit_Click" Width="147px" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                        Width="147px" />
                    <asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return to Search" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="QuestionID List" Width="95px"></asp:Label></td>
                <td align="left">
                    <asp:GridView ID="gvRemediation" runat="server" AutoGenerateColumns="False" OnRowCommand="gvRemediation_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="QuestionID" HeaderText="Question ID" />
                            <asp:ButtonField CommandName="ViewRemediation" Text="View Remediation" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="RemediationID" Width="95px"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtRID" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Topic Title" Width="101px"></asp:Label></td>
                <td align="left">
                    <asp:TextBox ID="txtTitle" runat="server" Width="592px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Label ID="lblM" runat="server" Visible="False" Width="423px"></asp:Label><asp:Label
                        ID="lblRemediation" runat="server" BackColor="Silver" Text="Remediation" Width="100%"
                        Height="14px"></asp:Label>
                    <asp:PlaceHolder ID="p1" runat="server"></asp:PlaceHolder>
                    <br />
                    <asp:TextBox ID="txtRem" runat="server" Rows="5" TextMode="MultiLine" Height="253px"
                        Width="774px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
