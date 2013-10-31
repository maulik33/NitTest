<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="SetOverride.aspx.cs" Inherits="ADMIN_SetOverride" EnableViewState="true" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SelectAllCheckboxes(chk) {
            $('#<%=gvUsers.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }

        function setupFunc() {
            showBusysign();
        }

        function hideBusysign() {
            document.body.style.cursor = "auto";
        }

        function showBusysign() {
            document.body.style.cursor = "Progress";
        }

        //function to check header checkbox based on child checkboxes condition
        function Selectchildcheckboxes(header) {
            var headerchk = document.getElementById(header);
            if (headerchk.checked) {
                headerchk.checked = false;
            }
        }

        function ResumeValidation() {
            var checkedCheckboxes = $("#<%=gvUsers.ClientID%> input[id*='ch']:checkbox:checked").size();
            var resumeItem = $('#ctl00_ContentPlaceHolder1_hdResume').val();
            var resumePageItems = $('#ctl00_ContentPlaceHolder1_hdResumePagelevel').val();
            if ((resumeItem == '' || resumeItem == null) && checkedCheckboxes == 0) {
                alert('Please select atleast one test that needs to be resumed');
            }

            if ((resumeItem != '' || resumeItem != null) && checkedCheckboxes == 0) {
                var resumeArray = resumeItem.split('|');
                var resumePageArray = resumePageItems.split('|');
                var itemexist = false;
                for (var pi = 0; pi < resumePageArray.length - 1; pi++) {
                    for (var i = 0; i < resumeArray.length - 1; i++) {
                        if (resumePageArray[pi] == resumeArray[i]) {
                            itemexist = true;
                        }
                    }
                    if (itemexist == false || pi == resumeArray.length - 1) {
                        break;
                    }
                }
                if (itemexist == true) {
                    alert('Please select atleast one test that needs to be resumed');
                }
            }
        }
    </script>
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table width="100%">
                     <tr> 
                        <td class="reportParamLabel" style="width: 72px">
                            Program of study
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" NotSelectedText="Selection Required"
                                OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="reportParamLabel" style="width: 72px">
                            Institution
                        </td>
                        <td align="left" colspan="4">
                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowNotSelected="true" NotSelectedText="Not Selected" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="reportParamTopLabel" style="width: 72px">
                            Cohort
                        </td>
                        <td align="left" colspan="4">
                            <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" SelectionMode="Multiple"
                                ShowNotSelected="false" ShowSelectAll="True" Width="338px" OnSelectedIndexChanged="lbxCohort_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td class="reportParamLabel" style="width: 72px">
                            <span>User Name</span>
                        </td>
                        <td align="left" style="width: 309px">
                            <asp:TextBox ID="txtUserName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="reportParamLabel" style="width: 83px">
                            <span>Test Name</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTestName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="reportParamLabel" style="width: 72px">
                            <span>First Name</span>
                        </td>
                        <td align="left" style="width: 309px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="reportParamLabel" style="width: 83px">
                            <span>Last Name</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 72px">
                        </td>
                        <td align="left" colspan="4">
                            <asp:CheckBox runat="server" ID="ShowOnlyIncompleteCheckBox" Checked="true" Text="Show only incomplete Tests" />
                        </td>
                    </tr>
                </table>
                <div style="text-align: center">
                    <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SearchDeletedTests" runat="server" Text="Search Deleted Tests" OnClick="SearchDeletedTests_Click" />
                </div>
                <div style="text-align: left">
                    <asp:Label runat="server" ID="RowCountLabel" ForeColor="Green" Visible="false" Font-Bold="true" />
                </div>
                <asp:Label Text="Search has returned more than 3000 tests. System has restricted the number of tests returned by the Search to 3000. If you could not find the test you are looking for, please refine your Search Criteria."
                    runat="server" ID="RowCountWarningLabel" ForeColor="Red" Visible="false" />
                <asp:MultiView ID="SearchMultiView" runat="server">
                    <asp:View ID="v_delete" runat="server">
                        <div style="text-align: center">
                            <asp:Label runat="server" ID="ActiveLabel" Visible="true" Font-Bold="true" Align="center"
                                Text="Search result for Active Tests." />
                        </div>
                        <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AllowSorting="True"
                            EmptyDataText="No Records" DataKeyNames="UserTestId" OnPageIndexChanged="grid_PageIndexChanged"
                            Width="100%" OnSorting="grid_Sorting" OnPageIndexChanging="grid_PageIndexChanging"
                            AutoGenerateColumns="False" OnRowCommand="grid_RowCommand" OnRowDataBound="grid_OnRowDataBound"
                            OnRowDeleting="gvUsers_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="Horizontal"
                            PageSize="20">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="UserTestId" HeaderText="User Test Id" SortExpression="UserTestId" />
                                <asp:TemplateField SortExpression="Student.FirstName" HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.FirstName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Student.LastName" HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.LastName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Student.UserName" HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InstitutionNameWithProgOfStudy" HeaderText="Institution Name" SortExpression="InstitutionNameWithProgOfStudy" />
                                <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                                <asp:TemplateField SortExpression="Test.TestName" HeaderText="Test Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="TestName" Text='<%#DataBinder.Eval(Container.DataItem, "Test.TestName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TestStarted" HeaderText="Test Taken on" SortExpression="TestStarted"
                                    DataFormatString="{0:MM/dd/yy hh:mm:ss tt}" />
                                <asp:BoundField DataField="TestCompleted" HeaderText="End Time" SortExpression="TestCompleted"
                                    DataFormatString="{0:MM/dd/yy hh:mm:ss tt}" />
                                <asp:BoundField DataField="TimeUsed" HeaderText="Time Used" SortExpression="TimeUsed" />
                                <asp:BoundField DataField="AnsweredCount" HeaderText="Questions Answered" SortExpression="AnsweredCount" />
                                <asp:BoundField DataField="NumberOfQuestions" HeaderText="Question Count" SortExpression="NumberOfQuestions" />
                                <asp:BoundField DataField="TestStatus" HeaderText="Test Status" SortExpression="TestStatus" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headerSelectAll" runat="server" Visible="false" Text="" onclick="javascript:SelectAllCheckboxes(this);"
                                            ToolTip="SelectAll/DeSelectAll" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ForeColor="#333333" ID="ch" runat="server" OnCheckedChanged="UpdateAssignedItem"
                                            AutoPostBack="True" />
                                        <asp:HiddenField ID="Active" runat="server" Value='<%# Bind("Active") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" />
                            <RowStyle BackColor="#FCF3A2" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                        <div style="text-align: right">
                            <input id="hdResume" runat="server" type="hidden" />
                            <input id="hdResumePagelevel" runat="server" type="hidden" />
                            <input id="hdCheckListCount" runat="server" type="hidden" value="0" />
                            <input id="hdActiveCount" runat="server" type="hidden" value="0" />
                            <asp:Button ID="btnResume" runat="server" Text="Resume" OnClick="Resume_Click" Visible="false"
                                OnClientClick="ResumeValidation();" />
                        </div>
                    </asp:View>
                    <asp:View ID="v_display" runat="server">
                        <div style="text-align: center">
                            <asp:Label runat="server" ID="DeletedLabel" Visible="true" Font-Bold="true" Text="Search result for Deleted Tests." />
                        </div>
                        <asp:GridView ID="gvDisplayDeletedTests" runat="server" AllowPaging="True" AllowSorting="True"
                            EmptyDataText="No Records" DataKeyNames="UserTestId" OnPageIndexChanged="grid_PageIndexChanged"
                            Width="100%" OnSorting="grid_Sorting" OnPageIndexChanging="grid_PageIndexChanging"
                            AutoGenerateColumns="False" OnRowDataBound="grid_OnRowDataBound" CellPadding="4"
                            ForeColor="#333333" GridLines="Horizontal" PageSize="20">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="UserTestId" HeaderText="User Test Id" SortExpression="UserTestId" />
                                <asp:TemplateField SortExpression="Student.FirstName" HeaderText="First Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.FirstName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Student.LastName" HeaderText="Last Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.LastName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Student.UserName" HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Student.UserName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InstitutionNameWithProgOfStudy" HeaderText="Institution Name" SortExpression="InstitutionNameWithProgOfStudy" />
                                <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                                <asp:TemplateField SortExpression="Test.TestName" HeaderText="Test Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="TestName" Text='<%#DataBinder.Eval(Container.DataItem, "Test.TestName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TestStarted" HeaderText="Test Taken on" SortExpression="TestStarted"
                                    DataFormatString="{0:MM/dd/yy hh:mm:ss tt}" />
                                <asp:BoundField DataField="TestCompleted" HeaderText="End Time" SortExpression="TestCompleted"
                                    DataFormatString="{0:MM/dd/yy hh:mm:ss tt}" />
                                <asp:BoundField DataField="TimeUsed" HeaderText="Time Used" SortExpression="TimeUsed" />
                                <asp:BoundField DataField="AnsweredCount" HeaderText="Questions Answered" SortExpression="AnsweredCount" />
                                <asp:BoundField DataField="NumberOfQuestions" HeaderText="Question Count" SortExpression="NumberOfQuestions" />
                                <asp:BoundField DataField="TestStatus" HeaderText="Test Status" SortExpression="TestStatus" />
                                <asp:BoundField DataField="UserName" HeaderText="Deleted By" SortExpression="UserName" />
                                <asp:BoundField DataField="TestDeletedDate" HeaderText="Deleted Date" SortExpression="TestDeletedDate"
                                    DataFormatString="{0:MM/dd/yy hh:mm:ss tt}" />
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" />
                            <RowStyle BackColor="#FCF3A2" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                    </asp:View>
                </asp:MultiView>
                <div style="text-align: left;">
                    <asp:Label runat="server" ID="TimezoneHintLabel" Text="Date & Time displayed under 'Test Taken
                on' column is adjusted for Student's Timezone." Visible="false" Font-Size="X-Small" ForeColor="GrayText"></asp:Label>
                </div>
                <asp:HiddenField runat="server" ID="hdnGridConfig" Value="TestStarted|DESC" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Search" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="SearchDeletedTests" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1"
        runat="server">
        <Animations>
            <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="setupFunc();" />
                </Parallel>
            </OnUpdating>
            <OnUpdated>
                <Parallel duration="0">
                    <ScriptAction Script="hideBusysign();" />
                </Parallel>
            </OnUpdated>
        </Animations>
    </cc1:UpdatePanelAnimationExtender>
</asp:Content>
