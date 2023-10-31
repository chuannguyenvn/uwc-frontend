using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class LogisticCard : ReportCard
    {
        public DataUnit DistanceTravelledDataUnit;
        public DataUnit FuelConsumptionDataUnit;

        public LogisticCard() : base(nameof(LogisticCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/LogisticCard"));

            DistanceTravelledDataUnit = new DataUnit("Distance travelled", RelativeChange.Mode.LowerIsBetter, "kms");
            Add(DistanceTravelledDataUnit);

            FuelConsumptionDataUnit = new DataUnit("Fuel used", RelativeChange.Mode.LowerIsBetter, "l");
            Add(FuelConsumptionDataUnit);
        }


        public override void UpdateData(GetDashboardReportResponse response)
        {
            DistanceTravelledDataUnit.UpdateValue(1f, 1f);
            FuelConsumptionDataUnit.UpdateValue(1f, 1f);
        }
    }
}