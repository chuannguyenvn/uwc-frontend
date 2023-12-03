﻿using System;
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
        public RoutingOptimizationScope RoutingOptimizationScope { get; private set; } = RoutingOptimizationScope.None;

        public AutoAssignmentOptimizationStrategy AutoAssignmentOptimizationStrategy { get; private set; } =
            AutoAssignmentOptimizationStrategy.TimeEfficient;

        public SetAssigningOptionsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CONFIGURE_ASSIGNING_SETTINGS))
        {
            ConfigureUss(nameof(SetAssigningOptionsStep));

            _settingList = new SettingList();
            AddToContainer(_settingList);

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_ROUTE),
                () => RoutingOptimizationScope.None.ToString(),
                new Dictionary<string, Action>
                {
                    { Sentence.TasksView.NONE, () => RoutingOptimizationScope = RoutingOptimizationScope.None },
                    { Sentence.TasksView.SELECTED, () => RoutingOptimizationScope = RoutingOptimizationScope.Selected },
                    { Sentence.TasksView.ALL, () => RoutingOptimizationScope = RoutingOptimizationScope.All },
                }, false));

            _settingList.Add(new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_AUTO_ASSIGNMENT),
                () => AutoAssignmentOptimizationStrategy.TimeEfficient.ToString(),
                new Dictionary<string, Action>
                {
                    {
                        Sentence.TasksView.TIME_EFFICIENT, () => AutoAssignmentOptimizationStrategy = AutoAssignmentOptimizationStrategy.TimeEfficient
                    },
                    {
                        Sentence.TasksView.COST_OPTIMIZED, () => AutoAssignmentOptimizationStrategy = AutoAssignmentOptimizationStrategy.CostOptimized
                    },
                }, false));
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