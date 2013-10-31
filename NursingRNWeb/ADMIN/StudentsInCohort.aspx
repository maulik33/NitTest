<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_StudentsInCohort" CodeBehind="StudentsInCohort.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="left" border="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Cohort > Student List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use the Institution, Cohort, and Group dropdown menus to generate a list of Students.
            </td>
        </tr>
        <tr class="datatable1">
            <td align="right">
                <table border="0" cellpadding="4" cellspacing="0">
                    <tr>
                        <td align="right">
                            &nbsp;<input name="srch" type="text" />
                        </td>
                        <td>
                            <asp:ImageButton ID="seabtn" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                                onmouseout="roll(this)" onmouseover="roll(this)" Width="75" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="datatable2">
            <td align="left">
                <table width="98%" border="0" cellspacing="4" cellpadding="0">
                    <tr class="datatable2" runat="server" id="trProgramodStudy" visible="false">
                        <td>
                            Program of Study:
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" ShowNotSelected="false"
                                OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  width="20%">
                            Select Institution:
                        </td>
                        <td>
                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged"
                                ShowNotSelected="true">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Select Cohort:
                        </td>
                        <td>
                            <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohort_SelectedIndexChanged"
                                ShowNotSelected="true">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Select Group:
                        </td>
                        <td>
                            <KTP:KTPDropDownList ID="ddGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddGroup_SelectedIndexChanged"
                                ShowNotSelected="true">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                      <tr class="datatable1">
                        <td align="center" colspan="2">
                            <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                OnClick="btnPrintPDF_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                OnClick="btnPrintExcel_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                OnClick="btnPrintExcelDataOnly_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <b>N=<asp:Label ID="lblStudentNumber" runat="server"></asp:Label></b><br />
                <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                    OnRowCommand="gvStudents_RowCommand" Width="100%" CellPadding="5" AllowSorting="True"
                    OnSorting="gvStudents_Sorting">
                    <RowStyle CssClass="datatable2a" />
                    <HeaderStyle CssClass="datatablelabels" />
                    <AlternatingRowStyle CssClass="datatable1a" />
                    <Columns>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                        <asp:TemplateField HeaderText="Institution">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionNameWithProgOfStudy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cohort">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="cohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="GroupName" Text='<%#DataBinder.Eval(Container.DataItem,"Group.GroupName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="ProgramName" Text='<%#DataBinder.Eval(Container.DataItem,"Program.ProgramName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Tests" />
                        <asp:ButtonField CommandName="Edit" Text="Edit" />
                        <asp:ButtonField CommandName="Tests" Text="Test Dates" />
                    </Columns>
                </asp:GridView>
                <asp:HiddenField runat="server" ID="hdnGridConfig" Value="UserName|ASC" />
                <asp:HiddenField runat="server" ID="hdnInstitutionId" />
            </td>
        </tr>
    </table>
</asp:Content>
