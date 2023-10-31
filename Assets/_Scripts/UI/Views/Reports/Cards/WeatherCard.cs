using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class WeatherCard : ReportCard
    {
        public DataUnit CurrentTemperatureDataUnit;
        public DataUnit ChanceOfPrecipitationDataUnit;

        public WeatherCard() : base(nameof(WeatherCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/WeatherCard"));

            CurrentTemperatureDataUnit = new DataUnit("Current temperature", RelativeChange.Mode.None, "°C");
            Add(CurrentTemperatureDataUnit);

            ChanceOfPrecipitationDataUnit = new DataUnit("Chance of precipitation", RelativeChange.Mode.None);
            Add(ChanceOfPrecipitationDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
        }
    }
}