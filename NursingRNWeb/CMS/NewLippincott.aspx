<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" Inherits="CMS_NewLippincott" Title="Kaplan Nursing" Codebehind="NewLippincott.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivLipp');
        });
    </script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="headfont" colspan="2">
                <b>
                    <asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="formtable">
                    <tr class="datatablelabels">
                        <td align="left" colspan="2">
                            Enter details in fields below&nbsp;<br />
                             <asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label3" runat="server" Text="Remediation Topic Title:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:DropDownList ID="ddlRemediation" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label4" runat="server" Text="Add Lippincott Into Question"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtQuestion" runat="server" Width="229px"></asp:TextBox>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" Width="63px" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label5" runat="server" Text="Question list:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:GridView ID="gvQuestions" DataKeyNames="Id" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="No question been assigned." 
                                onrowdeleting="gvQuestions_RowDeleting" >
                                <Columns>
                                    <asp:BoundField DataField="Id" Visible="false" />
                                    <asp:BoundField DataField="QuestionID" HeaderText="Question ID" SortExpression="QuestionID" />
                                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label6" runat="server" Text="Lippincott Explanation #1:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtLippincottExplanation1" runat="server" Height="222px" TextMode="MultiLine" Width="502px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label1" runat="server" Text="Lippincott Title #1:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtLippincottTitle1" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label8" runat="server" Text="Lippincott Explanation #2:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtLippincottExp2" runat="server" Height="222px" TextMode="MultiLine" Width="502px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="datatable2">
                        <td align="left" style="width: 30%">
                            <asp:Label ID="Label7" runat="server" Text="Lippincott Title #2:"></asp:Label>
                        </td>
                        <td align="left" class="datatable">
                            <asp:TextBox ID="txtLippincottTitle2" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="formtableApply">
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="  Save " OnClick="btnSave_Click" />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" 
                                Height="26px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
