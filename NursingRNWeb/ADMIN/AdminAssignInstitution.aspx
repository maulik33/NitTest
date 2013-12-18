<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_AdminAssignInstitution" CodeBehind="AdminAssignInstitution.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <asp:Label ID="lblTitle" runat="server" Text="Edit > Administrator" Width="199px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to assign Administrator to specific Institutions (this page is not complete, need to check with Kaplan">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 293px">
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            User Name:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUserName" runat="server" Width="180px" Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 21px">
                            First Name:
                        </td>
                        <td class="datatable" align="left" style="height: 21px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="179px" Enabled="False">
                            </asp:TextBox>
                        </td>
                        <td rowspan="4" align="left" valign="middle">
                            <br />
                            <br />
                        &nbsp;
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Last Name:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="186px" Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Admin Level:
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddLevel" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2" style="height: 42px">
                            <asp:HiddenField ID="hiddenDDLevel" runat="server" />
                            <%--<a href="admin_student_add_detail.aspx"></a>&nbsp;--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trProgramofStudy" runat="server">
            <td align="left">
                <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>&nbsp;&nbsp;&nbsp
                <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged">
                </KTP:KTPDropDownList>
                 <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramofStudy"
                                                InitialValue="-1" ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="Label1" runat="server" Text="Please select one institution for Local admin and technical admin. ">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:GridView ID="gvInst" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInst_RowDataBound"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="ch" runat="server" />
                                <asp:HiddenField ID="Active" runat="server" Value='<%# Bind("Active") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="InstitutionID" HeaderText="Institution ID" />
                        <asp:BoundField DataField="InstitutionNameWithProgOfStudy" HeaderText="Institution Name" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:ImageButton ID="btSave" runat="server" OnClick="btSave_Click" ValidationGroup="validateSearch" ImageUrl="~/Images/assign.gif" />
            </td>
        </tr>
    </table>
</asp:Content>
