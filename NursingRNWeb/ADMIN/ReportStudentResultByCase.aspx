<%@ Page Language="C#" MasterPageFile="~/ADMIN/ReportMaster.master" AutoEventWireup="true" Inherits="ADMIN_ReportStudentResultByCase" Codebehind="ReportStudentResultByCase.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(4, 'ctl00_tdReportStudentResultByCase');
        });

        function Validate() {
            var ddInstitution = document.getElementById('<%=ddInstitution.ClientID%>');
            var ddCohorts = document.getElementById('<%=ddCohort.ClientID %>');
            var ddCase = document.getElementById('<%=ddCase.ClientID %>');

            if (ddInstitution.options[ddInstitution.selectedIndex].value == '-1') {
                alert('Select Institution');
                return false;
            }

            if (ddCohorts.options[ddCohorts.selectedIndex].value == '-1') {
                alert('Select Cohort');
                return false;
            }

            if (ddCase.options[ddCase.selectedIndex].value == '-1') {
                alert('Select Case');
                return false;
            }

            return true;
        }
    </script>
    <!-- report body -->
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblTitle" runat="server" Text="Student Results by Case"></asp:Label>
                </b>
                <br />
            </td>
        </tr>
        <tr>
            <td style="height: 101px">
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="5" align="right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" style="height: 23px">
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                    <td style="height: 23px">
                                        <asp:ImageButton ID="searchbtn" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                                            onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="Searchbtn_Click"
                                            OnClientClick="return Validate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td colspan="5" align="left">
                            <table style="width: 100%;" >
                             <tr runat="server" id="trProgramofStudy">
                                        <td class="reportParamLabel" >
                                            Program of Study 
                                            
                                        </td>
                                        <td colspan="4"><KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList></td>
                                </tr>
                                <tr >
                                     <td class="reportParamLabel" style="white-space:nowrap;">
                                        Select Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>                                     
                                </tr>
                                <tr>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Select Cohort
                                    </td>
                                    <td >
                                        <KTP:KTPDropDownList ID="ddCohort" runat="server" ShowSelectAll="false" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td></td>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Select Case
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddCase" runat="server" AutoPostBack="True" ShowSelectAll="false" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile the report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                </table>
                <!-- end report body -->
            </td>
        </tr>
    </table>
    <asp:GridView ID="gridUsers" Width="100%" runat="server" AutoGenerateColumns="False"
        OnRowCommand="gridUsers_RowCommand" CellPadding="5" AllowSorting="True" OnSorting="gridUsers_Sorting"
        DataKeyNames="EnrollmentID">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="StudentId" HeaderText="User ID" SortExpression="StudentId" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="FullName" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:ButtonField HeaderText="View" CommandName="View" Text="View" />
        </Columns>
    </asp:GridView>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="StudentId|ASC" />
</asp:Content>
