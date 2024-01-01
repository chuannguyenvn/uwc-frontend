using Commons.Communications.Reports;
using Commons.Types;
using LocalizationNS;
using UnityEngine;

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
            _currentTemperatureDataUnit.UpdateValue(Mathf.Round(response.CurrentTemperature * 2) / 2, -1f);
            var precipitationText = response.ChanceOfPrecipitation switch
            {
                ChanceOfPrecipitation.None => Localization.GetSentence(Sentence.ReportingView.PRECIPITATION_CHANCE_NONE),
                ChanceOfPrecipitation.Slight => Localization.GetSentence(Sentence.ReportingView.PRECIPITATION_CHANCE_SLIGHT),
                ChanceOfPrecipitation.Moderate => Localization.GetSentence(Sentence.ReportingView.PRECIPITATION_CHANCE_MODERATE),
                ChanceOfPrecipitation.High => Localization.GetSentence(Sentence.ReportingView.PRECIPITATION_CHANCE_HIGH),
                _ => throw new System.NotImplementedException()
            };
            _chanceOfPrecipitationDataUnit.UpdateValue(precipitationText);
        }
    }
}