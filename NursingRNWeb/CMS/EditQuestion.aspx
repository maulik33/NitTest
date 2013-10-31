<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="CMS_EditQuestion" CodeBehind="EditQuestion.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="~/CMS/Controls/AlternateEditQuestion.ascx" TagName="AltEditQuestion"
    TagPrefix="ucAltEditQuestion" %>
<%@ Register Src="Controls/SubCategories.ascx" TagName="SubCategories" TagPrefix="ucSubCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3);
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="part2">
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="formtable">
                        <td colspan="2" class="headfont">
                            <b>Content Management - Search, View, Edit</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">To locate a test or question, drill down through the dropdown menus and click Search
                            Questions. To locate a question quickly for editing, enter the QID and then click
                            Search Questions.
                            <asp:HiddenField ID="hdnQuestionId" runat="server" />
                            <asp:HiddenField ID="hdnURL" runat="server" />
                            <asp:HiddenField ID="hdnQuestionType" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <KTP:Messenger ID="ktpMessage" runat="server"></KTP:Messenger>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="15%">
                            <asp:Label ID="lblQID" runat="server" Text="QID:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQID" runat="server" Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblQuestionID" runat="server" Text="QuestionID:"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQuestionID" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblItemTitle" runat="server" Text="Item Title"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtItemTitle" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblNorming" runat="server" Text="Norming"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNorming" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Button ID="btnUploadQues" runat="server" Text="Upload Questions" OnClick="btnUploadQues_Click" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit Question" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Inactive Question" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnReturn" runat="server" Text="Return to Search" OnClick="btnReturn_Click" />
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View Question" />
                            &nbsp;<asp:Button ID="btnRemediation" runat="server" OnClick="btnRemediation_Click"
                                Text="View Remediation" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <font color="red">
                                <asp:Label ID="lblError" runat="server" Text="" EnableViewState="False"></asp:Label>
                            </font>
                        </td>
                    </tr>
                </table>
                <br />
                <table id="Table" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="datatablelabels">
                        <td colspan="2" align="left">
                            <asp:Label ID="Label1" runat="server" Text="Category Information" Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="Table3" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr runat="server" id="trCopy" visible="False" class="copy">
                        <td colspan="2">
                            <asp:Label ID="lblCopyDetail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="20%" style="height: 25px;">
                            <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged"
                                NotSelectedText="Selection Required">
                            </KTP:KTPDropDownList>
                            <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False" CssClass="programtype"></asp:Label>
                        </td>
                    </tr>
                    <ucSubCategories:SubCategories ID="ucSubCategories" runat="server" />
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblActive" runat="server" Text="Question Status" Width="182px"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdoActive" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                            (please unassign this question from all tests to avoid numbering discrepancies)
                        </td>
                    </tr>
                </table>
                <br />
                <table id="Table1" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="datatablelabels">
                        <td colspan="2" align="left">this is a test
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="20%">
                            <asp:Label ID="lblQuestionType" runat="server" Text="Question Type" Width="182px"></asp:Label>
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddQuestionType" runat="server" OnSelectedIndexChanged="ddQuestionType_SelectedIndexChanged"
                                AutoPostBack="True">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblFileType" runat="server" Text="Type of File" Width="182px"></asp:Label>
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddTypeOfFile" runat="server" ShowNotSelected="false">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" colspan="2">
                            <table>
                                <tr>
                                    <td width="50%" valign="top">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStem" runat="server" Text="Stem" Width="400px" BackColor="Silver" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div>
                                                    </div>
                                                    <asp:TextBox ID="txtStem" runat="server" Rows="5" TextMode="MultiLine" Width="400px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAnswerChoices" runat="server" BackColor="Silver" Text="Answer Choices"
                                                        Width="400px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="D_Answers" runat="server" title="Answers">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" valign="top">
                                        <ucAltEditQuestion:AltEditQuestion ID="ucAltEditQuestion" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblExplanation" runat="server" Text="Explanation" Width="100%" BackColor="Silver">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtExplanation" runat="server" Rows="15" TextMode="MultiLine" Width="100%">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblListeningFileURL" runat="server" Text="Listening File Url" Width="100%"
                                            BackColor="Silver" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtListeningFileURL" runat="server" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblExhibit" runat="server" BackColor="Silver" Text="Exhibit" Width="100%"
                                            Visible="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="D_Exhibit" runat="server" title="Answers">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblRemediation" runat="server" BackColor="Silver" Text="Remediation"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblTopicTitle" runat="server" Text="Topic Title" Width="100px"></asp:Label>&nbsp<KTP:KTPDropDownList
                                            ID="ddTopicTitle" runat="server">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="D_Remediation" runat="server" title="Remediation">
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblStimulus" runat="server" Text="Stimulus" Width="100%" BackColor="Silver">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtStimulus" runat="server" Rows="5" TextMode="MultiLine" Width="100%">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="Label2" runat="server" BackColor="Silver" Text="Question Taxonomy Table"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table id="Table2" border="0" cellpadding="0" cellspacing="1" width="100%" bgcolor="#FFFFFF">
                                            <tr class="datatable2">
                                                <td>
                                                    <asp:Label ID="lblProductLine" runat="server" Text="Product Line" Width="94px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductLine" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="lblPointb" runat="server" Text="Point Biserials" Width="94px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtPointB" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="Label3" runat="server" Text="Statistics" Width="105px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtStatistics" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="Label4" runat="server" Text="Creator" Width="107px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtCreator" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="lblDCreated" runat="server" Text="Date Created" Width="106px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtDCreated" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="Label5" runat="server" Text="Editor" Width="101px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtEditor" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px; height: 26px;">
                                                    <asp:Label ID="lblDEdited" runat="server" Text="Date Edited" Width="106px"></asp:Label>
                                                </td>
                                                <td style="width: 494px; height: 26px;">
                                                    <asp:TextBox ID="txtDEdited" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="Label6" runat="server" Text="2nd Editor" Width="106px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txt2Editor" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px; height: 26px;">
                                                    <asp:Label ID="lbl2DEdit" runat="server" Text="Date 2nd Edited"></asp:Label>
                                                </td>
                                                <td style="width: 494px; height: 26px;">
                                                    <asp:TextBox ID="txtD2Edit" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px">
                                                    <asp:Label ID="lblSBD" runat="server" Text="Source/SBD" Width="99px"></asp:Label>
                                                </td>
                                                <td style="width: 494px">
                                                    <asp:TextBox ID="txtSBD" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px; height: 26px">
                                                    <asp:Label ID="lblFeedback" runat="server" Text="FeedBack" Width="107px"></asp:Label>
                                                </td>
                                                <td style="width: 494px; height: 26px">
                                                    <asp:TextBox ID="txtFeedback" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px; height: 26px">
                                                    <asp:Label ID="lblWho" runat="server" Text="Who Owns" Width="104px"></asp:Label>
                                                </td>
                                                <td style="width: 494px; height: 26px">
                                                    <asp:TextBox ID="txtWho" runat="server" Width="241px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="datatable2">
                                                <td style="width: 117px; height: 26px" valign="top">
                                                    <asp:Label ID="lblWhere" runat="server" Text="Where Used" Width="99px"></asp:Label>
                                                </td>
                                                <td style="width: 494px; height: 26px">
                                                    <br />
                                                    <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign Question To Test" /><br />
                                                    <br />
                                                    <asp:GridView ID="gvWhere" runat="server" AutoGenerateColumns="False" Width="466px"
                                                        DataKeyNames="TestID" OnRowDataBound="gvWhere_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Product Name">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="TestNumber" runat="server" Value='<%# Bind("TestNumber") %>' />
                                                                    <asp:Label runat="server" ID="ProductName" Text='<%#DataBinder.Eval(Container.DataItem,"Product.ProductName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                                                            <asp:TemplateField HeaderText="QNumber">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOrder" runat="server" Width="50px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelection" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <a href="~/CMS/Controls/AlternateEditQuestion.ascx">~/CMS/Controls/AlternateEditQuestion.ascx</a>
            </td>
        </tr>
    </table>
</asp:Content>
