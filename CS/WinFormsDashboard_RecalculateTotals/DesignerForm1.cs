namespace WinFormsDashboard_RecalculateTotals {
    public partial class DesignerForm1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        public DesignerForm1() {
            InitializeComponent();
            dashboardDesigner.CreateRibbon();
            dashboardDesigner.LoadDashboard(@"..\..\Dashboards\dashboard1.xml");
            new RecalculateGridTotalsModule().Attach(dashboardDesigner);
        }
    }
}