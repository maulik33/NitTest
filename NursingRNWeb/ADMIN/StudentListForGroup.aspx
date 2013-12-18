<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_StudentListForGroup" Codebehind="StudentListForGroup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Group > Student List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use the check box to assign or unassign a student from this Group, then click Save.
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable2">
                        <td align="Left">
                            <table width="98%" border="0" cellspacing="4" cellpadding="0">
                                <tr>
                                    <td width="20%">
                                        <b>Group Name:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroupName" runat="server" Width="422px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Cohort Name:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCohortName" runat="server" Height="15px" Text="Not Assigned" Width="420px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Institution Name:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInstitutionName" runat="server" Text="Not Assigned" Width="413px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Program Name:</b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProgram" runat="server" Text="" Width="413px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="datatable1">
                        <td align="right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <input type="text" name="srch">
                                    </td>
                                    <td>
                                        <img src="../Temp/images/viewall.gif" width="75" height="25" border="0" alt="" onmouseover="roll(this)"
                                            onmouseout="roll(this)" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"
                                OnRowCommand="gvStudents_RowCommand" OnRowDataBound="gvStudents_RowDataBound"
                                Width="100%" CellPadding="5" AllowSorting="True" OnSorting="gvStudents_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ch" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="IsAssigned" runat="server" Value='<%#DataBinder.Eval(Container.DataItem,"Institution.Active")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Tests" />
                                    <asp:ButtonField CommandName="Edit" Text="Edit" />
                                    <asp:ButtonField CommandName="Tests" Text="Test Dates" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="UserName|ASC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:ImageButton ID="btAssign" runat="server" OnClick="btAssign_Click" ImageUrl="~/Images/assign.gif"
                    Width="73px" Height="20px" />
            </td>
        </tr>
    </table>
</asp:Content>
