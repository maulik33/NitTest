<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_GroupEdit" Codebehind="GroupEdit.aspx.cs" %>

<%@ Register TagPrefix="Saravana" TagName="Calendar" Src="Calender.ascx" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(1, 'ctl00_Div6');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table id="Table1" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" class="headfont">
                            <b>
                                <asp:Label ID="lblTitle" runat="server" Text="Edit > Group" Width="515px"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit a Group" Width="522px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="left" border="0" class="formtable">
                                <tr class="datatablelabels">
                                    <td align="left" colspan="2">
                                        Enter details in fields below
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 120px">
                                        Group Name:
                                    </td>
                                    <td class="datatable" align="left">
                                         <asp:TextBox ID="txtGroupName" runat="server" Width="180px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGroupName"
                                            ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                                    </td>
                                    </tr>
                                    <tr class="datatable2" id="trProgramofStudy" runat="server">
                                        <td align="left" style="width: 120px">
                                            <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study:"></asp:Label>
                                        </td>
                                        <td class="datatable" align="left">
                                            <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged" ShowNotSelected="false"></KTP:KTPDropDownList>
                                            <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="datatable2">
                                        <td align="left" style="width: 120px">
                                            Institution:
                                        </td>
                                        <td align="left" class="datatable">
                                           
                                             <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged"  NotSelectedText="Not Selected">
                                             </KTP:KTPDropDownList>
                                        </td>
                                    </tr>
                                    <tr class="datatable2">
                                        <td align="left" style="width: 120px; height: 39px">
                                            Cohort:
                                        </td>
                                        <td align="left" class="datatable" style="height: 39px">
                                           
                                               <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="false" >
                                               </KTP:KTPDropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddCohort"
                                                InitialValue="-1" ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1"
                                                Width="123px"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="formtableApply">
                                        <td align="center" colspan="2">
                                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Temp/images/btn_save.gif"
                                                Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                                OnClick="btnSave_Click" ValidationGroup="Form1"></asp:ImageButton>
                                            &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                                AlternateText="Delete Group" ImageUrl="~/Temp/images/btn_del.gif" onMouseOver="roll(this)"
                                                onMouseOut="roll(this)" />
                                        </td>
                                    </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblM" runat="server" Width="130px" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
