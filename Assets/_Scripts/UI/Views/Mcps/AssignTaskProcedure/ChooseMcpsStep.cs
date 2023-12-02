using LocalizationNS;
using UI.Reusables.Procedure;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseMcpsStep : Step
    {
        private McpsView _mcpsView;

        public ChooseMcpsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED))
        {
            CreateMcpList();
            Deactivate();
        }

        private void CreateMcpList()
        {
            _mcpsView = new McpsView(true);
            AddToContainer(_mcpsView);
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }

        protected override void Activate()
        {
            base.Activate();
            _mcpsView.FocusView();
        }
        
        protected override void Deactivate()
        {
            base.Deactivate();
            _mcpsView.UnfocusView();
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}