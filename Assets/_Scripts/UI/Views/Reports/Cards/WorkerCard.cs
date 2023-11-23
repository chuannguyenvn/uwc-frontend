using Commons.Communications.Reports;
using LocalizationNS;

namespace UI.Views.Reports.Cards
{
    public class WorkerCard : ReportCard
    {
        private DataUnit _onlineWorkersDataUnit;
        private DataUnit _totalWorkersDataUnit;

        public WorkerCard() : base(nameof(WorkerCard))
        {
            ConfigureUss(nameof(WorkerCard));

            CreateOnlineWorkers();
            CreateTotalWorkers();
        }

        private void CreateTotalWorkers()
        {
            _totalWorkersDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.TOTAL_WORKERS), RelativeChange.Mode.None);
            Add(_totalWorkersDataUnit);
        }

        private void CreateOnlineWorkers()
        {
            _onlineWorkersDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.WORKERS_ONLINE), RelativeChange.Mode.None);
            Add(_onlineWorkersDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _onlineWorkersDataUnit.UpdateValue(response.OnlineWorkers, -1f);
            _totalWorkersDataUnit.UpdateValue(response.TotalWorkers, -1f);
        }
    }
}