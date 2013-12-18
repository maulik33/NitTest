<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="CMS_Lippincott" CodeBehind="Lippincott.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivLipp');
        });
    </script>
    <input runat="server" type="hidden" id="hdnMode" />
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="L1" runat="server" Text="View > Lippincott"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Custom Test
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="7" align="right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                    <td>
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
    <asp:GridView ID="gvLippincott" DataKeyNames="LippincottID" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="True"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnSorting="gvLippincott_Sorting" OnRowDataBound="gvLippincott_RowDataBound" OnRowDeleting="gvLippincott_RowDeleting"
        OnPageIndexChanging="gvLippincott_PageIndexChanging">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="LippincottID" HeaderText="Lippincott ID" SortExpression="LippincottID" />
            <asp:BoundField DataField="LippincottTitle" HeaderText="Lippincott Title" SortExpression="LippincottTitle">
                <HeaderStyle Width="350px" />
            </asp:BoundField>
            <asp:TemplateField SortExpression="Remediation.TopicTitle" HeaderText="Remediation Title">
                <ItemTemplate>
                    <asp:Label runat="server" ID="TopicTitle" Text='<%#DataBinder.Eval(Container.DataItem,"Remediation.TopicTitle")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <!-- search & sort condition is added code file.-->
                    <asp:HyperLink ID="hlNewLippincott" runat="server" NavigateUrl='<%#"NewLippincott.aspx?IID="+ Eval("LippincottID") + "&CMS=1&Mode=4" %>'>Edit</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="LippincottID|DESC" />
    <asp:HiddenField runat="server" ID="hdnSearch" Value="" />
    <br />
    <br />
    <asp:Button ID="btnNewLippincott" runat="server" Text="New Lippincott" OnClick="btnNewLippincott_Click" />
    <asp:Button ID="btnReadLippincott" runat="server" Text="Read Lippincott Template" OnClick="btnReadLippincott_Click" />
</asp:Content>
