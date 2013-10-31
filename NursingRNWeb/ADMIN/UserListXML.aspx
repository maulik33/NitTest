<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="ADMIN_UserListXML" Codebehind="UserListXML.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div15');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Assign > Student</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <b>Step 1: Choose Students</b>
            </td>
        </tr>
        <tr class="datatable1">
            <td colspan="7" align="right">
                &nbsp;<table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            &nbsp;<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                OnClick="searchButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvStudents" Width="100%" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" OnPageIndexChanging="gvStudents_PageIndexChanging" OnSorting="gvStudents_Sorting"
                    AllowSorting="True" DataKeyNames="UserID">
                    <RowStyle CssClass="datatable2a" />
                    <HeaderStyle CssClass="datatablelabels" />
                    <AlternatingRowStyle CssClass="datatable1a" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select All">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                    type="checkbox" />
                            </HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField SortExpression="LastName" DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField SortExpression="FirstName" DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField SortExpression="UserName" DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField SortExpression="UserCreateDate" DataField="UserCreateDate" HeaderText="Date/Time" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblM" runat="server" ForeColor="#C00000" Text="Selected Students have been assigned"
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <b>Step 2: Assign Students</b>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table align="left" border="0" class="datatable">
                     <tr class="datatable1">
                        <td class="datatable" align="Left">
                           Program of Study: &nbsp;
                        </td>
                        <td class="datatable" align="Left">
                           <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddProgramofStudy_SelectedIndexChanged"
                            NotSelectedText="Selection Required" ShowNotSelected="true"></KTP:KTPDropDownList>
                        <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramOfStudy" 
                        ErrorMessage="*Required Field" ValidationGroup="Form1" Display="Static" InitialValue="-1">
                        </asp:RequiredFieldValidator>
                        </td>
                    </tr>
             
                    <tr class="datatable1">
                        <td class="datatable" align="Left" width="25%">
                            Select Institution: &nbsp;
                        </td>
                        <td class="datatable" align="Left">
                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged" ShowNotAssigned="false" ShowNotSelected="false"  ShowSelectAll="false">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td class="datatable" align="Left">
                            Select Cohort: &nbsp;
                        </td>
                        <td class="datatable" align="Left">
                            <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCohort_SelectedIndexChanged" ShowNotAssigned="false" ShowNotSelected="false" ShowSelectAll="false" >
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable1">
                        <td class="datatable" align="Left">
                            Select Group: &nbsp;
                        </td>
                        <td class="datatable" align="Left">
                            <KTP:KTPDropDownList ID="ddGroup" runat="server" ShowNotAssigned="false" ShowNotSelected="false" ShowSelectAll="false">
                            </KTP:KTPDropDownList>
                            (Optional)
                        </td>
                    </tr>
                    <tr>
                        <td class="datatable" colspan="2">
                            <asp:ImageButton ID="btnAssign" runat="server" ImageUrl="~/Temp/images/assign.gif"
                                OnClick="btnAssign_Click" ValidationGroup="Form1" />
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField runat="server" ID="hdnGridConfig" Value="LastName|ASC" />
            </td>
        </tr>
    </table>
</asp:Content>
