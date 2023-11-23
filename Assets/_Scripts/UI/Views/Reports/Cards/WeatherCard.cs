using Commons.Communications.Reports;
using LocalizationNS;

namespace UI.Views.Reports.Cards
{
    public class WeatherCard : ReportCard
    {
        private DataUnit _currentTemperatureDataUnit;
        private DataUnit _chanceOfPrecipitationDataUnit;

        public WeatherCard() : base(nameof(WeatherCard))
        {
            ConfigureUss(nameof(WeatherCard));

            CreateCurrentTemperature();
            CreateChanceOfPrecipitation();
        }

        private void CreateCurrentTemperature()
        {
            _currentTemperatureDataUnit =
                new DataUnit(Localization.GetSentence(Sentence.ReportingView.CURRENT_TEMPERATURE), RelativeChange.Mode.None, "°C");
            Add(_currentTemperatureDataUnit);
        }

        private void CreateChanceOfPrecipitation()
        {
            _chanceOfPrecipitationDataUnit =
                new DataUnit(Localization.GetSentence(Sentence.ReportingView.CHANCE_OF_PRECIPITATION), RelativeChange.Mode.None);
            Add(_chanceOfPrecipitationDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
        }
    }
}