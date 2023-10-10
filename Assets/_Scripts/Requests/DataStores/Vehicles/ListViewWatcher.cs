using Commons.Communications.Mcps;
using Commons.Types;
using UnityEngine;

namespace Requests.DataStores.Vehicles
{
    public class ListViewWatcher : DataWatcher
    {
        public GetMcpDataResponse Response = new();

        public ListViewWatcher()
        {
            Request = new ParameterizedRequest<>(
                Endpoints.McpData.GET,
                RequestType.POST,
                null,
                (success, response) =>
                {
                    if (success)
                    {
                        Response = response;
                        DataRequested?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("Failed to get Mcp data");
                    }
                });
        }
    }
}