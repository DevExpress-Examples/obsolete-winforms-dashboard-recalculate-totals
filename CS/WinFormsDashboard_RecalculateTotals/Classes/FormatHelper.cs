using DevExpress.DashboardCommon.ViewerData;

namespace WinFormsDashboard_RecalculateTotals {
    class FormatHelper {
        public static string Format(object dataItem, object value) {
            if (dataItem is DimensionDescriptor)
                return ((DimensionDescriptor)dataItem).Format(value);
            if (dataItem is MeasureDescriptor)
                return ((MeasureDescriptor)dataItem).Format(value);
            return null;
        }
    }
}