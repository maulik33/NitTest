<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_CustomTest" Codebehind="CustomTest.aspx.cs" %>
<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        ExpandContextMenu(3, 'ctl00_DivCstmTst');
        $("tr.datatable1").delegate("#<%=TextBox1.ClientID%>", "keypress", function (event) {
            if (event.keyCode == '13') {
                $('#<%=ibtnSearch.ClientID%>').click();
                event.preventDefault();                
            }
        });
    });
</script>

    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="headfont">
                <b>View > Custom Test List</b></td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                Use this page to view or edit a Custom Test</td>
        </tr>
        <tr>
            <td style="height: 71px">
                <table align="left" border="0" class="datatable" cellspacing="0">
                    <tr class="datatable1">
                        <td colspan="3" align="left">
                            <asp:Label ID="lblProgramOfStudy" runat="server" Text="Program of Study"></asp:Label>
                            <KTP:KTPDropDownList ID="ddlProgramOfStudy" runat="server" AutoPostBack="True" NotSelectedText="Selection Required" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                            <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramOfStudy" ErrorMessage="*Required Field" ValidationGroup="validateSearch" Display="Static" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="4" align="right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;</td>
                                    <td>
                                        <asp:ImageButton ID="ibtnSearch" runat="server" ImageUrl="~/Temp/images/btn_search.gif"
                                            Width="75" Height="25" border="0" alt="" onMouseOver="roll(this)" 
                                            onMouseOut="roll(this)" onclick="ibtnSearch_Click" ValidationGroup="validateSearch" />
                                        </asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
  
    <asp:GridView ID="gvCustomTest" DataKeyNames="TestId" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        EmptyDataText="No Records to display" AllowSorting="True" BorderColor="#CC9966"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" 
        onpageindexchanging="gvCustomTest_PageIndexChanging" 
        onrowdatabound="gvCustomTest_RowDataBound" onrowdeleting="gvCustomTest_RowDeleting" 
        onsorting="gvCustomTest_Sorting" CssClass="gvCustomTest">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="TestId" HeaderText="Test ID" SortExpression="TestId" />
            <asp:BoundField DataField="TestName" HeaderText="Test Name" SortExpression="TestName">
            </asp:BoundField>
            <asp:BoundField DataField="ProductID" HeaderText="Test Type" SortExpression="ProductId"/>
            <asp:BoundField DataField="GroupId" HeaderText="Group" SortExpression="GroupId"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "QuestionCustomTset.aspx?TestID=" + Eval("TestID") + "&SearchCondition=" + SearchCondition + "&Sort=" + Sort +"&ProductId="+Eval("ProductID")%>'>Questions</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# "upload.aspx?TestID=" + Eval("TestID") +"&TestName="+Eval("TestName")+ "&TestType=" + Eval("ProductID")%>'>Upload</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# "NewCustomTest.aspx?TestID=" + Eval("TestID") + "&CMS=1&SearchCondition=" + SearchCondition + "&Sort=" + Sort+"&ProductId="+Eval("ProductID")%>'>Edit</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# "CopyCustomTest.aspx?TestID="+ Eval("TestID") + "&SearchCondition=" + SearchCondition + "&Sort=" + Sort %>'>Copy</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <br/>
    <asp:HiddenField runat="server" ID="hdnGridConfig" Value="TestId|DESC" />
     <table>
  <tr>
  <td> 
      <span style="color: #ff3300">*</span><span style="color: #003366"><strong>Test Type</strong></span>:
      <span style="color: #993300"><strong>1</strong></span>-Integrated Testing, <span
          style="color: #993300"><strong>3</strong></span>-Focused Review Tests, <span style="color: #993300">
              <strong>4</strong></span>-NCLEX-RN Prep</td>
  </tr>
   </table> 
    <br/>
    <asp:Button ID="btnNewTest" runat="server" Text="New Test" 
        onclick="btnNewTest_Click" />
    &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnTestCategories" runat="server" Text="Test Categories" 
        onclick="btnTestCategories_Click" />
    &nbsp; &nbsp;&nbsp;
    <asp:Button ID="btnUpload" runat="server" Text="Upload Questions" 
        Visible="False" />
</asp:Content>