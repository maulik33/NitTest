<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="Admin_InstitutionList" Title="Kaplan Nursing" CodeBehind="InstitutionList.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div9');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Institution List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit an Institution
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="7">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Institution:
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="statusRadioButton" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="statusRadioButton_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style=" width:10px;"></td>
                                    <td>
                                        Program of Study:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged"
                                            ShowNotSelected="false">
                                        </KTP:KTPDropDownList>
                                    </td>
                                     <td style=" width:30px;"></td>
                                    <td>
                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                            border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)" OnClick="searchButton_Click">
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrintPDF_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcel_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer."
                                            OnClick="btnPrintExcelDataOnly_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gridInstitutions" runat="server" AllowSorting="True" BackColor="White"
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="5" AllowPaging="True"
                    AutoGenerateColumns="False" DataKeyNames="InstitutionID" OnPageIndexChanged="gridInstitutions_PageIndexChanged"
                    OnRowCommand="gridInstitutions_RowCommand" CssClass="data1" Width="100%" OnPageIndexChanging="gridInstitutions_PageIndexChanging"
                    OnSorting="gridInstitutions_Sorting">
                    <RowStyle CssClass="datatable2a" />
                    <HeaderStyle CssClass="datatablelabels" />
                    <AlternatingRowStyle CssClass="datatable1a" />
                    <Columns>
                        <asp:BoundField DataField="InstitutionID" HeaderText="Institution ID" InsertVisible="False"
                            ReadOnly="True" SortExpression="InstitutionId" />
                        <asp:BoundField DataField="InstitutionNameWithProgOfStudy" HeaderText="Name" SortExpression="InstitutionName">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="CenterId" HeaderText="Center" SortExpression="CenterId" />
                        <asp:BoundField DataField="ContactPhone" HeaderText="Phone Number" SortExpression="ContactPhone" />
                        <asp:TemplateField SortExpression="TimeZones.Description" HeaderText="Time Zone">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="Description" Text='<%#DataBinder.Eval(Container.DataItem,"TimeZones.Description")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="Select" Text="Edit">
                            <ItemStyle CssClass="normal" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField runat="server" ID="hdnGridConfig" Value="InstitutionName|ASC" />
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        Height="50px" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ADMIN\Report\TestScheduleByDate.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>
