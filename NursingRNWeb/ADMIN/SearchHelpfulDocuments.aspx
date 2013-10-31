<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="SearchHelpfulDocuments.aspx.cs" Inherits="NursingRNWeb.ADMIN.SearchHelpfulDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var IsLink = document.getElementById('<%=hdnIsLink.ClientID%>').value;

            if (IsLink == "1") {
                ExpandContextMenu(2, 'ctl00_Div22');
            }
            else {
                ExpandContextMenu(2, 'ctl00_Div19');
            }
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblBreadCrumb" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
                <%--Use this page to view or edit or delete a Helpful Document--%>
            </td>
        </tr>
        <tr>
            <td valign="bottom">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblKeyword" runat="server" Text="Keyword:"></asp:Label>
                        </td>
                        <td style="width: 350px; white-space: nowrap">
                            <asp:TextBox ID="txtKeyword" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
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
    <asp:GridView ID="gvHelpfulDocs" DataKeyNames="Id" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="True"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnPageIndexChanging="gvHelpfulDocs_PageIndexChanging" OnRowDataBound="gvHelpfulDocs_RowDataBound"
        OnRowDeleting="gvHelpfulDocs_RowDeleting" OnSorting="gvHelpfulDocs_Sorting" OnDataBound="gvHelpfulDocs_DataBound">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:TemplateField SortExpression="FileName" HeaderText="File Name">
                <ItemTemplate>
                    <asp:HyperLink ID="hlFileName" CssClass="link" runat="server" Target="_blank" NavigateUrl='<%# "OpenHelpfulDoc.aspx?Id=" + Eval("Id") +"&DownloadActionType=0&IsLink=0"%>'
                        Text='<%#Eval("FileName")%>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FileName" HeaderText="Link">
                <ItemTemplate>
                    <asp:HyperLink ID="hlLink" runat="server" NavigateUrl='<%#Eval("FileName")%>' Text='<%#Eval("FileName")%>'
                        Target="_blank"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Admin.FullName">
                <ItemTemplate>
                    <asp:Label runat="server" ID="CreatedBy" Text='<%#DataBinder.Eval(Container.DataItem,"Admin.FullName")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedDateTime" SortExpression="CreatedDateTime" />
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hLView" runat="server" NavigateUrl='<%# "ViewHelpfulDocument.aspx?Id=" + Eval("Id") %>'>View</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HyperLink ID="hLEdit" runat="server" NavigateUrl='<%# "UploadHelpfulDocument.aspx?Id=" + Eval("Id") %>'>Edit</asp:HyperLink>
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
    <asp:HiddenField ID="hdnIsLink" runat="server" />
</asp:Content>
