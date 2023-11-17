using Commons.Communications.Reports;

namespace UI.Views.Reports.Cards
{
    public class McpCollectedCard : ReportCard
    {
        private DataUnit _mcpCollectedDataUnit;

        public McpCollectedCard() : base(nameof(McpCollectedCard))
        {
            ConfigureUss(nameof(McpCollectedCard));

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