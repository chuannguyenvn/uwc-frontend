using System;
using Authentication;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Commons.Types;
using Requests;
using UI.Reusables.Procedure;
using UI.Views.Tasks;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private readonly TasksView _tasksView;
        private SetAssigningOptionsStep _setAssigningOptionsStep;
        private ChooseMcpsStep _chooseMcpsStep;
        private ChooseWorkerStep _chooseWorkerStep;
        private ChooseDateTimeStep _chooseDateTimeStep;

        public AssignTaskFlow(TasksView tasksView)
        {
            _tasksView = tasksView;
        }

        protected override void CreateSteps()
        {
            _setAssigningOptionsStep = new SetAssigningOptionsStep(this, 1);
            AddStep(_setAssigningOptionsStep);

            _chooseMcpsStep = new ChooseMcpsStep(this, 2);
            AddStep(_chooseMcpsStep);

            _chooseWorkerStep = new ChooseWorkerStep(this, 3);
            AddStep(_chooseWorkerStep);

            _chooseDateTimeStep = new ChooseDateTimeStep(this, 4);
            AddStep(_chooseDateTimeStep);
        }

        protected override void SubmitResult(ClickEvent evt)
        {
            var dateTime = _chooseDateTimeStep.SelectedDateTime;
            int minutes = dateTime.Minute;
            int remainder = minutes % 15;
            int minutesToAdd = 15 - remainder;

            if (remainder != 0)
            {
                dateTime = dateTime.AddMinutes(minutesToAdd);
            }

            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
            
            DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(Endpoints.TaskData.AddTask,
                new AddTasksRequest
                {
                    AssignerAccountId = AuthenticationManager.Instance.UserAccountId,
                    AssigneeAccountId = ChooseWorkerStep.WorkerId != -1 ? ChooseWorkerStep.WorkerId : null,
                    McpDataIds = ChooseMcpsStep.ChosenMcpIds,
                    CompleteByTimestamp = dateTime.ToUniversalTime(),
                    RoutingOptimizationScope = SetAssigningOptionsStep.RoutingOptimizationScope,
                    AutoAssignmentOptimizationStrategy = SetAssigningOptionsStep.AutoAssignmentOptimizationStrategy
                }, success =>
                {
                    Reset();
                    _tasksView.BackToListView();
                }));
        }
    }
}