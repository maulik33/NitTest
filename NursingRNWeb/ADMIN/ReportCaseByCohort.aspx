<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ADMIN/ReportMaster.master" Inherits="ADMIN_ReportCaseByCohort" Codebehind="ReportCaseByCohort.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(4, 'ctl00_tdReportCasebyCohort');
        });

        function Validate() {
            var lbInstitution = document.getElementById('<%=lbInstitution.ClientID%>');
            var ddCase = document.getElementById('<%=ddCase.ClientID %>');
            var ddModule = document.getElementById('<%=ddModule.ClientID %>');

            if (lbInstitution.options.selectedIndex == -1) {
                alert('Select Institution');
                return false;
            }

            if (ddCase != null && ddCase.options[ddCase.selectedIndex].value == '-1') {
                alert('Select Case');
                return false;
            }

            if (ddModule != null && ddModule.options[ddModule.selectedIndex].value == '-1') {
                alert('Select Module');
                return false;
            }

            return true;
        }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Case Results by Cohort </b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td  align="left">
                            <table style="width: 100%;" >
                             <tr runat="server" id="trProgramofStudy">
                                        <td class="reportParamLabel" >
                                            Program of Study 
                                            
                                        </td>
                                        <td colspan="4"><KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="true" 
                                                ShowNotSelected="false">
                                            </KTP:KTPDropDownList></td>
                                    </tr>
                                <tr>
                                   <td class="reportParamTopLabel">
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <KTP:KTPListBox ID="lbInstitution" runat="server" AutoPostBack="True" ShowNotSelected="false" ShowSelectAll="false"  SelectionMode="Multiple">
                                        </KTP:KTPListBox>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Case
                                    </td>
                                    <td  align="left">
                                       <KTP:KTPDropDownList ID="ddCase" runat="server" ShowNotSelected="true" >
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td></td>
                                   <td class="reportParamLabel">
                                        Module
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddModule" runat="server"  ShowNotSelected="true" >
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>                               
                                <tr align="center">
                                    <td colspan="5" >
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();"></asp:ImageButton>&nbsp;&nbsp;&nbsp;
                                         <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="ImageButton1_Click1"  OnClientClick="return Validate();"/>&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_toexcel.gif"  OnClientClick="return Validate();"
                                            Style="margin-top: 3px;" OnClick="ImageButton2_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"  OnClientClick="return Validate();"
                                            Style="margin-top: 3px;" OnClick="ImageButton3_Click" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="There is not enough data to compile a cohort report"
                                Visible="False" Width="347px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" ID="gvCohorts" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="gvCohorts_RowCommand" OnRowDataBound="gvCohorts_RowDataBound"
                                CellPadding="5" AllowSorting="True" OnSorting="gvCohorts_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Cohort" SortExpression="CohortName" >
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NumberOfStudents" HeaderText="# Students" SortExpression="NumberOfStudents" />
                                    <asp:BoundField HeaderText=" % Correct" DataField="Percentage" SortExpression="Percentage" />
                                    <asp:ButtonField CommandName="Performance" Text="Cohort Result By Module" />
                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <input id="hdnCohortId" type="hidden" runat="server" value='<%# Eval("Cohort.CohortId") %>' />
                                            <input id="hdnInstitutionId" type="hidden" runat="server" value='<%# Eval("InstitutionId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;
</asp:Content>
