<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_AVPItems1" Codebehind="AVPItems.aspx.cs" %>
<%@ Register TagPrefix="asp" Namespace="WebControls" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivHtmlLnk');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > AVP Items</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit AVP items.
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td align="left">
                                <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                                <asp:KTPDropDownList ID="ddlProgramOfStudy" runat="server" AutoPostBack="True" ShowNotSelected="False"  OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged"></asp:KTPDropDownList>
                                <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                        </td>                                   

                        <td colspan="7" align="right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="txtTestName" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                            Width="75" Height="25" onMouseOver="roll(this)" onMouseOut="roll(this)" OnClick="searchButton_Click">
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvAVPItems" DataKeyNames="TestId" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="True"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnPageIndexChanging="gvAVPItems_PageIndexChanging" OnRowDataBound="gvAVPItems_RowDataBound"
        OnSorting="gvAVPItems_Sorting" OnRowDeleting="gvAVPItems_RowDeleting1" OnRowEditing="gvAVPItems_RowEditing1">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="TestId" HeaderText="Item ID" SortExpression="TestId" />
            <asp:BoundField DataField="TestName" HeaderText="AVP Item Name" SortExpression="TestName">
                <HeaderStyle Width="350px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Hyper Link Test">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#">Go</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ButtonType="Link"/>
            <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />
        </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="TestId|DESC" />
    <br />
    <br />
    <asp:Button ID="newAVPButton" runat="server" Text="New AVP Item" OnClick="newAVPButton_Click" />
</asp:Content>
