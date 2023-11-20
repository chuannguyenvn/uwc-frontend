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
            CreateMcpList();
        }

        private void CreateMcpList()
        {
            _scrollView = new ScrollView();
            AddToContainer(_scrollView);
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }
    }
}