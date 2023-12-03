using System;
using System.Collections.Generic;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Settings;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class SetAssigningOptions : Step
    {
        private SettingList _settingList;
        public OptimizeRoute OptimizeRoute { get; private set; } = OptimizeRoute.None;

        public SetAssigningOptions(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CONFIGURE_ASSIGNING_SETTINGS))
        {
            ConfigureUss(nameof(SetAssigningOptions));

            _settingList = new SettingList();
            AddToContainer(_settingList);

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_ROUTE),
                () => OptimizeRoute.None.ToString(),
                new Dictionary<string, Action>
                {
                    { Sentence.TasksView.NONE, () => OptimizeRoute = OptimizeRoute.None },
                    { Sentence.TasksView.SELECTED, () => OptimizeRoute = OptimizeRoute.Selected },
                    { Sentence.TasksView.ALL, () => OptimizeRoute = OptimizeRoute.All },
                }));
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }

        public override void Reset()
        {
            base.Reset();
        }
    }

    public enum OptimizeRoute
    {
        None,
        Selected,
        All,
    }
}