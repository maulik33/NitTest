<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="admin_UserList" Title="Kaplan Nursing" EnableViewState="true" CodeBehind="UserList.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div14');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Student List</b>
            </td>

        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Student
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="11"  align="center">
                            <table border="0" cellpadding="0" cellspacing="0">    
                                <tr>
                                  <td align="left">
                                      <asp:Label ID="lblProgOfStudyTxt" runat="server" Text="Program of Study:" Width="120px" visible="false" style="font-weight:bold"></asp:Label>
                                      <KTP:KTPDropDownList ID="ddlProgramOfStudy" Width="100px" runat="server" visible="false" ShowNotSelected="false" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                                   </td>
                                   <td  align="right" style="padding-left:25px;">
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                    <td align="right">
                                       <asp:ImageButton ID="searchButton" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                                        onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="searchButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        Select Institution:
                                    </td>
                                    <td colspan="2">
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" ShowNotSelected="true"   AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Select Cohort:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohort_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Select Group:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddGroup_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td colspan="2" align="right">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCreateAdhocGroup" runat="server" Text="Create Adhoc Group" OnClick="btnCreateAdhocGroup_Click"
                                                        Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                                        OnClick="ImageButton4_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div align="right" style="padding-right: 10px;">
                                 <asp:LinkButton ID="lbViewSecuritySummaryReport" runat="server" OnClick="lbViewSecuritySummaryReport_Click" Visible="False" Width="165px">Security Summary Report</asp:LinkButton><br />
                            </div>
                            <asp:Label ID="lblNumber" runat="server" Font-Bold="True"></asp:Label>
                            <asp:GridView ID="gridUsers" runat="server" AllowSorting="True" BackColor="White"
                                CellPadding="5" AutoGenerateColumns="False" DataKeyNames="UserId" OnPageIndexChanged="gridUsers_PageIndexChanged"
                                OnRowCommand="gridUsers_RowCommand" Width="100%" OnSorting="gridUsers_Sorting"
                                OnPageIndexChanging="gridUsers_PageIndexChanging" AllowPaging="True" PageSize="10">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="UserId" HeaderText="User Id" InsertVisible="False" ReadOnly="True"
                                        SortExpression="UserId" />
                                    <asp:BoundField DataField="LastName" Visible="false" HeaderText="Last Name" SortExpression="LastName" />
                                    <asp:BoundField DataField="FirstName" Visible="false" HeaderText="First Name" SortExpression="FirstName" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                    <asp:TemplateField SortExpression="Institution.InstitutionNameWithProgOfStudy" HeaderText="Institution Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionNameWithProgOfStudy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Cohort.CohortName" HeaderText="Cohort">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="cohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Group.GroupName" HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="GroupName" Text='<%#DataBinder.Eval(Container.DataItem,"Group.GroupName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="Select" Text="Edit" />
                                    <asp:ButtonField CommandName="Tests" Text="Test Dates" />
                                    <asp:ButtonField CommandName="ViewSecurityReport" Text="Security Report" Visible="False"  />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="UserName|ASC" />
                        </td>
                    </tr>
                </table>
    </table>
</asp:Content>
