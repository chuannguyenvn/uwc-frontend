using System;
using System.Collections.Generic;
using Commons.Types;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Settings;
using Step = UI.Reusables.Procedure.Step;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class SetAssigningOptionsStep : Step
    {
        private SettingList _settingList;
        public OptimizeRouting OptimizeRouting { get; private set; } = OptimizeRouting.None;
        public OptimizeAutoAssignment OptimizeAutoAssignment { get; private set; } = OptimizeAutoAssignment.TimeEfficient;

        public SetAssigningOptionsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CONFIGURE_ASSIGNING_SETTINGS))
        {
            ConfigureUss(nameof(SetAssigningOptionsStep));

            _settingList = new SettingList();
            AddToContainer(_settingList);

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_ROUTE),
                () => OptimizeRouting.None.ToString(),
                new Dictionary<string, Action>
                {
                    { Sentence.TasksView.NONE, () => OptimizeRouting = OptimizeRouting.None },
                    { Sentence.TasksView.SELECTED, () => OptimizeRouting = OptimizeRouting.Selected },
                    { Sentence.TasksView.ALL, () => OptimizeRouting = OptimizeRouting.All },
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
}