<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.master" Inherits="ADMIN_ProgramAddAssign" EnableViewState="true" Codebehind="ProgramAddAssign.aspx.cs" %>

<%@ Register Namespace="WebControls" TagPrefix="KTP" Assembly="NursingRNWeb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JS/jquery-1.4.3.min.js" type="text/javascript"></script>
    <table id="cFormHolder" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table align="left" border="0" width="100%">
                    <tr>
                        <td colspan="4" class="headfont">
                            <b>Add > Program > Assets</b>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                                Use the dropdown menus to select a test, then click Add. The test will display below
                                under View Tests in Program. This test will be automatically added to all the cohorts,
                                groups, and users assigned to this program.<br />
                                <br />
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" width="125px;" nowrap class="datatable">
                                <b>Program Name:</b>
                            </td>
                            <td align="left" width="165px;" class="datatable">
                                <asp:Label ID="lblPName" runat="server" Text="Label" Width="100%"></asp:Label>
                            </td>
                            <td align="center" width="125px;" class="datatable">
                                <b>Description:</b>
                            </td>
                            <td align="left" width="165px;" class="datatable">
                                <asp:Label ID="lblDescriptiption" runat="server" Text="Label" Width="100%"></asp:Label>
                            </td>
                            <td align="right" width="125px;" class="datatable">
                                <b>Program of Study:</b>
                            </td>
                            <td align="left" width="30px;" class="datatable">
                                <asp:Label ID="lblProgramOfStudyName" runat="server" Width="100%"></asp:Label>
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="D_Tests" runat="server">
                    <br />
                    <table align="left" border="0" class="datatable" cellpadding="3" width="100%">
                        <tr class="datatable1">
                            <td colspan="5" align="left">
                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" class="datatable" style="height: 27px">
                                            <asp:Label ID="lblTitle" runat="server" Text="Add Asset to Program (Filter) " Width="241px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" style="width: 119px">
                                <asp:Label ID="lblAssetType" runat="server" Text="Asset Type: " Width="189px"></asp:Label>
                            </td>
                            <td colspan="4" align="left" style="width: 231px">
                            <KTP:KTPDropDownList ID="ddlProgramofStudy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramOfStudy_SelectedIndexChanged"></KTP:KTPDropDownList>
                            <asp:RequiredFieldValidator ID="rfvProgramOfStudy" runat="server" ControlToValidate="ddlProgramofStudy"
                                                InitialValue="-1" ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
                          </td>
                        </tr>
                        <tr class="datatable2">
                            <td align="left" style="width: 119px">
                                <asp:Label ID="lblAssetGroup" runat="server" Text="Asset Group: "></asp:Label>
                            </td>
                            <td colspan="4" align="left" style="width: 231px">
                            <KTP:KTPDropDownList ID="ddlAssetGroup" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetGroup_SelectedIndexChanged" >
                                </KTP:KTPDropDownList>
                                <asp:RequiredFieldValidator ID="rfvAssetGroup" runat="server" ControlToValidate="ddlAssetGroup"
                                                InitialValue="-1" ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
                               
                            </td>
                        </tr>
                        <tr class="datatable2">
                         <td align="left" style="width: 119px">
                                <asp:Label ID="lblAssetName" runat="server" Text="Asset Name: "></asp:Label>
                            </td>
                             <td colspan="4" align="left" style="width: 231px">
                                <KTP:KTPListBox ID="ddlAssetName" runat="server" SelectionMode="Multiple" ShowNotSelected="false" ShowSelectAll="false"  Width="250px">
                                </KTP:KTPListBox>
                                 <asp:RequiredFieldValidator ID="rfvAssetName" runat="server" ControlToValidate="ddlAssetName"
                                           ErrorMessage="*Required Field" Display="Static" ValidationGroup="validateSearch"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <asp:ImageButton ID="addbtn" runat="server" ImageUrl="~/Temp/images/add.gif" Width="75"
                                    Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                    OnClick="addbtn_Click" ValidationGroup="validateSearch"></asp:ImageButton>
                                
                            </td>

                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblM" runat="server" Width="384px" ForeColor="#C00000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" border="0" class="datatable">
                    <tr class="datatable1">
                        <td colspan="8" align="right" style="width: 388px">
                            <table border="0" width="100%" cellpadding="0" cellspacing="2">
                                <tr>
                                    <td align="left">
                                        <b>View Assets in</b>
                                    </td>
                                    <td width="33%">
                                        <asp:HiddenField ID="HiddenProgramId" runat="server" />
                                    </td>
                                    <td>
                                        <!-- 					<asp:imagebutton id="seabtn" runat="server" ImageUrl="~/Temp/images/btn_search.gif" width="75" height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)" ></asp:imagebutton>	-->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvTests" runat="server" AutoGenerateColumns="False" DataKeyNames="TestId,Type,AssetGroupId"
                                OnRowDataBound="gvTests_RowDataBound" OnRowCommand="gvTests_RowCommand" CellPadding="5"
                                Width="100%">
                                <RowStyle CssClass="datatable2" />
                                <HeaderStyle CssClass="datatablelabels" />
                                <AlternatingRowStyle CssClass="datatable1" />
                                <Columns>
                                    <%--<asp:BoundField HeaderText="Test Category" ControlStyle-Width="10px" />
                                    <asp:BoundField DataField="Name" HeaderText="Test Name" />--%>
                                     <asp:TemplateField HeaderText="Program of Study" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Program.ProgramofStudyName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asset Category" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Product.ProductName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TestName" HeaderText="Asset Name" HeaderStyle-HorizontalAlign="Left"/>
                                    <asp:ButtonField ButtonType="Link" CommandName="Remove" Text="Remove" ControlStyle-Width="50px" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
