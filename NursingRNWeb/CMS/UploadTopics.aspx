<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/AdminMaster.master"
    CodeBehind="UploadTopics.aspx.cs" Inherits="NursingRNWeb.CMS.UploadTopics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../JS/jquery.MultiFile.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ExpandContextMenu(3, 'ctl00_DivUploadTopics');
        });
    </script>
    <div>
        <table align="left" border="0" class="formtable">
            <tr class="formtable">
                <td colspan="2" class="headfont">
                    <b>Content Management - Upload Topics</b>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    For instructions on using this page, please click here.
                    <%-- <asp:HyperLink ID="hlInstruction" runat="server" Target="_blank" NavigateUrl=""></asp:HyperLink>--%>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    To download topics template
                    <asp:LinkButton ID="lbDownloadTopicTemplate" runat="server" OnClick="lbDownloadTopicTemplate_Click">Click Here</asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" EnableViewState="false" />
                </td>
            </tr>
            <tr class="datatable2">
                <td style="width: 200px;">
                    Choose completed topics template:
                </td>
                <td style="height: 60px; width: 350px;" align="left">
                    <asp:FileUpload ID="fuTopics" runat="server" class="multi" accept="xlsx" maxlength="1"
                        Height="24px" Width="450px" size="59" />
                </td>
            </tr>
            <tr class="datatable2">
                <td>
                </td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Completed Template" OnClick="btnUpload_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 10px; text-align: left" id="divError" runat="server" visible="false">
        <div class="med_center_banner2_l" style="margin-top: 10px; clear: both;">
            <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
            Error\InValid topics
        </div>
        <br />
        <asp:GridView ID="gvInValidTopics" Style="clear: both;" runat="server" CellSpacing="5"
            AutoGenerateColumns="false">
            <RowStyle CssClass="datatable2a" BorderColor="Red" ForeColor="Red" />
            <HeaderStyle CssClass="datatablelabels" />
            <AlternatingRowStyle CssClass="datatable1a" />
            <Columns>
                <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" />
                <asp:BoundField DataField="Explanation" HeaderText="Description/Content" />
                <asp:BoundField DataField="ErrorMessage" HeaderText="Error Message" />
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin: 10px; text-align: left; clear: both;" id="divSuccess" runat="server"
        visible="false">
        <div class="med_center_banner2_l" style="margin-top: 10px;">
            <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
            Valid Topics
        </div>
        <p style="text-align: left;">
            <asp:Label ID="lblSuccessMsg" runat="server" EnableViewState="False" ForeColor="#00CC00"></asp:Label></p>
        <asp:GridView ID="gvValidTopicss" runat="server" CellSpacing="5" AutoGenerateColumns="false"
            Style="clear: both;">
            <RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels" />
            <AlternatingRowStyle CssClass="datatable1a" />
            <Columns>
                <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" />
                <asp:BoundField DataField="Explanation" HeaderText="Description/Content" />
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin: 10px; text-align: left;" id="divDuplicateTopics" runat="server"
        visible="false">
        <div class="med_center_banner2_l" style="margin-top: 10px;">
            <img src="../images/icon_book1.gif" width="13" height="13" alt="" />
            Duplicate Topics
        </div>
        <asp:GridView ID="gvDuplicateTopics" runat="server" Style="clear: both;" CellSpacing="5"
            AutoGenerateColumns="false">
            <RowStyle CssClass="datatable2a" />
            <HeaderStyle CssClass="datatablelabels" />
            <AlternatingRowStyle CssClass="datatable1a" />
            <Columns>
                <asp:BoundField DataField="TopicTitle" HeaderText="Topic Title" />
                <asp:BoundField DataField="Explanation" HeaderText="Description/Content" />
            </Columns>
        </asp:GridView>
    </div>
    <div id="divSave" runat="server">
        <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" OnClick="btnSave_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnCMS" runat="server" Text="View content in CMS" OnClick="btnCMS_Click"
            Visible="false" />
        <%--<asp:Button ID="btnCMS" runat="server" Text="View content in CMS" OnClick="btnCMS_Click"
                Visible="false" />--%>
    </div>
    <asp:HiddenField ID="hfTopic" runat="server" />
</asp:Content>
