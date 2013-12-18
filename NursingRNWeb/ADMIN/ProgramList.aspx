<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="Admin_ProgramList" Title="Kaplan Nursing"
    EnableViewState="true" Codebehind="ProgramList.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div11');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Program List</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Program
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="7" align="center">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left">
                                      <asp:Label ID="lblProgOfStudyTxt" runat="server" Text="Program of Study:" Width="120px" style="font-weight:bold"></asp:Label>
                                      <KTP:KTPDropDownList ID="ddlProgramOfStudy" Width="100px" runat="server" ShowNotSelected="false" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                                   </td>
                                    <td align="right" style="padding-left:25px;">
                                        <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="seabtn" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                            Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                            OnClick="seabtn_Click"></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="No items found" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridPrograms" runat="server" BackColor="White" AllowSorting="true"
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="True"
                                AutoGenerateColumns="False" DataKeyNames="ProgramID" PageSize="10"
                                OnRowCommand="gridPrograms_RowCommand"
                                CssClass="data1" Width="100%" OnPageIndexChanging="gridPrograms_PageIndexChanging"
                                OnSorting="gridPrograms_Sorting">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="ProgramId" HeaderText="Program ID" InsertVisible="False"
                                        ReadOnly="True" SortExpression="ProgramId" />
                                    <asp:BoundField DataField="ProgramName" HeaderText="Program Name" SortExpression="ProgramName" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:ButtonField CommandName="Select" Text="Edit">
                                        <ItemStyle CssClass="normal" />
                                    </asp:ButtonField>
                                    <asp:ButtonField CommandName="Tests" Text="Tests" />
                                    <asp:ButtonField CommandName="Copy" Text="Copy" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="ProgramName|ASC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
