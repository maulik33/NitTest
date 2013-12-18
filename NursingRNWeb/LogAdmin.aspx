<%@ Page Language="C#" AutoEventWireup="true" Inherits="LogAdmin" CodeBehind="LogAdmin.aspx.cs" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="NursingRNWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
        .modalBackground
        {
            background-color: Gray; /*            filter: alpha(opacity=70);
            opacity: 0.7; */
        }
        body
        {
            font-size: .80em;
            font-family: "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
            margin: 0px;
            padding: 0px;
        }
        
        td
        {
            vertical-align: top;
        }
    </style>
     <script type="text/javascript">
         function TabChanged(sender, args) {
             sender.get_clientStateField().value =
                sender.saveClientState();
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="accountLoginDiv" runat="server">
          <table>
              <tr>
                 <asp:label Text="Enter your KEC Credentials." Style="font-size:20px;font-weight: bold" runat="server"></asp:label>
             </tr>
              <tr>
                  <td style="width: 110px">
                     <asp:label Text="KEC UserName" runat="server"></asp:label>
                   </td>
                  <td>
                    <asp:TextBox ID ="txtUserName" runat="server"></asp:TextBox> 
                  </td>
            </tr>
            <tr>
                <td style="width: 110px" >
                    <asp:label Text="KEC Password  " runat="server"></asp:label>
                </td>
                <td>
                  <asp:TextBox ID ="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
               </td>
            </tr>
               <tr>
                <td style="width: 110px"></td>
               <td>
                   <asp:Button ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"  runat="server"></asp:Button>
               </td>
            </tr>
          </table>  
           <asp:label ID="errMsgLbl" Text="Invalid Domain User. Please Enter Valid KEC Credentials." Style="font-size:18px;color: red" Visible="False" runat="server"></asp:label>
        </div>   
        
     <div id="displayLogDiv" runat="server" Visible="False">   
     <h2>Nursing RN - Exception / Trace Management Console</h2>
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <asp:Label ID="MsgLabel" runat="server" EnableViewState="false"></asp:Label>
    <ajaxToolkit:TabContainer runat="server" ID="MainTabControl" ActiveTabIndex="0" Width="100%" OnClientActiveTabChanged="TabChanged">
        <ajaxToolkit:TabPanel HeaderText="Error Logs" runat="server" ID="ErrorTab">
            <ContentTemplate>
                <div>
                    <asp:DropDownList ID="LogFiles" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="GetLog" runat="server" Text="Get Log Details" OnClick="GetLogLog_Click" />
                    <br />
                </div>
<%--                <asp:UpdatePanel ID="mainContentUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                        <h3>
                            Summary</h3>
                        <asp:CheckBox runat="server" ID="chkGroupByException" Text="Group by Exception" />
                        <asp:GridView runat="server" ID="SummaryGrid" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="GroupName" HeaderText="Summary Text" />
                                <asp:BoundField DataField="Count" HeaderText="Count" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="true"
                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                            CellPadding="4" EnableModelValidation="True" GridLines="Horizontal" AutoGenerateColumns="False"
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
                            PageSize="100" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <asp:BoundField DataField="UserName" HeaderText="User" SortExpression="UserName" />
                                <asp:BoundField DataField="SessionId" HeaderText="Session ID" SortExpression="SessionId" />
                                <asp:BoundField DataField="UserHostAddress" HeaderText="Host" SortExpression="UserHostAddress" />
                                <asp:BoundField DataField="URL" HeaderText="URL" SortExpression="URL" />
                                <asp:BoundField DataField="DateLogged" HeaderText="Time" SortExpression="DateLogged"
                                    ItemStyle-Wrap="false" DataFormatString="{0:T}" />
                                <asp:BoundField DataField="ClassName" HeaderText="Class Name" SortExpression="ClassName" />
                                <asp:BoundField DataField="MethodName" HeaderText="Method Name" SortExpression="MethodName" />
                                <asp:BoundField DataField="ProcessedType" HeaderText="Type" SortExpression="ProcessedType" />
                                <asp:BoundField DataField="ExecutionTimeInMilliS" HeaderText="Exec Time" />
                                <asp:TemplateField HeaderText="Exception">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnExceptionDetails" runat="server" Text="Details" CommandArgument='<%# Eval("Exception") %>'
                                            OnClick="BtnViewDetails_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Input Params">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnInputDetails" runat="server" Text="Details" CommandArgument='<%# Eval("InputParameters") %>'
                                            OnClick="BtnViewDetails_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Output Params">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnOutputDetails" runat="server" Text="Details" CommandArgument='<%# Eval("OutPutParameters") %>'
                                            OnClick="BtnViewDetails_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Agent">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUserAgentDetails" runat="server" Text="Details" CommandArgument='<%# Eval("UserAgent") %>'
                                            OnClick="BtnViewDetails_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <p align="center">
                                    Sorry but no results were found. Please expand your search and try again.</p>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
<%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel HeaderText="Trace" runat="server" ID="TraceTab">
            <ContentTemplate>
                <div style="font-size: small; font-family: Segoe UI, Arial">
                    <div>
                        Show Trace Data from:&nbsp;
                        <asp:TextBox runat="server" ID="TraceDateFrom" MaxLength="10" Width="70px"></asp:TextBox>
                        &nbsp;&nbsp; To:&nbsp;<asp:TextBox runat="server" ID="TraceDateTo" MaxLength="10"
                            Width="70px"></asp:TextBox>
                        <br />
                        <asp:CheckBox runat="server" ID="TraceShowOnlyErrors" Text="Show only Trace Events that encountered error(s)" />
                        <br />
                        <asp:Button runat="server" ID="TraceRefresh" Text="Refresh" OnClick="TraceRefresh_Click">
                        </asp:Button>
                    </div>
                    <div style="float: left; width: 300px; overflow: auto">
                        <asp:TreeView ID="TraceFileList" runat="server" OnTreeNodePopulate="TraceFileList_TreeNodePopulate"
                            OnSelectedNodeChanged="TraceFileList_SelectedNodeChanged" ShowLines="True" EnableClientScript="false">
                            <Nodes>
                                <asp:TreeNode Text="Trace" ImageUrl="Images/Book_StackOfReportsHS.png" Value="Trace">
                                </asp:TreeNode>
                            </Nodes>
                        </asp:TreeView>
                    </div>
                    <div style="float: left; vertical-align: top">
                        Trace Information User:&nbsp;<asp:Label ID="TraceUserName" runat="server"></asp:Label><br />
                        Environment:&nbsp;<asp:Label ID="TraceEnvironment" runat="server"></asp:Label><br />
                        <asp:ListView runat="server" ID="TraceSessionContainer" OnItemDataBound="TraceSessionContainer_ItemDataBound">
                            <LayoutTemplate>
                                <table>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="TraceDataRow" runat="server" style="background-color: #FFFFB3">
                                    <td>
                                        <%# Eval("Time") %>
                                    </td>
                                    <td>
                                        <%# Eval("Message") %>
                                    </td>
                                    <td>
                                        <asp:Repeater runat="server" ID="TraceKeyValuePairList" DataSource='<%# DataBinder.Eval(Container.DataItem, "Data") %>'>
                                            <HeaderTemplate>
                                                <ul>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <%# Eval("Key") %>
                                                    =
                                                    <%# Eval("Value") %>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr id="TraceDataRow" runat="server" style="background-color: #D9ECFF">
                                    <td>
                                        <%# Eval("Time") %>
                                    </td>
                                    <td>
                                        <%# Eval("Message") %>
                                    </td>
                                    <td>
                                        <asp:Repeater runat="server" ID="TraceKeyValuePairList" DataSource='<%# DataBinder.Eval(Container.DataItem, "Data") %>'>
                                            <HeaderTemplate>
                                                <ul>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </ul>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <%# Eval("Key") %>
                                                    =
                                                    <%# Eval("Value") %>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div style="clear: both">
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel HeaderText="Administration" runat="server" ID="AdminTab">
            <ContentTemplate>
                <div>
                    <asp:Label ID="LogLevel" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ChangeToDebug" runat="server" Text="Change To Debug" OnClick="ChangeToDebug_Click" />&nbsp;&nbsp;&nbsp;Pass
                    Code:
                    <asp:TextBox TextMode="Password" runat="server" ID="PassCodeText"></asp:TextBox>
                </div>
                <br />
                <div>
                    <asp:Label ID="OldLogFilesCount" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ClearOldLog" runat="server" Text="Clear Old Log" OnClick="ClearOldLog_Click" />
                    <asp:Label runat="server" ID="daysOffset" Text="Show log files for last"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:TextBox
                        ID="daysTextBox" runat="server" Text="3" Width="40px"></asp:TextBox>&nbsp;<asp:LinkButton
                            runat="server" ID="RefreshFileListButton" Text="Refresh" OnClick="RefreshFileListButton_Click"></asp:LinkButton>
                </div>
                <br />
                Trace Enabled :
                <asp:Label runat="server" ID="TraceEnabledLabel"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Text="Toggle Trace Enabled Flag"
                    ID="TraceEnabledToggleButton" OnClick="TraceEnabledToggleButton_Click" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton runat="server" ID="RefreshAppSettings" Text="Refresh Application Settings"
                    OnClick="RefreshAppSettings_Click"></asp:LinkButton>
                <br />
                <asp:Label ID="TraceFilesSummary" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopup" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup" runat="server" Width="80%" Height="90%" Style="display: none">
        <asp:UpdatePanel ID="updPnlDetail" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--<asp:Label ID="lblDetail" runat="server" Text="Details" BackColor="lightblue" Width="95%" />--%>
                <asp:TextBox ID="lblDetail" runat="server" Text="Details" Width="100%" Height="92%"
                    ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div align="right" style="width: 95%">
            <asp:Button ID="btnClose" runat="server" Text="Close" Width="50px" />
        </div>
    </asp:Panel>
     </div>
    </form>
  <br />
  <br />
</body>
</html>
