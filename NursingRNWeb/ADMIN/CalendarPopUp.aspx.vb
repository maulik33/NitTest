Imports System.Text


Namespace CalendarControl

Partial Class CalendarPopUp
    Inherits System.Web.UI.Page
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        Dim sbScript As New StringBuilder()
        sbScript.Append("<script language='javascript'>")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("window.opener.document.forms[0].elements['")
        sbScript.Append(Request.QueryString("src"))
        sbScript.Append("'].value = '")
            sbScript.Append(Calendar1.SelectedDate.ToString("MM/dd/yyyy"))
        sbScript.Append("';")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("window.close();")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("</script>")
        RegisterStartupScript("CloseWindow", sbScript.ToString)
    End Sub
End Class

End Namespace
