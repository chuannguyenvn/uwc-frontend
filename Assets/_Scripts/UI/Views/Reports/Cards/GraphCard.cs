using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class GraphCard : ReportCard
    {
        public GraphCard() : base(nameof(GraphCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/GraphCard"));
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            
        }
    }
}