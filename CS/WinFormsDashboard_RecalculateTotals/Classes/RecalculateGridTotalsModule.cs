using DevExpress.DashboardCommon;
using DevExpress.DashboardExport;
using DevExpress.DashboardWin;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace WinFormsDashboard_RecalculateTotals {
    public class RecalculateGridTotalsModule {
        IDashboardControl dashboardControl;

        public void Attach(IDashboardControl dashboardControl) {
            if (dashboardControl != null) {
                this.dashboardControl = dashboardControl;
                this.dashboardControl.DashboardItemControlUpdated += DashboardControl_DashboardItemControlUpdated;
                this.dashboardControl.CustomExport += DashboardControl_CustomExport;
            }
        }
        public void Detach() {
            if (dashboardControl != null) {
                this.dashboardControl.DashboardItemControlUpdated -= DashboardControl_DashboardItemControlUpdated;
                this.dashboardControl.CustomExport -= DashboardControl_CustomExport;
            }
        }

        void DashboardControl_DashboardItemControlUpdated(object sender, DashboardItemControlEventArgs e) {
            if (e.GridControl != null) {
                var gridDashboardItem = (GridDashboardItem)dashboardControl.Dashboard.Items[e.DashboardItemName];
                var gridView = (GridView)e.GridControl.MainView;
                var gridContext = e.GridContext;
                RecalculateGridSummary(gridContext, gridDashboardItem, gridView);
                gridView.ColumnFilterChanged += (s, args) => {
                    RecalculateGridSummary(gridContext, gridDashboardItem, gridView);
                };
            }
        }

        void DashboardControl_CustomExport(object sender, DevExpress.DashboardCommon.CustomExportEventArgs e) {
            var detailBand = e.Report.Bands[BandKind.Detail];
            var gridFooters = detailBand.Controls.OfType<XRGridFooterPanel>();
            foreach (var gridFooter in gridFooters) {
                var gridControl = (GridControl)dashboardControl.GetUnderlyingControl(gridFooter.GridComponentName);
                var gridView = (GridView)gridControl.MainView;
                foreach (GridColumn column in gridView.Columns) {
                    var dataItemName = column.FieldName;
                    var columnTotals = gridFooter.GetColumnTotals(dataItemName);
                    for (int i = 0; i < columnTotals.Count; i++) {
                        columnTotals[i].Text = column.Summary[i].DisplayFormat;
                    }
                }
            }
        }

        void RecalculateGridSummary(GridContext gridContext, GridDashboardItem gridItem, GridView underlyingGridView) {
            var itemData = dashboardControl.GetItemData(gridItem.ComponentName);
            foreach (GridColumn column in underlyingGridView.Columns) {
                var gridDashboardColumn = gridContext.GetDashboardItemColumn(gridItem, column);
                if (gridDashboardColumn != null) {
                    var dataItemName = column.FieldName;
                    object dataItemDescriptor = null;
                    dataItemDescriptor = itemData.GetMeasures().FirstOrDefault(m => m.ID == dataItemName);
                    dataItemDescriptor = itemData.GetDimensions(DashboardDataAxisNames.DefaultAxis).FirstOrDefault(d => d.ID == dataItemName) ?? dataItemDescriptor;
                    if (dataItemDescriptor != null) {
                        UpdateColumnSummaryItemText(gridDashboardColumn, column, dataItemDescriptor);
                    }
                }
            }
        }

        void UpdateColumnSummaryItemText(GridColumnBase gridDashboardColumn, GridColumn gridcolumn, object dataItemDescriptor) {
            gridcolumn.Summary.Clear();
            foreach (GridColumnTotal total in gridDashboardColumn.Totals) {
                var gridSummaryItem = new GridColumnSummaryItem();
                gridcolumn.Summary.Add(gridSummaryItem);
                gridSummaryItem.SummaryType = DashboardTotalsHelper.GetGridColumnTotalType(total.TotalType);
                gridSummaryItem.DisplayFormat = total.TotalType != GridColumnTotalType.Auto ?
                    string.Format("{0} = {1}", DashboardTotalsHelper.GetTotalPrefix(total.TotalType), FormatHelper.Format(dataItemDescriptor, gridSummaryItem.SummaryValue)) :
                    "Auto: Not Supported";
            }
        }
    }
}
