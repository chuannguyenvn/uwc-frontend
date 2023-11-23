using Commons.Communications.Reports;
using LocalizationNS;

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
            _mcpCollectedDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.MCPS_COLLECTED), RelativeChange.Mode.HigherIsBetter);
            Add(_mcpCollectedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _mcpCollectedDataUnit.UpdateValue(response.TotalTasksCompleted, -1f);
        }
    }
}