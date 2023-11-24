using System;
using System.Collections;
using Authentication;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Tasks
{
    public class PersonalTaskListStore : ServerSendOnFocusedDataStore<GetTasksOfWorkerResponse>
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
    }
}