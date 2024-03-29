﻿using Commons.Communications.Reports;
using LocalizationNS;

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
            _tasksLeftDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.TASKS_LEFT), RelativeChange.Mode.None);
            Add(_tasksLeftDataUnit);
        }

        private void CreateTasksCreated()
        {
            _tasksCreatedDataUnit = new DataUnit(Localization.GetSentence(Sentence.ReportingView.TASKS_CREATED), RelativeChange.Mode.HigherIsBetter);
            Add(_tasksCreatedDataUnit);
        }

        public override void UpdateData(GetDashboardReportResponse response)
        {
            _tasksLeftDataUnit.UpdateValue(response.TotalTasksCreated - response.TotalTasksCompleted, -1f);
            _tasksCreatedDataUnit.UpdateValue(response.TotalTasksCreated, -1f);
        }
    }
}