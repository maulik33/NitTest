<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_ReleaseReview" Title="Untitled Page" Codebehind="ReleaseReview.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Preview", "");
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="part2">
                <!-- code body -->
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr class="formtable">
                        <td class="headfont">
                            <b>Release Content Management</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Review and approve or disapprove the items you want to Release to Production
                        </td>
                    </tr>
                    <asp:Panel ID="pnlQuestions" runat="server" Visible="false">
                        <tr class="datatablelabels">
                            <td align="left">
                                Content Questions
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="datatable2">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px" style="overflow-x:hidden" Width="100%">
                                <asp:GridView ID="gridQuestions" runat="server" AllowSorting="True" AllowPaging="False" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    AutoGenerateColumns="False" DataKeyNames="Id, QuestionId, ReleaseStatus"
                                    CssClass="data2" EmptyDataText="No Records to display" 
                                    OnRowDataBound="gridQuestions_RowDataBound" 
                                    onsorting="gridQuestions_Sorting" Width="100%">
                                    <RowStyle CssClass="datatable2" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QuestionId" HeaderText="Question ID" ReadOnly="True" Visible="True" SortExpression="QuestionId" />
                                        <asp:BoundField DataField="ProgramOfStudyName" HeaderText="Program of Study" SortExpression="ProgramofStudyName"/>
                                        <asp:BoundField DataField="TopicTitleId" HeaderText="Topic Title"  SortExpression="TopicTitleId"/>
                                        <asp:BoundField DataField="SystemId" HeaderText="System" SortExpression="SystemId"/>
                                        <asp:BoundField DataField="NursingProcessId" HeaderText="Nursing Process" SortExpression="NursingProcessId"/>
                                        <asp:BoundField DataField="ClientNeedsId" HeaderText="Client Needs" SortExpression="ClientNeedsId"/>
                                        <asp:BoundField DataField="ClientNeedsCategoryId" HeaderText="Client Need Category" SortExpression="ClientNeedsCategoryId" />
                                        <asp:BoundField DataField="ReleaseStatus" HeaderText="Release Status" Visible="False" />
                                        <asp:BoundField DataField="Id" HeaderText="QID" Visible="False" />
                                        <asp:TemplateField HeaderText="Preview">
                                            <ItemTemplate>
                                                <a href="javascript:openPopup('ViewQuestion.aspx?Mode=2&Id=<%# Eval("Id") %>')">Preview</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </asp:Panel>
                                <asp:HiddenField runat="server" ID="questionsSortConfig" Value="QuestionId|DESC" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlRemediations" runat="server" Visible="false">
                        <tr class="datatablelabels">
                            <td align="left">
                                Content Remediations
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="datatable2">
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="300px" style="overflow-x:hidden" Width="100%">
                                <asp:GridView ID="gridRemediations" runat="server" AutoGenerateColumns="False" DataKeyNames="RemediationID, ReleaseStatus"
                                    AllowSorting="True"
                                    EmptyDataText="No Records to display" 
                                    OnRowDataBound="gridQuestions_RowDataBound" 
                                    onsorting="gridRemediations_Sorting"  AllowPaging="False" Width="100%">
                                    <RowStyle CssClass="datatable2" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RemediationID" HeaderText="Remediation ID" Visible="false" />
                                        <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" SortExpression="TopicTitle"/>
                                        <asp:BoundField DataField="Explanation" HeaderText="Explanation" SortExpression="Explanation"/>
                                        <asp:BoundField DataField="ReleaseStatus" HeaderText="Status" Visible="False" />
                                        <asp:TemplateField HeaderText="Preview">
                                            <ItemTemplate>
                                                <a href="javascript:openPopup('EditR.aspx?Action=2&qid=<%# Eval("RemediationID") %>')">
                                                    Preview</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                               </asp:Panel>
                              <asp:HiddenField runat="server" ID="remediationsSortConfig" Value="TopicTitle|ASC" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlLippincott" runat="server" Visible="false">
                        <tr class="datatablelabels">
                            <td align="left">
                                Lippincott
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="datatable2">
                              <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="300px" style="overflow-x:hidden" Width="100%">
                                <asp:GridView ID="gridLippincott" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="No Records to display" AllowPaging="False" AllowSorting="True" BorderColor="#CC9966"
                                    DataKeyNames="LippincottID, ReleaseStatus" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" OnRowDataBound="gridQuestions_RowDataBound" 
                                    onsorting="gridLippincotts_Sorting" Width="100%">
                                    <RowStyle CssClass="datatable2a" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1a" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LippincottID" HeaderText="Lippincott ID" Visible="false" />
                                        <asp:BoundField DataField="LippincottTitle" HeaderText="Lippincott Title" SortExpression="LippincottTitle">
                                            <HeaderStyle Width="350px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Remediation Title" SortExpression="Remediation.TopicTitle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="TopicTitle" Text='<%#DataBinder.Eval(Container.DataItem,"Remediation.TopicTitle")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReleaseStatus" HeaderText="Status" Visible="False" />
                                        <asp:TemplateField HeaderText="Preview">
                                            <ItemTemplate>
                                                <a href="javascript:openPopup('NewLippincott.aspx?Mode=1&IID=<%# Eval("LippincottID") %>')">
                                                    Preview</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              </asp:Panel>
                              <asp:HiddenField runat="server" ID="lippincottsSortConfig" Value="LippincottTitle|ASC" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlTests" runat="server" Visible="false">
                        <tr class="datatablelabels">
                            <td align="left">
                                Custom Tests/AVP Items
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="datatable2">
                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical" Height="300px" style="overflow-x:hidden" Width="100%">
                                <asp:GridView ID="gridTests" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                    EmptyDataText="No Records to display" AllowSorting="True" BorderColor="#CC9966"
                                    DataKeyNames="TestID, ReleaseStatus" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    OnRowDataBound="gridQuestions_RowDataBound" 
                                    onsorting="gridTests_Sorting" Width="100%">
                                    <RowStyle CssClass="datatable2a" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1a" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Approve">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TestID" HeaderText="Test/AVP ID" SortExpression="TestId"/>
                                        <asp:BoundField DataField="ProgramOfStudyName" HeaderText="Program of Study" SortExpression="ProgramofStudyName"/>
                                        <asp:BoundField DataField="TestName" HeaderText="Test Name/AVP Item Name" SortExpression="TestName">
                                            <HeaderStyle Width="350px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Preview">
                                            <ItemTemplate>
                                                <a href="javascript:openPopup('<%# Eval("ProductId").ToString().Equals("4") ? Eval("URL", "{0}")  : "QuestionCustomTset.aspx?TestID=" + Eval("TestId")+"&SearchCondition= &Sort= &ProductId=" %>')">
                                                    <%# Eval("ProductId").ToString().Equals("4") ? "Preview AVP" : "Preview Test"%></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                    </asp:Panel>
                                <asp:HiddenField runat="server" ID="testsSortConfig" Value="TestId|DESC" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnRelease" runat="server" Text="Save & Continue" OnClick="btnRelease_Click" />
                                        <td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
