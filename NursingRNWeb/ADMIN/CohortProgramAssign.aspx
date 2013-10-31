<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_CohortProgramAssign" EnableViewState="true" CodeBehind="CohortProgramAssign.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table align="left" border="0" width="100%">
                    <tr>
                        <td colspan="4" class="headfont">
                            <b>Add > Cohort > Assign Programs</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            Select a Program to Assign to this Cohort, then click Save. Each Cohort can only
                            have one Program assigned.<br />
                            <br />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" width="120" nowrap class="datatable">
                            <b>Cohort Name:</b>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblCohortName" runat="server" Text="" Width="147px"></asp:Label>
                        </td>
                        <td align="center" width="150" class="datatable">
                            <b>Description:</b>
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblDescription" runat="server" Text="" Width="131px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="datatable">
                            Program Name:
                        </td>
                        <td colspan="3" align="left">
                            <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
                        </td>
                      </tr>
                       <tr>
                        <td align="left" width="150">
                          Program of Study:
                        </td>
                        <td align="left" class="datatable">
                            <asp:Label ID="lblProgOfStudy" runat="server" Text="" Width="131px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <table align="left" border="0" class="datatable" width="100%">
                    <tr class="datatable1">
                        <td colspan="4" align="right">
                            <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                <tr align="right">
                                  <td width="90%" >
                                        Program Name:
                                        <asp:TextBox ID="txtSearch" runat="server" Width="180px">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="../images/btn_search.gif"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblM" runat="server" Text="The Cohort has been updated" ForeColor="Red"
                                Visible="False" Width="195px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvPrograms" DataKeyNames="ProgramId" runat="server" AutoGenerateColumns="False"
                                OnRowCommand="gvPrograms_RowCommand" OnRowDataBound="gvPrograms_RowDataBound"
                                Width="100%" cellpadding="5" AllowSorting="True" OnSorting="gvPrograms_Sorting">
                                <rowstyle cssclass="datatable2" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1" />
                                <columns>
                       <asp:TemplateField>
                          <ItemTemplate>                           
                             <asp:RadioButton AutoPostBack="true"  OnCheckedChanged="rbsira_OnCheckedChanged"  id="rbsira"  runat="server"/>     
                         </ItemTemplate>
                       </asp:TemplateField> 
                       <asp:BoundField DataField="ProgramId" HeaderText="ProgramID" SortExpression="ProgramId" />
                       <asp:BoundField DataField="ProgramName" HeaderText="Program Name" SortExpression="ProgramName" />
                       <asp:BoundField DataField="Description" HeaderText="Description" />
                       <asp:ButtonField CommandName="Tests" Text="View Tests" />
                   </columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:imagebutton id="addbtn" runat="server" ImageUrl="../images/btn_save.gif"
                                width="75" height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                OnClick="addbtn_Click"></asp:imagebutton>
                            <asp:TextBox ID="txtSiraNo" runat="server" Visible="False">
                            </asp:TextBox>
                            <br />
                            <asp:HiddenField runat="server" ID="hdnGridConfig" Value="ProgramId|ASC" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
