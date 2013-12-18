<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_GroupTestDates" CodeBehind="GroupTestDates.aspx.cs" %>

<%@ Register TagPrefix="Saravana" TagName="Calendar" Src="Calender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table align="left" border="0" width="100%">
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblTestAssignMsg" runat="server" width="100%" forecolor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="headfont">
                            <b>View Group > Test Dates</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            Edit the Test Start and End Dates in the fields below, then click Save. If no Start
                            Date is given, the test will appear as Unavailable.<br />
                            <br />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="120" nowrap class="datatable">
                            <b>Institution:</b>
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;<asp:Label ID="lblIN" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                        <td align="center" width="150" class="datatable">
                            <strong>Program Name:</strong>
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;<asp:Label ID="lblPN" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="120" nowrap class="datatable" style="height: 21px">
                            <strong>Cohort Name:</strong>
                        </td>
                        <td align="left" class="datatable" style="height: 21px">
                            &nbsp;<asp:Label ID="lblCName" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                        <td align="center" width="150" class="datatable" style="height: 21px">
                            <b>Description:</b>
                        </td>
                        <td align="left" class="datatable" style="height: 21px">
                            &nbsp;<asp:Label ID="lblDescription" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="120" nowrap class="datatable">
                            <b>Group Name:</b>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblGroup" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                        <td align="center" width="150" class="datatable">
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="120" nowrap class="datatable">
                            <b>Cohort Start Date:</b>
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;<asp:Label ID="lblCSD" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                        <td align="center" width="150" class="datatable">
                            <b>Cohort End Date:</b>
                        </td>
                        <td align="left" class="datatable">
                            &nbsp;<asp:Label ID="lblCED" runat="server" Text="" Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 23px">
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="8" align="right">
                            <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                <tr>
                                    <td align="left">
                                        <asp:HiddenField ID="HiddenCohortId" runat="server" />
                                        <asp:HiddenField ID="HiddenGroupId" runat="server" />
                                        <asp:HiddenField ID="HiddenProgramId" runat="server" />
                                    </td>
                                    <td width="90%">
                                        Test Name:
                                        <asp:TextBox ID="TextBox1" runat="server" Width="180px">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="seabtn" runat="server" alt="" border="0" Height="25" ImageUrl="../images/btn_search.gif"
                                            onmouseout="roll(this)" onmouseover="roll(this)" Width="75" OnClick="seabtn_Click" />
                                    </td>
                                </tr>
                            </table>
                             <p class="alignleft">
                            <asp:Label ID="lblM" runat="server" ForeColor="Red"></asp:Label><p/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvTests" runat="server" DataKeyNames="TestId,Type" AutoGenerateColumns="False"
                                OnRowDataBound="gvTests_RowDataBound" OnRowCommand="gvTests_RowCommand" Width="100%"
                                CellPadding="0" GridLine="Both">
                                <rowstyle cssclass="datatable2" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1" />
                                <columns>
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" ItemStyle-Width="16%"/>
                                    <asp:BoundField DataField="TestType" HeaderText="Test Type" ItemStyle-Width="3%" />
                                    <asp:TemplateField HeaderText="Test Start Date" ItemStyle-Width="31%">
                                        <ItemTemplate>
                                            <input type="text" size="10" name="tbSD" id="tbSD" runat="server">
                                            <asp:DropDownList ID="ddTime_S" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:DropDownList ID="ddMin_S" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddAMPM_S" runat="server">
                                                <asp:ListItem>AM</asp:ListItem>
                                                <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <a id="LnkCalendar" runat="server">
                                                <asp:Image ID="BtnCalendar" runat="server" ImageUrl="../images/cal_16.gif"></asp:Image></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test End Date" ItemStyle-Width="31%">
                                        <ItemTemplate>
                                            <input type="text" size="10" name="tbED" id="tbED" runat="server" /></>
                                            <asp:DropDownList ID="ddTime_E" runat="server">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:DropDownList ID="ddMin_E" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddAMPM_E" runat="server">
                                                <asp:ListItem>AM</asp:ListItem>
                                                <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <a id="LnkCalendar2" runat="server">
                                                <asp:Image ID="BtnCalendar2" runat="server" ImageUrl="../images/cal_16.gif"></asp:Image></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="SetAll" Text="Set Dates" />
                                    <asp:ButtonField CommandName="Save" Text="Save" />
                                    <asp:TemplateField>
                                        <ItemTemplate>                                            
                                            <asp:HiddenField ID="TestStartDate" runat="server" Value='<%# Bind("TestStartDate") %>' />
                                            <asp:HiddenField ID="TestEndDate" runat="server" Value='<%# Bind("TestEndDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </columns>
                            </asp:GridView>
                            <asp:ImageButton ID="btnAssign" runat="server" ImageUrl="~/Images/assign.gif" OnClick="btnAssign_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="spacer">
            &nbsp;</tr>
    </table>
</asp:Content>
