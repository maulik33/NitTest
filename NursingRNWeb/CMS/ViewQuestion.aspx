<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMS_ViewQuestion" CodeBehind="ViewQuestion.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="~/CMS/Controls/AlternateEditQuestion.ascx" TagName="AltEditQuestion"
    TagPrefix="ucAltEditQuestion" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kaplan Nursing</title>
    <style type="text/css" media="all">
        @import "../css/basic.css";
    </style>
    <link rel="alternate stylesheet" type="text/css" media="all" title="medium" href="../css/basic.css" />
    <script type="text/javascript" src="../js/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="header" border="0" cellpadding="0" cellspacing="0" width="940px" height="100%">
        <tr>
            <td width="172">
                <asp:HiddenField ID="hdnVType" runat="server" />
                <asp:HiddenField ID="hdnQuestionId" runat="server" />
                <asp:HiddenField ID="hdnURL" runat="server" />
                <asp:HiddenField ID="hdnQuestionNumber" runat="server" />
                <asp:HiddenField ID="hdnTestId" runat="server" />
            </td>
            <td align="right" valign="bottom">
                <table width="172" border="0" cellpadding="0" cellspacing="0" class="tesm">
                    <tr>
                        <td colspan="2" align="right" height="20" nowrap>
                            <img src="../images/backNav.gif" width="75" height="25" border="0" alt="" onmouseover="roll(this)"
                                onmouseout="roll(this)" onclick="history.back();" /><img src="../images/spacer.gif"
                                    width="10">
                            <img src="../images/printbtn_over.gif" width="75" height="25" border="0" alt="" onmouseover="roll(this)"
                                onmouseout="roll(this)" />
                            <img src="../images/btn_excel.gif" width="110" height="25" border="0" alt="" onmouseover="roll(this)"
                                onmouseout="roll(this)" />
                            <img src="../images/help_f.gif" width="75" height="25" border="0" alt="" onmouseover="roll(this)"
                                onmouseout="roll(this)" onclick="history.back();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="28">
                            &nbsp;
                        </td>
                        <td width="40%" align="center" background="../images/key_blu.gif">
                            Admin View
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="content" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="part2" style="text-align: left;">
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClick="btnReturn_Click" Text="Return to Search" />
                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="Edit Question" />
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="Next Question" />
                <asp:Button ID="btnPreviouse" runat="server" OnClick="btnPreviouse_Click" Text="Previous Question" />&nbsp;
                <br />
                <br />
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="datatable2">
                        <td width="25%">
                            <asp:Label ID="lblProgramOfStudyName" runat="server" Text="Program of Study:" Width="140px" Font-Bold="True"
                                Font-Size="Larger"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lProgramOfStudyName" runat="server" Width="100px" Font-Bold="True" Font-Size="Larger">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td width="25%">
                            <asp:Label ID="lblQuestionID" runat="server" Text="QuestionID:" Width="30px" Font-Bold="True"
                                Font-Size="Larger"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lQuestionID" runat="server" Width="100px" Font-Bold="True" Font-Size="Larger">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td width="25%">
                            <asp:Label ID="lblNorming" runat="server" Text="Norming:" Width="30px" Font-Bold="True"
                                Font-Size="Larger"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lNorming" runat="server" Width="100px" Font-Bold="True" Font-Size="Larger">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="D_Title" runat="server">
                </div>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblStem" runat="server" BackColor="Silver" Text="Stem" Width="100%" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="D_Stem" runat="server" title="Stem" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAnswerChoices" runat="server" BackColor="Silver" Text="Answer Choices"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="D_Answers" runat="server" title="Answers" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <ucAltEditQuestion:AltEditQuestion ID="ucAltEditQuestion" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:Label ID="lblExhibit" runat="server" BackColor="Silver" Text="Exhibit" Width="100%">
                </asp:Label><br />
                <div id="D_Exhibit" runat="server" title="Exhibit">
                    Exhibit Tab1:
                    <asp:TextBox ID="TBT_1" runat="server">
                    </asp:TextBox>
                    &nbsp;<asp:TextBox ID="TBE_1" runat="server" TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox><br />
                    Exhibit Tab2:
                    <asp:TextBox ID="TBT_2" runat="server">
                    </asp:TextBox>
                    <br />
                    <asp:TextBox ID="TBE_2" runat="server" TextMode="MultiLine" Width="100%" ReadOnly="True">
                    </asp:TextBox><br />
                    Exhibit Tab3:
                    <asp:TextBox ID="TBT_3" runat="server" ReadOnly="True">
                    </asp:TextBox>
                    &nbsp;<asp:TextBox ID="TBE_3" runat="server" TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox><br />
                    <br />
                </div>
                <asp:Label ID="lblExplanation" runat="server" BackColor="Silver" Text="Explanation"
                    Width="100%"></asp:Label><br />
                <br />
                <div id="D_Explanation" runat="server" title="Answers">
                </div>
                <asp:Label ID="lblListeningFileURL" runat="server" BackColor="Silver" Text="Listening File Url"
                    Width="100%"></asp:Label><br />
                <asp:TextBox ID="txt_ListeningFileURL" runat="server" ReadOnly="True" Width="100%">
                </asp:TextBox>
                </div>
                <asp:Label ID="lblRemediation" runat="server" BackColor="Silver" Text="Remediation"
                    Width="100%"></asp:Label><br />
                <br />
                <asp:Label ID="lblTopicTitle" runat="server" Text="Topic Title" Width="182px"></asp:Label>
                <KTP:KTPDropDownList ID="ddTopicTitle" runat="server" Enabled="False">
                </KTP:KTPDropDownList>
                <br />
                <br />
                <div id="D_Remediation" runat="server" title="Remediation">
                </div>
                <br />
                <br />
                <asp:Label ID="lblStimulus" runat="server" BackColor="Silver" Text="Stimulus" Width="100%">
                </asp:Label><br />
                <br />
                <div id="D_Stimulus" runat="server" title="Answers">
                </div>
                <br />
                <asp:Label ID="Label1" runat="server" BackColor="Silver" Text="Production Category Information"
                    Width="100%"></asp:Label><br />
                <br />
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblClientNeeds" runat="server" Text="Client Needs" Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddClientNeeds" runat="server" AutoPostBack="True"  Enabled="false">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblClientNeedsCategory" runat="server" Text="Client Needs Category"
                                Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddClientNeedsCategory" runat="server" AutoPostBack="True"
                                Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px; height: 24px">
                            <asp:Label ID="lblClinicalNursing" runat="server" Text="Nursing Process" Width="182px">
                            </asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">
                            <KTP:KTPDropDownList ID="ddNursingProcess" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px; height: 20px;">
                            <asp:Label ID="lblLevelOfDifficulty" runat="server" Text="Level Of Difficulty" Width="182px">
                            </asp:Label>
                        </td>
                        <td style="width: 100px; height: 20px;">
                            <KTP:KTPDropDownList ID="ddLevelOfDifficulty" runat="server" AutoPostBack="True"
                                Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px; height: 24px">
                            <asp:Label ID="lblDemography" runat="server" Text="Demography" Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">
                            <KTP:KTPDropDownList ID="ddDemography" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblBloom" runat="server" Text="Cognitive Level" Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddBloom" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblScpecialityArea" runat="server" Text="Scpeciality Area" Width="182px">
                            </asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddScpecialityArea" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px; height: 24px">
                            <asp:Label ID="lblSystem" runat="server" Text="System" Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px; height: 24px">
                            <KTP:KTPDropDownList ID="ddSystem" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td>
                            <asp:Label ID="lblCriticalThinking" runat="server" Text="Critical Thinking" Width="182px">
                            </asp:Label>
                        </td>
                        <td>
                            <KTP:KTPDropDownList ID="ddCriticalThinking" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="height: 24px">
                            <asp:Label ID="lblClinicalConcepts" runat="server" Text="Clinical Concepts" Width="182px">
                            </asp:Label>
                        </td>
                        <td style="height: 24px">
                            <KTP:KTPDropDownList ID="ddClinicalConcepts" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <asp:Panel ID="NonPNCategories" runat="server">
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblAccreditationCategories" runat="server" Text="Accreditation Categories"
                                Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddAccreditationCategories" runat="server" AutoPostBack="True"
                                Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="width: 100px">
                            <asp:Label ID="lblQSENKSACompetencies" runat="server" Text="QSEN KSA Competencies"
                                Width="182px"></asp:Label>
                        </td>
                        <td style="width: 100px">
                            <KTP:KTPDropDownList ID="ddQSENKSACompetencies" runat="server" AutoPostBack="True"
                                Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    </asp:Panel>
                    <tr class="datatable2">
                        <td style="height: 24px">
                            <asp:Label ID="lblConcepts" runat="server" Text="Concepts" Width="182px">
                            </asp:Label>
                        </td>
                        <td style="height: 24px">
                            <KTP:KTPDropDownList ID="ddConcepts" runat="server" AutoPostBack="True" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <asp:Label ID="lblActive" runat="server" Text="Question Status" Width="182px"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdoActive" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td style="height: 24px">
                            <asp:Label ID="lblWhere" runat="server" Text="Where Used" Width="99px"></asp:Label>
                        </td>
                        <td style="height: 24px">
                            <asp:GridView ID="gvWhere" runat="server" AutoGenerateColumns="False" Width="293px">
                                <rowstyle cssclass="datatable2" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1" />
                                <columns>
                                    <asp:TemplateField HeaderText="Product Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="cohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Product.ProductName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                                </columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                &nbsp;
                <br />
                <asp:Label ID="Label2" runat="server" BackColor="Silver" Text="Question Taxonomy Table"
                    Width="100%"></asp:Label><br />
                <br />
                <table>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblProductLine" runat="server" Text="Product Line" Width="94px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lProductLine" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblPointb" runat="server" Text="Point Biserials" Width="94px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lPointb" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblStatistics" runat="server" Text="Statistics" Width="105px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lStatistics" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblCreator" runat="server" Text="Creator" Width="107px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lCreator" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblDCreated" runat="server" Text="Date Created" Width="106px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lDCreated" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblEditor" runat="server" Text="Editor" Width="101px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lEditor" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 26px">
                            <asp:Label ID="lblDEdited" runat="server" Text="Date Edited" Width="106px"></asp:Label>
                        </td>
                        <td style="width: 494px; height: 26px">
                            <asp:Label ID="lDEdited" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lbl2Editor" runat="server" Text="2nd Editor" Width="106px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="l2Editor" runat="server" Text="" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 26px">
                            <asp:Label ID="lbl2DEdit" runat="server" Text="Date 2nd Edited"></asp:Label>
                        </td>
                        <td style="width: 494px; height: 26px">
                            <asp:Label ID="l2DEdit" runat="server" Text="Label" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px">
                            <asp:Label ID="lblSBD" runat="server" Text="Source/SBD" Width="99px"></asp:Label>
                        </td>
                        <td style="width: 494px">
                            <asp:Label ID="lSBD" runat="server" Text="Label" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 26px">
                            <asp:Label ID="lblFeedback" runat="server" Text="FeedBack" Width="107px"></asp:Label>
                        </td>
                        <td style="width: 494px; height: 26px">
                            <asp:Label ID="lFeedback" runat="server" Text="Label" Width="281px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 26px">
                            <asp:Label ID="lblWho" runat="server" Text="Who Owns" Width="104px"></asp:Label>
                        </td>
                        <td style="width: 494px; height: 26px">
                            <asp:Label ID="lWho" runat="server" Text="Label" Width="281px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
