<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="UploadQuestions.aspx.cs" Inherits="NursingRNWeb.CMS.UploadQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JS/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivUploadQues');
        });
    </script>
    <div>
        <div>
            <table align="left" border="0" class="formtable">
                <tr class="formtable">
                    <td colspan="2" class="headfont">
                        <b>Content Management - Upload Questions</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">For instructions on using this page, please
                        <asp:LinkButton ID="lbInstructionDocument" runat="server" OnClick="lbInstructionDocument_Click">click here.</asp:LinkButton>
                        <br />
                    </td>
                </tr>
                <tr class="tdleft">
                    <td>Program of Study</td>
                    <td>
                <asp:DropDownList ID="ddlProgramofStudy" runat="server">
                    <asp:ListItem Value="-1">Selection Required</asp:ListItem>
                    <asp:ListItem Value="1">RN</asp:ListItem>
                    <asp:ListItem Value="2">PN</asp:ListItem>
                </asp:DropDownList>
                        &nbsp;&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldProgramOfstudy" runat="server" ControlToValidate="ddlProgramofStudy"
                ErrorMessage="*Required Field" ValidationGroup="validdownload" Display="Dynamic"
                InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdleft">
                    <td>To download question template</td>
                    <td>
                        <asp:DropDownList ID="ddlTemplate" runat="server" Height="23px" Width="186px">
                            <asp:ListItem Value="-1">Not Selected</asp:ListItem>
                            <asp:ListItem Value="1">Multiple Choice/Single Select</asp:ListItem>
                            <asp:ListItem Value="2">Multiple Choice/Multi Select</asp:ListItem>
                            <asp:ListItem Value="3">Numerical Fill-In</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnDownloadTemplate" runat="server" Text="Download Template"
                            OnClick="btnDownloadTemplate_Click" ValidationGroup="validdownload"
                            Width="130px" />

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlTemplate" runat="server" ControlToValidate="ddlTemplate"
                            ErrorMessage="*Required Field" ValidationGroup="validdownload" Display="Dynamic"
                            InitialValue="-1"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" EnableViewState="false" />
                    </td>
                </tr>
                <tr class="datatable2">
                    <td style="width: 226px;">Choose completed question template:
                    </td>
                    <td style="height: 60px; width: 350px;" align="left">
                        <asp:FileUpload ID="fuQuestions" runat="server" class="multi" accept="docx|zip|docm"
                            maxlength="1" Height="24px" Width="450px" size="59" />
                    </td>
                </tr>
                <tr class="datatable2">
                    <td style="width: 226px"></td>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Completed Template" OnClick="btnUpload_Click"
                            EnableViewState="False" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div style="margin: 10px; text-align: left" id="divError" runat="server" visible="false">
            <div class="med_center_banner2_l" style="margin-top: 10px; clear: both;">
                <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
                Error\InValid templates
            </div>
            <br />
            <asp:GridView ID="gvInValidQuestions" runat="server" CellSpacing="5" AutoGenerateColumns="false"
                Style="clear: both;">
                <RowStyle CssClass="datatable2a" BorderColor="Red" ForeColor="Red" />
                <HeaderStyle CssClass="datatablelabels" />
                <AlternatingRowStyle CssClass="datatable1a" />
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="Uploaded File" />
                    <asp:BoundField DataField="QuestionId" HeaderText="Question Id" />
                    <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" />
                </Columns>
            </asp:GridView>
        </div>
        <div style="margin: 10px; text-align: left" id="divSuccess" runat="server" visible="false">
            <div class="med_center_banner2_l" style="margin-top: 10px; clear: both">
                <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
                Valid templates
            </div>
            <p style="text-align: left">
                <asp:Label ID="lblSuccessMsg" runat="server" EnableViewState="False" ForeColor="#00CC00"></asp:Label>
            </p>

            <asp:GridView ID="gvValidQuestions" runat="server" Style="clear: both;" CellSpacing="5"
                AutoGenerateColumns="false">
                <RowStyle CssClass="datatable2a" />
                <HeaderStyle CssClass="datatablelabels" />
                <AlternatingRowStyle CssClass="datatable1a" />
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="Uploaded File" />
                    <asp:TemplateField HeaderText="Question Id">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="questionId" Text='<%#DataBinder.Eval(Container.DataItem,"Question.QuestionId")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="divSave" runat="server">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false" />&nbsp;&nbsp;
            <asp:Button ID="btnCMS" runat="server" Text="View content in CMS" OnClick="btnCMS_Click"
                Visible="false" />
        </div>
    </div>
    <asp:HiddenField ID="hfDocType" runat="server" />
    <asp:HiddenField ID="hfUnZippedLocation" runat="server" />
</asp:Content>
