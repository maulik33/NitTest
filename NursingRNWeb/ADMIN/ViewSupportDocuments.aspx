<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ViewSupportDocuments.aspx.cs" Inherits="NursingRNWeb.ADMIN.ViewSupportDocuments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <td>
                <table class="datatable" align="left">
                    <tr>
                        <td align="left" width="15%">Keyword:</td>
                        <td ><asp:TextBox ID="TxtKeyword" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" width="15%">Uploaded By:</td>
                        <td ><asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" width="15%">Start Date </td>
                        <td >&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="txtSD" runat="server" ></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Temp/images/show_calendar.gif">
                            </asp:Image>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="15%">
                            End Date </td>
                            <td >&nbsp;&nbsp;&nbsp;&nbsp<asp:TextBox ID="txtED" runat="server" ></asp:TextBox>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/show_calendar.gif">
                            </asp:Image></td>
                       
                    </tr>
        <tr>
            <td colspan="3">
                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                    Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                    OnClick="btnSearch_Click"></asp:ImageButton>
            </td>
        </tr>
        </table>
        </td> </tr>
    </table>
    <asp:GridView ID="gvSupportDocs" DataKeyNames="Id" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="True"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnPageIndexChanging="gvSupportDocs_PageIndexChanging" OnRowDataBound="gvSupportDocs_RowDataBound"
        OnRowDeleting="gvSupportDocs_RowDeleting" OnSorting="gvSupportDocs_Sorting">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date" SortExpression="CreateDate"  />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HLView" runat="server" NavigateUrl='<%# "ViewSupportDoc.aspx?GUID=" + Eval("GUID") + "&Type=" + Eval("Type")%>'>View</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HLEdit" runat="server" NavigateUrl='<%# "UploadSupportDocument.aspx?Id=" + Eval("Id") %>'>Edit</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Id|DESC" />
</asp:Content>
