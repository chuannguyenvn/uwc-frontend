using Commons.Communications.Reports;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class TaskCard : ReportCard
    {
        public DataUnit TasksLeftDataUnit;
        public DataUnit TasksCreatedDataUnit;

        public TaskCard() : base(nameof(TaskCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/TaskCard"));

            TasksLeftDataUnit = new DataUnit("Tasks left", RelativeChange.Mode.None);
            Add(TasksLeftDataUnit);

            TasksCreatedDataUnit = new DataUnit("Tasks created", RelativeChange.Mode.HigherIsBetter);
            Add(TasksCreatedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            TasksLeftDataUnit.UpdateValue(response.TotalTasksCompleted - response.TotalTasksCreated, -1f);
            TasksCreatedDataUnit.UpdateValue(response.TotalTasksCreated, -1f);
        }
    }
}