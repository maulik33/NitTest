<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_CohortEdit" EnableViewState="true" CodeBehind="CohortEdit.aspx.cs" %>

<%@ Register TagPrefix="Saravana" TagName="Calendar" Src="Calender.ascx" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var InValidCohort = false;
        $(document).ready(function () {
            ExpandContextMenu(1, 'ctl00_Div5');

            if (InValidCohort != true) {
                InValidCohort = '<%= this.IsInValidCohort %>' == 'True';
            }
            else {
                InValidCohort = false;
            }
            var ErrorMessage = '<%= this.ErrorMessage %>'
            if (InValidCohort) {
                alert(ErrorMessage);
            }
        });
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
            min-height: 150px;
            background: #f0f0fe;
            border-width: 3px;
        }
    </style>
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="PopulateAnnotation"
        PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="black" Radius="6" Corners="All" />
    <asp:Panel ID="pnlPopup" runat="server" Style="display: none">
        <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow">
            <div style="height: 100px; padding-top: 10px; padding-left: 10px; padding-right: 10px;">
                <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label runat="server" Text="Annotation Not Available" ID="lblAnnotation"></asp:Label>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="vertical-align: bottom;">
                <asp:Button ID="btnClose" runat="server" Text="Close" />
                <br />
            </div>
        </asp:Panel>
    </asp:Panel>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" class="headfont">
                            <b>
                                <asp:Label ID="lblTitle" runat="server" Text="Edit > Cohort" Width="615px"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" style="height: 38px">
                            <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit a Cohort"
                                Width="617px" Height="40px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="left" border="0" class="formtable">
                                <tr class="datatablelabels">
                                    <td align="left" colspan="2">
                                        Enter details in fields below
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px">
                                        Cohort Name:
                                    </td>
                                    <td class="datatable" align="left">
                                        <!--&nbsp;&nbsp;<input type="button" name="find" value="Find" onclick="submit_form('find');">-->
                                        &nbsp;<asp:TextBox ID="txtCohortName" runat="server" Width="180px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCohortName"
                                            ErrorMessage="*Required Field" ValidationGroup="validsave"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px; height: 32px;">
                                        Class Code:
                                    </td>
                                    <td align="left" class="datatable" style="height: 32px">
                                        &nbsp;<asp:TextBox ID="txtDescription" runat="server" Width="180px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="datatable2" runat="server" id="trProgramodStudy">
                                    <td class="normtext">
                                        Program of Study:
                                    </td>
                                    <td align="left">
                                        &nbsp;<KTP:KTPDropDownList ID="ddProgramofStudy" runat="server" AutoPostBack="true" 
                                             onselectedindexchanged="ddProgramofStudy_SelectedIndexChanged" ShowNotSelected="false"
                                            >
                                        </KTP:KTPDropDownList>
                                        <asp:Label ID="lblProgramofStudyVal" runat="server" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px">
                                        Institution:
                                    </td>
                                    <td align="left" class="datatable">
                                        &nbsp;<KTP:KTPDropDownList ID="ddInstitution" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                                        </KTP:KTPDropDownList>
                                        &nbsp;&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddInstitution"
                                            ErrorMessage="*Required Field" ValidationGroup="validsave" Display="Dynamic"
                                            InitialValue="-1"></asp:RequiredFieldValidator>
                                        <asp:LinkButton runat="server" Visible="false" ID="PopulateAnnotation" OnClick="PopulateAnnotation_Click">View Annotation</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px; height: 24px">
                                        Cohort Start Date:
                                    </td>
                                    <td align="left" class="datatable" style="height: 24px">
                                        &nbsp;<asp:TextBox ID="txtSD" runat="server" Width="145px"></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Temp/images/show_calendar.gif">
                                        </asp:Image>
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px">
                                        Cohort End Date:
                                    </td>
                                    <td align="left" class="datatable">
                                        &nbsp;<asp:TextBox ID="txtED" runat="server" Width="147px"></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Temp/images/show_calendar.gif">
                                        </asp:Image>
                                    </td>
                                </tr>
                                <tr class="datatable2">
                                    <td align="left" style="width: 143px">
                                        Cohort Status:
                                    </td>
                                    <td align="left" class="datatable">
                                        &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="formtableApply">
                                    <td align="center" colspan="2">
                                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/temp/images/btn_save.gif"
                                            Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                            OnClick="btnSave_Click" ValidationGroup="validsave"></asp:ImageButton>
                                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                            AlternateText="Delete Cohort" ImageUrl="~/temp/images/btn_del.gif" onMouseOver="roll(this)"
                                            onMouseOut="roll(this)" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblM" runat="server" Width="130px" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
