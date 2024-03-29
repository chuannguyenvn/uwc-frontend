﻿using System;
using Commons.Models;
using Commons.Types;
using LocalizationNS;
using UI.Reusables.Procedure;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;
using Step = UI.Reusables.Procedure.Step;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public class ChooseWorkerStep : Step
    {
        public static int WorkerId { get; private set; } = -1;

        public static event Action WorkerIdChanged;

        public static void SetWorkerId(int workerId)
        {
            WorkerId = workerId;
            WorkerIdChanged?.Invoke();
        }

        private WorkersView _workersView;

        public ChooseWorkerStep(Flow flow, int stepIndex) : base(flow, stepIndex, false,
            Localization.GetSentence(Sentence.TasksView.CHOOSE_THE_WORKERS_TO_ASSIGN),
            Localization.GetSentence(Sentence.TasksView.LEAVE_THIS_STEP_EMPTY_IF_YOU_WANT_TO_ASSIGN_THE_TASK_TO_ALL_WORKERS))
        {
            WorkerId = -1;

            CreateWorkerList();
            Deactivate();
        }

        private void CreateWorkerList()
        {
            _workersView = new WorkersView(true);
            AddToContainer(_workersView);
        }

        protected override bool CheckStepCompletion()
        {
            Debug.Log("SetAssigningOptionsStep.RoutingOptimizationScope != RoutingOptimizationScope.All: " +
                      (SetAssigningOptionsStep.RoutingOptimizationScope != RoutingOptimizationScope.All));

            Debug.Log("WorkerId != -1: " + (WorkerId != -1));

            return SetAssigningOptionsStep.RoutingOptimizationScope == RoutingOptimizationScope.All ||
                   WorkerId != -1;
        }

        protected override void Activate()
        {
            base.Activate();
            _workersView.FocusView();
        }

        protected override void Deactivate()
        {
            base.Deactivate();
            _workersView.UnfocusView();
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}