<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true"
    Inherits="ADMIN_Percentile" Title="Kaplan Nursing" CodeBehind="Percentile.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<%@ Register TagPrefix="asp" Namespace="WebControls" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivProbab');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View &gt; Percentile Rank/Probability</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to assign percentile or probability of passing (only applies to NCLEX
                Diagnostic and Readiness tests) values to a test.
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td>
                            <table border="0">
                              <tr>
                                    <td align="left">
                                        <asp:Label ID="lblProgramofStudytxt" runat="server" Text="Program of Study"></asp:Label>
                                    </td>
                                    <td align="left">
                                       <asp:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProgramOfStudy_SelectedIndexChanged"></asp:KTPDropDownList>
                                       <asp:Label ID="lblProgramofStudyVal" runat="server" Text="" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 131px">
                                        Test Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddProducts" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProducts_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 131px">
                                        Test Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddTests" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddTests_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand" ShowFooter="True"
                                OnRowDeleting="GridView1_RowDeleting" PageSize="50" Width="90%">
                                <rowstyle cssclass="datatable2a" />
                                <headerstyle cssclass="datatablelabels" />
                                <alternatingrowstyle cssclass="datatable1a" />
                                <columns>

<asp:TemplateField HeaderText="Number Correct"> <EditItemTemplate>
  <asp:TextBox ID="txtNumberCorrect" runat="server" Text='<%# Bind("NumberCorrect") %>' Width="40px"></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:TextBox ID="txtNewNumberCorrect" runat="server"  Width="40px" ></asp:TextBox > </FooterTemplate>
<ItemTemplate>
  <asp:Label ID="Label2" runat="server" Text='<%# Bind("NumberCorrect") %>' ></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Correct %">
<EditItemTemplate>
  <asp:TextBox ID="txtCorrect" runat="server" Text='<%# Bind("Correct") %>' Width="40px"></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:TextBox ID="txtNewCorrect" runat="server"  Width="40px"></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
  <asp:Label ID="Label3" runat="server" Text='<%# Bind("Correct") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Percentile Rank" >
<EditItemTemplate>
  <asp:TextBox ID="txtPercentileRank" runat="server" Text='<%# Eval("PercentileRank") %>' Width="40px"></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:TextBox ID="txtNewPercentileRank" runat="server"  Width="40px"></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
  <asp:Label ID="Label4" runat="server" Text='<%# Bind("PercentileRank") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Probability" >
<EditItemTemplate>
  <asp:TextBox ID="txtProbability" runat="server" Text='<%# Eval("Probability") %>' Width="40px"></asp:TextBox>
</EditItemTemplate>
<FooterTemplate>
  <asp:TextBox ID="txtNewProbability" runat="server" Width="40px"></asp:TextBox>
</FooterTemplate>
<ItemTemplate>
  <asp:Label ID="Label6" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>

    <asp:BoundField DataField="id" HeaderText="ID" Visible="False" />

<asp:TemplateField HeaderText="Edit" ShowHeader="False">
<EditItemTemplate>
  <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
  <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
</EditItemTemplate>
<FooterTemplate>
  <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="AddNew" Text="Add New"></asp:LinkButton>
</FooterTemplate>
<ItemTemplate>
  <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />

</columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
