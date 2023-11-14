using UI.Reusables.DateAndTimePicker;
using UI.Reusables.Procedure;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseDatetimeStep : Step
    {
        private DatetimePicker _datetimePicker;

        public ChooseDatetimeStep(Flow flow, int stepIndex) : base(flow, stepIndex, false, "Choose the date and time to collect the selected MCPs.")
        {
            _datetimePicker = new DatetimePicker();
            AddToContainer(_datetimePicker);
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }
    }
}