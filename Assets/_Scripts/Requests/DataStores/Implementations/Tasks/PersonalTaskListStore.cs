using System;
using System.Collections;
using Authentication;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Commons.HubHandlers;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Tasks
{
    public class PersonalTaskListStore : ServerSendInBackgroundDataStore<GetTasksOfWorkerResponse>
    {
        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendPostRequest<GetTasksOfWorkerResponse>(
                Endpoints.TaskData.GetTasksOfWorker,
                new GetTasksOfWorkerRequest()
                {
                    WorkerId = AuthenticationManager.Instance.UserAccountId,
                },
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
        }
    }
}