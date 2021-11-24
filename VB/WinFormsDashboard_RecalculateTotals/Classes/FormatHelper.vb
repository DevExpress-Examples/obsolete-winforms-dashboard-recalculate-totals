Imports DevExpress.DashboardCommon.ViewerData

Namespace WinFormsDashboard_RecalculateTotals

    Friend Class FormatHelper

        Public Shared Function Format(ByVal dataItem As Object, ByVal value As Object) As String
            If TypeOf dataItem Is DimensionDescriptor Then Return CType(dataItem, DimensionDescriptor).Format(value)
            If TypeOf dataItem Is MeasureDescriptor Then Return CType(dataItem, MeasureDescriptor).Format(value)
            Return Nothing
        End Function
    End Class
End Namespace
