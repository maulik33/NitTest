<%@ Page Title="" Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="AssignStudentTest.aspx.cs" Inherits="NursingRNWeb.ADMIN.AssignStudentTest" %>

<%@ Register TagPrefix="Saravana" TagName="Calendar" Src="Calender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivProbab');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>Add/Edit &gt; Assign Test</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
            <asp:Label runat="server" ID="lblError" Visible="False" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="lblM" Visible="False" Text="Please Select Test Name" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr>
                        <td>
                            <asp:GridView ID="gvAdhocGroupTest" runat="server" AutoGenerateColumns="False" DataKeyNames="AdhocGroupTestDetailID"
                                ShowFooter="True" PageSize="50" Width="100%" OnRowDataBound="gvAdhocGroupTest_RowDataBound"
                                OnRowCommand="gvAdhocGroupTest_RowCommand">
                                <RowStyle CssClass="datatable2a" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1a" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Test Name">
                                        <EditItemTemplate >
                                            <asp:DropDownList ID="ddTest" runat="server" AppendDataBoundItems="true" DataTextField="">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTest" runat="server" Text='<%# Eval("Test.TestName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlNewTest" runat="server" AppendDataBoundItems="true">                                                 
                                                <asp:ListItem>Not Selected</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTestStartDate" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <input size="6" type="text" name="tbSD" id="tbSD" runat="server" />
                                            <asp:DropDownList ID="ddlNewTime_S" runat="server">
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
                                            <asp:DropDownList ID="ddNewAMPM_S" runat="server">
                                                <asp:ListItem>AM</asp:ListItem>
                                                <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <a id="LnkNewCalendar" runat="server">
                                                <asp:Image ID="BtnNewCalendar" runat="server" ImageUrl="../images/cal_16.gif"></asp:Image></a>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTestEndDate" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <input size="6" type="text" name="tbED" id="tbED" runat="server" />
                                            <asp:DropDownList ID="ddlNewTime_E" runat="server">
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
                                            <asp:DropDownList ID="ddNewAMPM_E" runat="server">
                                                <asp:ListItem>AM</asp:ListItem>
                                                <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                            <a id="LnkNewCalendar2" runat="server">
                                                <asp:Image ID="BtnNewCalendar2" runat="server" ImageUrl="../images/cal_16.gif"></asp:Image></a>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add New" Visible="true" ShowHeader="False">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="False" CommandName="AddNew"
                                                Text="Save"></asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
