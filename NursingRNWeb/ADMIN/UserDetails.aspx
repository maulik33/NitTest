<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="UserDetails.aspx.cs" Inherits="NursingRNWeb.ADMIN.UserDetails" Title="Kaplan Nursing" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
      
    </script>
    <asp:Panel runat="server" ID="panel" DefaultButton="searchButton">
        <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr id="trunautor" runat="server" visible="false">
        <td width="80%" border="0" cellpadding="0" cellspacing="0" align="center" style="line-height: 25pt;">
                                  
                                        <b>Unfortunately, the function you are attempting cannot be completed.<br />
                                                You are not authorized to view this page.<br />
                                            We apologize for the inconvenience.<br />
                                            Kaplan Nursing Team.</b>
                                    </td>
                                </tr>                    
            <tr id="trdata" runat="server" visible="false">
                <td>
                    <table align="left" border="0" class="datatable">
                        <tr class="datatable1">
                            <td colspan="8" align="left">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr id="trProgramOfStudy" runat="server">
                                        <td align="left" style="height: 33px">
                                            <asp:Label ID="lblProgramOfStudyText" runat="server" Text="Program of Study"></asp:Label>
                                        </td>
                                        <td style="height: 33px" colspan="2">
                                            <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramOfStudy"
                                                InitialValue="-1" ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trUserType" runat="server">
                                        <td align="left" style="height: 33px">
                                            <asp:Label ID="lblUsertypeText" runat="server" Text="User Type"></asp:Label>
                                        </td>
                                        <td style="height: 33px" colspan="2">
                                            <asp:RadioButtonList ID="statusRadioButton" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Admin</asp:ListItem>
                                                <asp:ListItem Value="0">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="height: 33px">
                                            <asp:TextBox ID="txtSearch" runat="server">
                                            </asp:TextBox>&nbsp;
                                        </td>
                                        <td style="height: 33px">
                                            <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                                OnClick="searchButton_Click" onMouseOver="roll(this)" onMouseOut="roll(this)" ValidationGroup="validateSearch"/>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblM" Visible="false" runat="server" Text="No items found"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gridUser" runat="server" AllowSorting="True" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="5" AllowPaging="True"
                                    AutoGenerateColumns="False" DataKeyNames="UserName" PageSize="10" CssClass="data1"
                                    Width="100%" OnPageIndexChanged="gridInstitutions_PageIndexChanged" OnPageIndexChanging="gridUserDetails_PageIndexChanging"
                                    OnSorting="gridUserDetails_Sorting">
                                    <RowStyle CssClass="datatable2a" />
                                    <HeaderStyle CssClass="datatablelabels" />
                                    <AlternatingRowStyle CssClass="datatable1a" />
                                    <Columns>
                                        <asp:BoundField DataField="UserId" HeaderText="User Id" SortExpression="UserId" />
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                                        <asp:BoundField DataField="UserPass" HeaderText="Password" SortExpression="UserPass" />
                                        <asp:BoundField DataField="InstitutionName" HeaderText="Institution Name" SortExpression="InstitutionName" />
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField runat="server" ID="hdnGridConfig" Value="UserId|ASC" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
