<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="CMS_SearchQuestion" CodeBehind="SearchQuestion.aspx.cs" %>

<%@ Register Src="~/CMS/Controls/SearchQuestionCriteria.ascx" TagName="SearchQuestionCriteria"
    TagPrefix="ucSearchQuestionCriteria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3);
        });
    </script>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="part2">
                <!-- code body -->
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="formtable">
                        <td class="headfont">
                            <b>Content Management - Search, View, Edit</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 14px">
                            To locate a test or question, drill down through the dropdown menus and click Search
                            Questions. To locate a question quickly for editing, enter the QID and then click
                            Search Questions.
                        </td>
                    </tr>
                    <ucSearchQuestionCriteria:SearchQuestionCriteria ID="ucSearchQuestionCriteria" runat="server" />
                </table>
                <br />
                <br />
                <div class="lab1">
                    <asp:Label ID="Label1" runat="server" Text="Search Results :"></asp:Label>
                    <asp:Label ID="lblNumberQ" runat="server" Width="290px"></asp:Label><br />
                </div>
                <asp:GridView ID="gvQuestions" runat="server" AllowSorting="True" 
                    DataKeyNames="QID" AutoGenerateColumns="False"
                    Width="100%" OnRowCommand="gvQuestions_RowCommand" AllowPaging="True" PageSize="300"
                    OnPageIndexChanged="gvQuestions_PageIndexChanged" OnPageIndexChanging="gvQuestions_PageIndexChanging"
                    CellPadding="5" onsorting="gvQuestions_Sorting" OnRowDataBound="gvQuestions_RowDataBound">
                    <RowStyle CssClass="datatable2" />
                    <HeaderStyle CssClass="datatablelabels" />
                    <AlternatingRowStyle CssClass="datatable1 datatablefont" />
                    <Columns>
                        <asp:ButtonField CommandName="ViewQuestion" Text="View" DataTextField="QN" />
                        <asp:BoundField DataField="QuestionID" HeaderText="QID" SortExpression="QuestionID" />
                        <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" SortExpression="TopicTitle"/>
                        <asp:BoundField DataField="LevelofDifficulty" HeaderText="Level of Difficulty" SortExpression="LevelofDifficulty"/>
                        <asp:BoundField DataField="NursingProcess" HeaderText="Nursing Process" SortExpression="NursingProcess"/>
                        <asp:BoundField DataField="ClinicalConcept" HeaderText="Clinical Concepts" SortExpression="ClinicalConcept" />
                        <asp:BoundField DataField="Demographic" HeaderText="Demographics" SortExpression="Demographic"/>
                        <asp:BoundField DataField="ReleaseStatus" HeaderText="Status" SortExpression="ReleaseStatus"/>
                        <asp:ButtonField CommandName="EditQuestion" Text="Edit/Inactive" SortExpression="EditQuestion" />
                        <asp:ButtonField CommandName="CopyQuestion" Text="Copy" SortExpression="CopyQuestion" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gvRem" runat="server" AutoGenerateColumns="False" DataKeyNames="RemediationID"
                    OnRowCommand="gvRem_RowCommand" OnRowDataBound="gvrem_RowDataBound" Width="667px">
                    <RowStyle BackColor="#F0F0FE" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E9ECF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#CBCBFA" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#CBCBFA" ForeColor="#333333" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="#F3F3F3" />
                    <Columns>
                        <asp:BoundField DataField="RemediationID" HeaderText="Remediation ID" />
                        <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" />
                        <asp:BoundField DataField="Explanation" HeaderText="Explanation" />
                        <asp:BoundField DataField="ReleaseStatus" HeaderText="Status" />
                        <asp:ButtonField CommandName="EditR" Text="Edit/Delete" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="QuestionID|ASC" />
</asp:Content>
