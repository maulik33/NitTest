Imports NursingLibrary


Partial Class ADMIN_P_CohortByStudent
    'ADMIN_P_RemedationTimeByStudent
    Inherits System.Web.UI.Page
    'Private _Sort As String
    'Public Property Sort() As String
    '    Get
    '        Return IIf(_Sort = "", "TestName", _Sort)
    '    End Get
    '    Set(ByVal value As String)
    '        _Sort = value
    '    End Set
    'End Property
    'Private Property act() As SV.PrintActions
    '    Get
    '        Dim o As Object = Me.ViewState("act")
    '        If o Is Nothing Then Return SV.PrintActions.ShowPreview Else Return o
    '    End Get
    '    Set(ByVal value As SV.PrintActions)
    '        Me.ViewState("act") = value
    '    End Set
    'End Property

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    SV.CheckSession(Me.Page, SV.AdiminTypes.Tech)

    '    If Not (IsPostBack) Then
    '        Me.Sort = Me.Page.Request.QueryString("Sort")
    '        act = Page.Request.QueryString("act")

    '        Dim u As SV.AdiminTypes = SV.GetAdminRole(Me.Page)
    '        If u = SV.AdiminTypes.Local OrElse u = SV.AdiminTypes.Tech Then
    '            Dim cs As New cStudent()
    '            cs.PopulateInstitutionByID(ddInstitution, Convert.ToInt32(Session("InstitutionID").ToString()))
    '        Else
    '            Dim cs As New cStudent()
    '            cs.PopulateInstitution(ddInstitution)
    '            ddInstitution.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        End If

    '        Dim II As String = Me.Page.Request.QueryString("Institution")
    '        If II <> "" AndAlso IsNumeric(II) Then
    '            ddInstitution.SelectedValue = II
    '        End If

    '        Dim cc As New cCohort
    '        cc.PopulateCohort(ddCohorts, Me.ddInstitution.SelectedValue)
    '        ddCohorts.Items.Insert(0, New ListItem("Not Selected", "0"))

    '        '     Dim cp As New cProduct()
    '        '     cp.PopulateProducts(ddProducts)
    '        '     ddProducts.Items.Insert(0, New ListItem("Not Selected", "0"))
    '        Dim IP As String = Me.Page.Request.QueryString("TestType")

    '        If IP <> "" AndAlso IsNumeric(IP) Then
    '            ddProducts.SelectedValue = IP
    '        End If

    '        'Dim ct As New cTest()
    '        'ct.PopulateTests(ddTests, Convert.ToInt32(ddProducts.SelectedValue))
    '        'ddTests.Items.Insert(0, New ListItem("Not Selected", "0"))




    '        Dim IC As String = Me.Page.Request.QueryString("Cohort")
    '        If IC <> "" AndAlso IsNumeric(IC) Then
    '            ddCohorts.SelectedValue = IC
    '        End If

    '        Dim IT As String = Me.Page.Request.QueryString("TestName")
    '        If IT <> "" AndAlso IsNumeric(IT) Then
    '            ddTests.SelectedValue = IT
    '        End If
    '    End If

    '    Fill_ds()



    'End Sub

    'Private Function BuildDs(ByVal dt As Data.DataTable) As Data.DataSet

    '    Dim ds As New RemediationByStudent
    '    Dim rh As RemediationByStudent.HeadRow = ds.Head.NewRow
    '    rh.InstitutionName = Me.ddInstitution.SelectedItem.Text
    '    rh.CohortName = Me.ddCohorts.SelectedItem.Text
    '    'rh.GroupName = Me.ddProducts.SelectedItem.Text()
    '    rh.TestName = Me.ddTests.SelectedItem.Text
    '    rh.ReportName = "Remediation by Student"
    '    ds.Head.Rows.Add(rh)

    '    dt.DefaultView.Sort = Me.Sort
    '    For Each r As Data.DataRowView In dt.DefaultView
    '        Dim rd As RemediationByStudent.DetailRow = ds.Detail.NewRow
    '        rd.FirstName = r("FirstName").ToString
    '        rd.LastName = r("LastName").ToString
    '        rd.Remediated = r("Remediation")
    '        rd.Explanation = r("Explanation").ToString
    '        rd.HeadID = rh.HeadID
    '        ds.Detail.Rows.Add(rd)
    '    Next
    '    Return ds


    'End Function

    'Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    'Private Sub Fill_ds()
    '    If Me.ddInstitution.SelectedValue = "0" OrElse Me.ddCohorts.SelectedValue = "0" Then Exit Sub

    '    Dim cc As New cCohort
    '    Dim dt As Data.DataTable = cc.getRemedationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue))
    '    Me.GridView1.DataSource = dt
    '    Me.GridView1.DataBind()


    '    rpt.Load(Server.MapPath("~/Admin/Report/TestRemediationByStudent.rpt"))
    '    rpt.SetDataSource(BuildDs(dt))
    '    Select Case act
    '        Case SV.PrintActions.ExportExcel
    '            rpt.ReportDefinition.Sections(1).SectionFormat.EnableSuppress = True
    '            'CType(rpt.ReportDefinition.ReportObjects.Item("Picture2"), CrystalDecisions.CrystalReports.Engine.PictureObject).Width = 1
    '            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, True, "TestRemediationByStudent")
    '        Case SV.PrintActions.ExportExcelDataOnly
    '            rpt.ReportDefinition.Sections(1).SectionFormat.EnableSuppress = True
    '            rpt.ReportDefinition.Sections("Section5").SectionFormat.EnableSuppress = True
    '            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, True, "TestRemediationByStudent")
    '        Case SV.PrintActions.PDFPrint
    '            rpt.ReportDefinition.Sections(2).SectionFormat.EnableSuppress = True
    '            'CType(rpt.ReportDefinition.ReportObjects.Item("Text11"), CrystalDecisions.CrystalReports.Engine.TextObject).Text = ""
    '            rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, True, "TestRemediationByStudent")
    '        Case SV.PrintActions.ShowPreview
    '            Me.CrystalReportViewer1.ReportSource = rpt
    '            Me.CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX
    '        Case SV.PrintActions.DirectPrint
    '            rpt.PrintToPrinter(1, False, 0, 0)
    '    End Select
    'End Sub

    'Protected Overrides Sub OnUnload(ByVal e As System.EventArgs)
    '    MyBase.OnUnload(e)
    '    rpt.Close()
    '    rpt.Dispose()
    'End Sub

    'Protected Sub ddInstitution_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddInstitution.SelectedIndexChanged
    '    Dim cc As New cCohort
    '    cc.PopulateCohort(ddCohorts, Me.ddInstitution.SelectedValue)
    '    ddCohorts.Items.Insert(0, New ListItem("Not Selected", "0"))

    '    'Fill_ds()
    'End Sub

    'Protected Sub ddProducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddProducts.SelectedIndexChanged
    '    Dim ct As New cTest()
    '    ct.PopulateTests(ddTests, Convert.ToInt32(ddProducts.SelectedValue))
    '    ddTests.Items.Insert(0, New ListItem("Not Selected", "0"))
    '    Fill_ds()
    'End Sub

    'Protected Sub ddTests_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTests.SelectedIndexChanged
    '    Fill_ds()
    'End Sub

    'Protected Sub ddCohorts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddCohorts.SelectedIndexChanged

    '    Fill_ds()
    'End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    rpt.Close()
    '    rpt.Dispose()
    'End Sub
End Class
