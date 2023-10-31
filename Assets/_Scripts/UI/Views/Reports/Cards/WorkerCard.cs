using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class WorkerCard : ReportCard
    {
        public DataUnit OnlineWorkersDataUnit;
        public DataUnit TotalWorkersDataUnit;
        public WorkerCard() : base(nameof(WorkerCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/WorkerCard"));
            
            OnlineWorkersDataUnit = new DataUnit("Workers online", RelativeChange.Mode.None);
            Add(OnlineWorkersDataUnit);
            
            TotalWorkersDataUnit = new DataUnit("Total workers", RelativeChange.Mode.None);
            Add(TotalWorkersDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            OnlineWorkersDataUnit.UpdateValue(response.OnlineWorkers, -1f);
            TotalWorkersDataUnit.UpdateValue(response.TotalWorkers, -1f);
        }
    }
}