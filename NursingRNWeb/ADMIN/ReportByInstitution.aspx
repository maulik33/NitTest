<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ADMIN/ReportMaster.master" Inherits="Admin_ReportByInstitution" Codebehind="ReportByInstitution.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP"  Assembly="NursingRNWeb"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_tdReportByInstitution');
        });

        function Validate() {
            var lbInstitution = document.getElementById('<%=lbInstitution.ClientID%>');
            var lbCohort = document.getElementById('<%=lbCohort.ClientID %>');
            var ddTests = document.getElementById('<%=ddTests.ClientID %>');
            var ddProducts = document.getElementById('<%=ddProducts.ClientID %>');

            if (lbInstitution.options.selectedIndex == -1 || lbInstitution.options.selectedIndex == 0) {
                alert('Select Institution');
                return false;
            }

            if (lbCohort != null && lbCohort.options.selectedIndex == -1) {
                alert('Select Cohort');
                return false;
            }

            if (ddProducts != null && ddProducts.options[ddProducts.selectedIndex].value == '-1') {
                alert('Select Test Type');
                return false;
            }

            if (ddTests != null && ddTests.options[ddTests.selectedIndex].value == '-1') {
                alert('Select Test Name');
                return false;
            }

            return true;
        }

    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Tests by Institution</b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td colspan="11" align="left">
                            <table style="width: 100%;">
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
                                    <td colspan="4" >
                                        <KTP:KTPListBox ID="lbInstitution" runat="server" AutoPostBack="True" ShowSelectAll="false"
                                            SelectionMode="Multiple" />
                                    </td>
                                   </tr>
                                    <tr>
                                    <td class="reportParamTopLabel">
                                        Cohort
                                    </td>
                                    <td colspan="4" >
                                        <KTP:KTPListBox ID="lbCohort" runat="server" AutoPostBack="True"  SelectionMode="Multiple" ShowNotSelected="false" width="275px"/>
                                    </td>                                    
                                </tr>
                                <tr>
                                <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowSelectAll="true" >
                                        </KTP:KTPDropDownList>
                                    </td>                                    
                                    <td></td>
                                    <td class="reportParamLabel" style="white-space:nowrap;">
                                        Test Name
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddTests" runat="server" ShowSelectAll="false">
                                        </KTP:KTPDropDownList>                                      
                                    </td>
                                </tr>                                
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSubmit_Click" OnClientClick="return Validate();" />
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="ImageButton1_Click1" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClick="ImageButton2_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClick="ImageButton3_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
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
                                    <asp:TemplateField HeaderText="Cohort" SortExpression="Cohort.CohortName">
                                        <ItemTemplate>
                                            <asp:Label  runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Institution" SortExpression="Institution.InstitutionName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:BoundField DataField="NStudents" HeaderText="# Students" SortExpression="NStudents" />
                                    <asp:BoundField HeaderText=" % Correct" DataField="Percantage" SortExpression="Percantage" />
                                    <asp:BoundField HeaderText=" Normed % Correct" DataField="Normed"  DataFormatString="{0:F1}" SortExpression="Normed" />
                                    <asp:ButtonField CommandName="Performance" Text="Institution Performance Report" />
                                    <asp:TemplateField ControlStyle-Width="0" HeaderStyle-Width="0">
                                        <ItemTemplate>
                                            <input id="hdnInstitutionId" type="hidden" runat="server" value='<%# Eval("Institution.InstitutionId") %>' />
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
     <asp:HiddenField runat="server" ID="hdnGridConfig" Value="Cohort.CohortName|ASC" />
</asp:Content>
