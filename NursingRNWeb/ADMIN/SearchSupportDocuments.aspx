﻿<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="SearchSupportDocuments.aspx.cs" Inherits="NursingRNWeb.ADMIN.SearchSupportDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div19');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > View Support Documents List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit or delete a Support Document
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table>
                    <tr>
                        <td style="width: 100px; white-space: nowrap">
                            <asp:Label ID="lblKeyword" runat="server" Text="Keyword:"></asp:Label>&nbsp;<asp:TextBox
                                ID="txtKeyword" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                alt="Search Button" onMouseOver="roll(this)" onMouseOut="roll(this)" OnClick="btnSearch_Click">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" ViewStateMode="Disabled"></asp:Label>
    <asp:GridView ID="gvSupportDocs" DataKeyNames="Id" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="True"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnPageIndexChanging="gvSupportDocs_PageIndexChanging" OnRowDataBound="gvSupportDocs_RowDataBound"
        OnRowDeleting="gvSupportDocs_RowDeleting" OnSorting="gvSupportDocs_Sorting" OnDataBound="gvSupportDocs_DataBound">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:TemplateField SortExpression="FileName" HeaderText="FileName">
                <ItemTemplate>
                    <asp:HyperLink ID="hlFileName" runat="server" Target="_blank" NavigateUrl='<%# "OpenSupportDoc.aspx?Id=" + Eval("Id") +"&DownloadActionType=0"%>'
                        Text='<%#Eval("FileName")%>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Admin.FullName" HeaderText="Uploaded By">
                <ItemTemplate>
                    <asp:Label runat="server" ID="CreatedBy" Text='<%#DataBinder.Eval(Container.DataItem,"Admin.FullName")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDateTime" HeaderText="Uploaded Date" SortExpression="CreatedDateTime" />
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hLView" runat="server" NavigateUrl='<%# "ViewSupportDocument.aspx?Id=" + Eval("Id") %>'>View</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hLEdit" runat="server" NavigateUrl='<%# "UploadSupportDocument.aspx?Id=" + Eval("Id") %>'>Edit</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" Visible="false" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Id")%>'
                        OnClick="lnkDownload_Click" Text="Download">Download</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Id|DESC" />
</asp:Content>
