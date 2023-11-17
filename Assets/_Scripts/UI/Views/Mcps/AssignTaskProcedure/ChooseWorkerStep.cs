using Commons.Models;
using UI.Reusables.Procedure;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseWorkerStep : Step
    {
        private ScrollView _scrollView;

        public ChooseWorkerStep(Flow flow, int stepIndex) : base(flow, stepIndex, true, "Choose the workers to assign.",
            "Leave this step empty if you want to use smart assignment.")
        {
            CreateWorkerList();
        }

        private void CreateWorkerList()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Workers/WorkersView"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Workers/WorkerListEntry"));

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