<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_AdminView" EnableViewState="false" CodeBehind="AdminView.aspx.cs" %>

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
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit an Administrator"></asp:Label>
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
                            <asp:TextBox ID="txtUserName" runat="server" Width="180px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            Password:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassword" runat="server" Width="180px" Enabled="False">
                            </asp:TextBox>
                            <asp:HiddenField ID="hiddenDDLevel" runat="server" />
                            <asp:HiddenField ID="hiddenUserId" runat="server" />
                        </td>
                        <td rowspan="7" align="left" valign="middle">
                            <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click" Width="101px">Edit</asp:LinkButton><br />
                            <br />
                            <asp:LinkButton ID="lbNew" runat="server" OnClick="lbNew_Click" Width="128px">Add Additional Admin</asp:LinkButton>
                            <asp:LinkButton ID="lbAssign" runat="server" Width="128px" OnClick="lbAssign_Click">Assign Institution</asp:LinkButton>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 21px">
                            First Name:
                        </td>
                        <td class="datatable" align="left" style="height: 21px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="179px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Last Name:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="186px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            E-mail:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="180px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Admin Level:
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddLevel" runat="server" Enabled="False">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 87px">
                            Can Upload Helpful Documents:
                        </td>
                        <td class="datatable" align="left" style="width: 436px">
                            <asp:CheckBox ID="chkAbleToUpload" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2" style="height: 42px">
                            <a href="admin_student_add_detail.aspx"></a>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvInst" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="InstitutionID" HeaderText="Institution ID" />
                        <asp:BoundField DataField="InstitutionNameWithProgOfStudy" HeaderText="Institution Name" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
