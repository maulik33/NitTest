<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ReportInstitutionByState.aspx.cs" Inherits="ADMIN_ReportInstitutionByStateAndType" %>

<%@ Register Assembly="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div21');
        });
    </script>
    <script type="text/javascript">
        function Validate() {
            var ddState = document.getElementById('<%=ddState.ClientID%>');
            var ddType = document.getElementById('<%=ddType.ClientID %>');
            if (ddState.options[ddState.selectedIndex].value == '-1') {
                alert('Select State');
                return false;
            }
            if (ddType.options[ddType.selectedIndex].value == 'Not Selected') {
                alert('Select Type');
                return false;
            }
            return true;
        }
    </script>
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Reports for Administrators > Institutions by State</b>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable_rep">
                    <tr class="datatable2">
                        <td align="left">
                            <table width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="reportParamLabel">
                                        State
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddState" runat="server" AutoPostBack="True" ShowNotSelected="true">
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="reportParamLabel">
                                        Type
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddType" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Not Selected</asp:ListItem>
                                            <asp:ListItem>Select All</asp:ListItem>
                                            <asp:ListItem>OI</asp:ListItem>
                                            <asp:ListItem>OD</asp:ListItem>
                                            <asp:ListItem>OG</asp:ListItem>
                                        </KTP:KTPDropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Temp/images/btn_submit.gif"
                                            OnClick="btnSearch_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnPrinterFriendly" runat="server" ImageUrl="~/Images/btn_pfv.gif"
                                            OnClick="btnPrinterFriendly_Click" OnClientClick="return Validate();" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/Images/btn_toexcel.gif"
                                            Style="margin-top: 3px;" OnClick="btnExcel_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="btnExcelData" runat="server" ImageUrl="~/Images/btn_toexceldata.gif"
                                            Style="margin-top: 3px;" OnClick="btnExcelData_Click" OnClientClick="return Validate();"
                                            ToolTip="Please adjust your Page Setup options to print in Landscape format before sending this file to your printer." />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblMessage" runat="server" Text="There is not enough data to compile this report" ViewStateMode="Disabled"
                                Visible="False" Width="347px"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvInstitutions" Width="100%" runat="server" AllowSorting="True"
                                OnSorting="gvInstitutions_Sorting" AutoGenerateColumns="false">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Institution ID" SortExpression="InstitutionId">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblInstitutionID" Text='<%#DataBinder.Eval(Container.DataItem,"InstitutionId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Institution Name" SortExpression="InstitutionName">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblInstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"InstitutionName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDescription" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Center" SortExpression="CenterId">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCenterId" Text='<%#DataBinder.Eval(Container.DataItem,"CenterId")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone Number" SortExpression="ContactPhone">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblContactPhone" Text='<%#DataBinder.Eval(Container.DataItem,"ContactPhone")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time Zone" SortExpression="TimeZone">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTimeZone" Text='<%#DataBinder.Eval(Container.DataItem,"TimeZone")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date" SortExpression="CreateDate">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCreateDate" Text='<%#DataBinder.Eval(Container.DataItem,"CreateDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="InstitutionName|ASC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
