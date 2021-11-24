Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardExport
Imports DevExpress.DashboardWin
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.UI
Imports System.Linq

Namespace WinFormsDashboard_RecalculateTotals

    Public Class RecalculateGridTotalsModule

        Private dashboardControl As IDashboardControl

        Public Sub Attach(ByVal dashboardControl As IDashboardControl)
            If dashboardControl IsNot Nothing Then
                Me.dashboardControl = dashboardControl
                AddHandler Me.dashboardControl.DashboardItemControlUpdated, AddressOf DashboardControl_DashboardItemControlUpdated
                AddHandler Me.dashboardControl.CustomExport, AddressOf DashboardControl_CustomExport
            End If
        End Sub

        Public Sub Detach()
            If dashboardControl IsNot Nothing Then
                RemoveHandler dashboardControl.DashboardItemControlUpdated, AddressOf DashboardControl_DashboardItemControlUpdated
                RemoveHandler dashboardControl.CustomExport, AddressOf DashboardControl_CustomExport
            End If
        End Sub

        Private Sub DashboardControl_DashboardItemControlUpdated(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
            If e.GridControl IsNot Nothing Then
                Dim gridDashboardItem = CType(dashboardControl.Dashboard.Items(e.DashboardItemName), GridDashboardItem)
                Dim gridView = CType(e.GridControl.MainView, GridView)
                Dim gridContext = e.GridContext
                RecalculateGridSummary(gridContext, gridDashboardItem, gridView)
                AddHandler gridView.ColumnFilterChanged, Sub(s, args) RecalculateGridSummary(gridContext, gridDashboardItem, gridView)
            End If
        End Sub

        Private Sub DashboardControl_CustomExport(ByVal sender As Object, ByVal e As CustomExportEventArgs)
            Dim detailBand = e.Report.Bands(BandKind.Detail)
            Dim gridFooters = detailBand.Controls.OfType(Of XRGridFooterPanel)()
            For Each gridFooter In gridFooters
                Dim gridControl = CType(dashboardControl.GetUnderlyingControl(gridFooter.GridComponentName), GridControl)
                Dim gridView = CType(gridControl.MainView, GridView)
                For Each column As GridColumn In gridView.Columns
                    Dim dataItemName = column.FieldName
                    Dim columnTotals = gridFooter.GetColumnTotals(dataItemName)
                    For i As Integer = 0 To columnTotals.Count - 1
                        columnTotals(i).Text = column.Summary(i).DisplayFormat
                    Next
                Next
            Next
        End Sub

        Private Sub RecalculateGridSummary(ByVal gridContext As GridContext, ByVal gridItem As GridDashboardItem, ByVal underlyingGridView As GridView)
            Dim itemData = dashboardControl.GetItemData(gridItem.ComponentName)
            For Each column As GridColumn In underlyingGridView.Columns
                Dim gridDashboardColumn = gridContext.GetDashboardItemColumn(gridItem, column)
                If gridDashboardColumn IsNot Nothing Then
                    Dim dataItemName = column.FieldName
                    Dim dataItemDescriptor As Object = Nothing
                    dataItemDescriptor = itemData.GetMeasures().FirstOrDefault(Function(m) Equals(m.ID, dataItemName))
                    dataItemDescriptor = If(itemData.GetDimensions(DashboardDataAxisNames.DefaultAxis).FirstOrDefault(Function(d) Equals(d.ID, dataItemName)), dataItemDescriptor)
                    If dataItemDescriptor IsNot Nothing Then
                        UpdateColumnSummaryItemText(gridDashboardColumn, column, dataItemDescriptor)
                    End If
                End If
            Next
        End Sub

        Private Sub UpdateColumnSummaryItemText(ByVal gridDashboardColumn As GridColumnBase, ByVal gridcolumn As GridColumn, ByVal dataItemDescriptor As Object)
            gridcolumn.Summary.Clear()
            For Each total As GridColumnTotal In gridDashboardColumn.Totals
                Dim gridSummaryItem = New GridColumnSummaryItem()
                gridcolumn.Summary.Add(gridSummaryItem)
                gridSummaryItem.SummaryType = DashboardTotalsHelper.GetGridColumnTotalType(total.TotalType)
                gridSummaryItem.DisplayFormat = If(total.TotalType <> GridColumnTotalType.Auto, String.Format("{0} = {1}", DashboardTotalsHelper.GetTotalPrefix(total.TotalType), FormatHelper.Format(dataItemDescriptor, gridSummaryItem.SummaryValue)), "Auto: Not Supported")
            Next
        End Sub
    End Class
End Namespace
