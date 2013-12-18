<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ViewSupportDocument.aspx.cs" EnableViewState="false" Inherits="NursingRNWeb.ADMIN.ViewSupportDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
    </div>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>View Help documents</b>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="formtable">
                    <%--<tr class="datatablelabels">
                        <td align="left" colspan="3">
                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="Document uploaded successfully "></asp:Label>
                        </td>
                    </tr>--%>
                    <tr class="datatable2">
                        <td style="width: 98px; vertical-align: top">
                            <asp:Label ID="lbTitle" runat="server" Text="Title"></asp:Label>
                        </td>
                        <td align="left" class="datatable" style="width: 600px;">
                            <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="vertical-align: top">
                            <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblDescription" runat="server" Text="Title"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="vertical-align: top">
                            <asp:Label ID="lblUploadedDate" runat="server" Text="Uploaded On"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblUploadedOn" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="vertical-align: top">
                            <asp:Label ID="lblUploadedUser" runat="server" Text="Uploaded By"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblUploadedBy" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2" id="uploadedFile" runat="server">
                        <td align="left">
                            <asp:Label ID="lblUploadedFile" runat="server" Text="File"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:LinkButton ID="lbtnOpenFile" runat="server" OnClick="lbtnOpenFile_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
