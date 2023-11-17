using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

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
            _totalWorkersDataUnit = new DataUnit("Total workers", RelativeChange.Mode.None);
            Add(_totalWorkersDataUnit);
        }

        private void CreateOnlineWorkers()
        {
            _onlineWorkersDataUnit = new DataUnit("Workers online", RelativeChange.Mode.None);
            Add(_onlineWorkersDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _onlineWorkersDataUnit.UpdateValue(response.OnlineWorkers, -1f);
            _totalWorkersDataUnit.UpdateValue(response.TotalWorkers, -1f);
        }
    }
}