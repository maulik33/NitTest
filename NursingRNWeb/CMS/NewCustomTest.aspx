<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_NewCustomTest" Title="Kaplan Nursing" Codebehind="NewCustomTest.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3);
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to edit custom test
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below&nbsp;<br />
                             <div style="margin-left:15px"><asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red"></asp:Label></div>
                        </td>
                    </tr>
                       <tr class="datatable2">
                        <td align="left" width="25%">
                            <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                           <asp:DropDownList ID="ddlProgramofStudy" runat="server"></asp:DropDownList>
                           <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            <asp:Label ID="Label1" runat="server" Text="New Test Name:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="TextBox1" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            <asp:Label ID="Label3" runat="server" Text="Test Type:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                        <table>
                        <tr class="datatable2">
                            <td align="left" width="53%">
                       
                             <KTP:KTPDropDownList ID="ddProducts" runat="server" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                            </td>
                            <td align="right" width="25%">
                            <asp:Label runat="server" Text="Seconds/Question"></asp:Label>&nbsp;&nbsp; 
                            </td>
                            <td>
                            <asp:DropDownList ID="ddSecondsPerQuestion" runat="server" Width="100%">
                           <asp:ListItem value="72" Selected="true">1x</asp:ListItem>
                           <asp:ListItem value="108">1.5x</asp:ListItem>
                           <asp:ListItem value="144">2x</asp:ListItem>
                           <asp:ListItem value="216">3x</asp:ListItem>
                            <asp:ListItem value="0">Untimed</asp:ListItem>
                           </asp:DropDownList>         
                                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;
                            <asp:CheckBox ID="chbFRDefault" runat="server" Enabled="false" />Add to Default
                            Group
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="  Save " onclick="btnSave_Click"/>
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
