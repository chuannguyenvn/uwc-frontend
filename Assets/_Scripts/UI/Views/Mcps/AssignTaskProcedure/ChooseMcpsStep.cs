﻿using System.Collections.Generic;
using LocalizationNS;
using Maps;
using UI.Reusables.Procedure;
using Action = System.Action;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseMcpsStep : Step
    {
        public static bool IsActivated { get; private set; } = false;

        private static bool _isOrdered = true;

        public static bool IsOrdered
        {
            get => _isOrdered;
            set
            {
                _isOrdered = value;
                OrderSettingChanged?.Invoke();
            }
        }

        public static List<int> ChosenMcpIds { get; private set; } = new();
        public static event Action McpListChanged;
        public static event Action OrderSettingChanged;

        public static void AddMcp(int mcpId)
        {
            if (ChosenMcpIds.Contains(mcpId)) return;
            ChosenMcpIds.Add(mcpId);
            McpListChanged?.Invoke();
        }

        public static void RemoveMcp(int mcpId)
        {
            if (!ChosenMcpIds.Contains(mcpId)) return;
            ChosenMcpIds.Remove(mcpId);
            McpListChanged?.Invoke();
        }

        private McpsView _mcpsView;

        public ChooseMcpsStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_MCPS_THAT_YOU_WANT_TO_BE_COLLECTED))
        {
            ChosenMcpIds = new List<int>();

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
            return ChosenMcpIds.Count > 0;
        }

        protected override void Activate()
        {
            base.Activate();
            _mcpsView.FocusView();
            IsActivated = true;
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            _mcpsView.UnfocusView();
            IsActivated = false;
        }

        public override void Reset()
        {
            base.Reset();
            ChosenMcpIds = new List<int>();
            MapDrawer.Instance.UpdateAssignedMcps();
        }
    }
}