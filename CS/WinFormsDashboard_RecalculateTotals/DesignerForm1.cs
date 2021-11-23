
using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Localization;
using DevExpress.DashboardCommon.Native;
using DevExpress.DashboardCommon.Viewer;
using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardCommon.ViewModel;
using DevExpress.DashboardExport;
using DevExpress.DashboardWin;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
