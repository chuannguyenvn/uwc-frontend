using Commons.Communications.Reports;
using LocalizationNS;
using UnityEngine;

namespace UI.Views.Reports.Cards
{
    public class McpCollectedCard : ReportCard
    {
        private DataUnit _mcpCollectedDataUnit;
        private DataUnit _mcpCapacityDataUnit;

        public McpCollectedCard() : base(nameof(McpCollectedCard))
        {
            ConfigureUss(nameof(McpCollectedCard));

            CreateMcpCollected();
            CreateMcpCapacity();
        }

        private void CreateMcpCollected()
        {
            _mcpCollectedDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.MCPS_COLLECTED), RelativeChange.Mode.HigherIsBetter);
            Add(_mcpCollectedDataUnit);
        }

        private void CreateMcpCapacity()
        {
            _mcpCapacityDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.MCP_CAPACITY), RelativeChange.Mode.None, "%");
            Add(_mcpCapacityDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _mcpCollectedDataUnit.UpdateValue(response.TotalTasksCompleted, -1f);
            _mcpCapacityDataUnit.UpdateValue(Mathf.Round(response.AverageMcpCapacity * 100), -1f);
        }
    }
}