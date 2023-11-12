using Commons.Models;
using UI.Reusables.Procedure;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseMcpsStep : Step
    {
        private ScrollView _scrollView;

        public ChooseMcpsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false, "Choose the MCPs that you want to be collected.")
        {
            _scrollView = new ScrollView();
            AddToContainer(_scrollView);
            for (int i = 0; i < 5; i++)
            {
                var entry = new McpListEntry(new McpData() { Address = "Test" }, Random.Range(0f, 100f));
                _scrollView.Add(entry);
            }
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }
    }
}