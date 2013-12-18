<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="admin_GroupList" Title="Kaplan Nursing" EnableViewState="true" CodeBehind="GroupList.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div13');
        });
    </script>
   
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr  style="width: 125px;height:30%">
            <td  class="headfont">
                <b>View > Group List</b>
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Group
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                             <tr id="trProgramofStudy" runat="server">
                                 <td class="reportParamLabel" style="width: 120px" >
                                        <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                                    </td>
                                    <td>
                                       <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged" ShowNotSelected="false"></KTP:KTPDropDownList>
                                     </td>
                                </tr>
                             <tr>
                                    <td class="reportParamLabel" style="width: 120px">
                                        Institution
                                    </td>
                                    <td colspan="4">
                                        <ktp:ktpdropdownlist ID="ddInstitution" runat="server" AutoPostBack="True" 
                                            ShowNotSelected="false" ShowSelectAll="true" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                                        </ktp:ktpdropdownlist>
                                    </td>
                                </tr>
                             <tr>
                                  <td class="reportParamTopLabel" style="width: 120px">
                                        Cohort
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbxCohort" runat="server" AutoPostBack="True" 
                                            SelectionMode="Multiple" ShowNotSelected="false" ShowSelectAll="True"
                                            Width="275px" onselectedindexchanged="lbxCohort_SelectedIndexChanged" />
                                    </td>
                                </tr>
                            </table>
                            
                           <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                              <td align="right">
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                               </td>
                                <td align="center">
                                    <asp:ImageButton ID="searchButton" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                                            onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="searchButton_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrintPDF_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcelDataOnly_Click" />
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>

<tr>
<td>
<asp:Label ID="lblM" runat="server" Visible="false" Text="No items found" ForeColor="Red"></asp:Label>
</td>
</tr>                    <tr>
                        <td>
                            <asp:GridView ID="gridGroups" runat="server" AllowSorting="True" BackColor="White"
                                CellPadding="5" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="GroupId"
                                PageSize="10" OnPageIndexChanged="gridGroups_PageIndexChanged" OnRowCommand="gridGroups_RowCommand"
                                Width="100%" OnSorting="gridGroups_Sorting" OnPageIndexChanging="gridGroups_PageIndexChanging">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="GroupId" HeaderText="Group Id" InsertVisible="False" ReadOnly="True"
                                        SortExpression="GroupId" />
                                    <asp:BoundField DataField="GroupName" HeaderText="Name" SortExpression="GroupName" />
                                    <asp:TemplateField SortExpression="Cohort.CohortName" HeaderText="Cohort">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="cohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Institution.InstitutionNameWithProgOfStudy" HeaderText="Institution Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionNameWithProgOfStudy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="Select" ButtonType="Link" Text="Edit" />
                                    <asp:ButtonField CommandName="Students" ButtonType="Link" Text="Students" />
                                    <asp:ButtonField CommandName="Tests" ButtonType="Link" Text="Tests" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="GroupName|ASC" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestScheduleByDate.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
