using Commons.Communications.Reports;

namespace UI.Views.Reports.Cards
{
    public class TaskCard : ReportCard
    {
        private DataUnit _tasksLeftDataUnit;
        private DataUnit _tasksCreatedDataUnit;

        public TaskCard() : base(nameof(TaskCard))
        {
            ConfigureUss(nameof(TaskCard));

            CreateTasksLeft();
            CreateTasksCreated();
        }

        private void CreateTasksLeft()
        {
            _tasksLeftDataUnit = new DataUnit("Tasks left", RelativeChange.Mode.None);
            Add(_tasksLeftDataUnit);
        }

        private void CreateTasksCreated()
        {
            _tasksCreatedDataUnit = new DataUnit("Tasks created", RelativeChange.Mode.HigherIsBetter);
            Add(_tasksCreatedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _tasksLeftDataUnit.UpdateValue(response.TotalTasksCompleted - response.TotalTasksCreated, -1f);
            _tasksCreatedDataUnit.UpdateValue(response.TotalTasksCreated, -1f);
        }
    }
}