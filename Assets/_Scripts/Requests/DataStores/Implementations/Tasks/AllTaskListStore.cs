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
using UnityEngine;

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
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.ADD_TASK,
                (AddTasksBroadcastData data) =>
                {
                    DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(new GetAllTasksResponse() { Tasks = data.NewTasks }));
                });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.FOCUS_TASK, (FocusTaskBroadcastData data) =>
            {
                DataStoreManager.Instance.ScheduleOnMainThread(() =>
                {
                    if (Data == null) return;
                    foreach (var taskData in Data.Tasks)
                    {
                        if (taskData.AssigneeId.HasValue && taskData.AssigneeId.Value == data.WorkerId &&
                            taskData.TaskStatus == TaskStatus.InProgress)
                        {
                            taskData.TaskStatus = TaskStatus.NotStarted;
                        }
                        else if (taskData.Id == data.TaskId)
                        {
                            taskData.TaskStatus = TaskStatus.InProgress;
                        }
                    }

                    OnDataUpdated(Data);
                });
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.COMPLETE_TASK, (CompleteTaskBroadcastData data) =>
            {
                DataStoreManager.Instance.ScheduleOnMainThread(() =>
                {
                    if (Data == null) return;
                    var task = Data.Tasks.Find(t => t.Id == data.TaskId);
                    if (task == null) return;
                    task.TaskStatus = TaskStatus.Completed;
                    OnDataUpdated(Data);
                });
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Tasks.REJECT_TASK, (CompleteTaskBroadcastData data) =>
            {
                DataStoreManager.Instance.ScheduleOnMainThread(() =>
                {
                    if (Data == null) return;
                    var task = Data.Tasks.Find(t => t.Id == data.TaskId);
                    if (task == null) return;
                    task.TaskStatus = TaskStatus.Rejected;
                    OnDataUpdated(Data);
                });
            });
        }
    }
}