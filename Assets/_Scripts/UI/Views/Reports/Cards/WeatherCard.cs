using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class WeatherCard : ReportCard
    {
        private DataUnit _currentTemperatureDataUnit;
        private DataUnit _chanceOfPrecipitationDataUnit;

        public WeatherCard() : base(nameof(WeatherCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/WeatherCard"));

            CreateCurrentTemperature();
            CreateChanceOfPrecipitation();
        }

        private void CreateCurrentTemperature()
        {
            _currentTemperatureDataUnit = new DataUnit("Current temperature", RelativeChange.Mode.None, "°C");
            Add(_currentTemperatureDataUnit);
        }

        private void CreateChanceOfPrecipitation()
        {
            _chanceOfPrecipitationDataUnit = new DataUnit("Chance of precipitation", RelativeChange.Mode.None);
            Add(_chanceOfPrecipitationDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
        }
    }
}