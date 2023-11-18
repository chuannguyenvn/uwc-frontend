using System.Collections;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Tasks
{
    public class AllTaskListStore : ServerSendOnFocusedDataStore<GetAllTasksResponse>
    {
        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetAllTasksResponse>(
                Endpoints.TaskData.GetAllTasks,
                null,
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                    }
                }
            );
        }
    }
}