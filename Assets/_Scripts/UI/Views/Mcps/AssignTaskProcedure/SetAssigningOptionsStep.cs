using System;
using System.Collections.Generic;
using Commons.Types;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Settings;
using UnityEngine.UIElements;
using Step = UI.Reusables.Procedure.Step;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class SetAssigningOptionsStep : Step
    {
        private SettingList _settingList;
        private ChoiceSettingListEntry _setAssigningOptionsStep;
        private ChoiceSettingListEntry _optimizeAssignmentSetting;

        public static RoutingOptimizationScope RoutingOptimizationScope { get; private set; } = RoutingOptimizationScope.All;

        public static AutoAssignmentOptimizationStrategy AutoAssignmentOptimizationStrategy { get; private set; } =
            AutoAssignmentOptimizationStrategy.TimeEfficient;

        public SetAssigningOptionsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CONFIGURE_ASSIGNING_SETTINGS))
        {
            ConfigureUss(nameof(SetAssigningOptionsStep));

            _settingList = new SettingList();
            AddToContainer(_settingList);

            _setAssigningOptionsStep = new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_ROUTE),
                () => RoutingOptimizationScope.ToString(),
                new List<string>()
                {
                    Sentence.TasksView.NONE,
                    Sentence.TasksView.SELECTED,
                    Sentence.TasksView.ALL,
                },
                new Dictionary<string, Action>
                {
                    {
                        RoutingOptimizationScope.None.ToString(), () =>
                        {
                            RoutingOptimizationScope = RoutingOptimizationScope.None;
                            ChooseMcpsStep.IsOrdered = true;
                            flow.RefreshCompletionStatus();
                            _optimizeAssignmentSetting.style.display = DisplayStyle.None;
                        }
                    },
                    {
                        RoutingOptimizationScope.Selected.ToString(), () =>
                        {
                            RoutingOptimizationScope = RoutingOptimizationScope.Selected;
                            ChooseMcpsStep.IsOrdered = false;
                            flow.RefreshCompletionStatus();
                            _optimizeAssignmentSetting.style.display = DisplayStyle.Flex;
                        }
                    },
                    {
                        RoutingOptimizationScope.All.ToString(), () =>
                        {
                            RoutingOptimizationScope = RoutingOptimizationScope.All;
                            ChooseMcpsStep.IsOrdered = false;
                            flow.RefreshCompletionStatus();
                            _optimizeAssignmentSetting.style.display = DisplayStyle.Flex;
                        }
                    },
                }, false);
            _settingList.Add(_setAssigningOptionsStep);

            _optimizeAssignmentSetting = new ChoiceSettingListEntry(Localization.GetSentence(Sentence.TasksView.OPTIMIZE_AUTO_ASSIGNMENT),
                () => AutoAssignmentOptimizationStrategy.ToString(),
                new List<string>()
                {
                    Sentence.TasksView.TIME_EFFICIENT,
                    Sentence.TasksView.COST_OPTIMIZED,
                },
                new Dictionary<string, Action>
                {
                    {
                        AutoAssignmentOptimizationStrategy.TimeEfficient.ToString(),
                        () =>
                        {
                            AutoAssignmentOptimizationStrategy = AutoAssignmentOptimizationStrategy.TimeEfficient;
                            flow.RefreshCompletionStatus();
                        }
                    },
                    {
                        AutoAssignmentOptimizationStrategy.CostOptimized.ToString(),
                        () =>
                        {
                            AutoAssignmentOptimizationStrategy = AutoAssignmentOptimizationStrategy.CostOptimized;
                            flow.RefreshCompletionStatus();
                        }
                    },
                }, false);
            _settingList.Add(_optimizeAssignmentSetting);
            
            _setAssigningOptionsStep.Refresh();
            _optimizeAssignmentSetting.Refresh();
        }

        protected override bool CheckStepCompletion()
        {
            return true;
        }

        public override void Reset()
        {
            base.Reset();

            RoutingOptimizationScope = RoutingOptimizationScope.All;
            AutoAssignmentOptimizationStrategy = AutoAssignmentOptimizationStrategy.TimeEfficient;
            ChooseMcpsStep.IsOrdered = false;
            
            _setAssigningOptionsStep.Refresh();
            _optimizeAssignmentSetting.Refresh();
        }
    }
}