<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="UploadHelpfulDocument.aspx.cs" Inherits="NursingRNWeb.ADMIN.UploadHelpfulDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(1, 'ctl00_Div20');
        });
    </script>
    <table>
        <tr>
            <td colspan="2" class="headfont">
                <b>Add/Edit Documents and Links</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit documents and links"
                    Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 98px; vertical-align: middle">
                            <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                        </td>
                        <td align="left" class="datatable" style="width: 650px;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="450px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="Please enter document title."
                                Display="Dynamic" ControlToValidate="txtTitle" ValidationGroup="DocumentUpload"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="vertical-align: top">
                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="62px"
                                Style="margin-left: 0px" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2" id="uploadedFile" runat="server" visible="false">
                        <td align="left">
                            <asp:Label ID="lblLabel" runat="server" Text="Uploaded File"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:LinkButton ID="lbtnOpenFile" runat="server" OnClick="lbtnOpenFile_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr class="datatable2" runat="server" id="fileUploadControlRow">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="File"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:FileUpload ID="fuHelpfulDocuments" runat="server" Width="450px" Height="24px"
                                size="59" />
                                <br />
                            <asp:Label runat="server" ID="FileUploadHelpText" Text="Supported File Types: Text (txt), Word (doc, docx), Excel (xls, xlsx), PowerPoint (ppt, pptx) and PDFs. Make sure the file size does not exceed 10 MB."
                                Font-Size="X-Small" ForeColor="GrayText"></asp:Label>
                        </td>
                    </tr> 
                     <tr class="datatable2" runat="server" id="linkUploadControlRow">
                          <td>
                            <asp:Label ID="lblLink" runat="server" Text="Link"></asp:Label>
                        </td>
                        <td align="left" class="datatable"> <asp:TextBox ID="txtLink" runat="server" Width="450px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td>
                        </td>
                        <td align="center" class="datatable">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Document/Link" OnClick="btnUpload_Click"
                                ValidationGroup="DocumentUpload" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
