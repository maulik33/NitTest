<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="admin_AdminList" Title="Kaplan Nursing" CodeBehind="AdminList.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div10');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Administrator List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit an Administrator
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="8" align="left">
                            <table border="0" cellpadding="0" cellspacing="0">
                               <tr id="trProgramofStudy" runat="server">
                                 <td align="left" style="height: 33px">
                                        <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                                    </td>
                                    <td style="height: 33px" colspan="2">
                                       <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                                      <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramofStudy"
                                                InitialValue="-1" ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
                                     </td>
                                </tr>
                                    <tr>
                                        <td align="left" style="height: 33px">
                                            <asp:TextBox ID="txtSearch" runat="server">
                                            </asp:TextBox>&nbsp;
                                        </td>
                                        <td style="height: 33px">
                                            <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                                OnClick="searchButton_Click" ValidationGroup="validateSearch" />
                                        </td>
                                        <td align="left">
                                            <asp:ImageButton ID="btnPrintPDF" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                                OnClick="btnPrintPDF_Click" ValidationGroup="validateSearch"/>&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnPrintExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                                Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                                OnClick="btnPrintExcel_Click"  ValidationGroup="validateSearch"/>&nbsp;&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnPrintExcelDataOnly" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                                Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                                OnClick="btnPrintExcelDataOnly_Click" ValidationGroup="validateSearch" />
                                        </td>
                                    </tr>
                            </table>
                            <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridAdmins" runat="server" AllowSorting="True" BackColor="White"
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="5" AllowPaging="True"
                                AutoGenerateColumns="False" DataKeyNames="UserId" PageSize="10" OnPageIndexChanged="gridAdmins_PageIndexChanged"
                                OnRowCommand="gridAdmins_RowCommand" CssClass="data1" Width="100%" OnPageIndexChanging="gridAdmins_PageIndexChanging"
                                OnSorting="gridAdmins_Sorting">
                                <rowstyle cssclass="datatable2a" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1a" />
                                <columns>
                                    <asp:BoundField DataField="UserId" HeaderText="User Id" InsertVisible="False" ReadOnly="True"
                                        SortExpression="UserId" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                    <asp:TemplateField SortExpression="Institution.InstitutionName" HeaderText="Institution Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionNameWithProgOfStudy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="Select" ButtonType="Link" Text="Edit" />
                                </columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="UserName|ASC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestScheduleByDate.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
