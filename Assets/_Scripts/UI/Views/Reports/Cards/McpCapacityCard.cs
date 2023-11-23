using Commons.Communications.Reports;
using LocalizationNS;

namespace UI.Views.Reports.Cards
{
    public class McpCapacityCard : ReportCard
    {
        private DataUnit _mcpCapacityDataUnit;

        public McpCapacityCard() : base(nameof(McpCapacityCard))
        {
            ConfigureUss(nameof(McpCapacityCard));

            CreateMcpCapacity();
        }

        private void CreateMcpCapacity()
        {
            _mcpCapacityDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.MCP_CAPACITY), RelativeChange.Mode.None, "%");
            Add(_mcpCapacityDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _mcpCapacityDataUnit.UpdateValue(1f, -1f);
        }
    }
}