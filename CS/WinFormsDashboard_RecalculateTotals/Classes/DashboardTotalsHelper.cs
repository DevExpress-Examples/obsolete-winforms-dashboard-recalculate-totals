using DevExpress.DashboardCommon;
using DevExpress.DashboardCommon.Localization;
using DevExpress.Data;

namespace WinFormsDashboard_RecalculateTotals {
    public class DashboardTotalsHelper {
        public static SummaryItemType GetGridColumnTotalType(GridColumnTotalType dashboardTotalType) {
            switch (dashboardTotalType) {
                case GridColumnTotalType.Sum: return SummaryItemType.Sum;
                case GridColumnTotalType.Avg: return SummaryItemType.Average;
                case GridColumnTotalType.Count: return SummaryItemType.Count;
                case GridColumnTotalType.Max: return SummaryItemType.Max;
                case GridColumnTotalType.Min: return SummaryItemType.Min;
                default: return SummaryItemType.Custom;
            };
        }
        public static string GetTotalPrefix(GridColumnTotalType dashboardTotalType) {
            switch (dashboardTotalType) {
                case GridColumnTotalType.Sum: return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeSum);
                case GridColumnTotalType.Avg: return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeAvg);
                case GridColumnTotalType.Count: return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeCount);
                case GridColumnTotalType.Max: return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeMax);
                case GridColumnTotalType.Min: return DashboardLocalizer.GetString(DashboardStringId.GridTotalTypeMin);
                default: return null;
            };
        }
    }
}