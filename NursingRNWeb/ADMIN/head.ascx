<%@ Control Language="C#" AutoEventWireup="true" Inherits="ADMIN_header" CodeBehind="head.ascx.cs" %>
<script type="text/javascript">

    function GotoPreviousPage() {

        if (document.getElementById('hdnPreviousPage') != null) {
            var prevLocation = window.location.toString();
            window.location = prevLocation.substr(0, prevLocation.lastIndexOf("/")) + document.getElementById('hdnPreviousPage').value;
        }
        else {
            history.back();
        }
    }
</script>
<table cellpadding="0" cellspacing="0" border="0" align="center">
    <tr>
        <td>
            <table id="header" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="left">
                        <div style="margin-left: 5px">
                            <asp:ImageButton runat="server" ID="HomePageLogoButton" ImageUrl="../images/NursingLogo.png"
                                OnClick="HomePageLogoButton_Click" /></div>
                    </td>
                    <td align="right" valign="bottom">
                        <table width="172" border="0" cellpadding="0" cellspacing="0" class="tesm">
                            <tr>
                                <td colspan="2" align="right" height="20" nowrap>
                                    <asp:ImageButton ID="backbtn" runat="server" ImageUrl="../images/backNav.gif" Width="75"
                                        Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                        OnClientClick="GotoPreviousPage();return false;">
                                    </asp:ImageButton>
                                    <img src="../images/spacer.gif" width="10">&nbsp;
                                    <asp:ImageButton ID="excelbtn" runat="server" ImageUrl="../images/btn_excel.gif"
                                        Width="110" Height="25" border="0" alt="" onMouseOver="roll(this)" onMouseOut="roll(this)"
                                        OnClick="excelbtn_Click" Visible="False">
                                    </asp:ImageButton>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" background="../images/key_blu.gif">
                                    <span style="font-weight: bold; font-size: 9pt; font-family: Arial">Admin View</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
