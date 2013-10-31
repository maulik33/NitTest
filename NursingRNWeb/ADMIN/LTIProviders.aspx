<%@ Page Language="C#" MasterPageFile="~/ADMIN/AdminMaster.master" AutoEventWireup="true" CodeBehind="LTIProviders.aspx.cs" Inherits="LTIProviders" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div id="accountLoginDiv" runat="server" align="left">
          <table>
              <tr>
                 <asp:label Text="Enter your KEC Credentials." Style="font-size:16px;font-weight: bold" runat="server" ></asp:label>
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
        <div id="displayStringDiv" runat="server" Visible="false">

        <table cellspacing="2" style="padding:2% 1% 2% 1%;width:100%">
            <tr>
            	<td colspan="2">
            	     <asp:label ID="ltiDivLabel" Text="Add or View List of LtiProviders." Style="font-size:16px;font-weight: bold" runat="server" ></asp:label>
	    	</td>
	    </tr>

            <tr>
            	<td colspan="2">
            		<asp:label ID="ltiErrMsgLbl" Style="font-size:18px;color: red" Visible="False" runat="server"></asp:label>
		</td>
	    </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Name:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="NameTextBox" runat="server" Width="650px" MaxLength="50"></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Title:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="TitleTextBox" runat="server" Width="650px" MaxLength="100"></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    URL:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="URLTextBox" runat="server" Width="650px" MaxLength="100"></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Consumer Key:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="ConsumerKeyTextBox" runat="server" Width="650px" MaxLength="100"></asp:TextBox>
                </td> 
            </tr>
             <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Consumer Secret:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="ConsumerSecretTextBox" runat="server" Width="650px" MaxLength="100"></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Custom Parameters:
                </td>
                <td class="datatable" align="left" style="height: 56px">
                    <asp:TextBox ID="CustomParametersTextBox" rows="2" TextMode="multiline" AcceptsReturn="false" VerticalScrollBarVisibility="Visible" runat="server" Width="650px" MaxLength="2000" ></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Description:
                </td>
                <td class="datatable" align="left" style="height: 28px">
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Width="650px" MaxLength="100"></asp:TextBox>
                </td> 
            </tr>
            <tr class="datatable2">
                <td align="left" style="width: 147px; height: 28px;">
                    Status:
                </td>
                <td align="left" style="height: 28px">
                    <asp:DropDownList runat="server" ID="StatusDropDown">
                          <asp:ListItem Text="active" Value="true" />
                          <asp:ListItem Text="inactive" Value="false" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="datatable">
                <td align="right" >
                    <asp:Button runat="server" ID ="takeAction" OnClick="SaveOrEdit"/>
                    </td>
                <td align="left">
                     <asp:Button runat="server" ID ="goBackToListSubmit" Text="Go Back To List" OnClick="GoBackToList"/>
                </td>            
            </tr>

        </table>
 
<table cellspacing="2" style="padding:2% 1% 2% 1%;width:100%">
    <tr>
        <td>
  <asp:GridView ID="gvLTIProviders" runat="server" DataKeyNames="Id" AllowPaging="False"
        AutoGenerateColumns="False" EmptyDataText="No Records to display" AllowSorting="False"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%"
        OnRowDataBound="gvLTIProviders_RowDataBound" OnRowEditing="gvLTIProviders_RowEditing" OnRowCommand="gvLTIProviders_RowCommand">
        <RowStyle CssClass="datatable2a" />
        <HeaderStyle CssClass="datatablelabels" />
        <AlternatingRowStyle CssClass="datatable1a" />
        <Columns>
            <asp:BoundField DataField="Id" Visible="false"/>
            <asp:BoundField DataField="Name" HeaderText="LTI Provider Name" />
            <asp:BoundField DataField="Description" HeaderText="LTI Provider Description" />
            <asp:BoundField DataField="Url" HeaderText="LTI Provider URL" />
            <asp:BoundField DataField="Active" HeaderText="Active"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="BtnView" runat="server" Text="View" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ButtonType="Link"/>
             <asp:TemplateField>
            <ItemTemplate>
            <asp:LinkButton ID="BtnStatusChange" runat="server" CommandName="ChangeStatus" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            </td>
        </tr>
    </table>
      </div>
</asp:Content>
