using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class McpCollectedCard : ReportCard
    {
        public DataUnit McpCollectedDataUnit;

        public McpCollectedCard() : base(nameof(McpCollectedCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/McpCollectedCard"));
            
            McpCollectedDataUnit = new DataUnit("MCPs collected", RelativeChange.Mode.HigherIsBetter);
            Add(McpCollectedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            McpCollectedDataUnit.UpdateValue(response.TotalTasksCompleted, 1f);
        }
    }
}