# Dashboard for WinForms - How to Recalculate Totals when You Filter a Grid

The Dashboard does not fully support Grid column filters out of the box. These filters can only be used to select which data to show in the Grid item and they do not affect Total values calculated by the dashboard data engine. As a result, the Grid item shows filtered data when you apply grid column filters, but Totals are unchanged.

The example shows how to recalculate totals when when you apply column filters to the Grid dashboard item.

> The example supports all the dashboard's [Total types](https://docs.devexpress.com/Dashboard/114794/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/dashboard-item-settings/grid/totals#totals-overview) except for "Auto". If you apply the "Auto" total type in the dashboard, "Not Supported" is shown instead of the total value.

## Example Overview

### RecalculateGridTotalsModule

Implements the required functionality to recalculate Grid totals. The [DashboardViewer.DashboardItemControlUpdated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlUpdated) event allows you to access the underlying `GridControl` object and change its options. The `GridView` column's [Summary](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Columns.GridColumn.Summary) is used to modify column totals in the underlying UI control.

The example also modifies Total values when you export data. The [DashboardViewer.CustomExport](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.CustomExport) event handler overrides the export procedure.

The module has the `Attach` and `Detach` public methods that allow you attach/detach it to/from your `Dashboard Designer` or `Dashboard Viewer` controls.

### Helper classes

* `DashboardTotalsHelper` is implemenented to get the underlying Grid Control's total type based on the Dashboard Grid item's total type.

* `FormatHelper` allows you to format values using the dashboard's data items formatting settings. 

<!-- default file list -->
## Files to Look At

* [RecalculateGridTotalsModule.cs](./CS/WinFormsDashboard_RecalculateTotals/Classes/RecalculateGridTotalsModule.cs) (VB: [RecalculateGridTotalsModule.vb](./VB/WinFormsDashboard_RecalculateTotals/Classes/RecalculateGridTotalsModule.vb))
* [DashboardTotalsHelper.cs](./CS/WinFormsDashboard_RecalculateTotals/Classes/DashboardTotalsHelper.cs) (VB: [DashboardTotalsHelper.vb](./VB/WinFormsDashboard_RecalculateTotals/Classes/DashboardTotalsHelper.vb))
* [FormatHelper.cs](./CS/WinFormsDashboard_RecalculateTotals/Classes/FormatHelper.cs) (VB: [FormatHelper.vb](./VB/WinFormsDashboard_RecalculateTotals/Classes/FormatHelper.vb))
* [DesignerForm1.cs](./CS/WinFormsDashboard_RecalculateTotals/DesignerForm1.cs) (VB: [DesignerForm1.vb](./VB/WinFormsDashboard_RecalculateTotals/DesignerForm1.vb))

<!-- default file list end -->

## Documentation

* [Totals](https://docs.devexpress.com/Dashboard/114794/winforms-dashboard/winforms-designer/create-dashboards-in-the-winforms-designer/dashboard-item-settings/grid/totals)
* [Access to Underlying Controls](https://docs.devexpress.com/Dashboard/18019/winforms-dashboard/winforms-viewer/access-to-underlying-controls)