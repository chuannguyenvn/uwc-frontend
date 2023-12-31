using System;
using System.Collections;
using Authentication;
using Commons.Communications.Messages;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Commons.HubHandlers;
using Commons.Types;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Tasks
{
    public class AllTaskListStore : ServerSendInBackgroundDataStore<GetAllTasksResponse>
    {
        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendPostRequest<GetAllTasksResponse>(
                Endpoints.TaskData.GetAllTasks,
                null,
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                        callback?.Invoke();
                    }
                }
            );
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.ADD_TASK, (AddTasksBroadcastData data) =>
            {
                Data.Tasks = data.NewTasks;
                DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(Data));
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.FOCUS_TASK, (FocusTaskBroadcastData data) =>
            {
                var task = Data.Tasks.Find(t => t.Id == data.TaskId);
                task.TaskStatus = TaskStatus.InProgress;

                foreach (var taskData in Data.Tasks)
                {
                    if (taskData.AssigneeId.HasValue && taskData.AssigneeId.Value == data.WorkerId)
                    {
                        taskData.TaskStatus = TaskStatus.NotStarted;
                    }
                }

                DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(Data));
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.COMPLETE_TASK, (CompleteTaskBroadcastData data) =>
            {
                var task = Data.Tasks.Find(t => t.Id == data.TaskId);
                task.TaskStatus = TaskStatus.Completed;
                DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(Data));
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.REJECT_TASK, (CompleteTaskBroadcastData data) =>
            {
                var task = Data.Tasks.Find(t => t.Id == data.TaskId);
                task.TaskStatus = TaskStatus.Rejected;
                DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(Data));
            });
        }
    }
}