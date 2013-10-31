<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="StudentListForTest.aspx.cs" EnableViewState="true" Title="Kaplan Nursing"
    Inherits="NursingRNWeb.ADMIN.StudentListForTest" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(2, 'ctl00_Div14');
        });
    </script>
    <%--    to be moved to a common jscript file--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[type=checkbox][id*=chkSelected]").click(disableButton);
            disableButton();
        });
        function disableButton() {
            if ($("input[type=checkbox][id*=chkSelected]:checked").length > 0) {
                $("#btnAssignTest input").attr("disabled", false);
                $("#btnContainer").fadeTo("fast", 1);
            }
            else {
                $("#btnAssignTest input").attr("disabled", true);
                $("#btnContainer").fadeTo("fast", 0.0);
            }
        }
    </script>
    <style type="text/css">
        .modalBackground
        {
            background-color: #666699;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .modalWindow
        {
            min-width: 500px;
            min-height: 200px;
            background: #f0f0fe;
            border-width: 3px;
        }
    </style>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Create Adhoc Group</b>
            </td>
        </tr>
        <tr>
            <td colspan="2"  align="left">
                Use this page to create adhoc group&nbsp;  
                <br /> <asp:Label runat="server" ID="lblError" Text="Institution and Cohort are Mandatory !" Visible="False" ForeColor="Red"></asp:Label>
            </td>
          
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable2">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Select Institution:
                                    </td>
                                    <td colspan="2">
                                        <KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>                                        
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Select Cohort:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddCohort_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>                                      
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Select Group:
                                    </td>
                                    <td>
                                        <KTP:KTPDropDownList ID="ddGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddGroup_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                   
                                    <td colspan="2" align="right">
                                         Keyword : <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="searchButton" runat="server" alt="" border="0" Height="25" ImageUrl="~/Temp/images/btn_search.gif"
                                            onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="searchButton_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblNumber" runat="server" Font-Bold="True"></asp:Label>
                            <asp:GridView ID="gridStudents" runat="server" BackColor="White" CellPadding="5"
                                AutoGenerateColumns="False" DataKeyNames="UserId" Width="100%">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:BoundField DataField="UserId" HeaderText="User Id" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                    <asp:TemplateField SortExpression="Institution.InstitutionName" HeaderText="Institution Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="InstitutionName" Text='<%#DataBinder.Eval(Container.DataItem,"Institution.InstitutionName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cohort">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="cohortName" Text='<%#DataBinder.Eval(Container.DataItem,"Cohort.CohortName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="GroupName" Text='<%#DataBinder.Eval(Container.DataItem,"Group.GroupName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelected" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <span id="btnContainer">
                                <asp:Button ID="btnAssignADA" runat="server" Text="Assign ADA" />
                                <asp:Button ID="btnAssignTest" Text="Assign Test" runat="server" OnClick="btnAssignTest_Click" Visible="false"></asp:Button>
                            </span>
                        </td>
                    </tr>
                </table>
                </td>
                </tr>
    </table>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnAssignADA"
        PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnCancelScript="clearModalCtrls();" />
    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="black" Radius="6" Corners="All" />
    <asp:Panel ID="pnlPopup" runat="server" Style="display: none">
        <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow">
            <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="padding: 20px 40px 40px 180px;">
                        <table>
                            <tr>
                                <td>
                                    <b>ADA :</b>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtADA" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <asp:Button ID="btnSaveADA" runat="server" Text="Save" OnClick="btnSaveADA_Click" />
                <asp:Button ID="btnClose" runat="server" Text="Close" />
            </div>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
