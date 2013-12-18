<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="admin_CohortList" Title="Kaplan Nursing" CodeBehind="CohortList.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JS/jquery.tooltip.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div12');
            InitializeToolTip();
            function pageLoad(sender, args) {
                if (args.get_isPartialLoad()) {
                    InitializeToolTip();
                }
            }
            function InitializeToolTip() {
                $(".gridViewToolTip").tooltip({
                    track: true,
                    delay: 0,
                    showURL: false,
                    fade: 100,
                    bodyHandler: function () {
                        return $($(this).next().html());
                    },
                    showURL: false
                });
            }
        });
    </script>
    
   <style type="text/css">
    #tooltip {
	position: absolute;
	z-index: 30000;
	border: 1px solid #111;
	background-color: #eee;
	padding: 5px;
	opacity: 0.85;
    }
    #tooltip h3, #tooltip div { margin: 0; }
    </style>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Cohort List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Cohort.<br />
                Search for dates will display cohorts with a test date active any time during the
                range.
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td>
                            <table border="0">
                             <tr runat="server" id="trProgramodStudy" visible="false">
                                    <td>
                                        Program of Study:
                                    </td>
                                    <td align="left">
                                        <KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" 
                                            ShowNotSelected="false" onselectedindexchanged="ddProgramofStudy_SelectedIndexChanged" 
                                            >
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Institution:
                                    </td>
                                    <td>
                                        <KTP:KTPListBox ID="lbInstitution" runat="server" SelectionMode="Multiple" AutoPostBack="True"
                                            ShowNotSelected="false" ShowSelectAll="true" OnSelectedIndexChanged="lbInstitution_SelectedIndexChanged">
                                        </KTP:KTPListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 131px">
                                        Test Type:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowNotSelected="True"
                                            OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 131px">
                                        Test Name:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddTests" runat="server" AutoPostBack="True" ShowNotSelected="True"
                                            OnSelectedIndexChanged="ddTests_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0">
                                <tr>
                                    <td>
                                        Test Date From:<asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Date Format"
                                            Operator="DataTypeCheck" ControlToValidate="txtDateFrom" Type="Date" Display="Dynamic">
                                        </asp:CompareValidator>
                                    </td>
                                    <td>
                                        Date To:
                                        <asp:TextBox ID="txtDateTo" runat="server">
                                        </asp:TextBox>
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Invalid Date Format"
                                            Operator="DataTypeCheck" ControlToValidate="txtDateTo" Type="Date" Display="Dynamic">
                                        </asp:CompareValidator>
                                    </td>
                                    <td style="width: 6px">
                                        <asp:ImageButton ID="searchByDatesButton" runat="server" ImageUrl="~/Images/btn_search.gif"
                                            OnClick="searchByDatesButton_Click" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblDateError" EnableViewState="false" runat="server" ForeColor="Red"></asp:Label>
                            <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="To date should be greater than or equal to From date"
                                Operator="GreaterThanEqual" ControlToValidate="txtDateTo" ControlToCompare="txtDateFrom"
                                Type="Date" Display="Dynamic">
                            </asp:CompareValidator>--%>
                        </td>
                    </tr>
                    <tr class="datatable1">
                        <td align="right">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="10%" align="left">
                                        Cohort:
                                    </td>
                                    <td width="30%" align="left">
                                        <asp:RadioButtonList ID="statusRadioButton" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="statusRadioButton_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" width="50%">
                                        Cohort Name:<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="10%">
                                        <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                            OnClick="searchButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="datatable1">
                        <td align="center">
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
                    <tr>
                        <td>
                            <asp:GridView ID="gridCohorts" runat="server" AllowSorting="True" BackColor="White"
                                CellPadding="5" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="CohortId"
                                OnPageIndexChanged="gridCohorts_PageIndexChanged" OnRowCommand="gridCohorts_RowCommand"
                                CssClass="data1" Width="100%" OnRowDataBound="gridCohorts_RowDataBound" OnPageIndexChanging="gridCohorts_PageIndexChanging"
                                OnSorting="gridCohorts_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="CohortId" HeaderText="Cohort Id" InsertVisible="False"
                                        ReadOnly="True" SortExpression="CohortId" />
                                    <asp:BoundField DataField="CohortName" HeaderText="Cohort Name" SortExpression="CohortName" />
                                    <asp:TemplateField SortExpression="Institution.InstitutionNameWithProgOfStudy" HeaderText="Institution Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionNameWithProgOfStudy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CohortStartDate" HeaderText="Cohort Start Date" SortExpression="CohortStartDate" />
                                    <asp:BoundField DataField="CohortEndDate" HeaderText="Cohort End Date" SortExpression="CohortEndDate" />
                                    <asp:BoundField DataField="CohortDescription" HeaderText="Cohort Code" SortExpression="CohortDescription" />
                                    <asp:BoundField DataField="StudentCount" HeaderText="N=" />
                                    <asp:BoundField DataField="RepeatingStudentCount" HeaderText="R=" />
                                    <asp:ButtonField CommandName="Select" Text="Edit" />
                                    <asp:ButtonField CommandName="Students" Text="Students" />
                                    <asp:ButtonField CommandName="Program" Text="Program" />
                                    <asp:ButtonField CommandName="Tests" Text="Tests" />
                                    <asp:TemplateField HeaderText="Annotation" HeaderStyle-Font-Bold="false">
                                        <ItemTemplate>
                                            <div class="tag">
                                                <asp:Label runat="server" ID="lblAnnotation" Visible="false" Text="Annotation" class="gridViewToolTip"
                                                    href="#"></asp:Label>
                                                <div id="tooltip" style="display: none;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <%#DataBinder.Eval(Container.DataItem,"Institution.Annotation")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="CohortName|ASC" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>
                <asp:HiddenField runat="server" ID="hfAddPermission" Value="false" />
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
