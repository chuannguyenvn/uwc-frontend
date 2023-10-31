using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class McpCapacityCard : ReportCard
    {
        public DataUnit McpCapacityDataUnit;
        
        public McpCapacityCard() : base(nameof(McpCapacityCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/McpCapacityCard")); 
            
            McpCapacityDataUnit = new DataUnit("MCP capacity", RelativeChange.Mode.None, "%");
            Add(McpCapacityDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            McpCapacityDataUnit.UpdateValue(1f, -1f);
        }
    }
}