<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="BulkModifyProgram.aspx.cs" Inherits="NursingRNWeb.ADMIN.BulkModifyProgram" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.assigned > input').addClass("assignedchkclass");
            $('.Modified > input').attr("disabled", "disabled"); 
            $(".assignedchkclass").click(function () {
                var tr = $(this).closest("tr");
                var modifiedCheckBox = $(tr).find('.Modified > input');
                $(modifiedCheckBox).attr('checked', !$(modifiedCheckBox).attr('checked'));
            });
        });

        function Validate() {
            var ddProgramOfStudy = document.getElementById('<%=ddlProgramOfStudy.ClientID%>');
            var ddProduct = document.getElementById('<%=ddProducts.ClientID%>');
            var ddTest = document.getElementById('<%=ddlTest.ClientID %>');
            if (ddProgramOfStudy.options[ddProgramOfStudy.selectedIndex].value == "-1" || ddProduct.options[ddProduct.selectedIndex].value == "-1" || ddTest.options[ddTest.selectedIndex].value == "-1") {
                alert('Must select a Program Of Study, Test Category and Test Name.');
                return false;
            }

            return true;
        }
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table align="left" border="0" width="100%">
                    <tr>
                        <td colspan="4" align="left">
                            Use the dropdown menu to select a test categories and test name to modify(add/remove)
                            the program(s). You may also filter by using the Filter Program List and clicking
                            the "Search" button.<br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <br />
                    <table align="left" border="0" class="datatable" cellpadding="3" width="100%">
                        <tr class="datatable1">
                            <td colspan="5" align="left">
                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr class="datatablelabels">
                                        <td align="left" class="datatable" style="height: 20px; font-weight:bold;">
                                            <asp:Label ID="lblTitle" runat="server" Text="Modify Program(s)" Width="241px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" style="width: 119px">
	                            <asp:Label ID="lblProgramOfStudy" runat="server" Text="Program of Study: "></asp:Label>
                            </td>
                            <td colspan="4" align="left" style="width: 231px">
                                <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" AutoPostBack="True" NotSelectedText="Selection Required" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" style="width: 119px">
                                <asp:Label ID="lblTestCategory" runat="server" Text="Test Category: " Width="189px"></asp:Label>
                            </td>
                            <td colspan="2" align="left" style="width: 240px">
                                <KTP:KTPDropDownList ID="ddProducts" runat="server" AutoPostBack="True" ShowNotSelected="false"
                                    ShowNotAssigned="false" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                </KTP:KTPDropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblFilterProgram" runat="server" Text="Filter Program List: " Width="189px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProgramListFilter" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" style="width: 119px">
                                <asp:Label ID="lblTestName" runat="server" Text="Test Name: "></asp:Label>
                            </td>
                            <td colspan="4" align="left" style="width: 231px">
                                <KTP:KTPDropDownList ID="ddlTest" runat="server" SelectionMode="Multiple" Width="250px"
                                    ShowNotSelected="false" ShowSelectAll="false" AutoPostBack="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                </KTP:KTPDropDownList>
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td colspan="5" align="center">
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                    OnClick="btnSearch_Click" OnClientClick="return Validate();"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblM" runat="server" Width="384px" ForeColor="#C00000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" class="datatable" style="border: 0px;">
                    <tr>
                        <td>
                            <asp:GridView ID="gridPrograms" DataKeyNames="IsTestAssignedToProgram" runat="server"
                                BackColor="White" AllowSorting="true" BorderColor="#CC9966" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                 CssClass="data1" Width="100%" OnSorting="gridPrograms_Sorting"
                                OnRowDataBound="gridPrograms_RowDataBound">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="ProgramId" HeaderText="Program ID" ReadOnly="True" SortExpression="ProgramId" />
                                    <asp:BoundField DataField="ProgramName" HeaderText="Program Name" SortExpression="ProgramName" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="ProgramOfStudyName" HeaderText="Program of Study" SortExpression="ProgramOfStudyName" />
                                    <asp:TemplateField HeaderText="Assignment">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAssigned" CssClass="assigned" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modified(During current session)" HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkModified" CssClass="Modified" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="ProgramId|DESC" />
                            <asp:HiddenField runat="server" ID="hdnAssignedProgramIds" />
                            <asp:HiddenField runat="server" ID="hdnModifiedProgramIds" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Visible="false" OnClick="btnSave_Click" Text="Save" />
                            <asp:Button ID="btnCancel" runat="server" Visible="false" OnClick="btnCancel_Click"
                                Text="Cancel" /><br />
                            <asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
