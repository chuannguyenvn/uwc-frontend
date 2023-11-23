using Commons.Models;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Workers;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseWorkerStep : Step
    {
        private ScrollView _scrollView;

        public ChooseWorkerStep(Flow flow, int stepIndex) : base(flow, stepIndex, true,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_WORKERS_TO_ASSIGN),
            Localization.GetSentence(Sentence.TasksView.LEAVE_THIS_STEP_EMPTY_IF_YOU_WANT_TO_ASSIGN_THE_TASK_TO_ALL_WORKERS))
        {
            CreateWorkerList();
        }

        private void CreateWorkerList()
        {
            styleSheets.AddByName(nameof(WorkersView));
            styleSheets.AddByName(nameof(WorkerListEntry));

            _scrollView = new ScrollView();
            AddToContainer(_scrollView);
            for (int i = 0; i < 5; i++)
            {
                var entry = new WorkerListEntry(new UserProfile()
                {
                    FirstName = "Worker",
                    LastName = "Name",
                    Address = "Test address"
                });
                _scrollView.Add(entry);
            }
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }
    }
}