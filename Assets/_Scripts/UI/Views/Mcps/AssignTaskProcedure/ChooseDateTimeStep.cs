﻿using UI.Reusables.DateAndTimePicker;
using UI.Reusables.Procedure;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseDateTimeStep : Step
    {
        private DateTimePicker _dateTimePicker;

        public ChooseDateTimeStep(Flow flow, int stepIndex) : base(flow, stepIndex, false, "Choose the date and time to collect the selected MCPs.")
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
    }
}