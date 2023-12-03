using System;
using System.Collections.Generic;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Settings;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class SetAssigningOptionsStep : Step
    {
        private SettingList _settingList;
        public OptimizeRoute OptimizeRoute { get; private set; } = OptimizeRoute.None;
        public OptimizeAutoAssignment OptimizeAutoAssignment { get; private set; } = OptimizeAutoAssignment.TimeEfficient;

        public SetAssigningOptionsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CONFIGURE_ASSIGNING_SETTINGS))
        {
            ConfigureUss(nameof(SetAssigningOptionsStep));

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

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_AUTO_ASSIGNMENT),
                () => OptimizeAutoAssignment.TimeEfficient.ToString(),
                new Dictionary<string, Action>
                {
                    { Sentence.TasksView.TIME_EFFICIENT, () => OptimizeAutoAssignment = OptimizeAutoAssignment.TimeEfficient },
                    { Sentence.TasksView.COST_OPTIMIZED, () => OptimizeAutoAssignment = OptimizeAutoAssignment.CostOptimized },
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

    public enum OptimizeAutoAssignment
    {
        TimeEfficient,
        CostOptimized,
    }
}