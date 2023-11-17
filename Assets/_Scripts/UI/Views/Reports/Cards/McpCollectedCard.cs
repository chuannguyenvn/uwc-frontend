using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class McpCollectedCard : ReportCard
    {
        private DataUnit _mcpCollectedDataUnit;

        public McpCollectedCard() : base(nameof(McpCollectedCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/McpCollectedCard"));

            CreateMcpCollected();
        }

        private void CreateMcpCollected()
        {
            _mcpCollectedDataUnit = new DataUnit("MCPs collected", RelativeChange.Mode.HigherIsBetter);
            Add(_mcpCollectedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _mcpCollectedDataUnit.UpdateValue(response.TotalTasksCompleted, -1f);
        }
    }
}