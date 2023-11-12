using Commons.Models;
using UI.Reusables.Procedure;
using UI.Views.Workers;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseWorkerStep : Step
    {
        private ScrollView _scrollView;

        public ChooseWorkerStep(Flow flow, int stepIndex) : base(flow, stepIndex, true, "Choose the workers to assign.",
            "Leave this step empty if you want to use smart assignment.")
        {
            _scrollView = new ScrollView();
            AddToContainer(_scrollView);
            for (int i = 0; i < 5; i++)
            {
                var entry = new WorkerListEntry(new UserProfile()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Address = "Test"
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