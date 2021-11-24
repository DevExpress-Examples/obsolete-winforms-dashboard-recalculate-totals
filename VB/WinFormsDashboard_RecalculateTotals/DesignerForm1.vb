Namespace WinFormsDashboard_RecalculateTotals

    Public Partial Class DesignerForm1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
            dashboardDesigner.CreateRibbon()
            dashboardDesigner.LoadDashboard("..\..\Dashboards\dashboard1.xml")
            Call New RecalculateGridTotalsModule().Attach(dashboardDesigner)
        End Sub
    End Class
End Namespace
