using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class WeatherCard : ReportCard
    {
        public WeatherCard() : base(nameof(WeatherCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/WeatherCard"));
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            
        }
    }
}