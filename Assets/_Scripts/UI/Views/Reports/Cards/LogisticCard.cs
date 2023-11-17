using Commons.Communications.Reports;

namespace UI.Views.Reports.Cards
{
    public class LogisticCard : ReportCard
    {
        private DataUnit _distanceTraveledDataUnit;
        private DataUnit _fuelConsumptionDataUnit;

        public LogisticCard() : base(nameof(LogisticCard))
        {
            ConfigureUss(nameof(LogisticCard));

            CreateDistanceTraveled();
            CreateFuelConsumption();
        }

        private void CreateDistanceTraveled()
        {
            _distanceTraveledDataUnit = new DataUnit("Distance traveled", RelativeChange.Mode.LowerIsBetter, "kms");
            Add(_distanceTraveledDataUnit);
        }

        private void CreateFuelConsumption()
        {
            _fuelConsumptionDataUnit = new DataUnit("Fuel used", RelativeChange.Mode.LowerIsBetter, "l");
            Add(_fuelConsumptionDataUnit);
        }


        public override void UpdateData(GetDashboardReportResponse response)
        {
            _distanceTraveledDataUnit.UpdateValue(1f, 1f);
            _fuelConsumptionDataUnit.UpdateValue(1f, 1f);
        }
    }
}