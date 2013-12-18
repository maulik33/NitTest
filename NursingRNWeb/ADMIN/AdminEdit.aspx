<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_AdminEdit" Title="Kaplan Nursing" EnableViewState="true" CodeBehind="AdminEdit.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(1, 'ctl00_Div3');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblTitle" runat="server" Text="Edit > Administrator" Width="464px"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit an Administrator">
                </asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 574px">
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below
                            <asp:HiddenField ID="hiddenUserId" runat="server" />
                            <asp:HiddenField ID="hiddenDDLevel" runat="server" />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            User Name:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <asp:TextBox ID="txtUserName" runat="server" Width="180px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            Password:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <asp:TextBox ID="txtPassword" runat="server" Width="180px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 21px; width: 87px;">
                            First Name:
                        </td>
                        <td class="datatable" align="left" style="height: 21px; width: 436px;">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="179px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            Last Name:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <asp:TextBox ID="txtLastName" runat="server" Width="177px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="112px">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px; height: 35px">
                            E-mail:
                        </td>
                        <td class="datatable" align="left" style="width: 436px; height: 35px;">
                            <asp:TextBox ID="txtEmail" runat="server" Width="176px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="112px">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*Wrong Email Format" Font-Size="Small" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="Form1" Width="131px">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            Admin Level:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <KTP:KTPDropDownList ID="ddLevel" runat="server" ShowNotSelected="false" ShowNotAssigned="false">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            Can Upload Helpful Documents:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <asp:CheckBox ID="chkAbleToUpload" runat="server" />
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2" style="height: 42px">
                            <a href="admin_student_add_detail.aspx"></a>
                            <asp:ImageButton ID="saveButton" runat="server" OnClick="saveButton_Click" ImageUrl="~/temp/images/btn_save.gif"
                                ValidationGroup="Form1" />
                            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="deleteButton" runat="server" OnClick="deleteButton_Click"
                                AlternateText="Delete Administrator" ImageUrl="~/temp/images/btn_del.gif" onMouseOver="roll(this)"
                                onMouseOut="roll(this)" />
                            <br />
                            <asp:Label ID="lblM" runat="server" ForeColor="Red" Text="User Name exists" Visible="False"
                                Width="148px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
