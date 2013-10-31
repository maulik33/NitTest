<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_UserEdit" CodeBehind="UserEdit.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Src="Controls/Address.ascx" TagName="Address" TagPrefix="ucAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function openPopup() {
            var studentId = $("#studentIdField").val();
            var page = 'AuditTrailView.aspx?USERID=' + studentId;
            var name = 'audit_trail';
            var effects = 'resizable:yes,scroll:yes,dialogHeight:500px;dialogwidth:1000px,height:500px;width:1000px;status:no,toolbar:no,menubar=no,statusbar:no,titlebar:no';
            window.showModalDialog(page, name, effects);
        }

        function openStudentRepeat() {
            ClearRepeatValue();
            var oldInstitution = document.getElementById('<%=hdnInstitution.ClientID%>').value;
            var newInstitution = document.getElementById('<%=ddInstitution.ClientID%>').value;
            if (oldInstitution == newInstitution) {
                var page = 'StudentRepeat.aspx';
                var name = 'student_repeat';
                var effects = 'dialogHeight:350px;dialogwidth:350px,height:350px;width:350px;status:no,toolbar:no,titlebar=0,statusbar=0,menubar=no,resizable:yes,scroll:yes';
                var respond = window.showModalDialog(page, name, effects);
                if (respond.closed.toString() == "OK") {
                    document.getElementById('<%=hdnExpiryDateChange.ClientID%>').value = respond.expiryDate.toString();
                    document.getElementById('<%=lblRepeatValue.ClientID%>').innerHTML = respond.expiryDate.toString();
                   if (respond.expiryDate.toString() != "") {
                      document.getElementById('<%=lblRepeat.ClientID%>').style.display = "inline";
                        document.getElementById('<%=lblRepeatValue.ClientID%>').style.display = "inline";
                    } else {
                        document.getElementById('<%=lblRepeat.ClientID%>').style.display = "none";
                        document.getElementById('<%=lblRepeatValue.ClientID%>').style.display = "none";
                    }
                } 
              } 
            return true;
        }

        function ClearRepeatValue() {
            var oldCohort = document.getElementById('<%=hdnCohort.ClientID%>').value;
            var newCohort = document.getElementById('<%=ddCohort.ClientID%>').value;
            var expDate = document.getElementById('<%=hdnExpiryDate.ClientID%>').value;
            if ((oldCohort == newCohort) && (expDate != String.empty)) {
                document.getElementById('<%=lblRepeatValue.ClientID%>').innerHTML = expDate;
                document.getElementById('<%=hdnExpiryDateChange.ClientID%>').value = expDate;
                document.getElementById('<%=lblRepeat.ClientID%>').style.display = "inline";
                document.getElementById('<%=lblRepeatValue.ClientID%>').style.display = "inline";
            }
            else {
                document.getElementById('<%=lblRepeatValue.ClientID%>').innerHTML = String.empty;
                document.getElementById('<%=hdnExpiryDateChange.ClientID%>').value = String.empty;
                document.getElementById('<%=lblRepeat.ClientID%>').style.display = "none";
                document.getElementById('<%=lblRepeatValue.ClientID%>').style.display = "none";
            }

            return true;
        }
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2" class="headfont">
                <b>
                    <asp:Label ID="lblTitle" runat="server" Text="Edit > Student" Width="482px"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblSubTitle" runat="server" Text="Use this page to edit a Student"
                    Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 509px">
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below
                            <asp:HiddenField ID="hdnStartDate" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hdnEndDate" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px; height: 28px;">
                            User Name:
                        </td>
                        <td class="datatable" align="left" style="height: 28px">
                            <asp:TextBox ID="txtUserName" runat="server" Width="180px" MaxLength="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqUserName" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Password:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtPassword" runat="server" Width="180px" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            First Name:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="180px" MaxLength="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Last Name:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtLastName" runat="server" Width="180px" MaxLength="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Kaplan Profile Id:
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtKaplanID" runat="server" Width="176px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            EnrollmentID:
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="TextBox1" runat="server" Width="180px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Telephone:
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtTelephone" MaxLength="50" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="55%">
                            <asp:Label ID="lblProgOfStudyTxt" runat="server" Text="Program of Study:" Visible="false"
                                Width="200px"></asp:Label>
                        </td>
                        <td align="left" style="width: 240px;">
                            <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" NotSelectedText="Selection Required"
                                Visible="false" Width="158px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                            <asp:Label ID="lblProgOfStudy" runat="server" Text="" Width="50px" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramOfStudy"
                                ErrorMessage="*Required Field" ValidationGroup="Form1" Display="Dynamic" InitialValue="-1"
                                Width="111px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="height: 21px; width: 147px;">
                            Institution:
                        </td>
                        <td class="datatable" align="left" style="height: 21px">
                            <KTP:KTPDropDownList ID="ddInstitution" runat="server" ShowNotAssigned="true" ShowNotSelected="false"
                                ShowSelectAll="false" AutoPostBack="True" OnSelectedIndexChanged="ddInstitution_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                            <asp:Label ID="lblI" runat="server" Width="308px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Cohort:
                        </td>
                        <asp:HiddenField ID="studentIdField" ClientIDMode="Static" runat="server"></asp:HiddenField>
                        <td class="datatable" align="left">
                            <KTP:KTPDropDownList ID="ddCohort" runat="server" AutoPostBack="True" ShowNotAssigned="true"
                                ShowNotSelected="false" ShowSelectAll="false" OnSelectedIndexChanged="ddCohort_SelectedIndexChanged">
                            </KTP:KTPDropDownList>
                            <asp:HyperLink ID="audit_trail" Width="82px" onclick="javascript:openPopup();" NavigateUrl="#"
                                Visible="False" runat="server">Audit Trail</asp:HyperLink>
                            <asp:Label ID="lblRepeat" runat="server" Text="Repeat Student-" Style="display: none;"></asp:Label><asp:Label
                                ID="lblRepeatValue" runat="server" Text="enter time" Style="display: none;"></asp:Label>
                            <br />
                            <asp:Label ID="lblC" runat="server" Width="308px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Group:
                        </td>
                        <td align="left">
                            <KTP:KTPDropDownList ID="ddGroup" runat="server" ShowNotAssigned="true" ShowNotSelected="false"
                                ShowSelectAll="false">
                            </KTP:KTPDropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            E-mail:
                        </td>
                        <td class="datatable" align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="180px" MaxLength="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*Required Field" Font-Size="Small" ValidationGroup="Form1" Width="123px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Start Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserStartDate" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/cal_16.gif"
                                OnClick="ImageButton1_Click" />
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
                            <asp:Calendar ID="startDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Black" Height="180px" OnSelectionChanged="startDateCalendar_SelectionChanged"
                                Visible="False" Width="200px">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            </asp:Calendar>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            End Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserExpireDate" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="btnEndDateCalendar" runat="server" ImageUrl="../images/cal_16.gif"
                                OnClick="btnEndDateCalendar_Click" />&nbsp;
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
                            <asp:Calendar ID="EndDateCalendar" runat="server" BackColor="White" BorderColor="#999999"
                                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="Black" Height="180px" OnSelectionChanged="EndDateCalendar_SelectionChanged"
                                Visible="False" Width="200px">
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            </asp:Calendar>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            ADA:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rbtADA" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <ucAddress:Address ID="ucAddress" runat="server" />
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter Emergency Contact Person Details
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Name:
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtEmergencyContactName" runat="server" Width="180px" MaxLength="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 147px">
                            Phone:
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtEmergencyPhone" MaxLength="50" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2">
                            <a href="admin_student_add_detail.aspx"></a>&nbsp;
                            <asp:ImageButton ID="btSave" runat="server" OnClick="btSave_Click" ImageUrl="~/Temp/images/btn_save.gif"
                                ValidationGroup="Form1" />&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="btDelete" runat="server" OnClick="btDelete_Click" AlternateText="Delete Student"
                                ImageUrl="~/Temp/images/btn_del.gif" onMouseOver="roll(this)" onMouseOut="roll(this)" />
                            <br />
                            <asp:Label ID="lblM" runat="server" ForeColor="Red" Visible="False" Text="This User Name exists"
                                Width="208px"></asp:Label><br />
                            <br />
                            <asp:TextBox ID="txtIntegrated" runat="server" Width="180px" Visible="False"></asp:TextBox>
                            <asp:HiddenField ID="hdnCohort" runat="server" />
                            <asp:HiddenField ID="hdnInstitution" runat="server" />
                            <asp:HiddenField ID="hdnUser" runat="server" />
                            <asp:HiddenField ID="hdnExpiryDate" runat="server" />
                            <asp:HiddenField ID="hdnExpiryDateChange" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
