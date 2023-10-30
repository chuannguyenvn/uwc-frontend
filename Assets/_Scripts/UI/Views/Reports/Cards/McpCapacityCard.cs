using Commons.Communications.Reports;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class McpCapacityCard : ReportCard
    {
        public McpCapacityCard() : base(nameof(McpCapacityCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/McpCapacityCard")); 
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            
        }
    }
}