using Commons.Communications.Reports;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class TaskCard : ReportCard
    {
        public TaskCard() : base(nameof(TaskCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/TaskCard"));
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            
        }
    }
}