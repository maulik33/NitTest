<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_InstitutionEdit" CodeBehind="InstitutionEdit.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="Saravana" TagName="Calendar" Src="Calender.ascx" %>
<%@ Register Src="~/ADMIN/Controls/Address.ascx" TagName="Address" TagPrefix="ucAddress" %>
<%@ Register Src="Controls/InstitutionContacts.ascx" TagName="InstitutionContacts"
    TagPrefix="uc1" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var InValidInstitution = false;
            ExpandContextMenu(1, 'ctl00_Div2');
           
            var ErrorMessage = '<%= this.ErrorMessage %>'
            if (ErrorMessage!='') {
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
            min-width: 930px;
            min-height: 100px;
            background: #f0f0fe;
            border-width: 3px;
        }
    </style>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblTitle" runat="server" Width="355px">Edit > Institution</asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit an Institution"
                    Width="349px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 566px">
                <table align="left" border="0" class="formtable">
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="btnManageContacts" runat="server" Text="Manage Contacts" OnClick="btnManageContacts_Click" />
                        </td>
                    </tr>
                    <tr class="datatablelabels">
                        <td align="left" colspan="2" style="height: 27px">
                            Enter details in fields below
                        </td>
                    </tr>
                    
                      <tr class="datatable2">
                        <td align="left" width="55%">
                            <asp:Label ID="lblProgOfStudyTxt" runat="server" Text="Program of Study:" Width="200px"></asp:Label>
                        </td>
                         <td align="left" style="width: 240px;">
                             <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server"  AutoPostBack="true" ShowNotSelected="false"  OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged" />
                         </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="25%">
                            Institution Name:
                        </td>
                        <td align="left" style="width: 431px">
                            &nbsp;<asp:TextBox ID="txtName" runat="server" Width="275px" MaxLength="80"></asp:TextBox>
                            <asp:Label ID="lblProgOfStudy" runat="server" Text="" Width="50px" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rf_name" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*Required Field" ValidationGroup="Form1" Width="111px">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Institution Description:
                        </td>
                        <td align="left" class="datatable" style="width: 431px">
                            &nbsp;<asp:TextBox ID="txtDescription" runat="server" Width="275px" MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Center Name:
                        </td>
                        <td align="left" style="width: 431px">
                            &nbsp;<asp:TextBox ID="txtCenterName" runat="server" Width="275px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 32px">
                            FacilityID:
                        </td>
                        <td align="left" style="height: 32px; width: 431px;">
                            &nbsp;<asp:TextBox ID="txtFacility" runat="server" Width="275px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFacility"
                                ErrorMessage="*Required Field" ValidationGroup="Form1">
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="Label7" runat="server" ForeColor="Red" Text=" Must be a number" Width="200px">
                            </asp:Label>
                            <br />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFacility"
                                ErrorMessage="ID should be a number between 0 and 2147483647" MaximumValue="2147483647"
                                Type="Integer" MinimumValue="0" ValidationGroup="Form1"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 32px">
                            Contact Name:
                        </td>
                        <td align="left" style="height: 32px; width: 431px;">
                            &nbsp;<asp:TextBox ID="txtContactName" runat="server" Width="275px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Contact Phone Number:
                        </td>
                        <td align="left" style="width: 431px">
                            &nbsp;<asp:TextBox ID="txtPhone" runat="server" Width="275px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Contact Email:
                        </td>
                        <td align="left" style="width: 431px">
                            &nbsp;<asp:TextBox ID="txtEmail" runat="server" MaxLength="80" Width="275px">
                            </asp:TextBox>
                            <asp:RegularExpressionValidator ID="regexEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Wrong Email Format" Font-Size="Small" ValidationExpression="\w+([!@#$%^&*()-+.']\w+)*@\w+([!@#$%^&*()-+.']\w+)*\.\w+([!@#$%^&*()-+.']\w+)*"
                                Width="150px" Display="Dynamic" ValidationGroup="Form1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 30px">
                            Default Program:
                        </td>
                        <td align="left" style="height: 30px; width: 431px;">
                            &nbsp;<asp:DropDownList ID="ddProgram" runat="server" Width="280px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 30px">
                            Time Zone:
                        </td>
                        <td align="left" style="height: 30px; width: 431px;">
                            &nbsp;<asp:DropDownList ID="ddTimeZone" runat="server" Width="280px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Allowed IP Address:
                        </td>
                        <td align="left" style="width: 431px" valign="top">
                            &nbsp;
                            <table style="width: 354px">
                                <tr>
                                    <td rowspan="6">
                                        <asp:TextBox ID="txtIP" runat="server" Width="188px" Height="108px" TextMode="MultiLine">
                                        </asp:TextBox>
                                    </td>
                                    <td colspan="2" nowrap="nowrap">
                                        <asp:Label ID="Label1" runat="server" Text="You can enter IP addresses of three types, one per line:">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" nowrap="nowrap">
                                        <asp:Label ID="Label2" runat="server" Text="1) List each individually (eg. 192.141.1.1)">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" nowrap="nowrap">
                                        <asp:Label ID="Label3" runat="server" Text="2) List an IP address range (eg. 192.141.1.1-10)">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:Label ID="Label4" runat="server" Text="- Allows 192.141.1.1 to 192.141.1.10"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" nowrap="nowrap">
                                        <asp:Label ID="Label5" runat="server" Text="3) List a full IP address group (eg. 192.141.1.*)">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:Label ID="Label6" runat="server" Text="- Allows 192.141.1.1 to 192.141.1.255"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIP"
                                ErrorMessage="Wrong IP format" ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                                ValidationGroup="Form1" Enabled="False">
                            </asp:RegularExpressionValidator>
                            <asp:Label ID="lblErr" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Student Pay Link:
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rbStudentPayLink" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Institution Status:
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdInstitutionStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left">
                            Contractual Start Date:
                        </td>
                        <td align="left">
                            &nbsp;<asp:TextBox ID="txtContractualStartDate" runat="server"></asp:TextBox>
                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/cal_16.gif"
                                OnClick="ImageButton1_Click" CausesValidation="false" />--%>
                            <asp:Image ID="ContractualStartDate" runat="server" ImageUrl="~/Images/show_calendar.gif" />
                            &nbsp;
                            <%-- <asp:Calendar ID="ContractualStartDateCalendar" runat="server" BackColor="White"
                                BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana"
                                Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="startDateCalendar_SelectionChanged"
                                Visible="False" Width="200px">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            </asp:Calendar>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Wrong date format."
                                ControlToValidate="txtContractualStartDate" Display="Dynamic" SetFocusOnError="True"
                                ValidationGroup="Form1" ValidationExpression="^(?=\d)(?:(?:(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2}))($|\ (?=\d)))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ [AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblErrContractualdate" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" valign="top">
                            Annotation:
                        </td>
                        <td align="left">
                            &nbsp;<asp:TextBox ID="txtAnnotation" runat="server" TextMode="MultiLine" MaxLength="1000"
                                Rows="6" Width="397px">
                            </asp:TextBox>
                            <br />
                            <asp:RegularExpressionValidator ID="regextxtAnnotation" ControlToValidate="txtAnnotation"
                                ValidationGroup="Form1" runat="server" ValidationExpression="^[\s\S]{0,1000}$"
                                Text="You have entered more than 1000 characters!" />
                        </td>
                    </tr>
                    <ucAddress:Address ID="ucAddress" runat="server" />
                    <tr class="datatable2">
                       <div id="divSecurity" runat="server" Visible="False">
                        <td align="left" valign="top">
                             <asp:Label ID="lblSecurity" runat="server" Text="Security:"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:checkbox ID="chkSecurity" runat="server"></asp:checkbox>
                            <asp:Label ID="lblSecurityTxt" runat="server" Text="IT Security"></asp:Label>
                        </td>
                       </div>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Temp/images/btn_save.gif"
                                Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                OnClick="btnSave_Click" ValidationGroup="Form1" CausesValidation="true"></asp:ImageButton>
                            &nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Temp/images/btn_del.gif"
                                OnClick="btnDelete_Click" AlternateText="Delete Institution" onMouseOver="roll(this)"
                                onMouseOut="roll(this)"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnManageContacts"
        PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
    <ajaxToolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="black" Radius="6" Corners="All" />
    <asp:Panel ID="pnlPopup" runat="server" Style="display: none;">
        <div style="overflow: auto; height: 600px; overflow-x: hidden;">
            <asp:Panel ID="pnlInner" runat="server" CssClass="modalWindow">
                <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="padding-left: 5px; padding-top: 5px;">
                            <uc1:InstitutionContacts ID="ucInstitutionContact" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <br />
                    <asp:Button ID="btnClose" runat="server" Text="Close" />
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>
</asp:Content>
