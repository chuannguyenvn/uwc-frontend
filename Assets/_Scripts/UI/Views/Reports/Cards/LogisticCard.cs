using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class LogisticCard : ReportCard
    {
        public LogisticCard() : base(nameof(LogisticCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/LogisticCard"));
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            
        }
    }
}