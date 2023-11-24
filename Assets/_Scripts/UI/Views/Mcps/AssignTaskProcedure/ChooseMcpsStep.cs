using LocalizationNS;
using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseMcpsStep : Step
    {
        private ScrollView _scrollView;

        public ChooseMcpsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED))
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