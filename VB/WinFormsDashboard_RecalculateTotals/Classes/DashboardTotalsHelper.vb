Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.Localization
Imports DevExpress.Data

Namespace WinFormsDashboard_RecalculateTotals

    Public Class DashboardTotalsHelper

        Public Shared Function GetGridColumnTotalType(ByVal dashboardTotalType As GridColumnTotalType) As SummaryItemType
            Select Case dashboardTotalType
                Case GridColumnTotalType.Sum
                    Return SummaryItemType.Sum
                Case GridColumnTotalType.Avg
                    Return SummaryItemType.Average
                Case GridColumnTotalType.Count
                    Return SummaryItemType.Count
                Case GridColumnTotalType.Max
                    Return SummaryItemType.Max
                Case GridColumnTotalType.Min
                    Return SummaryItemType.Min
                Case Else
                    Return SummaryItemType.Custom
            End Select
        End Function

        Public Shared Function GetTotalPrefix(ByVal dashboardTotalType As GridColumnTotalType) As String
            Select Case dashboardTotalType
                Case GridColumnTotalType.Sum
                    Return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeSum)
                Case GridColumnTotalType.Avg
                    Return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeAvg)
                Case GridColumnTotalType.Count
                    Return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeCount)
                Case GridColumnTotalType.Max
                    Return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeMax)
                Case GridColumnTotalType.Min
                    Return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeMin)
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace
