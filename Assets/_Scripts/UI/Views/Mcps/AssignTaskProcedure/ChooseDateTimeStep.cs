using LocalizationNS;
using UI.Reusables.DateAndTimePicker;
using UI.Reusables.Procedure;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseDateTimeStep : Step
    {
        private DateTimePicker _dateTimePicker;

        public ChooseDateTimeStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_DATE_AND_TIME_TO_COLLECT_THE_SELECTED_MCPS))
        {
            CreateDateTimePicker();
        }

        private void CreateDateTimePicker()
        {
            _dateTimePicker = new DateTimePicker();
            AddToContainer(_dateTimePicker);
        }

        protected override void Activate()
        {
            base.Activate();
            _dateTimePicker.Refresh();
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }

        public override void Reset()
        {
            base.Reset();
            _dateTimePicker.Reset();
        }
    }
}